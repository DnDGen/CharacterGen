using CharacterGen.Common.Abilities.Feats;
using CharacterGen.Common.Abilities.Skills;
using CharacterGen.Common.Abilities.Stats;
using CharacterGen.Common.CharacterClasses;
using CharacterGen.Common.Combats;
using CharacterGen.Common.Races;
using CharacterGen.Generators.Abilities.Feats;
using CharacterGen.Selectors;
using CharacterGen.Tables;
using System.Collections.Generic;
using System.Linq;

namespace CharacterGen.Generators.Domain.Abilities.Feats
{
    public class FeatsGenerator : IFeatsGenerator
    {
        private IRacialFeatsGenerator racialFeatsGenerator;
        private IClassFeatsGenerator classFeatsGenerator;
        private IAdditionalFeatsGenerator additionalFeatsGenerator;
        private ICollectionsSelector collectionsSelector;

        public FeatsGenerator(IRacialFeatsGenerator racialFeatsGenerator, IClassFeatsGenerator classFeatsGenerator,
            IAdditionalFeatsGenerator additionalFeatsGenerator, ICollectionsSelector collectionsSelector)
        {
            this.racialFeatsGenerator = racialFeatsGenerator;
            this.classFeatsGenerator = classFeatsGenerator;
            this.additionalFeatsGenerator = additionalFeatsGenerator;
            this.collectionsSelector = collectionsSelector;
        }

        public IEnumerable<Feat> GenerateWith(CharacterClass characterClass, Race race, Dictionary<string, Stat> stats,
            Dictionary<string, Skill> skills, BaseAttack baseAttack)
        {
            var racialFeats = racialFeatsGenerator.GenerateWith(race, skills, stats);
            var classFeats = classFeatsGenerator.GenerateWith(characterClass, race, stats, racialFeats, skills);
            var automaticFeats = racialFeats.Union(classFeats);
            var additionalFeats = additionalFeatsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack, automaticFeats);

            var allFeats = automaticFeats.Union(additionalFeats);

            var featsToCombine = allFeats.Where(f => CanCombine(f, allFeats));
            var featsToRemove = new List<Feat>();
            var combinedFeatNames = new List<string>();

            foreach (var featToCombine in featsToCombine)
            {
                if (combinedFeatNames.Contains(featToCombine.Name))
                    continue;

                var combinableFeats = featsToCombine.Where(f => CanCombine(f, featsToCombine));
                if (combinableFeats.Count() < 2)
                    continue;

                var otherFeats = Enumerable.Empty<Feat>();

                if (combinableFeats.Any(f => f.Frequency.TimePeriod == FeatConstants.Frequencies.Constant))
                {
                    var featToKeep = combinableFeats.First(f => f.Frequency.TimePeriod == FeatConstants.Frequencies.Constant);
                    otherFeats = combinableFeats.Except(new[] { featToKeep });
                }
                else if (combinableFeats.Any(f => f.Frequency.TimePeriod == FeatConstants.Frequencies.AtWill))
                {
                    var featToKeep = combinableFeats.First(f => f.Frequency.TimePeriod == FeatConstants.Frequencies.AtWill);
                    otherFeats = combinableFeats.Except(new[] { featToKeep });
                }
                else
                {
                    otherFeats = combinableFeats.Except(new[] { featToCombine });
                    featToCombine.Frequency.Quantity = combinableFeats.Sum(f => f.Frequency.Quantity);
                }

                featsToRemove.AddRange(otherFeats);
                combinedFeatNames.Add(featToCombine.Name);
            }

            var featsWithRemovableStrengths = allFeats.Where(f => CanRemoveStrength(f, allFeats));
            combinedFeatNames.Clear();

            foreach (var featToRemove in featsWithRemovableStrengths)
            {
                if (combinedFeatNames.Contains(featToRemove.Name))
                    continue;

                var removableFeats = featsWithRemovableStrengths.Where(f => f.Name == featToRemove.Name);

                var maxStrength = removableFeats.Max(f => f.Strength);
                var featToKeep = removableFeats.First(f => f.Strength == maxStrength);
                var otherFeats = removableFeats.Except(new[] { featToKeep });

                featsToRemove.AddRange(otherFeats);
                combinedFeatNames.Add(featToRemove.Name);
            }

            if (allFeats.Any(f => f.Foci.Contains(FeatConstants.Foci.All)))
            {
                var featsWithAllFocus = allFeats.Where(f => f.Foci.Contains(FeatConstants.Foci.All));
                var featNamesWithAllFocus = featsWithAllFocus.Select(f => f.Name);
                var redundantFeats = allFeats.Where(f => featNamesWithAllFocus.Contains(f.Name) && f.Foci.Contains(FeatConstants.Foci.All) == false);
                featsToRemove.AddRange(redundantFeats);

                foreach (var feat in featsWithAllFocus)
                    feat.Foci = new[] { FeatConstants.Foci.All };
            }

            var remainingFeats = allFeats.Except(featsToRemove);
            var featsWithCombinableFoci = remainingFeats.Where(f => CanCombineFoci(f, remainingFeats));
            var combinedFeatNamesAndStrengths = new Dictionary<string, List<int>>();

            foreach (var featToKeep in featsWithCombinableFoci)
            {
                if (combinedFeatNamesAndStrengths.ContainsKey(featToKeep.Name) == false)
                    combinedFeatNamesAndStrengths[featToKeep.Name] = new List<int>();

                if (combinedFeatNamesAndStrengths[featToKeep.Name].Contains(featToKeep.Strength))
                    continue;

                combinedFeatNamesAndStrengths[featToKeep.Name].Add(featToKeep.Strength);

                var removableFeats = featsWithCombinableFoci.Where(f => f.Name == featToKeep.Name && f.Strength == featToKeep.Strength);

                foreach (var featToRemove in removableFeats)
                {
                    var matchingFoci = featToKeep.Foci.Intersect(featToRemove.Foci);
                    if (matchingFoci.Any())
                        continue;

                    featToKeep.Foci = featToKeep.Foci.Union(featToRemove.Foci);
                    featsToRemove.Add(featToRemove);
                }
            }

            allFeats = allFeats.Except(featsToRemove);

            return allFeats;
        }

        private bool CanCombineFoci(Feat feat, IEnumerable<Feat> allFeats)
        {
            if (feat.Frequency.TimePeriod != string.Empty)
                return false;

            var count = allFeats.Count(f => f.Name == feat.Name
                                        && f.Foci.Any() && feat.Foci.Any()
                                        && f.Strength == feat.Strength
                                        && f.Frequency.TimePeriod == string.Empty);

            return count > 1;
        }

        private bool CanRemoveStrength(Feat feat, IEnumerable<Feat> allFeats)
        {
            if (feat.Frequency.TimePeriod != string.Empty)
                return false;

            var featNamesAllowingMultipleTakes = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.FeatGroups, GroupConstants.TakenMultipleTimes);
            if (featNamesAllowingMultipleTakes.Contains(feat.Name))
                return false;

            var count = allFeats.Count(f => f.Name == feat.Name
                                        && f.Foci.Except(feat.Foci).Any() == false
                                        && f.Frequency.TimePeriod == string.Empty);

            return count > 1;
        }

        private bool CanCombine(Feat feat, IEnumerable<Feat> allFeats)
        {
            if (feat.Frequency.TimePeriod == string.Empty)
                return false;

            var count = allFeats.Count(f => f.Name == feat.Name
                                        && f.Foci.Except(feat.Foci).Any() == false
                                        && f.Strength == feat.Strength
                                        && FrequenciesCanCombine(f.Frequency, feat.Frequency));

            return count > 1;
        }

        private bool FrequenciesCanCombine(Frequency firstFrequency, Frequency secondFrequency)
        {
            return firstFrequency.TimePeriod == secondFrequency.TimePeriod
                || firstFrequency.TimePeriod == FeatConstants.Frequencies.AtWill
                || secondFrequency.TimePeriod == FeatConstants.Frequencies.AtWill
                || firstFrequency.TimePeriod == FeatConstants.Frequencies.Constant
                || secondFrequency.TimePeriod == FeatConstants.Frequencies.Constant;
        }
    }
}