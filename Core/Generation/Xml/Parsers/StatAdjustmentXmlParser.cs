using System;
using System.Collections.Generic;
using System.Xml;
using NPCGen.Core.Generation.Xml.Parsers.Interfaces;

namespace NPCGen.Core.Generation.Xml.Parsers
{
    public class StatAdjustmentXmlParser : IStatAdjustmentXmlParser
    {
        private IStreamLoader streamLoader;

        public StatAdjustmentXmlParser(IStreamLoader streamLoader)
        {
            this.streamLoader = streamLoader;
        }

        public Dictionary<String, Int32> Parse(String filename)
        {
            var results = new Dictionary<String, Int32>();

            using (var stream = streamLoader.LoadStream(filename))
            {
                var xmlDocument = new XmlDocument();
                xmlDocument.Load(stream);

                var objects = xmlDocument.DocumentElement.ChildNodes;
                foreach (XmlNode node in objects)
                {
                    var race = node.SelectSingleNode("race").InnerText;
                    var adjustment = Convert.ToInt32(node.SelectSingleNode("adjustment").InnerText);

                    results.Add(race, adjustment);
                }
            }

            return results;
        }
    }
}