using System;
using System.Collections.Generic;
using System.IO;
using Moq;
using NPCGen.Mappers;
using NPCGen.Tables.Interfaces;
using NUnit.Framework;

namespace NPCGen.Tests.Unit.Mappers
{
    [TestFixture]
    public class AdjustmentXmlMapperTests
    {
        private const String filename = "AdjustmentXmlMapperTests.xml";

        private Dictionary<String, Int32> results;

        [SetUp]
        public void Setup()
        {
            MakeXmlFile();

            var mockStreamLoader = new Mock<IStreamLoader>();
            mockStreamLoader.Setup(l => l.LoadFor(filename)).Returns(GetStream());

            var Mapper = new AdjustmentXmlMapper(mockStreamLoader.Object);
            results = Mapper.Parse(filename);
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