using System;
using System.IO;
using System.Linq;
using Moq;
using NPCGen.Mappers.Interfaces;
using NPCGen.Mappers.Percentiles;
using NPCGen.Tables.Interfaces;
using NUnit.Framework;

namespace NPCGen.Tests.Unit.Mappers.Percentiles
{
    [TestFixture]
    public class PercentileXmlMapperTests
    {
        private IPercentileMapper percentileMapper;
        private Mock<IStreamLoader> mockStreamLoader;
        private const String tableName = "PercentileXmlMapperTests";
        private String filename;

        [SetUp]
        public void Setup()
        {
            filename = tableName + ".xml";
            MakeXmlFile();

            mockStreamLoader = new Mock<IStreamLoader>();
            mockStreamLoader.Setup(l => l.LoadFor(filename)).Returns(() => GetStream());

            percentileMapper = new PercentileXmlMapper(mockStreamLoader.Object);
        }

        [Test]
        public void AppendXmlFileExtensionToTableName()
        {
            percentileMapper.Map(tableName);
            mockStreamLoader.Verify(l => l.LoadFor(filename), Times.Once);
        }

        [Test]
        public void LoadXmlFromStream()
        {
            var table = percentileMapper.Map(tableName);

            Assert.That(table[1], Is.EqualTo("one through five"));
            Assert.That(table[2], Is.EqualTo("one through five"));
            Assert.That(table[3], Is.EqualTo("one through five"));
            Assert.That(table[4], Is.EqualTo("one through five"));
            Assert.That(table[5], Is.EqualTo("one through five"));
            Assert.That(table[6], Is.EqualTo("six only"));
            Assert.That(table[7], Is.Empty);
            Assert.That(table.Count(), Is.EqualTo(7));
        }

        private Stream GetStream()
        {
            return new FileStream(filename, FileMode.Open);
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
            File.WriteAllText(filename, content);
        }
    }
}