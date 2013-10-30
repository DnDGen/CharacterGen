using System;
using System.Collections.Generic;

namespace NPCGen.Core.Generation.Xml.Parsers.Interfaces
{
    public interface ILanguagesXmlParser
    {
        Dictionary<String, IEnumerable<String>> Parse(String filename);
    }
}