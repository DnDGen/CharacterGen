using System;
using System.Collections.Generic;

namespace NPCGen.Mappers.Interfaces
{
    public interface ILanguagesMapper
    {
        Dictionary<String, IEnumerable<String>> Map(String tableName);
    }
}