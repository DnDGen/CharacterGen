using System;
using System.Collections.Generic;

namespace CharacterGen.Mappers
{
    public interface PercentileMapper
    {
        Dictionary<int, string> Map(string tableName);
    }
}