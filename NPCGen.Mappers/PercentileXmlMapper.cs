using System;
using System.Collections.Generic;
using System.Xml;
using NPCGen.Mappers.Interfaces;
using NPCGen.Tables.Interfaces;

namespace NPCGen.Mappers
{
    public class PercentileXmlMapper : IPercentileMapper
    {
        private IStreamLoader streamLoader;

        public PercentileXmlMapper(IStreamLoader streamLoader)
        {
            this.streamLoader = streamLoader;
        }

        public Dictionary<Int32, String> Parse(String filename)
        {
            var results = new Dictionary<Int32, String>();
            var xmlDocument = new XmlDocument();

            using (var stream = streamLoader.LoadFor(filename))
                xmlDocument.Load(stream);

            var objects = xmlDocument.DocumentElement.ChildNodes;
            foreach (XmlNode node in objects)
            {
                var lowerLimit = Convert.ToInt32(node.SelectSingleNode("lower").InnerText);
                var content = node.SelectSingleNode("content").InnerText;
                var upperLimit = Convert.ToInt32(node.SelectSingleNode("upper").InnerText);

                for (var roll = lowerLimit; roll <= upperLimit; roll++)
                    results.Add(roll, content);
            }

            return results;
        }
    }
}