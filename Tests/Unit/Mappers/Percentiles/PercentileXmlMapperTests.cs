using CharacterGen.Mappers;
using CharacterGen.Mappers.Domain.Percentiles;
using CharacterGen.Tables;
using Moq;
using NUnit.Framework;
using System;
using System.IO;
using System.Linq;

namespace CharacterGen.Tests.Unit.Mappers.Percentiles
{
    [TestFixture]
    public class PercentileXmlMapperTests
    {
        private IPercentileMapper mapper;
        private Mock<IStreamLoader> mockStreamLoader;
        private const String tableName = "PercentileXmlMapperTests";
        private String filename;
        private String filePath;

        [SetUp]
        public void Setup()
        {
            filename = tableName + ".xml";
            filePath = Path.Combine(TestContext.CurrentContext.TestDirectory, filename);
            MakeXmlFile();

            mockStreamLoader = new Mock<IStreamLoader>();
            mockStreamLoader.Setup(l => l.LoadFor(filename)).Returns(() => GetStream());

            mapper = new PercentileXmlMapper(mockStreamLoader.Object);
        }

        private void MakeXmlFile()
        {
            var content = @"<?xml version=""1.0"" encoding=""utf-8"" ?>
                            <percentile>
                                <object>
                                    <lower>1</lower>
                                    <content>one through five</content>
                                    <upper>5</upper>
                                </object>
                                <object>
                                    <lower>6</lower>
                                    <content>six only</content>
                                    <upper>6</upper>
                                </object>
                                <object>
                                    <lower>7</lower>
                                    <content></content>
                                    <upper>7</upper>
                                </object>
                            </percentile>";
            File.WriteAllText(filePath, content);
        }

        private Stream GetStream()
        {
            return new FileStream(filePath, FileMode.Open);
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
            var table = mapper.Map(tableName);

            Assert.That(table[1], Is.EqualTo("one through five"));
            Assert.That(table[2], Is.EqualTo("one through five"));
            Assert.That(table[3], Is.EqualTo("one through five"));
            Assert.That(table[4], Is.EqualTo("one through five"));
            Assert.That(table[5], Is.EqualTo("one through five"));
            Assert.That(table[6], Is.EqualTo("six only"));
            Assert.That(table[7], Is.Empty);
            Assert.That(table.Count(), Is.EqualTo(7));
        }

        [Test]
        public void EmptyFileReturnsEmptyMapping()
        {
            MakeEmptyXmlFile();
            var table = mapper.Map(tableName);
            Assert.That(table, Is.Empty);
        }

        private void MakeEmptyXmlFile()
        {
            var content = @"<?xml version=""1.0"" encoding=""utf-8"" ?>
                            <percentile>
                            </percentile>";
            File.WriteAllText(filePath, content);
        }
    }
}