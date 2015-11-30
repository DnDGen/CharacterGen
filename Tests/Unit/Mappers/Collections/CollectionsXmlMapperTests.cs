using CharacterGen.Mappers;
using CharacterGen.Mappers.Domain.Collections;
using CharacterGen.Tables;
using Moq;
using NUnit.Framework;
using System;
using System.IO;
using System.Linq;

namespace CharacterGen.Tests.Unit.Mappers.Collections
{
    [TestFixture]
    public class CollectionsXmlMapperTests
    {
        private const String tableName = "CollectionsXmlMapperTests";

        private String filename;
        private String filePath;
        private ICollectionsMapper mapper;
        private Mock<IStreamLoader> mockStreamLoader;

        [SetUp]
        public void Setup()
        {
            filename = tableName + ".xml";
            filePath = Path.Combine(TestContext.CurrentContext.TestDirectory, filename);
            MakeXmlFile();

            mockStreamLoader = new Mock<IStreamLoader>();
            mockStreamLoader.Setup(l => l.LoadFor(filename)).Returns(() => GetStream());

            mapper = new CollectionsXmlMapper(mockStreamLoader.Object);
        }

        private void MakeXmlFile()
        {
            var content = @"<?xml version=""1.0"" encoding=""utf-8"" ?>
                            <collections>
                                <object>
                                    <name>first name</name>
                                    <item>first item</item>
                                    <item>second item</item>
                                </object>
                                <object>
                                    <name>second name</name>
                                    <item>third item</item>
                                </object>
                                <object>
                                    <name>empty name</name>
                                </object>
                            </collections>";
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

            var items = table["first name"];
            Assert.That(items, Contains.Item("first item"));
            Assert.That(items, Contains.Item("second item"));
            Assert.That(items.Count(), Is.EqualTo(2));

            items = table["second name"];
            Assert.That(items, Contains.Item("third item"));
            Assert.That(items.Count(), Is.EqualTo(1));

            items = table["empty name"];
            Assert.That(items, Is.Empty);
        }

        [Test]
        public void EmptyFileReturnsEmptyMapping()
        {
            MakeEmptyXmlFile();
            var table = mapper.Map(tableName);
            Assert.That(table, Is.Empty);
        }

        [Test]
        public void DuplicateNamesThrowError()
        {
            var content = @"<?xml version=""1.0"" encoding=""utf-8"" ?>
                            <collections>
                                <object>
                                    <name>first name</name>
                                    <item>first item</item>
                                    <item>second item</item>
                                </object>
                                <object>
                                    <name>first name</name>
                                    <item>other item</item>
                                </object>
                                <object>
                                    <name>second name</name>
                                    <item>third item</item>
                                </object>
                                <object>
                                    <name>empty name</name>
                                </object>
                            </collections>";
            File.WriteAllText(filePath, content);

            Assert.Throws<ArgumentException>(() => mapper.Map(tableName));
        }

        private void MakeEmptyXmlFile()
        {
            var content = @"<?xml version=""1.0"" encoding=""utf-8"" ?>
                            <collections>
                            </collections>";
            File.WriteAllText(filePath, content);
        }
    }
}