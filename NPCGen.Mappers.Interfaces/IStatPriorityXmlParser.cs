using System;
using System.Collections.Generic;
using NPCGen.Common;

namespace NPCGen.Mappers.Interfaces
{
    public interface IStatPriorityXmlParser
    {
        Dictionary<String, StatPriority> Parse(String filename);
    }
}