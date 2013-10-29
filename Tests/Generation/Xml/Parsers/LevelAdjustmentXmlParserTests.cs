using System;
using System.IO;
using System.Linq;
using Moq;
using NPCGen.Core.Generation.Xml.Parsers;
using NPCGen.Core.Generation.Xml.Parsers.Interfaces;
using NUnit.Framework;

namespace NPCGen.Tests.Generation.Xml.Parsers
{
    [TestFixture]
    public class LevelAdjustmentXmlParserTests
    {
        private ILevelAdjustmentXmlParser parser;
        private Mock<IStreamLoader> mockStreamLoader;
        private const String filename = "LevelAdjustmentXmlParserTests.xml";

        [SetUp]
        public void Setup()
        {
            MakeXmlFile();

            mockStreamLoader = new Mock<IStreamLoader>();
            mockStreamLoader.Setup(l => l.LoadStream(filename)).Returns(GetStream());

            parser = new LevelAdjustmentXmlParser(mockStreamLoader.Object);
        }

        [Test]
        public void LoadXmlFromStream()
        {
            var objects = parser.Parse(filename);

            Assert.That(objects.Count(), Is.EqualTo(1));
            Assert.That(objects.ContainsKey("race"), Is.True);
            Assert.That(objects["race"], Is.EqualTo(1));
        }

        private Stream GetStream()
        {
            return new FileStream(filename, FileMode.Open);
        }

        private void MakeXmlFile()
        {
            var content = "<?xml version=\"1.0\" encoding=\"utf-8\" ?><levelAdjustments><object><race>race</race><adjustment>1</adjustment></object></levelAdjustments>";
            File.WriteAllText(filename, content);
        }
    }
}