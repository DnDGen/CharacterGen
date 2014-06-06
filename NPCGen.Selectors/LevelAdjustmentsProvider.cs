using System;
using System.Collections.Generic;
using NPCGen.Core.Generation.Providers.Interfaces;
using NPCGen.Core.Generation.Xml.Parsers.Interfaces;

namespace NPCGen.Core.Generation.Providers
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