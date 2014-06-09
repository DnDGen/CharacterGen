using System;
using NPCGen.Common;
using NPCGen.Mappers.Interfaces;
using NPCGen.Selectors.Interfaces;

namespace NPCGen.Selectors
{
    public class StatPriorityProvider : IStatPriorityProvider
    {
        private IStatPriorityXmlParser statPriorityXmlParser;

        public StatPriorityProvider(IStatPriorityXmlParser statPriorityXmlParser)
        {
            this.statPriorityXmlParser = statPriorityXmlParser;
        }

        public StatPriority GetStatPriorities(String className)
        {
            var priorities = statPriorityXmlParser.Parse("StatPriorities.xml");
            return priorities[className];
        }
    }
}