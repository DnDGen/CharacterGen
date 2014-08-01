using System;
using System.Collections.Generic;

namespace NPCGen.Selectors.Interfaces
{
    public interface IPercentileSelector
    {
        String SelectPercentileFrom(String tableName);
        IEnumerable<String> SelectAllResults(String tableName);
    }
}