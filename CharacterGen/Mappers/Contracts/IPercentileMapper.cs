using System;
using System.Collections.Generic;

namespace CharacterGen.Mappers
{
    public interface IPercentileMapper
    {
        Dictionary<Int32, String> Map(String tableName);
    }
}