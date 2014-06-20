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

namespace NPCGen.Tests.Unit.Mappers
{
    [TestFixture]
    public class AdjustmentXmlMapperTests
    {
        private IAdjustmentMapper mapper;
        private Mock<ICollectionsMapper> mockInnerMapper;
        private Dictionary<String, IEnumerable<String>> table;

        [SetUp]
        public void Setup()
        {
            table = new Dictionary<String, IEnumerable<String>>();
            mockInnerMapper = new Mock<ICollectionsMapper>();
            mapper = new AdjustmentXmlMapper(mockInnerMapper.Object);

            table.Add("name", new[] { "9266" });
            mockInnerMapper.Setup(m => m.Map("table name")).Returns(table);
        }

        [Test]
        public void GetCollectionFromInnerMapper()
        {
            mapper.Map("table name");
            mockInnerMapper.Verify(l => l.Map("table name"), Times.Once);
        }

        [Test]
        public void ThrowExceptionIfAnyEmptyCollections()
        {
            table.Add("empty", Enumerable.Empty<String>());
            Assert.That(() => mapper.Map("table name"), Throws.Exception);
        }

        [Test]
        public void ConvertColelctionToAdjustment()
        {
            var results = mapper.Map("table name");
            Assert.That(results.ContainsKey("name"), Is.True);
            Assert.That(results["name"], Is.EqualTo(9266));
        }
    }
}