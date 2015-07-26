using System;
using System.Collections.Generic;

namespace CharacterGen.Mappers
{
    public interface ICollectionsMapper
    {
        Dictionary<String, IEnumerable<String>> Map(String tableName);
    }
}