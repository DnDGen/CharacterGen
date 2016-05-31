using System.Collections.Generic;

namespace CharacterGen.Domain.Selectors.Collections
{
    internal interface ICollectionsSelector
    {
        IEnumerable<string> SelectFrom(string tableName, string tableEntry);
        Dictionary<string, IEnumerable<string>> SelectAllFrom(string tableName);
        T SelectRandomFrom<T>(IEnumerable<T> collection);
        string SelectRandomFrom(string tableName, string tableEntry);
    }
}