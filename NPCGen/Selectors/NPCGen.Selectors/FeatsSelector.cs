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
                racialFeatSelection.FeatId = featData[SelectorConstants.RacialFeatIdIndex];
                racialFeatSelection.SizeRequirement = featData[SelectorConstants.RacialSizeRequirementIndex];
                racialFeatSelection.Strength = Convert.ToInt32(featData[SelectorConstants.RacialStrengthIndex]);
                racialFeatSelection.MinimumHitDieRequirement = Convert.ToInt32(featData[SelectorConstants.RacialMinimumHitDiceRequirementIndex]);
                racialFeatSelection.Focus = featData[SelectorConstants.RacialFocusIndex];
                racialFeatSelection.Frequency.Quantity = Convert.ToInt32(featData[SelectorConstants.RacialFrequencyQuantityIndex]);
                racialFeatSelection.Frequency.TimePeriod = featData[SelectorConstants.RacialFrequencyTimePeriodIndex];

                racialFeatSelections.Add(racialFeatSelection);
            }

            return racialFeatSelections;
        }

        public IEnumerable<AdditionalFeatSelection> SelectAdditional()
        {
            var additionalFeatIds = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.FeatGroups, TableNameConstants.Set.Collection.Groups.Additional);
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
            additionalFeatSelection.IsFighterFeat = Convert.ToBoolean(featData[SelectorConstants.AdditionalIsFighterFeatIndex]);
            additionalFeatSelection.IsWizardFeat = Convert.ToBoolean(featData[SelectorConstants.AdditionalIsWizardFeatIndex]);
            additionalFeatSelection.RequiredBaseAttack = Convert.ToInt32(featData[SelectorConstants.AdditionalBaseAttackRequirementIndex]);
            additionalFeatSelection.FocusType = featData[SelectorConstants.AdditionalFocusTypeIndex];
            additionalFeatSelection.Frequency.Quantity = Convert.ToInt32(featData[SelectorConstants.AdditionalFrequencyQuantityIndex]);
            additionalFeatSelection.Frequency.TimePeriod = featData[SelectorConstants.AdditionalFrequencyTimePeriodIndex];
            additionalFeatSelection.Strength = Convert.ToInt32(featData[SelectorConstants.AdditionalStrengthIndex]);

            additionalFeatSelection.RequiredFeatIds = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.AdditionalFeatFeatRequirements, featId);

            var tableName = String.Format(TableNameConstants.Formattable.Adjustments.FEATClassRequirements, featId);
            additionalFeatSelection.RequiredCharacterClasses = adjustmentsSelector.SelectFrom(tableName);

            tableName = String.Format(TableNameConstants.Formattable.Adjustments.FEATSkillRankRequirements, featId);
            additionalFeatSelection.RequiredSkillRanks = adjustmentsSelector.SelectFrom(tableName);

            tableName = String.Format(TableNameConstants.Formattable.Adjustments.FEATStatRequirements, featId);
            additionalFeatSelection.RequiredStats = adjustmentsSelector.SelectFrom(tableName);

            return additionalFeatSelection;
        }

        public IEnumerable<CharacterClassFeatSelection> SelectClass(String characterClassName)
        {
            var classFeatIds = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.FeatGroups, characterClassName);
            var classFeatSelections = new List<CharacterClassFeatSelection>();

            foreach (var classFeatId in classFeatIds)
            {
                var tableName = String.Format(TableNameConstants.Formattable.Collection.CLASSFeatData, characterClassName);
                var featData = collectionsSelector.SelectFrom(tableName, classFeatId).ToArray();

                var classFeatSelection = new CharacterClassFeatSelection();
                classFeatSelection.FeatId = featData[SelectorConstants.ClassFeatIdIndex];
                classFeatSelection.FocusType = featData[SelectorConstants.ClassFocusTypeIndex];
                classFeatSelection.Frequency.Quantity = Convert.ToInt32(featData[SelectorConstants.ClassFrequencyQuantityIndex]);
                classFeatSelection.Frequency.TimePeriod = featData[SelectorConstants.ClassFrequencyTimePeriodIndex];
                classFeatSelection.MinimumLevel = Convert.ToInt32(featData[SelectorConstants.ClassMinimumLevelRequirementIndex]);
                classFeatSelection.Strength = Convert.ToInt32(featData[SelectorConstants.ClassStrengthIndex]);

                classFeatSelections.Add(classFeatSelection);
            }

            return classFeatSelections;
        }
    }
}