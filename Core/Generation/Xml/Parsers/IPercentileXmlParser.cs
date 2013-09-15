using System;
using System.Collections.Generic;

namespace NPCGen.Core.Generation.Xml.Parsers
{
    public interface IPercentileXmlParser
    {
        List<PercentileObject> Parse(String filename);
    }
}