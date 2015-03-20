using System;
using System.Collections.Generic;
using NPCGen.Mappers.Interfaces;
using NPCGen.Selectors.Interfaces;

namespace NPCGen.Selectors
{
    public class CollectionsSelector : ICollectionsSelector
    {
        private ICollectionsMapper mapper;

        public CollectionsSelector(ICollectionsMapper mapper)
        {
            this.mapper = mapper;
        }

        public IEnumerable<String> SelectFrom(String tableName, String tableEntry)
        {
            var table = mapper.Map(tableName);
            return table[tableEntry];
        }

        public Dictionary<String, IEnumerable<String>> SelectAllFrom(String tableName)
        {
            return mapper.Map(tableName);
        }
    }
}