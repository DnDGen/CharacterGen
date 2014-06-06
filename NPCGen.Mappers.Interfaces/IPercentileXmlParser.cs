using System;
using System.Collections.Generic;
using NPCGen.Mappers.Interfaces.Objects;

namespace NPCGen.Mappers.Interfaces
{
    public interface IPercentileXmlParser
    {
        IEnumerable<PercentileObject> Parse(String filename);
    }
}