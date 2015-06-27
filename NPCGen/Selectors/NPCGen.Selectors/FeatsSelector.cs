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
        private const Int32 RacialSizeRequirementIndex = 0;
        private const Int32 RacialMinimumHitDiceRequirementIndex = 1;
        private const Int32 RacialStrengthIndex = 2;
        private const Int32 RacialFocusIndex = 3;
        private const Int32 RacialFrequencyQuantityIndex = 4;
        private const Int32 RacialFrequencyTimePeriodIndex = 5;
        private const Int32 ClassMinimumLevelRequirementIndex = 0;
        private const Int32 ClassFocusTypeIndex = 1;
        private const Int32 ClassStrengthIndex = 2;
        private const Int32 ClassFrequencyQuantityIndex = 3;
        private const Int32 ClassFrequencyTimePeriodIndex = 4;
        private const Int32 AdditionalIsFighterFeatIndex = 0;
        private const Int32 AdditionalIsWizardFeatIndex = 1;
        private const Int32 AdditionalBaseAttackRequirementIndex = 2;
        private const Int32 AdditionalFocusTypeIndex = 3;

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
                var racialFeatSelection = new RacialFeatSelection();
                racialFeatSelection.FeatId = racialFeatId;

                var tableName = String.Format(TableNameConstants.Formattable.Collection.RACEFeatData, raceId);
                var featData = collectionsSelector.SelectFrom(tableName, racialFeatId).ToArray();

                racialFeatSelection.SizeRequirement = featData[RacialSizeRequirementIndex];
                racialFeatSelection.Strength = Convert.ToInt32(featData[RacialStrengthIndex]);
                racialFeatSelection.MinimumHitDieRequirement = Convert.ToInt32(featData[RacialMinimumHitDiceRequirementIndex]);
                racialFeatSelection.Focus = featData[RacialFocusIndex];
                racialFeatSelection.Frequency.Quantity = Convert.ToInt32(featData[RacialFrequencyQuantityIndex]);
                racialFeatSelection.Frequency.TimePeriod = featData[RacialFrequencyTimePeriodIndex];

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
            additionalFeatSelection.IsFighterFeat = Convert.ToBoolean(featData[AdditionalIsFighterFeatIndex]);
            additionalFeatSelection.IsWizardFeat = Convert.ToBoolean(featData[AdditionalIsWizardFeatIndex]);
            additionalFeatSelection.RequiredBaseAttack = Convert.ToInt32(featData[AdditionalBaseAttackRequirementIndex]);
            additionalFeatSelection.FocusType = featData[AdditionalFocusTypeIndex];

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
                var classFeatSelection = new CharacterClassFeatSelection();
                classFeatSelection.FeatId = classFeatId;

                var tableName = String.Format(TableNameConstants.Formattable.Collection.CLASSFeatData, characterClassName);
                var featData = collectionsSelector.SelectFrom(tableName, classFeatId).ToArray();

                classFeatSelection.FocusType = featData[ClassFocusTypeIndex];
                classFeatSelection.Frequency.Quantity = Convert.ToInt32(featData[ClassFrequencyQuantityIndex]);
                classFeatSelection.Frequency.TimePeriod = featData[ClassFrequencyTimePeriodIndex];
                classFeatSelection.MinimumLevel = Convert.ToInt32(featData[ClassMinimumLevelRequirementIndex]);
                classFeatSelection.Strength = Convert.ToInt32(featData[ClassStrengthIndex]);

                classFeatSelections.Add(classFeatSelection);
            }

            return classFeatSelections;
        }
    }
}