using System;
using System.Collections.Generic;

namespace CharacterGen.Selectors
{
    public interface ICollectionsSelector
    {
        IEnumerable<String> SelectFrom(String tableName, String tableEntry);
        Dictionary<String, IEnumerable<String>> SelectAllFrom(String tableName);
        T SelectRandomFrom<T>(IEnumerable<T> collection);
        String SelectRandomFrom(String tableName, String tableEntry);
    }
}