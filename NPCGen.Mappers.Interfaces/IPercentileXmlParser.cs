using NPCGen.Core.Generation.Xml.Parsers.Objects;
using System;
using System.Collections.Generic;

namespace NPCGen.Core.Generation.Xml.Parsers.Interfaces
{
    public interface IPercentileXmlParser
    {
        IEnumerable<PercentileObject> Parse(String filename);
    }
}