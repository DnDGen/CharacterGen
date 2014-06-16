using System;
using System.IO;
using System.Linq;
using Moq;
using NPCGen.Mappers;
using NPCGen.Mappers.Interfaces;
using NPCGen.Tables.Interfaces;
using NUnit.Framework;

namespace NPCGen.Tests.Unit.Mappers
{
    [TestFixture]
    public class LanguagesXmlMapperTests
    {
        private const String tableName = "LanguagesXmlMapperTests";

        private String filename;
        private ILanguagesMapper mapper;
        private Mock<IStreamLoader> mockStreamLoader;

        [SetUp]
        public void Setup()
        {
            filename = tableName + ".xml";
            MakeXmlFile();

            mockStreamLoader = new Mock<IStreamLoader>();
            mockStreamLoader.Setup(l => l.LoadFor(filename)).Returns(() => GetStream());

            mapper = new LanguagesXmlMapper(mockStreamLoader.Object);
        }

        [Test]
        public void AppendXmlFileExtensionToTableName()
        {
            mapper.Map(tableName);
            mockStreamLoader.Verify(l => l.LoadFor(filename), Times.Once);
        }

        [Test]
        public void LoadXmlFromStream()
        {
            var results = mapper.Map(tableName);
            Assert.That(results.ContainsKey("race"), Is.True);

            var languages = results["race"];
            Assert.That(languages.Contains("first language"), Is.True);
            Assert.That(languages.Contains("second language"), Is.True);
            Assert.That(languages.Count(), Is.EqualTo(2));
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