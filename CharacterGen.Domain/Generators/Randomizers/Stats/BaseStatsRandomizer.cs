using CharacterGen.Abilities.Stats;
using CharacterGen.Randomizers.Stats;
using System;
using System.Collections.Generic;

namespace CharacterGen.Domain.Generators.Randomizers.Stats
{
    internal abstract class BaseStatsRandomizer : IStatsRandomizer
    {
        protected abstract int defaultValue { get; }

        private Generator generator;

        public BaseStatsRandomizer(Generator generator)
        {
            this.generator = generator;
        }

        public Dictionary<string, Stat> Randomize()
        {
            var stats = generator.Generate(
                () => RollStats(RollStat),
                s => StatsAreAllowed(s.Values),
                () => RollStats(() => defaultValue),
                $"stats of value {defaultValue}");

            return stats;
        }

        private Dictionary<string, Stat> RollStats(Func<int> roll)
        {
            var stats = InitializeStats();

            foreach (var stat in stats.Values)
                stat.Value = roll();

            return stats;
        }

        private Dictionary<string, Stat> InitializeStats()
        {
            var stats = new Dictionary<string, Stat>();

            stats[StatConstants.Charisma] = new Stat(StatConstants.Charisma);
            stats[StatConstants.Constitution] = new Stat(StatConstants.Constitution);
            stats[StatConstants.Dexterity] = new Stat(StatConstants.Dexterity);
            stats[StatConstants.Intelligence] = new Stat(StatConstants.Intelligence);
            stats[StatConstants.Strength] = new Stat(StatConstants.Strength);
            stats[StatConstants.Wisdom] = new Stat(StatConstants.Wisdom);

            return stats;
        }

        protected abstract int RollStat();
        protected abstract bool StatsAreAllowed(IEnumerable<Stat> stats);
    }
}