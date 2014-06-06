using System;
using System.Collections.Generic;
using System.Xml;
using NPCGen.Core.Generation.Xml.Parsers.Interfaces;
using NPCGen.Core.Generation.Xml.Parsers.Objects;

namespace NPCGen.Core.Generation.Xml.Parsers
{
    public class StatPriorityXmlParser : IStatPriorityXmlParser
    {
        private IStreamLoader streamLoader;

        public StatPriorityXmlParser(IStreamLoader streamLoader)
        {
            this.streamLoader = streamLoader;
        }

        public Dictionary<String, StatPriorityObject> Parse(String filename)
        {
            var results = new Dictionary<String, StatPriorityObject>();

            using (var stream = streamLoader.LoadStream(filename))
            {
                var xmlDocument = new XmlDocument();
                xmlDocument.Load(stream);

                var objects = xmlDocument.DocumentElement.ChildNodes;
                foreach (XmlNode node in objects)
                {
                    var className = node.SelectSingleNode("className").InnerText;

                    var statPriorityObject = new StatPriorityObject();
                    statPriorityObject.FirstPriority = node.SelectSingleNode("first").InnerText;
                    statPriorityObject.SecondPriority = node.SelectSingleNode("second").InnerText;

                    results.Add(className, statPriorityObject);
                }
            }

            return results;
        }
    }
}