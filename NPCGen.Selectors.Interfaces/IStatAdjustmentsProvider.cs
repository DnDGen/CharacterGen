using System;
using System.Collections.Generic;
using NPCGen.Common.Races;

namespace NPCGen.Selectors.Interfaces
{
    public interface IStatAdjustmentsProvider
    {
        Dictionary<String, Int32> GetAdjustments(Race race);
    }
}