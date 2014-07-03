using System;
using System.Collections.Generic;
using NPCGen.Common.Abilities.Stats;

namespace NPCGen.Mappers.Interfaces
{
    public interface IStatPriorityMapper
    {
        Dictionary<String, StatPriority> Map(String tableName);
    }
}