using System;
using System.Collections.Generic;
using NPCGen.Core.Data.Stats;
using NPCGen.Core.Generation.Factories.Interfaces;
using NPCGen.Core.Generation.Randomizers.Stats.Interfaces;

namespace NPCGen.Core.Generation.Factories
{
    public class StatsFactory : IStatsFactory
    {
        public IStatsRandomizer StatsRandomizer { get; set; }

        public StatsFactory(IStatsRandomizer statsRandomizer)
        {
            StatsRandomizer = statsRandomizer;
        }

        public Dictionary<String, Stat> Generate()
        {
            return StatsRandomizer.Randomize();
        }
    }
}