using System;
using System.Collections.Generic;

namespace NPCGen.Selectors.Interfaces
{
    public interface IPercentileSelector
    {
        String GetPercentileFrom(String tableName);
        IEnumerable<String> GetAllResults(String tableName);
    }
}