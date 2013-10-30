using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using NPCGen.Core.Data.Races;
using NPCGen.Core.Generation.Xml.Parsers.Interfaces;

namespace NPCGen.Core.Generation.Xml.Parsers
{
    public class LanguagesXmlParser : ILanguagesXmlParser
    {
        private IStreamLoader streamLoader;

        public LanguagesXmlParser(IStreamLoader streamLoader)
        {
            this.streamLoader = streamLoader;
        }

        public Dictionary<String, IEnumerable<String>> Parse(String filename)
        {
            var results = new Dictionary<String, IEnumerable<String>>();

            using (var stream = streamLoader.LoadStream(filename))
            {
                var xmlDocument = new XmlDocument();
                xmlDocument.Load(stream);

                var objects = xmlDocument.DocumentElement.ChildNodes;
                foreach (XmlNode node in objects)
                {
                    var languages = new List<String>();
                    var languageNodes = node.SelectNodes("language");

                    foreach (XmlNode languageNode in languageNodes)
                        languages.Add(languageNode.InnerText);

                    var key = node.SelectSingleNode("key").InnerText;
                    results.Add(key, languages);
                }
            }

            foreach (var metarace in RaceConstants.Metaraces.GetMetaraces())
                if (!results.ContainsKey(metarace))
                    results.Add(metarace, Enumerable.Empty<String>());

            results.Add(String.Empty, Enumerable.Empty<String>());

            return results;
        }
    }
}