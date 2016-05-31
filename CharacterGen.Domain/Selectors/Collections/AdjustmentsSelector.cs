using System;
using System.Collections.Generic;
using System.Linq;

namespace CharacterGen.Domain.Selectors.Collections
{
    internal class AdjustmentsSelector : IAdjustmentsSelector
    {
        private ICollectionsSelector collectionsSelector;

        public AdjustmentsSelector(ICollectionsSelector collectionsSelector)
        {
            this.collectionsSelector = collectionsSelector;
        }

        public Dictionary<string, int> SelectFrom(string tableName)
        {
            var collectionTable = collectionsSelector.SelectAllFrom(tableName);
            var adjustmentTable = new Dictionary<string, int>();

            foreach (var kvp in collectionTable)
            {
                var firstItem = kvp.Value.First();
                adjustmentTable[kvp.Key] = Convert.ToInt32(firstItem);
            }

            return adjustmentTable;
        }
    }
}