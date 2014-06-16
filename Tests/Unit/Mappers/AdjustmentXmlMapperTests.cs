using System;
using System.IO;
using Moq;
using NPCGen.Mappers;
using NPCGen.Mappers.Interfaces;
using NPCGen.Tables.Interfaces;
using NUnit.Framework;

namespace NPCGen.Tests.Unit.Mappers
{
    [TestFixture]
    public class AdjustmentXmlMapperTests
    {
        private const String tableName = "AdjustmentXmlMapperTests";

        private String filename;
        private IAdjustmentMapper mapper;
        private Mock<IStreamLoader> mockStreamLoader;

        [SetUp]
        public void Setup()
        {
            filename = tableName + ".xml";
            MakeXmlFile();

            mockStreamLoader = new Mock<IStreamLoader>();
            mockStreamLoader.Setup(l => l.LoadFor(filename)).Returns(GetStream());

            mapper = new AdjustmentXmlMapper(mockStreamLoader.Object);
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
            Assert.That(results["race"], Is.EqualTo(1));
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