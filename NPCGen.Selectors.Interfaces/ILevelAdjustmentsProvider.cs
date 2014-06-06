using System;
using System.Collections.Generic;

namespace NPCGen.Selectors.Interfaces
{
    public interface ILevelAdjustmentsProvider
    {
        Dictionary<String, Int32> GetLevelAdjustments();
    }
}