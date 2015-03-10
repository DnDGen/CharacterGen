using System;
using System.Collections.Generic;
using NPCGen.Common.Abilities.Stats;
using NPCGen.Common.Races;
using NPCGen.Selectors.Interfaces;
using NPCGen.Tables.Interfaces;

namespace NPCGen.Selectors
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

                adjustments[stat] = statAdjustments[race.BaseRace.Id] + statAdjustments[race.Metarace.Id];
            }

            return adjustments;
        }
    }
}