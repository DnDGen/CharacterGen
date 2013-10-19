using D20Dice.Dice;
using Moq;
using NPCGen.Core.Generation.Providers;
using NPCGen.Core.Generation.Providers.Interfaces;
using NPCGen.Core.Generation.Xml.Parsers.Interfaces;
using NPCGen.Core.Generation.Xml.Parsers.Objects;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NPCGen.Tests.Generation.Providers
{
    [TestFixture]
    public class PercentileResultProviderTests
    {
        private IPercentileResultProvider percentileResultProvider;
        private List<PercentileObject> table;
        private Mock<IDice> mockDice;
        private Mock<IPercentileXmlParser> mockPercentileXmlParser;
        private const String tableName = "table";
        private const Int32 min = 1;
        private const Int32 max = 100;

        [SetUp]
        public void Setup()
        {
            var percentileObject = new PercentileObject();
            percentileObject.Content = "content";
            percentileObject.LowerLimit = min;
            percentileObject.UpperLimit = max;

            table = new List<PercentileObject>();
            table.Add(percentileObject);

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
            mockDice.Setup(d => d.Percentile(1, 0)).Returns(min - 1);
            var result = percentileResultProvider.GetPercentileResult(tableName);
            Assert.That(result, Is.EqualTo(String.Empty));
        }

        [Test]
        public void GetPercentileResultReturnsEmptyStringIfAboveRange()
        {
            mockDice.Setup(d => d.Percentile(1, 0)).Returns(max + 1);
            var result = percentileResultProvider.GetPercentileResult(tableName);
            Assert.That(result, Is.EqualTo(String.Empty));
        }

        [Test]
        public void GetPercentileResultReturnContentIfInInclusiveRange()
        {
            for (var roll = min; roll <= max; roll++)
            {
                mockDice.Setup(d => d.Percentile(1, 0)).Returns(roll);

                var result = percentileResultProvider.GetPercentileResult(tableName);
                Assert.That(result, Is.EqualTo("content"));
            }
        }

        [Test]
        public void GetAllResultsReturnsEmptyEnumerableForEmptyTable()
        {
            table.Clear();
            var results = percentileResultProvider.GetAllResults(tableName);

            Assert.That(results.Any(), Is.False);
        }

        [Test]
        public void GetAllResultsReturnsAllContentValues()
        {
            for (var i = 1; i < 10; i++)
                table.Add(new PercentileObject() { Content = String.Format("Item {0}", i) });

            var results = percentileResultProvider.GetAllResults(tableName);

            foreach (var percentileObject in table)
                Assert.That(results.Contains(percentileObject.Content), Is.True);

            Assert.That(results.Count(), Is.EqualTo(table.Count));
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