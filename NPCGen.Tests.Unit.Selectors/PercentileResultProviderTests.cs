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
    public class PercentileResultProviderTests
    {
        private const String tableName = "table";
        private const Int32 min = 1;
        private const Int32 max = 50;

        private IPercentileResultProvider percentileResultProvider;
        private Dictionary<Int32, String> table;
        private Mock<IDice> mockDice;
        private Mock<IPercentileXmlParser> mockPercentileXmlParser;

        [SetUp]
        public void Setup()
        {
            table = new Dictionary<Int32, String>();
            for (var i = min; i <= max; i++)
                table.Add(i, "content");

            mockPercentileXmlParser = new Mock<IPercentileXmlParser>();
            mockPercentileXmlParser.Setup(p => p.Parse(tableName + ".xml")).Returns(table);

            mockDice = new Mock<IDice>();
            percentileResultProvider = new PercentileResultProvider(mockPercentileXmlParser.Object, mockDice.Object);
        }

        [Test]
        public void GetPercentileResultCachesTable()
        {
            percentileResultProvider.GetPercentileResult(tableName);
            percentileResultProvider.GetPercentileResult(tableName);

            mockPercentileXmlParser.Verify(p => p.Parse(tableName + ".xml"), Times.Once());
        }

        [Test]
        public void GetPercentileResultReturnsEmptyStringForEmptyTable()
        {
            table.Clear();
            var result = percentileResultProvider.GetPercentileResult(tableName);
            Assert.That(result, Is.EqualTo(String.Empty));
        }

        [Test]
        public void GetPercentileResultReturnsEmptyStringIfBelowRange()
        {
            mockDice.Setup(d => d.Percentile(1)).Returns(min - 1);
            var result = percentileResultProvider.GetPercentileResult(tableName);
            Assert.That(result, Is.EqualTo(String.Empty));
        }

        [Test]
        public void GetPercentileResultReturnsEmptyStringIfAboveRange()
        {
            mockDice.Setup(d => d.Percentile(1)).Returns(max + 1);
            var result = percentileResultProvider.GetPercentileResult(tableName);
            Assert.That(result, Is.EqualTo(String.Empty));
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
        public void GetAllResultsIncludesEmptyStringIfEmptyTable()
        {
            table.Clear();
            var results = percentileResultProvider.GetAllResults(tableName);

            Assert.That(results.Contains(String.Empty), Is.True);
            Assert.That(results.Count(), Is.EqualTo(1));
        }

        [Test]
        public void GetAllResultsReturnsAllContentValues()
        {
            for (var i = max + 1; i < max + 10; i++)
            {
                var content = String.Format("Item {0}", i);
                table.Add(i, content);
            }

            var results = percentileResultProvider.GetAllResults(tableName);
            var distinctContent = table.Values.Distinct();

            foreach (var content in distinctContent)
                Assert.That(results, Contains.Item(content));

            var extras = distinctContent.Except(results);
            Assert.That(extras, Is.Empty);
        }

        [Test]
        public void GetAllResultsIncludesEmptyStringIfIncompleteTable()
        {
            var results = percentileResultProvider.GetAllResults(tableName);
            Assert.That(results.Contains(String.Empty), Is.True);
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