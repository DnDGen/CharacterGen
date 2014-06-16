using System;
using System.Collections.Generic;
using System.Xml;
using NPCGen.Mappers.Interfaces;
using NPCGen.Tables.Interfaces;

namespace NPCGen.Mappers
{
    public class AdjustmentXmlMapper : IAdjustmentMapper
    {
        private IStreamLoader streamLoader;

        public AdjustmentXmlMapper(IStreamLoader streamLoader)
        {
            this.streamLoader = streamLoader;
        }

        public Dictionary<String, Int32> Map(String tableName)
        {
            var filename = tableName + ".xml";
            var results = new Dictionary<String, Int32>();
            var xmlDocument = new XmlDocument();

            using (var stream = streamLoader.LoadFor(filename))
                xmlDocument.Load(stream);

            foreach (XmlNode node in xmlDocument.DocumentElement.ChildNodes)
            {
                var key = node.SelectSingleNode("key").InnerText;
                var adjustment = Convert.ToInt32(node.SelectSingleNode("adjustment").InnerText);

                results.Add(key, adjustment);
            }

            return results;
        }
    }
}