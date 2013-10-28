using System;
using System.Collections.Generic;
using System.Linq;
using D20Dice.Dice;
using NPCGen.Core.Data.CharacterClasses;
using NPCGen.Core.Data.Races;
using NPCGen.Core.Data.Stats;
using NPCGen.Core.Generation.Providers;
using NPCGen.Core.Generation.Randomizers.Stats.Interfaces;
using NPCGen.Core.Generation.Xml.Parsers.Objects;

namespace NPCGen.Core.Generation.Factories
{
    public static class StatsFactory
    {
        public static Dictionary<String, Stat> CreateUsing(IStatsRandomizer statsRandomizer, CharacterClass characterClass, Race race, IDice dice)
        {
            var stats = statsRandomizer.Randomize();

            var statPriorities = GetStatPriorities(characterClass.ClassName);
            var prioritizedStats = PrioritizeStats(stats, statPriorities);

            var statAdjustments = GetStatAdjustments(race);

            foreach (var stat in StatConstants.GetStats())
                prioritizedStats[stat].Value += statAdjustments[stat];

            var increasedStats = IncreaseStats(prioritizedStats, statPriorities, characterClass.Level, dice);

            return prioritizedStats;
        }

        private static Dictionary<String, Stat> IncreaseStats(Dictionary<String, Stat> prioritizedStats, StatPriorityObject priorities, Int32 level,
            IDice dice)
        {
            var increasedStats = new Dictionary<String, Stat>(prioritizedStats);

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

        private static Dictionary<String, Stat> PrioritizeStats(Dictionary<String, Stat> stats, StatPriorityObject statPriorities)
        {
            var prioritizedStats = new Dictionary<String, Stat>(stats);

            var maxStat = prioritizedStats.Keys.First(k => prioritizedStats[k].Value == prioritizedStats.Values.Max(s => s.Value));

            var temp = prioritizedStats[maxStat];
            prioritizedStats[maxStat] = prioritizedStats[statPriorities.FirstPriority];
            prioritizedStats[statPriorities.FirstPriority] = temp;

            var secondMaxStat = prioritizedStats.Keys.First(k => prioritizedStats[k].Value == prioritizedStats.Values
                .Where(s => s != prioritizedStats[statPriorities.FirstPriority]).Max(s => s.Value));

            temp = prioritizedStats[secondMaxStat];
            prioritizedStats[secondMaxStat] = prioritizedStats[statPriorities.SecondPriority];
            prioritizedStats[statPriorities.SecondPriority] = temp;

            return prioritizedStats;
        }

        private static StatPriorityObject GetStatPriorities(String className)
        {
            var statPriorityProvider = ProviderFactory.CreateStatPriorityProvider();
            return statPriorityProvider.GetStatPriorities(className);
        }

        private static Dictionary<String, Int32> GetStatAdjustments(Race race)
        {
            var statAdjustmentsProvider = ProviderFactory.CreateStatAdjustmentsProvider();
            return statAdjustmentsProvider.GetAdjustments(race);
        }
    }
}