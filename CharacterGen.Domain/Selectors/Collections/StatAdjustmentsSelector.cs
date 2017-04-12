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
            var tableName = string.Format(TableNameConstants.Formattable.Adjustments.AGEStatAdjustments, race.Age.Description);
            var agingEffects = innerSelector.SelectAllFrom(tableName);

            foreach (var stat in stats)
            {
                tableName = string.Format(TableNameConstants.Formattable.Adjustments.STATStatAdjustments, stat);
                var statAdjustments = innerSelector.SelectAllFrom(tableName);

                adjustments[stat] = statAdjustments[race.BaseRace];
                adjustments[stat] += statAdjustments[race.Metarace];
                adjustments[stat] += agingEffects[stat];
            }

            return adjustments;
        }
    }
}