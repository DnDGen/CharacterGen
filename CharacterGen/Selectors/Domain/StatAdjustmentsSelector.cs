using CharacterGen.Common.Abilities.Stats;
using CharacterGen.Common.Races;
using CharacterGen.Tables;
using System;
using System.Collections.Generic;

namespace CharacterGen.Selectors.Domain
{
    public class StatAdjustmentsSelector : IStatAdjustmentsSelector
    {
        private IAdjustmentsSelector innerSelector;

        public StatAdjustmentsSelector(IAdjustmentsSelector innerSelector)
        {
            this.innerSelector = innerSelector;
        }

        public Dictionary<String, Int32> SelectFor(Race race)
        {
            var adjustments = new Dictionary<String, Int32>();

            foreach (var stat in StatConstants.GetStats())
            {
                var tableName = String.Format(TableNameConstants.Formattable.Adjustments.STATStatAdjustments, stat);
                var statAdjustments = innerSelector.SelectFrom(tableName);

                adjustments[stat] = statAdjustments[race.BaseRace] + statAdjustments[race.Metarace];
            }

            return adjustments;
        }
    }
}