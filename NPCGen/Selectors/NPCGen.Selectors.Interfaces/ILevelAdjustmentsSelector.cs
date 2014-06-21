using System;
using System.Collections.Generic;

namespace NPCGen.Selectors.Interfaces
{
    public interface ILevelAdjustmentsSelector
    {
        Dictionary<String, Int32> GetAdjustments();
    }
}