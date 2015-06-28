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
                racialFeatSelection.FeatId = featData[RacialFeatSelection.FeatIdIndex];
                racialFeatSelection.SizeRequirement = featData[RacialFeatSelection.SizeRequirementIndex];
                racialFeatSelection.Strength = Convert.ToInt32(featData[RacialFeatSelection.StrengthIndex]);
                racialFeatSelection.MinimumHitDieRequirement = Convert.ToInt32(featData[RacialFeatSelection.MinimumHitDiceRequirementIndex]);
                racialFeatSelection.Focus = featData[RacialFeatSelection.FocusIndex];
                racialFeatSelection.Frequency.Quantity = Convert.ToInt32(featData[RacialFeatSelection.FrequencyQuantityIndex]);
                racialFeatSelection.Frequency.TimePeriod = featData[RacialFeatSelection.FrequencyTimePeriodIndex];

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
            additionalFeatSelection.IsFighterFeat = Convert.ToBoolean(featData[AdditionalFeatSelection.IsFighterFeatIndex]);
            additionalFeatSelection.IsWizardFeat = Convert.ToBoolean(featData[AdditionalFeatSelection.IsWizardFeatIndex]);
            additionalFeatSelection.RequiredBaseAttack = Convert.ToInt32(featData[AdditionalFeatSelection.BaseAttackRequirementIndex]);
            additionalFeatSelection.FocusType = featData[AdditionalFeatSelection.FocusTypeIndex];
            additionalFeatSelection.Frequency.Quantity = Convert.ToInt32(featData[AdditionalFeatSelection.FrequencyQuantityIndex]);
            additionalFeatSelection.Frequency.TimePeriod = featData[AdditionalFeatSelection.FrequencyTimePeriodIndex];
            additionalFeatSelection.Strength = Convert.ToInt32(featData[AdditionalFeatSelection.StrengthIndex]);

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
                classFeatSelection.FeatId = featData[CharacterClassFeatSelection.FeatIdIndex];
                classFeatSelection.FocusType = featData[CharacterClassFeatSelection.FocusTypeIndex];
                classFeatSelection.Frequency.Quantity = Convert.ToInt32(featData[CharacterClassFeatSelection.FrequencyQuantityIndex]);
                classFeatSelection.Frequency.TimePeriod = featData[CharacterClassFeatSelection.FrequencyTimePeriodIndex];
                classFeatSelection.MinimumLevel = Convert.ToInt32(featData[CharacterClassFeatSelection.MinimumLevelRequirementIndex]);
                classFeatSelection.Strength = Convert.ToInt32(featData[CharacterClassFeatSelection.StrengthIndex]);
                classFeatSelection.MaximumLevel = Convert.ToInt32(featData[CharacterClassFeatSelection.MaximumLevelRequirementIndex]);

                classFeatSelections.Add(classFeatSelection);
            }

            return classFeatSelections;
        }
    }
}