using System;
using System.Collections.Generic;
using NPCGen.Core.Data.Races;
using NPCGen.Core.Data.Stats;
using NPCGen.Core.Generation.Providers;
using NPCGen.Core.Generation.Randomizers.Stats.Interfaces;

namespace NPCGen.Core.Generation.Factories
{
    public static class StatsFactory
    {
        public static Dictionary<String, Stat> CreateUsing(IStatsRandomizer statsRandomizer, Race race)
        {
            var stats = statsRandomizer.Randomize();
            var statAdjustments = GetStatAdjustments(race);

            foreach (var stat in StatConstants.GetStats())
                stats[stat].Value += statAdjustments[stat];

            return stats;
        }

        private static Dictionary<String, Int32> GetStatAdjustments(Race race)
        {
            var statAdjustmentsProvider = ProviderFactory.CreateStatAdjustmentsProvider();
            return statAdjustmentsProvider.GetAdjustments(race);
        }
    }
}