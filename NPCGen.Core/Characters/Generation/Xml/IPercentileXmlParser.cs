using System;
using System.Collections.Generic;

namespace NPCGen.Core.Characters.Generation.Xml
{
    public interface IPercentileXmlParser
    {
        List<PercentileObject> Parse(String filename);
    }
}