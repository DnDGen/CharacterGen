using System;
using System.Collections.Generic;
using NPCGen.Mappers.Interfaces;
using NPCGen.Common.Races;
using NPCGen.Common.Stats;
using NPCGen.Selectors.Interfaces;

namespace NPCGen.Selectors
{
    public class StatAdjustmentsSelector : IStatAdjustmentsSelector
    {
        private IAdjustmentMapper adjustmentXmlMapper;

        public StatAdjustmentsSelector(IAdjustmentMapper adjustmentXmlMapper)
        {
            this.adjustmentXmlMapper = adjustmentXmlMapper;
        }

        public Dictionary<String, Int32> GetAdjustments(Race race)
        {
            var adjustments = new Dictionary<String, Int32>();

            foreach (var stat in StatConstants.GetStats())
            {
                var filename = String.Format("{0}StatAdjustments.xml", stat);
                var statAdjustments = adjustmentXmlMapper.Parse(filename);

                adjustments.Add(stat, statAdjustments[race.BaseRace]);

                if (!String.IsNullOrEmpty(race.Metarace))
                    adjustments[stat] += statAdjustments[race.Metarace];
            }

            return adjustments;
        }
    }
}