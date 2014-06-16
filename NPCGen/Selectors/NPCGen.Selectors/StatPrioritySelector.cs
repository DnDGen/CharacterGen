using System;
using NPCGen.Common;
using NPCGen.Mappers.Interfaces;
using NPCGen.Selectors.Interfaces;

namespace NPCGen.Selectors
{
    public class StatPrioritySelector : IStatPrioritySelector
    {
        private IStatPriorityMapper statPriorityXmlMapper;

        public StatPrioritySelector(IStatPriorityMapper statPriorityXmlMapper)
        {
            this.statPriorityXmlMapper = statPriorityXmlMapper;
        }

        public StatPriority GetStatPriorities(String className)
        {
            var priorities = statPriorityXmlMapper.Map("StatPriorities.xml");
            return priorities[className];
        }
    }
}