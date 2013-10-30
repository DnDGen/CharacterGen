using System;
using System.Collections.Generic;
using NPCGen.Core.Generation.Xml.Parsers.Interfaces;

namespace NPCGen.Core.Generation.Xml.Parsers
{
    public class LanguagesXmlParser : ILanguagesXmlParser
    {
        private IStreamLoader streamLoader;

        public LanguagesXmlParser(IStreamLoader streamLoader)
        {
            this.streamLoader = streamLoader;
        }

        public Dictionary<String, IEnumerable<String>> Parse(String tableName)
        {
            throw new NotImplementedException();
        }
    }
}