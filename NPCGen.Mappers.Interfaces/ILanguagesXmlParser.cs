using System;
using System.Collections.Generic;

namespace NPCGen.Mappers.Interfaces
{
    public interface ILanguagesXmlParser
    {
        Dictionary<String, IEnumerable<String>> Parse(String filename);
    }
}