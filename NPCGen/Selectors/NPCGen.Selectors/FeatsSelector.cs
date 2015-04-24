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
        private ICollectionsSelector collectionsSelector;
        private IAdjustmentsSelector adjustmentsSelector;
        private INameSelector nameSelector;

        public FeatsSelector(ICollectionsSelector collectionsSelector, IAdjustmentsSelector adjustmentsSelector, INameSelector nameSelector)
        {
            this.collectionsSelector = collectionsSelector;
            this.adjustmentsSelector = adjustmentsSelector;
            this.nameSelector = nameSelector;
        }

        public IEnumerable<RacialFeatSelection> SelectFor(Race race)
        {
            var racialFeatIds = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.FeatGroups, TableNameConstants.Set.Collection.Groups.Racial);
            var racialFeatSelections = new List<RacialFeatSelection>();

            foreach (var racialFeatId in racialFeatIds)
            {
                var racialFeatSelection = new RacialFeatSelection();
                racialFeatSelection.Name.Id = racialFeatId;
                racialFeatSelection.Name.Name = nameSelector.Select(racialFeatId);

                var featData = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.FeatData, racialFeatId);
                racialFeatSelection.SizeRequirement = featData.First();
                racialFeatSelection.FeatStrength = Convert.ToInt32(featData.Last());

                racialFeatSelection.BaseRaceIdRequirements = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.RacialFeatBaseRaceRequirements, racialFeatId);
                racialFeatSelection.HitDieRequirements = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.RacialFeatHitDieRequirements, racialFeatId)
                    .Select(r => Convert.ToInt32(r));
                racialFeatSelection.MetaraceIdRequirements = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.RacialFeatMetaraceRequirements, racialFeatId);
                racialFeatSelection.MetaraceSpeciesRequirements = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.RacialFeatMetaraceSpeciesRequirements, racialFeatId);

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
            additionalFeatSelection.Name.Name = nameSelector.Select(featId);

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

        public IEnumerable<CharacterClassFeatSelection> SelectFor(CharacterClass characterClass)
        {
            var classFeatIds = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.FeatGroups, TableNameConstants.Set.Collection.Groups.CharacterClasses);
            var classFeatSelections = new List<CharacterClassFeatSelection>();

            foreach (var classFeatId in classFeatIds)
            {
                var classFeatSelection = new CharacterClassFeatSelection();
                classFeatSelection.Name.Id = classFeatId;
                classFeatSelection.Name.Name = nameSelector.Select(classFeatId);

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