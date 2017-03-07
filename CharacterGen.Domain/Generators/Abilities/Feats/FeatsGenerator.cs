﻿using CharacterGen.Abilities.Feats;
using CharacterGen.Abilities.Skills;
using CharacterGen.Abilities.Stats;
using CharacterGen.CharacterClasses;
using CharacterGen.Combats;
using CharacterGen.Domain.Selectors.Collections;
using CharacterGen.Domain.Tables;
using CharacterGen.Races;
using System.Collections.Generic;
using System.Linq;

namespace CharacterGen.Domain.Generators.Abilities.Feats
{
    internal class FeatsGenerator : IFeatsGenerator
    {
        private IRacialFeatsGenerator racialFeatsGenerator;
        private IClassFeatsGenerator classFeatsGenerator;
        private IAdditionalFeatsGenerator additionalFeatsGenerator;
        private ICollectionsSelector collectionsSelector;

        public FeatsGenerator(IRacialFeatsGenerator racialFeatsGenerator, IClassFeatsGenerator classFeatsGenerator, IAdditionalFeatsGenerator additionalFeatsGenerator, ICollectionsSelector collectionsSelector)
        {
            this.racialFeatsGenerator = racialFeatsGenerator;
            this.classFeatsGenerator = classFeatsGenerator;
            this.additionalFeatsGenerator = additionalFeatsGenerator;
            this.collectionsSelector = collectionsSelector;
        }

        public IEnumerable<Feat> GenerateWith(CharacterClass characterClass, Race race, Dictionary<string, Stat> stats, IEnumerable<Skill> skills, BaseAttack baseAttack)
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

                var maxPower = removableFeats.Max(f => f.Power);
                var featToKeep = removableFeats.First(f => f.Power == maxPower);
                var otherFeats = removableFeats.Except(new[] { featToKeep });

                featsToRemove.AddRange(otherFeats);
                combinedFeatNames.Add(featToRemove.Name);
            }

            if (allFeats.Any(f => f.Foci.Contains(FeatConstants.Foci.All)))
            {
                var featsWithAllFocus = allFeats.Where(f => f.Foci.Contains(FeatConstants.Foci.All));
                var featNamesAllowingMultipleTakes = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.FeatGroups, GroupConstants.TakenMultipleTimes);
                var featNamesWithAllFocus = featsWithAllFocus.Select(f => f.Name).Except(featNamesAllowingMultipleTakes);

                var redundantFeats = allFeats.Where(f => featNamesWithAllFocus.Contains(f.Name) && f.Foci.Contains(FeatConstants.Foci.All) == false);
                featsToRemove.AddRange(redundantFeats);

                foreach (var feat in featsWithAllFocus)
                    feat.Foci = new[] { FeatConstants.Foci.All };
            }

            var remainingFeats = allFeats.Except(featsToRemove);
            var featsWithCombinableFoci = remainingFeats.Where(f => CanCombineFoci(f, remainingFeats));
            var combinedFeatNamesAndPowers = new Dictionary<string, List<int>>();

            foreach (var featToKeep in featsWithCombinableFoci)
            {
                if (combinedFeatNamesAndPowers.ContainsKey(featToKeep.Name) == false)
                    combinedFeatNamesAndPowers[featToKeep.Name] = new List<int>();

                if (combinedFeatNamesAndPowers[featToKeep.Name].Contains(featToKeep.Power))
                    continue;

                combinedFeatNamesAndPowers[featToKeep.Name].Add(featToKeep.Power);

                var removableFeats = featsWithCombinableFoci.Where(f => f.Name == featToKeep.Name && f.Power == featToKeep.Power);

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

            if (feat.Foci.Any() == false || feat.Foci.Contains(FeatConstants.Foci.All))
                return false;

            var count = allFeats.Count(f => f.Name == feat.Name
                                        && f.Foci.Any()
                                        && f.Foci.Contains(FeatConstants.Foci.All) == false
                                        && f.Power == feat.Power
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
                                        && f.Power == feat.Power
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