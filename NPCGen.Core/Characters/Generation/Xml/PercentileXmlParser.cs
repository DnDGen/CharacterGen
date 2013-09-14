using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml;

namespace NPCGen.Core.Characters.Generation.Xml
{
    public class PercentileXmlParser : IPercentileXmlParser
    {
        public List<PercentileObject> Parse(String filename)
        {
            var asm = Assembly.GetExecutingAssembly();
            var resources = asm.GetManifestResourceNames();

            if (!resources.Any(r => r.Contains(filename)))
                throw new FileNotFoundException();

            var streamSource = resources.First(r => r.Contains(filename));
            var results = new List<PercentileObject>();

            using (var stream = asm.GetManifestResourceStream(streamSource))
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