using System;
using System.Collections.Generic;
using System.Xml;
using NPCGen.Common;
using NPCGen.Mappers.Interfaces;
using NPCGen.Tables.Interfaces;

namespace NPCGen.Mappers.Collections
{
    public class StatPriorityXmlMapper : IStatPriorityMapper
    {
        private IStreamLoader streamLoader;

        public StatPriorityXmlMapper(IStreamLoader streamLoader)
        {
            this.streamLoader = streamLoader;
        }

        public Dictionary<String, StatPriority> Map(String tableName)
        {
            var filename = tableName + ".xml";
            var results = new Dictionary<String, StatPriority>();
            var xmlDocument = new XmlDocument();

            using (var stream = streamLoader.LoadFor(filename))
                xmlDocument.Load(stream);

            foreach (XmlNode node in xmlDocument.DocumentElement.ChildNodes)
            {
                var className = node.SelectSingleNode("className").InnerText;

                var statPriorityObject = new StatPriority();
                statPriorityObject.FirstPriority = node.SelectSingleNode("first").InnerText;
                statPriorityObject.SecondPriority = node.SelectSingleNode("second").InnerText;

                results.Add(className, statPriorityObject);
            }

            return results;
        }
    }
}