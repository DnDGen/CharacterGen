using System;
using System.Collections.Generic;
using System.Linq;
using NPCGen.Mappers.Interfaces;

namespace NPCGen.Mappers.Collections
{
    public class AdjustmentXmlMapper : IAdjustmentMapper
    {
        private ICollectionsMapper innerMapper;

        public AdjustmentXmlMapper(ICollectionsMapper innerMapper)
        {
            this.innerMapper = innerMapper;
        }

        public Dictionary<String, Int32> Map(String tableName)
        {
            var collectionTable = innerMapper.Map(tableName);
            var adjustmentTable = new Dictionary<String, Int32>();

            foreach (var kvp in collectionTable)
            {
                var firstItem = kvp.Value.First();
                var adjustment = Convert.ToInt32(firstItem);

                adjustmentTable.Add(kvp.Key, adjustment);
            }

            return adjustmentTable;
        }
    }
}