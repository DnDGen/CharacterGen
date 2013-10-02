using System;
using System.Collections.Generic;
using NPCGen.Core.Data.Stats;
using NPCGen.Core.Generation.Randomizers.Stats.Interfaces;

namespace NPCGen.Core.Generation.Factories.Interfaces
{
    public interface IStatsFactory
    {
        IStatsRandomizer StatsRandomizer { get; set; }

        Dictionary<String, Stat> Generate();
    }
}