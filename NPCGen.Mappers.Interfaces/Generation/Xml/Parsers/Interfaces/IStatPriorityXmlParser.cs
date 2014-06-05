using System;
using System.Collections.Generic;
using NPCGen.Core.Generation.Xml.Parsers.Objects;

namespace NPCGen.Core.Generation.Xml.Parsers.Interfaces
{
    public interface IStatPriorityXmlParser
    {
        Dictionary<String, StatPriorityObject> Parse(String filename);
    }
}