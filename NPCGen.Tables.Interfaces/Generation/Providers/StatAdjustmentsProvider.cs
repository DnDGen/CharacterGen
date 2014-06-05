using System;
using System.Collections.Generic;
using NPCGen.Core.Data.Races;
using NPCGen.Core.Data.Stats;
using NPCGen.Core.Generation.Providers.Interfaces;
using NPCGen.Core.Generation.Xml.Parsers.Interfaces;

namespace NPCGen.Core.Generation.Providers
{
    public class StatAdjustmentsProvider : IStatAdjustmentsProvider
    {
        private IAdjustmentXmlParser adjustmentXmlParser;

        public StatAdjustmentsProvider(IAdjustmentXmlParser adjustmentXmlParser)
        {
            this.adjustmentXmlParser = adjustmentXmlParser;
        }

        public Dictionary<String, Int32> GetAdjustments(Race race)
        {
            var adjustments = new Dictionary<String, Int32>();

            foreach (var stat in StatConstants.GetStats())
            {
                var filename = String.Format("{0}StatAdjustments.xml", stat);
                var statAdjustments = adjustmentXmlParser.Parse(filename);

                adjustments.Add(stat, statAdjustments[race.BaseRace]);

                if (!String.IsNullOrEmpty(race.Metarace))
                    adjustments[stat] += statAdjustments[race.Metarace];
            }

            return adjustments;
        }
    }
}