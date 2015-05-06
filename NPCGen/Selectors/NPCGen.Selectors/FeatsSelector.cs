using System;
using System.Collections.Generic;
using System.Linq;
using NPCGen.Common.CharacterClasses;
using NPCGen.Common.Races;
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

        private ICollectionsSelector collectionsSelector;
        private IAdjustmentsSelector adjustmentsSelector;

        public FeatsSelector(ICollectionsSelector collectionsSelector, IAdjustmentsSelector adjustmentsSelector)
        {
            this.collectionsSelector = collectionsSelector;
            this.adjustmentsSelector = adjustmentsSelector;
        }

        public IEnumerable<RacialFeatSelection> SelectRacial(String raceId)
        {
            var racialFeatIds = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.FeatGroups, TableNameConstants.Set.Collection.Groups.Racial);
            var racialFeatSelections = new List<RacialFeatSelection>();

            foreach (var racialFeatId in racialFeatIds)
            {
                var racialFeatSelection = new RacialFeatSelection();
                racialFeatSelection.FeatId = racialFeatId;

                var featData = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.FeatData, racialFeatId).ToList();
                racialFeatSelection.SizeRequirement = featData[RacialSizeRequirementIndex];
                racialFeatSelection.Strength = Convert.ToInt32(featData[RacialStrengthIndex]);
                racialFeatSelection.MinimumHitDieRequirement = Convert.ToInt32(featData[RacialMinimumHitDiceRequirementIndex]);

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

        public AdditionalFeatSelection SelectAdditional(String featId)
        {
            var additionalFeatSelection = new AdditionalFeatSelection();
            additionalFeatSelection.Name.Id = featId;

            var featData = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.FeatData, featId).ToArray();
            additionalFeatSelection.IsFighterFeat = Convert.ToBoolean(featData[0]);
            additionalFeatSelection.IsWizardFeat = Convert.ToBoolean(featData[1]);
            additionalFeatSelection.RequiredBaseAttack = Convert.ToInt32(featData[2]);
            additionalFeatSelection.SpecificApplicationType = featData[3];

            additionalFeatSelection.RequiredClassNames = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.AdditionalFeatClassNameRequirements, featId);
            additionalFeatSelection.RequiredFeatIds = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.AdditionalFeatFeatRequirements, featId);

            var tableName = String.Format(TableNameConstants.Formattable.Adjustments.FEATSkillRankRequirements, featId);
            additionalFeatSelection.RequiredSkillRanks = adjustmentsSelector.SelectFrom(tableName);

            tableName = String.Format(TableNameConstants.Formattable.Adjustments.FEATStatRequirements, featId);
            additionalFeatSelection.RequiredStats = adjustmentsSelector.SelectFrom(tableName);

            return additionalFeatSelection;
        }

        public IEnumerable<CharacterClassFeatSelection> SelectClass(String characterClassName)
        {
            var classFeatIds = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.FeatGroups, TableNameConstants.Set.Collection.Groups.CharacterClasses);
            var classFeatSelections = new List<CharacterClassFeatSelection>();

            foreach (var classFeatId in classFeatIds)
            {
                var classFeatSelection = new CharacterClassFeatSelection();
                classFeatSelection.Name.Id = classFeatId;

                var featData = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.FeatData, classFeatId);
                var strength = featData.First();
                classFeatSelection.Strength = Convert.ToInt32(strength);

                var classes = featData.Except(new[] { strength });
                foreach (var className in classes)
                {
                    var tableName = String.Format(TableNameConstants.Formattable.Adjustments.CLASSFeatLevelRequirements, className);
                    var levelRequirements = adjustmentsSelector.SelectFrom(tableName);

                    classFeatSelection.LevelRequirements[className] = levelRequirements[classFeatId];
                }

                classFeatSelections.Add(classFeatSelection);
            }

            return classFeatSelections;
        }

        public IEnumerable<object> racialFeatIds { get; set; }
    }
}