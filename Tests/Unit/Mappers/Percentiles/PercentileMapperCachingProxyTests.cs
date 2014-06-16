using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using NPCGen.Mappers.Interfaces;
using NPCGen.Mappers.Percentiles;
using NUnit.Framework;

namespace NPCGen.Tests.Unit.Mappers.Percentiles
{
    [TestFixture]
    public class PercentileMapperCachingProxyTests
    {
        private IPercentileMapper proxy;
        private Mock<IPercentileMapper> mockInnerMapper;
        private Dictionary<Int32, String> table;

        [SetUp]
        public void Setup()
        {
            table = new Dictionary<Int32, String>();
            mockInnerMapper = new Mock<IPercentileMapper>();
            mockInnerMapper.Setup(m => m.Map("table name")).Returns(table);

            proxy = new PercentileMapperCachingProxy(mockInnerMapper.Object);
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