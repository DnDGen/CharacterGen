using System;
using System.Collections.Generic;

namespace NPCGen.Mappers.Interfaces
{
    public interface ICollectionsMapper
    {
        Dictionary<String, IEnumerable<String>> Map(String tableName);
    }
}