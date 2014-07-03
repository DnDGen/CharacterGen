using System;
using System.Collections.Generic;
using System.Linq;
using NPCGen.Common.Abilities.Stats;
using NPCGen.Mappers.Interfaces;

namespace NPCGen.Mappers.Collections
{
    public class StatPriorityMapper : IStatPriorityMapper
    {
        private ICollectionsMapper innerMapper;

        public StatPriorityMapper(ICollectionsMapper innerMapper)
        {
            this.innerMapper = innerMapper;
        }

        public Dictionary<String, StatPriority> Map(String tableName)
        {
            var collectionTable = innerMapper.Map(tableName);

            if (collectionTable.Values.Any(v => v.Count() < 2))
            {
                var message = String.Format("Table {0} has too few items to be stat priorities.", tableName);
                throw new Exception(message);
            }

            var statPriorityTable = new Dictionary<String, StatPriority>();

            foreach (var kvp in collectionTable)
            {
                var statPriority = new StatPriority();
                statPriority.FirstPriority = kvp.Value.First();
                statPriority.SecondPriority = kvp.Value.Last();

                statPriorityTable.Add(kvp.Key, statPriority);
            }

            return statPriorityTable;
        }
    }
}