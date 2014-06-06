using System;
using System.Collections.Generic;
using System.Xml;
using NPCGen.Common;
using NPCGen.Mappers.Interfaces;
using NPCGen.Tables.Interfaces;

namespace NPCGen.Mappers
{
    public class StatPriorityXmlParser : IStatPriorityXmlParser
    {
        private IStreamLoader streamLoader;

        public StatPriorityXmlParser(IStreamLoader streamLoader)
        {
            this.streamLoader = streamLoader;
        }

        public Dictionary<String, StatPriority> Parse(String filename)
        {
            var results = new Dictionary<String, StatPriority>();

            using (var stream = streamLoader.LoadStream(filename))
            {
                var xmlDocument = new XmlDocument();
                xmlDocument.Load(stream);

                var objects = xmlDocument.DocumentElement.ChildNodes;
                foreach (XmlNode node in objects)
                {
                    var className = node.SelectSingleNode("className").InnerText;

                    var statPriorityObject = new StatPriority();
                    statPriorityObject.FirstPriority = node.SelectSingleNode("first").InnerText;
                    statPriorityObject.SecondPriority = node.SelectSingleNode("second").InnerText;

                    results.Add(className, statPriorityObject);
                }
            }

            return results;
        }
    }
}