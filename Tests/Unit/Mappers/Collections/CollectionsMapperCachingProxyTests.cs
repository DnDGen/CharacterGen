using CharacterGen.Mappers;
using CharacterGen.Mappers.Domain.Collections;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace CharacterGen.Tests.Unit.Mappers.Collections
{
    [TestFixture]
    public class CollectionsMapperCachingProxyTests
    {
        private ICollectionsMapper proxy;
        private Mock<ICollectionsMapper> mockInnerMapper;
        private Dictionary<String, IEnumerable<String>> table;

        [SetUp]
        public void Setup()
        {
            table = new Dictionary<String, IEnumerable<String>>();
            mockInnerMapper = new Mock<ICollectionsMapper>();
            mockInnerMapper.Setup(m => m.Map("table name")).Returns(table);

            proxy = new CollectionsMapperCachingProxy(mockInnerMapper.Object);
        }

        [Test]
        public void ReturnTableFromInnerMapper()
        {
            var result = proxy.Map("table name");
            Assert.That(result, Is.EqualTo(table));
        }

        [Test]
        public void CacheTable()
        {
            proxy.Map("table name");
            var result = proxy.Map("table name");

            Assert.That(result, Is.EqualTo(table));
            mockInnerMapper.Verify(p => p.Map(It.IsAny<String>()), Times.Once);
        }
    }
}