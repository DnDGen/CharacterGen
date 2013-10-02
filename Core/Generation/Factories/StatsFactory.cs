using System;
using System.Collections.Generic;
using NPCGen.Core.Data.Stats;
using NPCGen.Core.Generation.Randomizers.Stats.Interfaces;

namespace NPCGen.Core.Generation.Factories
{
    public static class StatsFactory
    {
        public static Dictionary<String, Stat> CreateUsing(IStatsRandomizer statsRandomizer)
        {
            return statsRandomizer.Randomize();
        }
    }
}