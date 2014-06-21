using System;
using System.Collections.Generic;
using NPCGen.Common.Races;
using NPCGen.Common.Stats;
using NPCGen.Mappers.Interfaces;
using NPCGen.Selectors.Interfaces;

namespace NPCGen.Selectors
{
    public class StatAdjustmentsSelector : IStatAdjustmentsSelector
    {
        private IAdjustmentMapper adjustmentMapper;

        public StatAdjustmentsSelector(IAdjustmentMapper adjustmentMapper)
        {
            this.adjustmentMapper = adjustmentMapper;
        }

        public Dictionary<String, Int32> GetAdjustmentsFor(Race race)
        {
            var adjustments = new Dictionary<String, Int32>();

            foreach (var stat in StatConstants.GetStats())
            {
                var tableName = String.Format("{0}StatAdjustments", stat);
                var statAdjustments = adjustmentMapper.Map(tableName);

                adjustments[stat] = statAdjustments[race.BaseRace] + statAdjustments[race.Metarace];
            }

            return adjustments;
        }
    }
}