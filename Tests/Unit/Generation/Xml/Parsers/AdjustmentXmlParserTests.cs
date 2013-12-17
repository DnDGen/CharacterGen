using System;
using System.Collections.Generic;
using System.IO;
using Moq;
using NPCGen.Core.Generation.Xml.Parsers;
using NPCGen.Core.Generation.Xml.Parsers.Interfaces;
using NUnit.Framework;

namespace NPCGen.Tests.Unit.Generation.Xml.Parsers
{
    [TestFixture]
    public class AdjustmentXmlParserTests
    {
        private const String filename = "AdjustmentXmlParserTests.xml";

        private Dictionary<String, Int32> results;

        [SetUp]
        public void Setup()
        {
            MakeXmlFile();

            var mockStreamLoader = new Mock<IStreamLoader>();
            mockStreamLoader.Setup(l => l.LoadStream(filename)).Returns(GetStream());

            var parser = new AdjustmentXmlParser(mockStreamLoader.Object);
            results = parser.Parse(filename);
        }

        [Test]
        public void LoadXmlFromStream()
        {
            Assert.That(results.ContainsKey("race"), Is.True);
            Assert.That(results["race"], Is.EqualTo(1));
        }

        [Test]
        public void ResultsContainsEmptyStringWithAdjustmentOfZero()
        {
            Assert.That(results.ContainsKey(String.Empty), Is.True);
            Assert.That(results[String.Empty], Is.EqualTo(0));
        }

        private Stream GetStream()
        {
            return new FileStream(filename, FileMode.Open);
        }

        private void MakeXmlFile()
        {
            var content = "<?xml version=\"1.0\" encoding=\"utf-8\" ?><adjustments><object><key>race</key><adjustment>1</adjustment></object></adjustments>";
            File.WriteAllText(filename, content);
        }
    }
}