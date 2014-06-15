using System;
using System.Collections.Generic;
using NPCGen.Mappers.Interfaces;
using NPCGen.Selectors.Interfaces;

namespace NPCGen.Selectors
{
    public class LevelAdjustmentsSelector : ILevelAdjustmentsSelector
    {
        private IAdjustmentMapper adjustmentXmlParser;

        public LevelAdjustmentsSelector(IAdjustmentMapper adjustmentXmlParser)
        {
            this.adjustmentXmlParser = adjustmentXmlParser;
        }

        public Dictionary<String, Int32> GetLevelAdjustments()
        {
            return adjustmentXmlParser.Parse("LevelAdjustments.xml");
        }
    }
}