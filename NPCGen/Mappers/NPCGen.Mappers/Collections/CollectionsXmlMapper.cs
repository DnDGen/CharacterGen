using System;
using System.Collections.Generic;
using System.Xml;
using NPCGen.Mappers.Interfaces;
using NPCGen.Tables.Interfaces;

namespace NPCGen.Mappers.Collections
{
    public class CollectionsXmlMapper : ICollectionsMapper
    {
        private IStreamLoader streamLoader;

        public CollectionsXmlMapper(IStreamLoader streamLoader)
        {
            this.streamLoader = streamLoader;
        }

        public Dictionary<String, IEnumerable<String>> Map(String tableName)
        {
            var filename = tableName + ".xml";
            var results = new Dictionary<String, IEnumerable<String>>();
            var xmlDocument = new XmlDocument();

            using (var stream = streamLoader.LoadFor(filename))
                xmlDocument.Load(stream);

            foreach (XmlNode node in xmlDocument.DocumentElement.ChildNodes)
            {
                var languages = new List<String>();
                var languageNodes = node.SelectNodes("language");

                foreach (XmlNode languageNode in languageNodes)
                    languages.Add(languageNode.InnerText);

                var key = node.SelectSingleNode("key").InnerText;
                results.Add(key, languages);
            }

            return results;
        }
    }
}