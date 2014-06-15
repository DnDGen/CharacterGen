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
        private const Int32 min = 1;
        private const Int32 max = 50;

        private IPercentileSelector percentileResultProvider;
        private Dictionary<Int32, String> table;
        private Mock<IDice> mockDice;
        private Mock<IPercentileMapper> mockPercentileXmlParser;

        [SetUp]
        public void Setup()
        {
            table = new Dictionary<Int32, String>();
            for (var i = min; i <= max; i++)
                table.Add(i, "content");
            for (var i = max + 1; i <= 100; i++)
                table.Add(i, i.ToString());

            mockPercentileXmlParser = new Mock<IPercentileMapper>();
            mockPercentileXmlParser.Setup(p => p.Parse(tableName + ".xml")).Returns(table);

            mockDice = new Mock<IDice>();
            mockDice.Setup(d => d.Percentile(1)).Returns(1);
            percentileResultProvider = new PercentileSelector(mockPercentileXmlParser.Object, mockDice.Object);
        }

        [Test]
        public void GetPercentileResultCachesTable()
        {
            percentileResultProvider.GetPercentileResult(tableName);
            percentileResultProvider.GetPercentileResult(tableName);

            mockPercentileXmlParser.Verify(p => p.Parse(tableName + ".xml"), Times.Once());
        }

        [Test]
        public void GetRange()
        {
            for (var roll = min; roll <= max; roll++)
            {
                mockDice.Setup(d => d.Percentile(1)).Returns(roll);
                var result = percentileResultProvider.GetPercentileResult(tableName);
                Assert.That(result, Is.EqualTo("content"));
            }
        }

        [Test]
        public void GetSingles()
        {
            for (var roll = max + 1; roll <= 100; roll++)
            {
                mockDice.Setup(d => d.Percentile(1)).Returns(roll);
                var result = percentileResultProvider.GetPercentileResult(tableName);
                Assert.That(result, Is.EqualTo(roll.ToString()));
            }
        }

        [Test]
        public void GetPercentileResultReturnContentIfInInclusiveRange()
        {
            for (var roll = min; roll <= max; roll++)
            {
                mockDice.Setup(d => d.Percentile(1)).Returns(roll);

                var result = percentileResultProvider.GetPercentileResult(tableName);
                Assert.That(result, Is.EqualTo("content"));
            }
        }

        [Test]
        public void GetAllResultsReturnsAllContentValues()
        {
            var results = percentileResultProvider.GetAllResults(tableName);
            var distinctContent = table.Values.Distinct();

            foreach (var content in distinctContent)
                Assert.That(results, Contains.Item(content));

            var extras = distinctContent.Except(results);
            Assert.That(extras, Is.Empty);
        }

        [Test]
        public void GetAllResultsCachesTable()
        {
            percentileResultProvider.GetAllResults(tableName);
            percentileResultProvider.GetAllResults(tableName);

            mockPercentileXmlParser.Verify(p => p.Parse(tableName + ".xml"), Times.Once());
        }
    }
}