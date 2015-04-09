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

        public IEnumerable<RacialFeatSelection> SelectRacial()
        {
            var racialFeatIds = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.FeatGroups, TableNameConstants.Set.Collection.Groups.Racial);
            var racialFeatSelections = new List<RacialFeatSelection>();

            foreach (var racialFeatId in racialFeatIds)
            {
                var racialFeatSelection = new RacialFeatSelection();
                racialFeatSelection.Name.Id = racialFeatId;

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
            throw new NotImplementedException();
        }

        public AdditionalFeatSelection SelectAdditional(String featName)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CharacterClassFeatSelection> SelectClassFeats()
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