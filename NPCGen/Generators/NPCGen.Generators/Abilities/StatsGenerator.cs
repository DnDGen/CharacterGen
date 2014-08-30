using System;
using System.Collections.Generic;
using System.Linq;
using D20Dice;
using NPCGen.Common.Abilities.Stats;
using NPCGen.Common.CharacterClasses;
using NPCGen.Common.Races;
using NPCGen.Generators.Interfaces.Abilities;
using NPCGen.Generators.Interfaces.Randomizers.Stats;
using NPCGen.Selectors.Interfaces;
using NPCGen.Selectors.Interfaces.Objects;

namespace NPCGen.Generators.Abilities
{
    public class StatsGenerator : IStatsGenerator
    {
        private IDice dice;
        private IStatPrioritySelector statPrioritySelector;
        private IStatAdjustmentsSelector statAdjustmentsSelector;

        public StatsGenerator(IDice dice, IStatPrioritySelector statPrioritySelector, IStatAdjustmentsSelector statAdjustmentsSelector)
        {
            this.dice = dice;
            this.statPrioritySelector = statPrioritySelector;
            this.statAdjustmentsSelector = statAdjustmentsSelector;
        }

        public Dictionary<String, Stat> GenerateWith(IStatsRandomizer statsRandomizer, CharacterClass characterClass, Race race)
        {
            var stats = statsRandomizer.Randomize();

            var statPriorities = statPrioritySelector.SelectFor(characterClass.ClassName);
            stats = PrioritizeStats(stats, statPriorities);
            stats = AdjustStats(race, stats);
            stats = IncreaseStats(stats, characterClass.Level, statPriorities);

            return stats;
        }

        private Dictionary<String, Stat> PrioritizeStats(Dictionary<String, Stat> stats, StatPrioritySelection priorities)
        {
            var max = stats.Values.Max(s => s.Value);
            var maxStat = stats.Keys.First(k => stats[k].Value == max);
            stats = SwapStat(stats, priorities.First, maxStat);

            max = stats.Values.Except(new[] { stats[priorities.First] }).Max(s => s.Value);
            maxStat = stats.Keys.First(k => stats[k].Value == max);
            stats = SwapStat(stats, priorities.Second, maxStat);

            return stats;
        }

        private Dictionary<String, Stat> SwapStat(Dictionary<String, Stat> stats, String priorityStat, String otherStat)
        {
            var temp = stats[otherStat];
            stats[otherStat] = stats[priorityStat];
            stats[priorityStat] = temp;

            return stats;
        }

        private Dictionary<String, Stat> AdjustStats(Race race, Dictionary<String, Stat> stats)
        {
            var statAdjustments = statAdjustmentsSelector.SelectFor(race);

            foreach (var stat in stats.Keys)
            {
                stats[stat].Value += statAdjustments[stat];
                stats[stat].Value = Math.Max(stats[stat].Value, 1);
            }

            return stats;
        }

        private Dictionary<String, Stat> IncreaseStats(Dictionary<String, Stat> stats, Int32 level, StatPrioritySelection priorities)
        {
            var count = level / 4;

            while (count-- > 0)
                IncreaseStat(stats, priorities);

            return stats;
        }

        private void IncreaseStat(Dictionary<String, Stat> stats, StatPrioritySelection priorities)
        {
            var roll = dice.Roll().d2();

            if (roll == 1)
                stats[priorities.First].Value++;
            else
                stats[priorities.Second].Value++;
        }
    }
}