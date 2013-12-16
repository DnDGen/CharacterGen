using System;
using System.Collections.Generic;
using System.Linq;
using D20Dice;
using NPCGen.Core.Data.CharacterClasses;
using NPCGen.Core.Data.Races;
using NPCGen.Core.Data.Stats;
using NPCGen.Core.Generation.Factories.Interfaces;
using NPCGen.Core.Generation.Providers;
using NPCGen.Core.Generation.Randomizers.Stats.Interfaces;
using NPCGen.Core.Generation.Xml.Parsers.Objects;

namespace NPCGen.Core.Generation.Factories
{
    public class StatsFactory : IStatsFactory
    {
        private IDice dice;

        public StatsFactory(IDice dice)
        {
            this.dice = dice;
        }

        public Dictionary<String, Stat> CreateWith(IStatsRandomizer statsRandomizer, CharacterClass characterClass, Race race)
        {
            var stats = statsRandomizer.Randomize();

            var statPriorities = GetStatPriorities(characterClass.ClassName);
            var prioritizedStats = PrioritizeStats(stats, statPriorities);

            var statAdjustments = GetStatAdjustments(race);

            foreach (var stat in StatConstants.GetStats())
                prioritizedStats[stat].Value += statAdjustments[stat];

            var increasedStats = IncreaseStats(prioritizedStats, statPriorities, characterClass.Level);

            return prioritizedStats;
        }

        private Dictionary<String, Stat> IncreaseStats(Dictionary<String, Stat> stats, StatPriorityObject priorities, Int32 level)
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

        private Dictionary<String, Stat> PrioritizeStats(Dictionary<String, Stat> stats, StatPriorityObject priorities)
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

        private StatPriorityObject GetStatPriorities(String className)
        {
            var statPriorityProvider = ProviderFactory.CreateStatPriorityProvider();
            return statPriorityProvider.GetStatPriorities(className);
        }

        private Dictionary<String, Int32> GetStatAdjustments(Race race)
        {
            var statAdjustmentsProvider = ProviderFactory.CreateStatAdjustmentsProvider();
            return statAdjustmentsProvider.GetAdjustments(race);
        }
    }
}