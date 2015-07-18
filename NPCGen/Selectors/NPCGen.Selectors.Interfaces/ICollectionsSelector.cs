using System;
using System.Collections.Generic;

namespace NPCGen.Selectors.Interfaces
{
    public interface ICollectionsSelector
    {
        IEnumerable<String> SelectFrom(String tableName, String tableEntry);
        Dictionary<String, IEnumerable<String>> SelectAllFrom(String tableName);
        String SelectRandomFrom(IEnumerable<String> collection);
        T SelectRandomFrom<T>(IEnumerable<T> collection);
        String SelectRandomFrom(String tableName, String tableEntry);
    }
}