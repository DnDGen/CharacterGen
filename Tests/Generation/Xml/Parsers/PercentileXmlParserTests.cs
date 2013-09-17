using System;
using System.IO;
using Moq;
using NPCGen.Core.Generation.Xml.Parsers;
using NPCGen.Core.Generation.Xml.Parsers.Interfaces;
using NUnit.Framework;

namespace NPCGen.Tests.Generation.Xml.Parsers
{
    [TestFixture]
    public class PercentileXmlParserTests
    {
        private IPercentileXmlParser percentileXmlParser;
        private Mock<IStreamLoader> mockStreamLoader;
        private const String filename = "filename";

        [SetUp]
        public void Setup()
        {
            mockStreamLoader = new Mock<IStreamLoader>();
            mockStreamLoader.Setup(l => l.LoadStream(filename)).Returns(GetStream());

            percentileXmlParser = new PercentileXmlParser(mockStreamLoader.Object);
        }

        [Test]
        public void LoadXmlFromStream()
        {
            var objects = percentileXmlParser.Parse(filename);

            Assert.That(objects.Count, Is.EqualTo(2));

            Assert.That(objects[0].LowerLimit, Is.EqualTo(1));
            Assert.That(objects[0].Content, Is.EqualTo("1-5"));
            Assert.That(objects[0].UpperLimit, Is.EqualTo(5));

            Assert.That(objects[1].LowerLimit, Is.EqualTo(6));
            Assert.That(objects[1].Content, Is.EqualTo("6"));
            Assert.That(objects[1].UpperLimit, Is.EqualTo(6));
        }

        private Stream GetStream()
        {
            MakeXmlFile();
            return new FileStream("PercentileXmlParserTests.xml", FileMode.Open);
        }

        private void MakeXmlFile()
        {
            var content = "<?xml version=\"1.0\" encoding=\"utf-8\" ?><percentile><object><lower>1</lower><content>1-5</content><upper>5</upper></object><object><lower>6</lower><content>6</content><upper>6</upper></object></percentile>";
            File.WriteAllText("PercentileXmlParserTests.xml", content);
        }
    }
}