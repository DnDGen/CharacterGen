using System;
using System.Collections.Generic;

namespace CharacterGen.Mappers.Domain.Percentiles
{
    public class PercentileMapperCachingProxy : IPercentileMapper
    {
        private IPercentileMapper innerMapper;
        private Dictionary<String, Dictionary<Int32, String>> cachedTables;

        public PercentileMapperCachingProxy(IPercentileMapper innerMapper)
        {
            this.innerMapper = innerMapper;
            cachedTables = new Dictionary<String, Dictionary<Int32, String>>();
        }

        public Dictionary<Int32, String> Map(String tableName)
        {
            if (cachedTables.ContainsKey(tableName))
                return cachedTables[tableName];

            var table = innerMapper.Map(tableName);
            cachedTables.Add(tableName, table);
            return table;
        }
    }
}