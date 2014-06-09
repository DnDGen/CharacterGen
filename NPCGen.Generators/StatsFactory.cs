using System;
using System.Collections.Generic;
using System.Linq;
using D20Dice;
using NPCGen.Common;
using NPCGen.Common.CharacterClasses;
using NPCGen.Common.Races;
using NPCGen.Common.Stats;
using NPCGen.Generators.Interfaces;
using NPCGen.Generators.Interfaces.Randomizers.Stats;
using NPCGen.Selectors.Interfaces;

namespace NPCGen.Generators
{
    public class StatsFactory : IStatsFactory
    {
        private IDice dice;
        private IStatPriorityProvider statPriorityProvider;
        private IStatAdjustmentsProvider statAdjustmentsProvider;

        public StatsFactory(IDice dice, IStatPriorityProvider statPriorityProvider, IStatAdjustmentsProvider statAdjustmentsProvider)
        {
            this.dice = dice;
            this.statPriorityProvider = statPriorityProvider;
            this.statAdjustmentsProvider = statAdjustmentsProvider;
        }

        public Dictionary<String, Stat> CreateWith(IStatsRandomizer statsRandomizer, CharacterClass characterClass, Race race)
        {
            var stats = statsRandomizer.Randomize();

            var statPriorities = statPriorityProvider.GetStatPriorities(characterClass.ClassName);
            var prioritizedStats = PrioritizeStats(stats, statPriorities);

            var statAdjustments = statAdjustmentsProvider.GetAdjustments(race);

            foreach (var stat in prioritizedStats.Keys)
            {
                var statValue = prioritizedStats[stat].Value;

                statValue += statAdjustments[stat];
                statValue = Math.Max(statValue, 1);

                prioritizedStats[stat].Value = statValue;
            }

            var increasedStats = IncreaseStats(prioritizedStats, statPriorities, characterClass.Level);

            return increasedStats;
        }

        private Dictionary<String, Stat> PrioritizeStats(Dictionary<String, Stat> stats, StatPriority priorities)
        {
            var prioritizedStats = new Dictionary<String, Stat>(stats);

            var maxStat = prioritizedStats.Keys.First(k => prioritizedStats[k].Value == prioritizedStats.Values.Max(s => s.Value));

            var temp = prioritizedStats[maxStat];
            prioritizedStats[maxStat] = prioritizedStats[priorities.FirstPriority];
            prioritizedStats[priorities.FirstPriority] = temp;

            var secondMaxStat = prioritizedStats.Keys.First(k => prioritizedStats[k].Value == prioritizedStats.Values
                .Where(s => s != prioritizedStats[priorities.FirstPriority]).Max(s => s.Value));

            temp = prioritizedStats[secondMaxStat];
            prioritizedStats[secondMaxStat] = prioritizedStats[priorities.SecondPriority];
            prioritizedStats[priorities.SecondPriority] = temp;

            return prioritizedStats;
        }

        private Dictionary<String, Stat> IncreaseStats(Dictionary<String, Stat> stats, StatPriority priorities, Int32 level)
        {
            var increasedStats = new Dictionary<String, Stat>(stats);

            var count = level / 4;
            while (count-- > 0)
            {
                var roll = dice.d2();

                if (roll == 1)
                    increasedStats[priorities.FirstPriority].Value++;
                else
                    increasedStats[priorities.SecondPriority].Value++;
            }

            return increasedStats;
        }
    }
}