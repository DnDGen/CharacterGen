using System;
using System.Collections.Generic;

namespace NPCGen.Core.Generation.Providers.Interfaces
{
    public interface ILevelAdjustmentsProvider
    {
        Dictionary<String, Int32> GetLevelAdjustments();
    }
}