using System;
using System.Collections.Generic;

namespace CharacterGen.Mappers
{
    public interface CollectionsMapper
    {
        Dictionary<string, IEnumerable<string>> Map(string tableName);
    }
}