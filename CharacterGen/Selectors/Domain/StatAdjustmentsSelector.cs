using CharacterGen.Common.Races;
using CharacterGen.Tables;
using System;
using System.Collections.Generic;

namespace CharacterGen.Selectors.Domain
{
    public class StatAdjustmentsSelector : IStatAdjustmentsSelector
    {
        private IAdjustmentsSelector innerSelector;
        private ICollectionsSelector collectionsSelector;

        public StatAdjustmentsSelector(IAdjustmentsSelector innerSelector, ICollectionsSelector collectionsSelector)
        {
            this.innerSelector = innerSelector;
            this.collectionsSelector = collectionsSelector;
        }

        public Dictionary<String, Int32> SelectFor(Race race)
        {
            var adjustments = new Dictionary<String, Int32>();
            var stats = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.StatGroups, GroupConstants.All);

            foreach (var stat in stats)
            {
                var tableName = String.Format(TableNameConstants.Formattable.Adjustments.STATStatAdjustments, stat);
                var statAdjustments = innerSelector.SelectFrom(tableName);

                adjustments[stat] = statAdjustments[race.BaseRace] + statAdjustments[race.Metarace];
            }

            return adjustments;
        }
    }
}