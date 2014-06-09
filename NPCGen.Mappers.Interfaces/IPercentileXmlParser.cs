using System;
using System.Collections.Generic;

namespace NPCGen.Mappers.Interfaces
{
    public interface IPercentileXmlParser
    {
        Dictionary<Int32, String> Parse(String filename);
    }
}