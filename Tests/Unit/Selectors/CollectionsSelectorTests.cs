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
        private ICollectionsSelector selector;
        private Mock<ICollectionsMapper> mockMapper;
        private Dictionary<String, IEnumerable<String>> collections;

        [SetUp]
        public void Setup()
        {
            mockMapper = new Mock<ICollectionsMapper>();
            selector = new CollectionsSelector(mockMapper.Object);
            collections = new Dictionary<String, IEnumerable<String>>();

            mockMapper.Setup(m => m.Map("table name")).Returns(collections);
        }

        [Test]
        public void SelectCollection()
        {
            collections["entry"] = Enumerable.Empty<String>();
            var collection = selector.SelectFrom("table name", "entry");
            Assert.That(collection, Is.EqualTo(collections["entry"]));
        }
    }
}