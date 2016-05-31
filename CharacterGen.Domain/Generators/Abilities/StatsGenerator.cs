using CharacterGen.Abilities.Stats;
using CharacterGen.CharacterClasses;
using CharacterGen.Domain.Selectors.Collections;
using CharacterGen.Domain.Selectors.Percentiles;
using CharacterGen.Domain.Tables;
using CharacterGen.Races;
using CharacterGen.Randomizers.Stats;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CharacterGen.Domain.Generators.Abilities
{
    internal class StatsGenerator : IStatsGenerator
    {
        private IBooleanPercentileSelector booleanPercentileSelector;
        private IStatAdjustmentsSelector statAdjustmentsSelector;
        private IAdjustmentsSelector adjustmentsSelector;
        private ICollectionsSelector collectionsSelector;

        public StatsGenerator(IBooleanPercentileSelector booleanPercentileSelector, IStatAdjustmentsSelector statAdjustmentsSelector, IAdjustmentsSelector adjustmentsSelector, ICollectionsSelector collectionsSelector)
        {
            this.booleanPercentileSelector = booleanPercentileSelector;
            this.statAdjustmentsSelector = statAdjustmentsSelector;
            this.adjustmentsSelector = adjustmentsSelector;
            this.collectionsSelector = collectionsSelector;
        }

        public Dictionary<string, Stat> GenerateWith(IStatsRandomizer statsRandomizer, CharacterClass characterClass, Race race)
        {
            var stats = statsRandomizer.Randomize();

            if (CanAdjustStats(statsRandomizer))
            {
                stats = PrioritizeStats(stats, characterClass);
                stats = AdjustStats(race, stats);
                stats = SetMinimumStats(stats);
                stats = IncreaseStats(stats, characterClass, race);
            }
            else
            {
                stats = SetMinimumStats(stats);
            }

            var undead = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.MetaraceGroups, GroupConstants.Undead);

            if (undead.Contains(race.Metarace))
                stats[StatConstants.Constitution].Value = 0;

            return stats;
        }

        private bool CanAdjustStats(IStatsRandomizer statsRandomizer)
        {
            if ((statsRandomizer is ISetStatsRandomizer) == false)
                return true;

            var setStatsRandomizer = statsRandomizer as ISetStatsRandomizer;
            return setStatsRandomizer.AllowAdjustments;
        }

        private Dictionary<string, Stat> PrioritizeStats(Dictionary<string, Stat> stats, CharacterClass characterClass)
        {
            var statPriorities = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.StatPriorities, characterClass.ClassName);
            if (statPriorities.Any() == false)
                return stats;

            var firstPriority = statPriorities.First();
            var max = stats.Values.Max(s => s.Value);
            var maxStat = stats.Keys.First(k => stats[k].Value == max);
            stats = SwapStat(stats, firstPriority, maxStat);

            var secondPriorities = statPriorities.Skip(1);
            var nonPriorityStatNames = stats.Keys.Except(statPriorities);

            while (secondPriorities.Any())
            {
                var priority = secondPriorities.First();
                var nonPriorityStats = stats.Where(kvp => nonPriorityStatNames.Contains(kvp.Key));

                max = nonPriorityStats.Max(kvp => kvp.Value.Value);

                if (max > stats[priority].Value)
                {
                    maxStat = nonPriorityStatNames.First(s => stats[s].Value == max);
                    stats = SwapStat(stats, priority, maxStat);
                }

                secondPriorities = secondPriorities.Skip(1);
            }

            return stats;
        }

        private Dictionary<string, Stat> SwapStat(Dictionary<string, Stat> stats, string priorityStat, string otherStat)
        {
            var temp = stats[otherStat];
            stats[otherStat] = stats[priorityStat];
            stats[priorityStat] = temp;

            return stats;
        }

        private Dictionary<string, Stat> AdjustStats(Race race, Dictionary<string, Stat> stats)
        {
            var statAdjustments = statAdjustmentsSelector.SelectFor(race);

            foreach (var stat in stats.Keys)
                stats[stat].Value += statAdjustments[stat];

            return stats;
        }

        private Dictionary<string, Stat> SetMinimumStats(Dictionary<string, Stat> stats)
        {
            foreach (var stat in stats.Values)
                stat.Value = Math.Max(stat.Value, 1);

            return stats;
        }

        private Dictionary<string, Stat> IncreaseStats(Dictionary<string, Stat> stats, CharacterClass characterClass, Race race)
        {
            var count = characterClass.Level / 4;

            while (count-- > 0)
            {
                var statToIncrease = GetStatToIncrease(stats, race, characterClass);
                stats[statToIncrease].Value++;
            }

            return stats;
        }

        private string GetStatToIncrease(Dictionary<string, Stat> stats, Race race, CharacterClass characterClass)
        {
            var statPriorities = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.StatPriorities, characterClass.ClassName);
            var undead = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.MetaraceGroups, GroupConstants.Undead);

            if (undead.Contains(race.Metarace))
                statPriorities = statPriorities.Except(new[] { StatConstants.Constitution });

            if (statPriorities.Any() == false)
            {
                var stat = collectionsSelector.SelectRandomFrom(stats.Keys);

                while (undead.Contains(race.Metarace) && stat == StatConstants.Constitution)
                    stat = collectionsSelector.SelectRandomFrom(stats.Keys);

                return stat;
            }

            var secondPriorityStats = statPriorities.Skip(1);
            if (secondPriorityStats.Any() == false)
                return statPriorities.First();

            var increaseFirst = booleanPercentileSelector.SelectFrom(TableNameConstants.Set.TrueOrFalse.IncreaseFirstPriorityStat);

            if (increaseFirst)
                return statPriorities.First();

            return collectionsSelector.SelectRandomFrom(secondPriorityStats);
        }
    }
}