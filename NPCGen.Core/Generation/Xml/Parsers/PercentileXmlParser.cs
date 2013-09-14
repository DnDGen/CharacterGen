using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml;

namespace NPCGen.Core.Generation.Xml.Parsers
{
    public class PercentileXmlParser : IPercentileXmlParser
    {
        public List<PercentileObject> Parse(String filename)
        {
            var executingAssembly = Assembly.GetExecutingAssembly();
            var resources = executingAssembly.GetManifestResourceNames();

            if (!resources.Any(r => r.Contains(filename)))
                throw new FileNotFoundException();

            var streamSource = resources.First(r => r.Contains(filename));
            var results = new List<PercentileObject>();

            using (var stream = executingAssembly.GetManifestResourceStream(streamSource))
            {
                var xmlDocument = new XmlDocument();
                xmlDocument.Load(stream);

                var objects = xmlDocument.DocumentElement.ChildNodes;
                foreach (XmlNode node in objects)
                {
                    var percentileObject = new PercentileObject();

                    percentileObject.LowerLimit = Convert.ToInt32(node.SelectSingleNode("lower").InnerText);
                    percentileObject.Content = node.SelectSingleNode("content").InnerText;
                    percentileObject.UpperLimit = Convert.ToInt32(node.SelectSingleNode("upper").InnerText);

                    results.Add(percentileObject);
                }
            }

            return results;
        }
    }
}