using System;
using System.Collections.Generic;
using NPCGen.Mappers.Interfaces;
using NPCGen.Selectors.Interfaces;

namespace NPCGen.Selectors
{
    public class LevelAdjustmentsProvider : ILevelAdjustmentsProvider
    {
        private IAdjustmentXmlParser adjustmentXmlParser;

        public LevelAdjustmentsProvider(IAdjustmentXmlParser adjustmentXmlParser)
        {
            this.adjustmentXmlParser = adjustmentXmlParser;
        }

        public Dictionary<String, Int32> GetLevelAdjustments()
        {
            return adjustmentXmlParser.Parse("LevelAdjustments.xml");
        }
    }
}