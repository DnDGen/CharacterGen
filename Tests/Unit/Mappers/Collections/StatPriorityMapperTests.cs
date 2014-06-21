using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Moq;
using NPCGen.Mappers;
using NPCGen.Mappers.Collections;
using NPCGen.Mappers.Interfaces;
using NPCGen.Tables.Interfaces;
using NUnit.Framework;

namespace NPCGen.Tests.Unit.Mappers.Collections
{
    [TestFixture]
    public class StatPriorityMapperTests
    {
        private IStatPriorityMapper mapper;
        private Mock<ICollectionsMapper> mockInnerMapper;
        private Dictionary<String, IEnumerable<String>> table;

        [SetUp]
        public void Setup()
        {
            table = new Dictionary<String, IEnumerable<String>>();
            mockInnerMapper = new Mock<ICollectionsMapper>();
            mapper = new StatPriorityMapper(mockInnerMapper.Object);

            table.Add("name", new[] { "priority 1", "priority 2" });
            mockInnerMapper.Setup(m => m.Map("table name")).Returns(table);
        }

        [Test]
        public void GetCollectionFromInnerMapper()
        {
            mapper.Map("table name");
            mockInnerMapper.Verify(l => l.Map("table name"), Times.Once);
        }

        [Test]
        public void ThrowExceptionIfAnyCollectionsWithFewerThanTwoItems()
        {
            table.Add("too few", new[] { "priority 3" });
            Assert.That(() => mapper.Map("table name"), Throws.Exception);
        }

        [Test]
        public void ConvertCollectionToStatPriority()
        {
            var results = mapper.Map("table name");
            Assert.That(results.Keys, Contains.Item("name"));
            Assert.That(results["name"].FirstPriority, Is.EqualTo("priority 1"));
            Assert.That(results["name"].SecondPriority, Is.EqualTo("priority 2"));
        }
    }
}