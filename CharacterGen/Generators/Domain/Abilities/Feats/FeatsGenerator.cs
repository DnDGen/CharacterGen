﻿using CharacterGen.Common.Abilities.Feats;
using CharacterGen.Common.Abilities.Skills;
using CharacterGen.Common.Abilities.Stats;
using CharacterGen.Common.CharacterClasses;
using CharacterGen.Common.Combats;
using CharacterGen.Common.Items;
using CharacterGen.Common.Races;
using CharacterGen.Generators.Abilities.Feats;
using CharacterGen.Selectors;
using CharacterGen.Tables;
using System;
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

        public IEnumerable<Feat> GenerateWith(CharacterClass characterClass, Race race, Dictionary<String, Stat> stats,
            Dictionary<String, Skill> skills, BaseAttack baseAttack)
        {
            var racialFeats = racialFeatsGenerator.GenerateWith(race, skills);
            var classFeats = classFeatsGenerator.GenerateWith(characterClass, race, stats, racialFeats, skills);
            var automaticFeats = racialFeats.Union(classFeats);
            var additionalFeats = additionalFeatsGenerator.GenerateWith(characterClass, race, stats, skills, baseAttack, automaticFeats);

            var allFeats = automaticFeats.Union(additionalFeats);

            var featsToCombine = allFeats.Where(f => CanCombine(f, allFeats));
            var featsToRemove = new List<Feat>();
            var combinedFeatIds = new List<String>();

            foreach (var featToCombine in featsToCombine)
            {
                if (combinedFeatIds.Contains(featToCombine.Name))
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
                combinedFeatIds.Add(featToCombine.Name);
            }

            var featsWithRemovableStrengths = allFeats.Where(f => CanRemoveStrength(f, allFeats));
            combinedFeatIds.Clear();

            foreach (var featToRemove in featsWithRemovableStrengths)
            {
                if (combinedFeatIds.Contains(featToRemove.Name))
                    continue;

                var removableFeats = featsWithRemovableStrengths.Where(f => CanRemoveStrength(f, featsWithRemovableStrengths));
                if (removableFeats.Count() < 2)
                    continue;

                var maxStrength = featsWithRemovableStrengths.Max(f => f.Strength);
                var featToKeep = featsWithRemovableStrengths.First(f => f.Strength == maxStrength);
                var otherFeats = removableFeats.Except(new[] { featToKeep });

                featsToRemove.AddRange(otherFeats);
                combinedFeatIds.Add(featToRemove.Name);
            }

            if (allFeats.Any(f => f.Focus == ProficiencyConstants.All))
            {
                var featIdsWithAllFocus = allFeats.Where(f => f.Focus == ProficiencyConstants.All).Select(f => f.Name);
                var redundantFeats = allFeats.Where(f => featIdsWithAllFocus.Contains(f.Name) && f.Focus != ProficiencyConstants.All);
                featsToRemove.AddRange(redundantFeats);
            }

            allFeats = allFeats.Except(featsToRemove);

            return allFeats;
        }

        private Boolean CanRemoveStrength(Feat feat, IEnumerable<Feat> allFeats)
        {
            if (feat.Frequency.TimePeriod != String.Empty)
                return false;

            var featIdsAllowingMultipleTakes = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.FeatGroups, GroupConstants.TakenMultipleTimes);
            if (featIdsAllowingMultipleTakes.Contains(feat.Name))
                return false;

            var count = allFeats.Count(f => f.Name == feat.Name
                                        && f.Focus == feat.Focus
                                        && f.Frequency.TimePeriod == String.Empty);

            return count > 1;
        }

        private Boolean CanCombine(Feat feat, IEnumerable<Feat> allFeats)
        {
            if (feat.Frequency.TimePeriod == String.Empty)
                return false;

            var count = allFeats.Count(f => f.Name == feat.Name
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
    }
}