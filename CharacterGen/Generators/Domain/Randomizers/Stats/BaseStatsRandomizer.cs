using CharacterGen.Common.Abilities.Stats;
using CharacterGen.Generators.Randomizers.Stats;
using System;
using System.Collections.Generic;

namespace CharacterGen.Generators.Domain.Randomizers.Stats
{
    public abstract class BaseStatsRandomizer : IterativeBuilder, IStatsRandomizer
    {
        protected abstract Int32 defaultValue { get; }

        public Dictionary<String, Stat> Randomize()
        {
            var stats = Build(() => RollStats(), s => StatsAreAllowed(s.Values));

            if (stats != null)
                return stats;

            stats = InitializeStats();
            foreach (var stat in stats.Values)
                stat.Value = defaultValue;

            return stats;
        }

        private Dictionary<String, Stat> RollStats()
        {
            var stats = InitializeStats();

            foreach (var stat in stats.Values)
                stat.Value = RollStat();

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