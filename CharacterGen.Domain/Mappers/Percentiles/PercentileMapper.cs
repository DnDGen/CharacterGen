using System.Collections.Generic;

namespace CharacterGen.Domain.Mappers.Percentiles
{
    internal interface PercentileMapper
    {
        Dictionary<int, string> Map(string tableName);
    }
}