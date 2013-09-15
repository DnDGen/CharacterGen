using NPCGen.Core.Data.Stats;
using NPCGen.Core.Generation.Randomizers.Stats;
using System;
using System.Collections.Generic;

namespace NPCGen.Core.Generation.Factories.Interfaces
{
    public interface IStatsFactory
    {
        IStatsRandomizer StatsRandomizer { get; set; }

        Dictionary<String, Stat> Generate();
    }
}