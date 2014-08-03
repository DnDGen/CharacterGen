using System;
using System.Collections.Generic;
using NPCGen.Selectors.Interfaces;

namespace NPCGen.Selectors
{
    public class CollectionsSelector : ICollectionsSelector
    {
        public IEnumerable<String> SelectFrom(String tableName)
        {
            throw new NotImplementedException();
        }
    }
}