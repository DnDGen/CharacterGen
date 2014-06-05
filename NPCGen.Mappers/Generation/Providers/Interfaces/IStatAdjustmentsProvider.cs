using System;
using System.Collections.Generic;
using NPCGen.Core.Data.Races;

namespace NPCGen.Core.Generation.Providers.Interfaces
{
    public interface IStatAdjustmentsProvider
    {
        Dictionary<String, Int32> GetAdjustments(Race race);
    }
}