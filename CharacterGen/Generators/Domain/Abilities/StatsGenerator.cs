using CharacterGen.Common.Abilities.Stats;
using CharacterGen.Common.CharacterClasses;
using CharacterGen.Common.Races;
using CharacterGen.Generators.Abilities;
using CharacterGen.Generators.Randomizers.Stats;
using CharacterGen.Selectors;
using CharacterGen.Selectors.Objects;
using CharacterGen.Tables;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CharacterGen.Generators.Domain.Abilities
{
    public class StatsGenerator : IStatsGenerator
    {
        private IBooleanPercentileSelector booleanPercentileSelector;
        private IStatPrioritySelector statPrioritySelector;
        private IStatAdjustmentsSelector statAdjustmentsSelector;
        private IAdjustmentsSelector adjustmentsSelector;

        public StatsGenerator(IBooleanPercentileSelector booleanPercentileSelector, IStatPrioritySelector statPrioritySelector,
            IStatAdjustmentsSelector statAdjustmentsSelector, IAdjustmentsSelector adjustmentsSelector)
        {
            this.booleanPercentileSelector = booleanPercentileSelector;
            this.statPrioritySelector = statPrioritySelector;
            this.statAdjustmentsSelector = statAdjustmentsSelector;
            this.adjustmentsSelector = adjustmentsSelector;
        }

        public Dictionary<String, Stat> GenerateWith(IStatsRandomizer statsRandomizer, CharacterClass characterClass, Race race)
        {
            var stats = statsRandomizer.Randomize();

            var statPriorities = statPrioritySelector.SelectFor(characterClass.ClassName);
            stats = PrioritizeStats(stats, statPriorities);
            stats = AdjustStats(race, stats);
            stats = IncreaseStats(stats, characterClass.Level, statPriorities);
            stats = ApplyAgeToStats(stats, race, characterClass);

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
                stats[stat].Value += statAdjustments[stat];

            foreach (var stat in stats.Values)
                stat.Value = Math.Max(stat.Value, 1);

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
            var increaseFirst = booleanPercentileSelector.SelectFrom(TableNameConstants.Set.TrueOrFalse.IncreaseFirstPriorityStat);

            if (increaseFirst)
                stats[priorities.First].Value++;
            else
                stats[priorities.Second].Value++;
        }

        private Dictionary<String, Stat> ApplyAgeToStats(Dictionary<String, Stat> stats, Race race, CharacterClass characterClass)
        {
            var tableName = String.Format(TableNameConstants.Formattable.Adjustments.CLASSRACEAges, characterClass.ClassName, race.BaseRace);
            var ages = adjustmentsSelector.SelectFrom(tableName);

            if (race.Age >= ages[AdjustmentConstants.MiddleAge])
            {
                stats[StatConstants.Strength].Value--;
                stats[StatConstants.Constitution].Value--;
                stats[StatConstants.Dexterity].Value--;
                stats[StatConstants.Intelligence].Value++;
                stats[StatConstants.Wisdom].Value++;
                stats[StatConstants.Charisma].Value++;
            }

            if (race.Age >= ages[AdjustmentConstants.Old])
            {
                stats[StatConstants.Strength].Value -= 2;
                stats[StatConstants.Constitution].Value -= 2;
                stats[StatConstants.Dexterity].Value -= 2;
                stats[StatConstants.Intelligence].Value++;
                stats[StatConstants.Wisdom].Value++;
                stats[StatConstants.Charisma].Value++;
            }

            if (race.Age >= ages[AdjustmentConstants.Venerable])
            {
                stats[StatConstants.Strength].Value -= 3;
                stats[StatConstants.Constitution].Value -= 3;
                stats[StatConstants.Dexterity].Value -= 3;
                stats[StatConstants.Intelligence].Value++;
                stats[StatConstants.Wisdom].Value++;
                stats[StatConstants.Charisma].Value++;
            }

            foreach (var stat in stats.Values)
                stat.Value = Math.Max(stat.Value, 1);

            return stats;
        }
    }
}