using CharacterGen.Mappers;
using CharacterGen.Selectors;
using CharacterGen.Selectors.Domain;
using RollGen;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CharacterGen.Tests.Unit.Selectors
{
    [TestFixture]
    public class PercentileSelectorTests
    {
        private const String tableName = "table name";

        private IPercentileSelector percentileSelector;
        private Dictionary<Int32, String> table;
        private Mock<IDice> mockDice;
        private Mock<IPercentileMapper> mockPercentileMapper;

        [SetUp]
        public void Setup()
        {
            table = new Dictionary<Int32, String>();
            for (var i = 1; i <= 5; i++)
                table.Add(i, "content");
            for (var i = 6; i <= 10; i++)
                table.Add(i, i.ToString());

            mockPercentileMapper = new Mock<IPercentileMapper>();
            mockPercentileMapper.Setup(p => p.Map(tableName)).Returns(table);

            mockDice = new Mock<IDice>();
            mockDice.Setup(d => d.Roll(1).Percentile()).Returns(1);
            percentileSelector = new PercentileSelector(mockPercentileMapper.Object, mockDice.Object);
        }

        [TestCase(1, "content")]
        [TestCase(2, "content")]
        [TestCase(3, "content")]
        [TestCase(4, "content")]
        [TestCase(5, "content")]
        [TestCase(6, "6")]
        [TestCase(7, "7")]
        [TestCase(8, "8")]
        [TestCase(9, "9")]
        [TestCase(10, "10")]
        public void GetPercentile(Int32 roll, String content)
        {
            mockDice.Setup(d => d.Roll(1).Percentile()).Returns(roll);
            var result = percentileSelector.SelectFrom(tableName);
            Assert.That(result, Is.EqualTo(content));
        }

        [Test]
        public void GetAllResultsReturnsAllContentValues()
        {
            var results = percentileSelector.SelectAllFrom(tableName);
            var distinctContent = table.Values.Distinct();

            foreach (var content in distinctContent)
                Assert.That(results, Contains.Item(content));

            var extras = distinctContent.Except(results);
            Assert.That(extras, Is.Empty);
        }

        [Test]
        public void IfRollNotPresentInTable_ThrowException()
        {
            mockDice.Setup(d => d.Roll(1).Percentile()).Returns(11);
            Assert.That(() => percentileSelector.SelectFrom(tableName), Throws.Exception.With.Message.EqualTo("11 is not a valid entry in the table table name"));
        }
    }
}