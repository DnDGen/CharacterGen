using System;
using D20Dice.Dice;
using Moq;
using NPCGen.Core.Generation.Randomizers.Providers;
using NPCGen.Core.Generation.Randomizers.Providers.Interfaces;
using NPCGen.Core.Generation.Xml.Parsers;
using NUnit.Framework;

namespace NPCGen.Tests.Generation.Xml.Data
{
    [TestFixture]
    public abstract class PercentileTests
    {
        protected IPercentileResultProvider percentileResultProvider;
        protected String tableName;

        private Mock<IDice> mockDice;

        [SetUp]
        public void Setup()
        {
            var embeddedResourceStreamLoader = new EmbeddedResourceStreamLoader();
            var percentileXmlParser = new PercentileXmlParser(embeddedResourceStreamLoader);
            mockDice = new Mock<IDice>();

            percentileResultProvider = new PercentileResultProvider(percentileXmlParser, mockDice.Object);
        }

        protected void AssertContentIsInRange(String content, Int32 minInclusive, Int32 maxInclusive)
        {
            for (var roll = minInclusive; roll <= maxInclusive; roll++)
            {
                mockDice.Setup(d => d.Percentile(1, 0)).Returns(roll);
                var result = percentileResultProvider.GetPercentileResult(tableName);
                Assert.That(result, Is.EqualTo(content));
            }
        }
    }
}