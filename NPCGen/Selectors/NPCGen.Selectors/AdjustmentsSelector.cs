using System;
using System.Collections.Generic;
using System.Linq;
using NPCGen.Mappers.Interfaces;
using NPCGen.Selectors.Interfaces;

namespace NPCGen.Selectors
{
    public class AdjustmentsSelector : IAdjustmentsSelector
    {
        private ICollectionsMapper collectionsMapper;

        public AdjustmentsSelector(ICollectionsMapper collectionsMapper)
        {
            this.collectionsMapper = collectionsMapper;
        }

        public Dictionary<String, Int32> SelectFrom(String tableName)
        {
            var collectionTable = collectionsMapper.Map(tableName);
            var adjustmentTable = new Dictionary<String, Int32>();

            foreach (var kvp in collectionTable)
            {
                var firstItem = kvp.Value.First();
                adjustmentTable[kvp.Key] = Convert.ToInt32(firstItem);
            }

            return adjustmentTable;
        }
    }
}