using System;
using System.Collections.Generic;
using System.Linq;
using D20Dice;
using NPCGen.Mappers.Interfaces;
using NPCGen.Selectors.Interfaces;

namespace NPCGen.Selectors
{
    public class PercentileSelector : IPercentileSelector
    {
        private IPercentileMapper percentileXmlMapper;
        private IDice dice;
        private Dictionary<String, Dictionary<Int32, String>> cachedTables;

        public PercentileSelector(IPercentileMapper percentileXmlMapper, IDice dice)
        {
            this.percentileXmlMapper = percentileXmlMapper;
            this.dice = dice;
            cachedTables = new Dictionary<String, Dictionary<Int32, String>>();
        }

        public String GetPercentileResult(String tableName)
        {
            if (!cachedTables.ContainsKey(tableName))
                CacheTable(tableName);

            var roll = dice.Percentile();
            return cachedTables[tableName][roll];
        }

        private void CacheTable(String tableName)
        {
            var table = percentileXmlMapper.Map(tableName);
            cachedTables.Add(tableName, table);
        }

        public IEnumerable<String> GetAllResults(String tableName)
        {
            if (!cachedTables.ContainsKey(tableName))
                CacheTable(tableName);

            return cachedTables[tableName].Values.Distinct();
        }
    }
}