using CharacterGen.Domain.Mappers.Collections;
using CharacterGen.Domain.Tables;
using Moq;
using NUnit.Framework;
using System.IO;
using System.Linq;

namespace CharacterGen.Tests.Unit.Mappers.Collections
{
    [TestFixture]
    public class CollectionsXmlMapperTests
    {
        private const string tableName = "CollectionsXmlMapperTests";

        private string fileName;
        private CollectionsMapper mapper;
        private Mock<StreamLoader> mockStreamLoader;
        private string contents;

        [SetUp]
        public void Setup()
        {
            fileName = tableName + ".xml";
            contents = @"<?xml version=""1.0"" encoding=""utf-8"" ?>
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

            mockStreamLoader = new Mock<StreamLoader>();
            mockStreamLoader.Setup(l => l.LoadFor(fileName)).Returns(() => GetStream());

            mapper = new CollectionsXmlMapper(mockStreamLoader.Object);
        }

        [Test]
        public void AppendXmlFileExtensionToTableName()
        {
            mapper.Map(tableName);
            mockStreamLoader.Verify(l => l.LoadFor(fileName), Times.Once);
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
            contents = @"<?xml version=""1.0"" encoding=""utf-8"" ?>
                         <collections>
                         </collections>";

            var table = mapper.Map(tableName);
            Assert.That(table, Is.Empty);
        }

        private Stream GetStream()
        {
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(contents);
            writer.Flush();
            stream.Position = 0;

            return stream;
        }

        [Test]
        public void DuplicateNamesThrowError()
        {
            contents = @"<?xml version=""1.0"" encoding=""utf-8"" ?>
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

            Assert.That(() => mapper.Map(tableName), Throws.ArgumentException);
        }
    }
}