using System.Collections.Generic;

namespace CharacterGen.Domain.Mappers.Collections
{
    internal interface CollectionsMapper
    {
        Dictionary<string, IEnumerable<string>> Map(string tableName);
    }
}