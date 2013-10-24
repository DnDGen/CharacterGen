using System;
using System.Collections.Generic;
using NPCGen.Core.Data.Races;
using NPCGen.Core.Data.Stats;
using NPCGen.Core.Generation.Providers.Interfaces;
using NPCGen.Core.Generation.Randomizers.Stats.Interfaces;

namespace NPCGen.Core.Generation.Factories
{
    public static class StatsFactory
    {
        public static Dictionary<String, Stat> CreateUsing(IStatsRandomizer statsRandomizer, IStatAdjustmentsProvider statAdjustmentsProvider,
            Race race)
        {
            var stats = statsRandomizer.Randomize();

            var statAdjustments = statAdjustmentsProvider.GetAdjustments(race);

            return stats;
        }
    }
}