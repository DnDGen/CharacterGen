using System;
using System.Collections.Generic;
using NPCGen.Common;

namespace NPCGen.Mappers.Interfaces
{
    public interface IStatPriorityMapper
    {
        Dictionary<String, StatPriority> Map(String tableName);
    }
}