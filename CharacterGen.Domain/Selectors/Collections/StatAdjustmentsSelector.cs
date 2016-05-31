using CharacterGen.Domain.Tables;
using CharacterGen.Races;
using System.Collections.Generic;

namespace CharacterGen.Domain.Selectors.Collections
{
    internal class StatAdjustmentsSelector : IStatAdjustmentsSelector
    {
        private IAdjustmentsSelector innerSelector;
        private ICollectionsSelector collectionsSelector;

        public StatAdjustmentsSelector(IAdjustmentsSelector innerSelector, ICollectionsSelector collectionsSelector)
        {
            this.innerSelector = innerSelector;
            this.collectionsSelector = collectionsSelector;
        }

        public Dictionary<string, int> SelectFor(Race race)
        {
            var adjustments = new Dictionary<string, int>();
            var stats = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.StatGroups, GroupConstants.All);

            foreach (var stat in stats)
            {
                var tableName = string.Format(TableNameConstants.Formattable.Adjustments.STATStatAdjustments, stat);
                var statAdjustments = innerSelector.SelectFrom(tableName);

                adjustments[stat] = statAdjustments[race.BaseRace] + statAdjustments[race.Metarace];
            }

            return adjustments;
        }
    }
}