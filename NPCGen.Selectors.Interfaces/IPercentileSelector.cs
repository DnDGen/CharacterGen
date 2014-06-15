using System;
using System.Collections.Generic;

namespace NPCGen.Selectors.Interfaces
{
    public interface IPercentileSelector
    {
        String GetPercentileResult(String tableName);
        IEnumerable<String> GetAllResults(String tableName);
    }
}