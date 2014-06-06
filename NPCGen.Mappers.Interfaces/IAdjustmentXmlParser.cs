using System;
using System.Collections.Generic;

namespace NPCGen.Mappers.Interfaces
{
    public interface IAdjustmentXmlParser
    {
        Dictionary<String, Int32> Parse(String filename);
    }
}