using System;
using System.Collections.Generic;

namespace NPCGen.Selectors.Interfaces
{
    public interface IPercentileResultProvider
    {
        String GetPercentileResult(String tableName);
        IEnumerable<String> GetAllResults(String tableName);
    }
}