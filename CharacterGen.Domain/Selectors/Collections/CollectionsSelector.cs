using CharacterGen.Domain.Mappers.Collections;
using RollGen;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CharacterGen.Domain.Selectors.Collections
{
    internal class CollectionsSelector : ICollectionsSelector
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
            var index = dice.Roll().d(count).AsSum() - 1;
            return collection.ElementAt(index);
        }

        public string FindGroupOf(string tableName, string item, params string[] filteredGroupNames)
        {
            var allGroups = SelectAllFrom(tableName);
            var filteredGroups = allGroups.Where(kvp => filteredGroupNames.Contains(kvp.Key));

            if (!filteredGroups.Any(kvp => kvp.Value.Contains(item)))
                throw new ArgumentException($"No filtered group from [{string.Join(", ", filteredGroupNames)}] in {tableName} is listed for {item}");

            var groupName = filteredGroups.First(kvp => kvp.Value.Contains(item)).Key;

            return groupName;
        }
    }
}