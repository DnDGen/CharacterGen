using System;
using System.Collections.Generic;
using NPCGen.Core.Generation.Xml.Parsers.Interfaces;

namespace NPCGen.Core.Generation.Xml.Parsers
{
    public class LevelAdjustmentXmlParser : ILevelAdjustmentXmlParser
    {
        private IStreamLoader streamLoader;

        public LevelAdjustmentXmlParser(IStreamLoader streamLoader)
        {
            this.streamLoader = streamLoader;
        }

        public Dictionary<String, Int32> Parse(String filename)
        {
            throw new NotImplementedException();
        }
    }
}