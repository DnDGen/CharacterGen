using EventGen;
using System.Collections.Generic;

namespace CharacterGen.Domain.Selectors.Collections
{
    internal class CollectionsSelectorEventGenDecorator : ICollectionsSelector
    {
        private ICollectionsSelector innerSelector;
        private GenEventQueue eventQueue;

        public CollectionsSelectorEventGenDecorator(ICollectionsSelector innerSelector, GenEventQueue eventQueue)
        {
            this.innerSelector = innerSelector;
            this.eventQueue = eventQueue;
        }

        public IEnumerable<string> SelectFrom(string tableName, string tableEntry)
        {
            eventQueue.Enqueue("CharacterGen", $"Beginning selection of {tableEntry} in {tableName}");
            var collection = innerSelector.SelectFrom(tableName, tableEntry);
            eventQueue.Enqueue("CharacterGen", $"Completed selection of [{string.Join(", ", collection)}]");

            return collection;
        }

        public Dictionary<string, IEnumerable<string>> SelectAllFrom(string tableName)
        {
            eventQueue.Enqueue("CharacterGen", $"Beginning selection of all in {tableName}");
            var collections = innerSelector.SelectAllFrom(tableName);
            eventQueue.Enqueue("CharacterGen", $"Completed selection of all in {tableName}");

            return collections;
        }

        public string SelectRandomFrom(string tableName, string tableEntry)
        {
            eventQueue.Enqueue("CharacterGen", $"Beginning random selection from {tableEntry} in {tableName}");
            var randomItem = innerSelector.SelectRandomFrom(tableName, tableEntry);
            eventQueue.Enqueue("CharacterGen", $"Completed random selection of {randomItem}");

            return randomItem;
        }

        public T SelectRandomFrom<T>(IEnumerable<T> collection)
        {
            eventQueue.Enqueue("CharacterGen", $"Beginning random selection from [{string.Join(", ", collection)}]");
            var randomItem = innerSelector.SelectRandomFrom(collection);
            eventQueue.Enqueue("CharacterGen", $"Completed random selection of {randomItem}");

            return randomItem;
        }

        public string FindGroupOf(string tableName, string item, params string[] filteredGroupNames)
        {
            eventQueue.Enqueue("CharacterGen", $"Beginning selection of group for {item} in {tableName} from [{string.Join(", ", filteredGroupNames)}]");
            var group = innerSelector.FindGroupOf(tableName, item, filteredGroupNames);
            eventQueue.Enqueue("CharacterGen", $"Completed selection of {group}");

            return group;
        }
    }
}