using System;

namespace NPCGen.Core.Generation.Randomizers.Providers.Interfaces
{
    public interface IPercentileResultProvider
    {
        String GetPercentileResult(String tableName);
    }
}