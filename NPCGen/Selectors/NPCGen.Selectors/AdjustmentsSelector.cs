using System;
using System.Collections.Generic;
using System.Linq;
using NPCGen.Selectors.Interfaces;

namespace NPCGen.Selectors
{
    public class AdjustmentsSelector : IAdjustmentsSelector
    {
        private ICollectionsSelector collectionsSelector;

        public AdjustmentsSelector(ICollectionsSelector collectionsSelector)
        {
            this.collectionsSelector = collectionsSelector;
        }

        public Dictionary<String, Int32> SelectFrom(String tableName)
        {
            var collectionTable = collectionsSelector.SelectAllFrom(tableName);
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