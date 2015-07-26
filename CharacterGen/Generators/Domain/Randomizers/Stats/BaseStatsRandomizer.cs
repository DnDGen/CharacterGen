using System;
using System.Collections.Generic;
using CharacterGen.Common.Abilities.Stats;
using CharacterGen.Generators.Randomizers.Stats;

namespace CharacterGen.Generators.Domain.Randomizers.Stats
{
    public abstract class BaseStatsRandomizer : IStatsRandomizer
    {
        public Dictionary<String, Stat> Randomize()
        {
            var stats = InitializeStats();

            do
            {
                foreach (var stat in stats.Values)
                    stat.Value = RollStat();
            } while (!StatsAreAllowed(stats.Values));

            return stats;
        }

        private Dictionary<String, Stat> InitializeStats()
        {
            var stats = new Dictionary<String, Stat>();

            stats[StatConstants.Charisma] = new Stat();
            stats[StatConstants.Constitution] = new Stat();
            stats[StatConstants.Dexterity] = new Stat();
            stats[StatConstants.Intelligence] = new Stat();
            stats[StatConstants.Strength] = new Stat();
            stats[StatConstants.Wisdom] = new Stat();

            return stats;
        }

        protected abstract Int32 RollStat();
        protected abstract Boolean StatsAreAllowed(IEnumerable<Stat> stats);
    }
}