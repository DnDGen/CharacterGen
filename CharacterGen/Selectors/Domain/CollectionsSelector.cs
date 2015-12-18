using CharacterGen.Mappers;
using RollGen;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CharacterGen.Selectors.Domain
{
    public class CollectionsSelector : ICollectionsSelector
    {
        private CollectionsMapper mapper;
        private Dice dice;

        public CollectionsSelector(CollectionsMapper mapper, Dice dice)
        {
            this.mapper = mapper;
            this.dice = dice;
        }

        public IEnumerable<string> SelectFrom(string tableName, string tableEntry)
        {
            var table = SelectAllFrom(tableName);

            if (table.ContainsKey(tableEntry) == false)
            {
                var message = string.Format("{0} is not a valid entry in the table {1}", tableEntry, tableName);
                throw new ArgumentException(message);
            }

            return table[tableEntry];
        }

        public Dictionary<string, IEnumerable<string>> SelectAllFrom(string tableName)
        {
            return mapper.Map(tableName);
        }

        public string SelectRandomFrom(string tableName, string tableEntry)
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