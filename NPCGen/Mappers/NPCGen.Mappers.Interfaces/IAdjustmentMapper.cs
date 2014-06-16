using System;
using System.Collections.Generic;

namespace NPCGen.Mappers.Interfaces
{
    public interface IAdjustmentMapper
    {
        Dictionary<String, Int32> Map(String tableName);
    }
}