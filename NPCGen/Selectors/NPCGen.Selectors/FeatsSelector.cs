using System;
using System.Collections.Generic;
using System.Linq;
using NPCGen.Selectors.Interfaces;
using NPCGen.Selectors.Interfaces.Objects;
using NPCGen.Tables.Interfaces;

namespace NPCGen.Selectors
{
    public class FeatsSelector : IFeatsSelector
    {
        private ICollectionsSelector collectionsSelector;
        private IAdjustmentsSelector adjustmentsSelector;

        public FeatsSelector(ICollectionsSelector collectionsSelector, IAdjustmentsSelector adjustmentsSelector)
        {
            this.collectionsSelector = collectionsSelector;
            this.adjustmentsSelector = adjustmentsSelector;
        }

        public IEnumerable<RacialFeatSelection> SelectRacial(String raceId)
        {
            var racialFeatIds = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.FeatGroups, raceId);
            var racialFeatSelections = new List<RacialFeatSelection>();

            foreach (var racialFeatId in racialFeatIds)
            {
                var tableName = String.Format(TableNameConstants.Formattable.Collection.RACEFeatData, raceId);
                var featData = collectionsSelector.SelectFrom(tableName, racialFeatId).ToArray();

                var racialFeatSelection = new RacialFeatSelection();
                racialFeatSelection.FeatId = featData[DataIndexConstants.RacialFeatData.FeatIdIndex];
                racialFeatSelection.SizeRequirement = featData[DataIndexConstants.RacialFeatData.SizeRequirementIndex];
                racialFeatSelection.Strength = Convert.ToInt32(featData[DataIndexConstants.RacialFeatData.StrengthIndex]);
                racialFeatSelection.MinimumHitDieRequirement = Convert.ToInt32(featData[DataIndexConstants.RacialFeatData.MinimumHitDiceRequirementIndex]);
                racialFeatSelection.FocusType = featData[DataIndexConstants.RacialFeatData.FocusIndex];
                racialFeatSelection.Frequency.Quantity = Convert.ToInt32(featData[DataIndexConstants.RacialFeatData.FrequencyQuantityIndex]);
                racialFeatSelection.Frequency.TimePeriod = featData[DataIndexConstants.RacialFeatData.FrequencyTimePeriodIndex];

                racialFeatSelections.Add(racialFeatSelection);
            }

            return racialFeatSelections;
        }

        public IEnumerable<AdditionalFeatSelection> SelectAdditional()
        {
            var additionalFeatIds = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.FeatGroups, GroupConstants.Additional);
            var additionalFeatSelections = new List<AdditionalFeatSelection>();

            foreach (var additionalFeatId in additionalFeatIds)
            {
                var additionalFeatSelection = SelectAdditional(additionalFeatId);
                additionalFeatSelections.Add(additionalFeatSelection);
            }

            return additionalFeatSelections;
        }

        private AdditionalFeatSelection SelectAdditional(String featId)
        {
            var additionalFeatSelection = new AdditionalFeatSelection();
            additionalFeatSelection.FeatId = featId;

            var featData = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.AdditionalFeatData, featId).ToArray();
            additionalFeatSelection.IsFighterFeat = Convert.ToBoolean(featData[DataIndexConstants.AdditionalFeatData.IsFighterFeatIndex]);
            additionalFeatSelection.IsWizardFeat = Convert.ToBoolean(featData[DataIndexConstants.AdditionalFeatData.IsWizardFeatIndex]);
            additionalFeatSelection.RequiredBaseAttack = Convert.ToInt32(featData[DataIndexConstants.AdditionalFeatData.BaseAttackRequirementIndex]);
            additionalFeatSelection.FocusType = featData[DataIndexConstants.AdditionalFeatData.FocusTypeIndex];
            additionalFeatSelection.Frequency.Quantity = Convert.ToInt32(featData[DataIndexConstants.AdditionalFeatData.FrequencyQuantityIndex]);
            additionalFeatSelection.Frequency.TimePeriod = featData[DataIndexConstants.AdditionalFeatData.FrequencyTimePeriodIndex];
            additionalFeatSelection.Strength = Convert.ToInt32(featData[DataIndexConstants.AdditionalFeatData.StrengthIndex]);

            var requiredFeats = collectionsSelector.SelectAllFrom(TableNameConstants.Set.Collection.RequiredFeats);
            if (requiredFeats.ContainsKey(featId))
                additionalFeatSelection.RequiredFeatIds = requiredFeats[featId];

            var featsWithClassRequirements = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.FeatGroups, GroupConstants.HasClassRequirements);
            if (featsWithClassRequirements.Contains(featId))
            {
                var tableName = String.Format(TableNameConstants.Formattable.Adjustments.FEATClassRequirements, featId);
                additionalFeatSelection.RequiredCharacterClasses = adjustmentsSelector.SelectFrom(tableName);
            }

            var featsWithSkillRequirements = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.FeatGroups, GroupConstants.HasSkillRequirements);
            if (featsWithSkillRequirements.Contains(featId))
            {
                var tableName = String.Format(TableNameConstants.Formattable.Adjustments.FEATSkillRankRequirements, featId);
                additionalFeatSelection.RequiredSkillRanks = adjustmentsSelector.SelectFrom(tableName);
            }

            var featWithStatRequirements = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.FeatGroups, GroupConstants.HasStatRequirements);
            if (featWithStatRequirements.Contains(featId))
            {
                var tableName = String.Format(TableNameConstants.Formattable.Adjustments.FEATStatRequirements, featId);
                additionalFeatSelection.RequiredStats = adjustmentsSelector.SelectFrom(tableName);
            }

            return additionalFeatSelection;
        }

        public IEnumerable<CharacterClassFeatSelection> SelectClass(String characterClassName)
        {
            var classFeatSelectionIds = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.FeatGroups, characterClassName);
            var classFeatSelections = new List<CharacterClassFeatSelection>();

            foreach (var classFeatSelectionId in classFeatSelectionIds)
            {
                var tableName = String.Format(TableNameConstants.Formattable.Collection.CLASSFeatData, characterClassName);
                var featData = collectionsSelector.SelectFrom(tableName, classFeatSelectionId).ToArray();

                var classFeatSelection = new CharacterClassFeatSelection();
                classFeatSelection.FeatId = featData[DataIndexConstants.CharacterClassFeatData.FeatIdIndex];
                classFeatSelection.FocusType = featData[DataIndexConstants.CharacterClassFeatData.FocusTypeIndex];
                classFeatSelection.Frequency.Quantity = Convert.ToInt32(featData[DataIndexConstants.CharacterClassFeatData.FrequencyQuantityIndex]);
                classFeatSelection.Frequency.TimePeriod = featData[DataIndexConstants.CharacterClassFeatData.FrequencyTimePeriodIndex];
                classFeatSelection.MinimumLevel = Convert.ToInt32(featData[DataIndexConstants.CharacterClassFeatData.MinimumLevelRequirementIndex]);
                classFeatSelection.Strength = Convert.ToInt32(featData[DataIndexConstants.CharacterClassFeatData.StrengthIndex]);
                classFeatSelection.MaximumLevel = Convert.ToInt32(featData[DataIndexConstants.CharacterClassFeatData.MaximumLevelRequirementIndex]);
                classFeatSelection.FrequencyQuantityStat = featData[DataIndexConstants.CharacterClassFeatData.FrequencyQuantityStatIndex];

                var requiredFeats = collectionsSelector.SelectAllFrom(TableNameConstants.Set.Collection.RequiredFeats);
                if (requiredFeats.ContainsKey(classFeatSelection.FeatId))
                    classFeatSelection.RequiredFeatIds = requiredFeats[classFeatSelection.FeatId];

                classFeatSelections.Add(classFeatSelection);
            }

            return classFeatSelections;
        }
    }
}