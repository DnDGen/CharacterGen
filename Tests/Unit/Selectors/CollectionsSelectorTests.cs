using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using NPCGen.Mappers.Interfaces;
using NPCGen.Selectors;
using NPCGen.Selectors.Interfaces;
using NUnit.Framework;

namespace NPCGen.Tests.Unit.Selectors
{
    [TestFixture]
    public class CollectionsSelectorTests
    {
        private const String TableName = "table name";

        private ICollectionsSelector selector;
        private Mock<ICollectionsMapper> mockMapper;
        private Dictionary<String, IEnumerable<String>> allCollections;

        [SetUp]
        public void Setup()
        {
            mockMapper = new Mock<ICollectionsMapper>();
            selector = new CollectionsSelector(mockMapper.Object);
            allCollections = new Dictionary<String, IEnumerable<String>>();

            mockMapper.Setup(m => m.Map(TableName)).Returns(allCollections);
        }

        [Test]
        public void SelectCollection()
        {
            allCollections["entry"] = Enumerable.Empty<String>();
            var collection = selector.SelectFrom(TableName, "entry");
            Assert.That(collection, Is.EqualTo(allCollections["entry"]));
        }

        [Test]
        public void SelectAllCollections()
        {
            var collections = selector.SelectAllFrom(TableName);
            Assert.That(collections, Is.EqualTo(allCollections));
        }
    }
}