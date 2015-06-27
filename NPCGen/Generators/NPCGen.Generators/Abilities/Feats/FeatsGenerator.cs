using System;
using System.Collections.Generic;
using System.Linq;
using NPCGen.Common.Abilities.Feats;
using NPCGen.Common.Abilities.Skills;
using NPCGen.Common.Abilities.Stats;
using NPCGen.Common.CharacterClasses;
using NPCGen.Common.Combats;
using NPCGen.Common.Races;
using NPCGen.Generators.Interfaces.Abilities.Feats;
using NPCGen.Selectors.Interfaces;
using NPCGen.Tables.Interfaces;

namespace NPCGen.Generators.Abilities.Feats
{
    public class FeatsGenerator : IFeatsGenerator
    {
        private IRacialFeatsGenerator racialFeatsGenerator;
        private IClassFeatsGenerator classFeatsGenerator;
        private IAdditionalFeatsGenerator additionalFeatsGenerator;
        private ICollectionsSelector collectionsSelector;
        private INameSelector nameSelector;

        public FeatsGenerator(IRacialFeatsGenerator racialFeatsGenerator, IClassFeatsGenerator classFeatsGenerator,
            IAdditionalFeatsGenerator additionalFeatsGenerator, ICollectionsSelector collectionsSelector,
            INameSelector nameSelector)
        {
            this.racialFeatsGenerator = racialFeatsGenerator;
            this.classFeatsGenerator = classFeatsGenerator;
            this.additionalFeatsGenerator = additionalFeatsGenerator;
            this.collectionsSelector = collectionsSelector;
            this.nameSelector = nameSelector;
        }

        public IEnumerable<Feat> GenerateWith(CharacterClass characterClass, Race race, Dictionary<String, Stat> stats,
            Dictionary<String, Skill> skills, BaseAttack baseAttack)
        {
            var racialFeats = racialFeatsGenerator.GenerateWith(race);
            var classFeats = classFeatsGenerator.GenerateWith(characterClass, stats);
            var automaticFeats = racialFeats.Union(classFeats);
            var additionalFeats = additionalFeatsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack, automaticFeats);
            var skillSynergyFeats = GetSkillSynergyFeats(skills);

            var allFeats = racialFeats.Union(classFeats)
                                      .Union(additionalFeats)
                                      .Union(skillSynergyFeats);

            var featsToCombine = allFeats.Where(f => CanCombine(f, allFeats));
            var featsToRemove = new List<Feat>();
            var combinedFeatIds = new List<String>();

            foreach (var featToCombine in featsToCombine)
            {
                if (combinedFeatIds.Contains(featToCombine.Name.Id))
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
                combinedFeatIds.Add(featToCombine.Name.Id);
            }

            var featsWithRemovableStrengths = allFeats.Where(f => CanRemoveStrength(f, allFeats));
            combinedFeatIds.Clear();

            foreach (var featToRemove in featsWithRemovableStrengths)
            {
                if (combinedFeatIds.Contains(featToRemove.Name.Id))
                    continue;

                var removableFeats = featsWithRemovableStrengths.Where(f => CanRemoveStrength(f, featsWithRemovableStrengths));
                if (removableFeats.Count() < 2)
                    continue;

                var maxStrength = featsWithRemovableStrengths.Max(f => f.Strength);
                var featToKeep = featsWithRemovableStrengths.First(f => f.Strength == maxStrength);
                var otherFeats = removableFeats.Except(new[] { featToKeep });

                featsToRemove.AddRange(otherFeats);
                combinedFeatIds.Add(featToRemove.Name.Id);
            }

            allFeats = allFeats.Except(featsToRemove);

            foreach (var feat in allFeats)
                feat.Name.Name = nameSelector.Select(feat.Name.Id);

            return allFeats;
        }

        private Boolean CanRemoveStrength(Feat feat, IEnumerable<Feat> allFeats)
        {
            if (feat.Frequency.TimePeriod != String.Empty)
                return false;

            var featIdsAllowingMultipleTakes = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.FeatGroups, TableNameConstants.Set.Collection.Groups.TakenMultipleTimes);
            if (featIdsAllowingMultipleTakes.Contains(feat.Name.Id))
                return false;

            var count = allFeats.Count(f => f.Name.Id == feat.Name.Id
                                        && f.Focus == feat.Focus
                                        && f.Frequency.TimePeriod == String.Empty);

            return count > 1;
        }

        private Boolean CanCombine(Feat feat, IEnumerable<Feat> allFeats)
        {
            if (feat.Frequency.TimePeriod == String.Empty)
                return false;

            var count = allFeats.Count(f => f.Name.Id == feat.Name.Id
                                        && f.Focus == feat.Focus
                                        && f.Strength == feat.Strength
                                        && FrequenciesCanCombine(f.Frequency, feat.Frequency));

            return count > 1;
        }

        private Boolean FrequenciesCanCombine(Frequency firstFrequency, Frequency secondFrequency)
        {
            return firstFrequency.TimePeriod == secondFrequency.TimePeriod
                || firstFrequency.TimePeriod == FeatConstants.Frequencies.AtWill
                || secondFrequency.TimePeriod == FeatConstants.Frequencies.AtWill
                || firstFrequency.TimePeriod == FeatConstants.Frequencies.Constant
                || secondFrequency.TimePeriod == FeatConstants.Frequencies.Constant;
        }

        private IEnumerable<Feat> GetSkillSynergyFeats(Dictionary<String, Skill> skills)
        {
            var synergyFeatIds = new List<String>();
            var synergyQualifyingSkills = skills.Where(kvp => kvp.Value.EffectiveRanks >= 5).Select(kvp => kvp.Key);

            foreach (var skill in synergyQualifyingSkills)
            {
                var synergy = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.SkillSynergyFeats, skill);
                synergyFeatIds.AddRange(synergy);
            }

            var synergyFeats = new List<Feat>();
            foreach (var synergyFeatId in synergyFeatIds)
            {
                var synergyFeat = new Feat();
                synergyFeat.Name.Id = synergyFeatId;

                synergyFeats.Add(synergyFeat);
            }

            return synergyFeats;
        }
    }
}