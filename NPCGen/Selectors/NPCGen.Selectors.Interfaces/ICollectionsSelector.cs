using System;
using System.Collections.Generic;

namespace NPCGen.Selectors.Interfaces
{
    public interface ICollectionsSelector
    {
        IEnumerable<String> SelectFrom(String tableName, String tableEntry);
        Dictionary<String, String> SelectFrom(String tableName);
    }
}