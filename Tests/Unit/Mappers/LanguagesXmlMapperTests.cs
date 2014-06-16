using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Moq;
using NPCGen.Common.Races;
using NPCGen.Mappers;
using NPCGen.Tables.Interfaces;
using NUnit.Framework;

namespace NPCGen.Tests.Unit.Mappers
{
    [TestFixture]
    public class LanguagesXmlMapperTests
    {
        private const String filename = "LanguagesXmlMapperTests.xml";

        private Dictionary<String, IEnumerable<String>> results;

        [SetUp]
        public void Setup()
        {
            MakeXmlFile();

            var mockStreamLoader = new Mock<IStreamLoader>();
            mockStreamLoader.Setup(l => l.LoadFor(filename)).Returns(GetStream());

            var Mapper = new LanguagesXmlMapper(mockStreamLoader.Object);
            results = Mapper.Parse(filename);
        }

        [Test]
        public void LoadXmlFromStream()
        {
            Assert.That(results.ContainsKey("race"), Is.True);

            var languages = results["race"];
            Assert.That(languages.Contains("first language"), Is.True);
            Assert.That(languages.Contains("second language"), Is.True);
            Assert.That(languages.Count(), Is.EqualTo(2));
        }

        [Test]
        public void ResultsContainsEmptyStringWithNoLanguages()
        {
            Assert.That(results.ContainsKey(String.Empty), Is.True);
            Assert.That(results[String.Empty].Any(), Is.False);
        }

        [Test]
        public void MetaracesNotInTableGivenEmptyEnumerableOfString()
        {
            foreach (var metarace in RaceConstants.Metaraces.GetMetaraces())
            {
                Assert.That(results.ContainsKey(metarace), Is.True);
                Assert.That(results[String.Empty].Any(), Is.False);
            }
        }

        private Stream GetStream()
        {
            return new FileStream(filename, FileMode.Open);
        }

        private void MakeXmlFile()
        {
            var content = "<?xml version=\"1.0\" encoding=\"utf-8\" ?><languages><object><key>race</key><language>first language</language><language>second language</language></object></languages>";
            File.WriteAllText(filename, content);
        }
    }
}