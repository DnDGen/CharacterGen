using System;
using NPCGen.Core.Generation.Providers.Interfaces;
using NPCGen.Core.Generation.Xml.Parsers.Interfaces;
using NPCGen.Core.Generation.Xml.Parsers.Objects;

namespace NPCGen.Core.Generation.Providers
{
    public class StatPriorityProvider : IStatPriorityProvider
    {
        private IStatPriorityXmlParser statPriorityXmlParser;

        public StatPriorityProvider(IStatPriorityXmlParser statPriorityXmlParser)
        {
            this.statPriorityXmlParser = statPriorityXmlParser;
        }

        public StatPriorityObject GetStatPriorities(String className)
        {
            var priorities = statPriorityXmlParser.Parse("StatPriorities.xml");
            return priorities[className];
        }
    }
}