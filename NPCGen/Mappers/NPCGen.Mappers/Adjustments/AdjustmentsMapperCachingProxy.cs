using System;
using System.Collections.Generic;
using NPCGen.Mappers.Interfaces;

namespace NPCGen.Mappers.Adjustments
{
    public class AdjustmentsMapperCachingProxy : IAdjustmentMapper
    {
        private IAdjustmentMapper innerMapper;
        private Dictionary<String, Dictionary<String, Int32>> cachedTables;

        public AdjustmentsMapperCachingProxy(IAdjustmentMapper innerMapper)
        {
            this.innerMapper = innerMapper;
            cachedTables = new Dictionary<String, Dictionary<String, Int32>>();
        }

        public Dictionary<String, Int32> Map(String tableName)
        {
            if (cachedTables.ContainsKey(tableName))
                return cachedTables[tableName];

            var table = innerMapper.Map(tableName);
            cachedTables.Add(tableName, table);
            return table;
        }
    }
}