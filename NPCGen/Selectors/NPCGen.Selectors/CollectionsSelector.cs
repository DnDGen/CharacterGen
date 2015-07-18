using System;
using System.Collections.Generic;
using System.Linq;
using D20Dice;
using NPCGen.Mappers.Interfaces;
using NPCGen.Selectors.Interfaces;

namespace NPCGen.Selectors
{
    public class CollectionsSelector : ICollectionsSelector
    {
        private ICollectionsMapper mapper;
        private IDice dice;

        public CollectionsSelector(ICollectionsMapper mapper, IDice dice)
        {
            this.mapper = mapper;
            this.dice = dice;
        }

        public IEnumerable<String> SelectFrom(String tableName, String tableEntry)
        {
            var table = SelectAllFrom(tableName);

            if (table.ContainsKey(tableEntry) == false)
            {
                var message = String.Format("{0} is not a valid entry in the table {1}", tableEntry, tableName);
                throw new ArgumentException(message);
            }

            return table[tableEntry];
        }

        public Dictionary<String, IEnumerable<String>> SelectAllFrom(String tableName)
        {
            return mapper.Map(tableName);
        }

        public String SelectRandomFrom(IEnumerable<String> collection)
        {
            return SelectRandomFrom<String>(collection);
        }

        public String SelectRandomFrom(String tableName, String tableEntry)
        {
            var collection = SelectFrom(tableName, tableEntry);
            return SelectRandomFrom(collection);
        }

        public T SelectRandomFrom<T>(IEnumerable<T> collection)
        {
            if (collection.Any() == false)
                throw new ArgumentException("Cannot select random from an empty collection");

            var count = collection.Count();
            var index = dice.Roll().d(count) - 1;
            return collection.ElementAt(index);
        }
    }
}