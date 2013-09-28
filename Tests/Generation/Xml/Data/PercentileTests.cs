using D20Dice.Dice;
using Moq;
using NPCGen.Core.Generation.Providers;
using NPCGen.Core.Generation.Providers.Interfaces;
using NPCGen.Core.Generation.Xml.Parsers;
using NUnit.Framework;
using System;

namespace NPCGen.Tests.Generation.Xml.Data
{
    [TestFixture]
    public abstract class PercentileTests
    {
        protected String tableName;

        private Mock<IDice> mockDice;
        private IPercentileResultProvider percentileResultProvider;

        [SetUp]
        public void Setup()
        {
            mockDice = new Mock<IDice>();

            var embeddedResourceStreamLoader = new EmbeddedResourceStreamLoader();
            var percentileXmlParser = new PercentileXmlParser(embeddedResourceStreamLoader);
            percentileResultProvider = new PercentileResultProvider(percentileXmlParser, mockDice.Object);
        }

        protected void AssertEmpty(Int32 roll)
        {
            AssertContentOnSingleRoll(String.Empty, roll);
        }

        protected void AssertEmpty(Int32 minInclusive, Int32 maxInclusive)
        {
            AssertContentIsInRange(String.Empty, minInclusive, maxInclusive);
        }

        protected void AssertContentIsInRange(String content, Int32 minInclusive, Int32 maxInclusive)
        {
            for (var roll = minInclusive; roll <= maxInclusive; roll++)
                AssertContentOnSingleRoll(content, roll);
        }

        protected void AssertContentOnSingleRoll(String content, Int32 roll)
        {
            mockDice.Setup(d => d.Percentile(1, 0)).Returns(roll);
            var result = percentileResultProvider.GetPercentileResult(tableName);
            Assert.That(result, Is.EqualTo(content), String.Format("Roll: {0}", roll));
        }
    }
}