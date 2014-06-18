using System;
using System.Collections.Generic;
using NPCGen.Mappers.Interfaces;

namespace NPCGen.Mappers.Collections
{
    public class CollectionsMapperCachingProxy : ICollectionsMapper
    {
        private ICollectionsMapper innerMapper;
        private Dictionary<String, Dictionary<String, IEnumerable<String>>> cachedTables;

        public CollectionsMapperCachingProxy(ICollectionsMapper innerMapper)
        {
            this.innerMapper = innerMapper;
            cachedTables = new Dictionary<String, Dictionary<String, IEnumerable<String>>>();
        }

        public Dictionary<String, IEnumerable<String>> Map(String tableName)
        {
            if (cachedTables.ContainsKey(tableName))
                return cachedTables[tableName];

            var table = innerMapper.Map(tableName);
            cachedTables.Add(tableName, table);
            return table;
        }
    }
}