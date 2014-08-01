using System;
using NPCGen.Common.Abilities.Stats;
using NPCGen.Mappers.Interfaces;
using NPCGen.Selectors.Interfaces;

namespace NPCGen.Selectors
{
    public class StatPrioritySelector : IStatPrioritySelector
    {
        private IStatPriorityMapper statPriorityMapper;

        public StatPrioritySelector(IStatPriorityMapper statPriorityMapper)
        {
            this.statPriorityMapper = statPriorityMapper;
        }

        public StatPriority SelectStatPrioritiesFor(String className)
        {
            var priorities = statPriorityMapper.Map("StatPriorities");
            return priorities[className];
        }
    }
}