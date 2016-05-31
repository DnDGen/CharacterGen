using CharacterGen.Domain.Tables;
using System.Collections.Generic;
using System.Xml;

namespace CharacterGen.Domain.Mappers.Collections
{
    internal class CollectionsXmlMapper : CollectionsMapper
    {
        private StreamLoader streamLoader;

        public CollectionsXmlMapper(StreamLoader streamLoader)
        {
            this.streamLoader = streamLoader;
        }

        public Dictionary<string, IEnumerable<string>> Map(string tableName)
        {
            var filename = tableName + ".xml";
            var results = new Dictionary<string, IEnumerable<string>>();
            var xmlDocument = new XmlDocument();

            using (var stream = streamLoader.LoadFor(filename))
                xmlDocument.Load(stream);

            foreach (XmlNode node in xmlDocument.DocumentElement.ChildNodes)
            {
                var items = new List<string>();
                var itemNodes = node.SelectNodes("item");

                foreach (XmlNode itemNode in itemNodes)
                    items.Add(itemNode.InnerText);

                var name = node.SelectSingleNode("name").InnerText;
                results.Add(name, items);
            }

            return results;
        }
    }
}