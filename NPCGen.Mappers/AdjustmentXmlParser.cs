using System;
using System.Collections.Generic;
using System.Xml;
using NPCGen.Mappers.Interfaces;
using NPCGen.Tables.Interfaces;

namespace NPCGen.Mappers
{
    public class AdjustmentXmlParser : IAdjustmentXmlParser
    {
        private IStreamLoader streamLoader;

        public AdjustmentXmlParser(IStreamLoader streamLoader)
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
                    var key = node.SelectSingleNode("key").InnerText;
                    var adjustment = Convert.ToInt32(node.SelectSingleNode("adjustment").InnerText);

                    results.Add(key, adjustment);
                }
            }

            results.Add(String.Empty, 0);

            return results;
        }
    }
}