using System;
using System.Collections.Generic;
using System.Linq;
using D20Dice;
using Moq;
using NPCGen.Mappers.Interfaces;
using NPCGen.Selectors;
using NPCGen.Selectors.Interfaces;
using NUnit.Framework;

namespace NPCGen.Tests.Unit.Selectors
{
    [TestFixture]
    public class PercentileSelectorTests
    {
        private const String tableName = "table";

        private IPercentileSelector percentileSelector;
        private Dictionary<Int32, String> table;
        private Mock<IDice> mockDice;
        private Mock<IPercentileMapper> mockPercentileMapper;

        [SetUp]
        public void Setup()
        {
            table = new Dictionary<Int32, String>();
            for (var i = 1; i <= 50; i++)
                table.Add(i, "content");
            for (var i = 51; i <= 100; i++)
                table.Add(i, i.ToString());

            mockPercentileMapper = new Mock<IPercentileMapper>();
            mockPercentileMapper.Setup(p => p.Map(tableName)).Returns(table);

            mockDice = new Mock<IDice>();
            mockDice.Setup(d => d.Percentile(1)).Returns(1);
            percentileSelector = new PercentileSelector(mockPercentileMapper.Object, mockDice.Object);
        }

        [Test]
        public void GetPercentileResultCachesTable()
        {
            percentileSelector.GetPercentileFrom(tableName);
            percentileSelector.GetPercentileFrom(tableName);

            mockPercentileMapper.Verify(p => p.Map(tableName), Times.Once());
        }

        [Test]
        public void GetRange()
        {
            for (var roll = 1; roll <= 50; roll++)
            {
                mockDice.Setup(d => d.Percentile(1)).Returns(roll);
                var result = percentileSelector.GetPercentileFrom(tableName);
                Assert.That(result, Is.EqualTo("content"));
            }
        }

        [Test]
        public void GetSingles()
        {
            for (var roll = 51; roll <= 100; roll++)
            {
                mockDice.Setup(d => d.Percentile(1)).Returns(roll);
                var result = percentileSelector.GetPercentileFrom(tableName);
                Assert.That(result, Is.EqualTo(roll.ToString()));
            }
        }

        [Test]
        public void GetAllResultsReturnsAllContentValues()
        {
            var results = percentileSelector.GetAllResults(tableName);
            var distinctContent = table.Values.Distinct();

            foreach (var content in distinctContent)
                Assert.That(results, Contains.Item(content));

            var extras = distinctContent.Except(results);
            Assert.That(extras, Is.Empty);
        }

        [Test]
        public void GetAllResultsCachesTable()
        {
            percentileSelector.GetAllResults(tableName);
            percentileSelector.GetAllResults(tableName);

            mockPercentileMapper.Verify(p => p.Map(tableName), Times.Once());
        }
    }
}