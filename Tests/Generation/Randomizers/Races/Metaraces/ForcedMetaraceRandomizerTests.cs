using System;
using Moq;
using NPCGen.Core.Data.Alignments;
using NPCGen.Core.Generation.Providers.Interfaces;
using NPCGen.Core.Generation.Randomizers.Races.Metaraces;
using NUnit.Framework;

namespace NPCGen.Tests.Generation.Randomizers.Races.Metaraces
{
    [TestFixture]
    public class ForcedMetaraceRandomizerTests
    {
        private IMetaraceRandomizer randomizer;
        private Mock<IPercentileResultProvider> mockPercentileResultProvider;

        [SetUp]
        public void Setup()
        {
            mockPercentileResultProvider = new Mock<IPercentileResultProvider>();
            randomizer = new TestForcedMetaraceRandomizer(mockPercentileResultProvider.Object);
        }

        [Test]
        public void NoMetaraceIsNotAllowed()
        {
            mockPercentileResultProvider.SetupSequence(p => p.GetPercentileResult(It.IsAny<String>())).Returns(String.Empty).Returns("metarace");

            randomizer.Randomize(new Alignment(), String.Empty);
            mockPercentileResultProvider.Verify(p => p.GetPercentileResult(It.IsAny<String>()), Times.Exactly(2));
        }

        [Test]
        public void MetaraceIsAllowed()
        {
            mockPercentileResultProvider.Setup(p => p.GetPercentileResult(It.IsAny<String>())).Returns("metarace");

            var metarace = randomizer.Randomize(new Alignment(), String.Empty);
            Assert.That(metarace, Is.EqualTo("metarace"));
        }

        private class TestForcedMetaraceRandomizer : ForcedMetaraceRandomizer
        {
            public TestForcedMetaraceRandomizer(IPercentileResultProvider percentileResultProvider) : base(percentileResultProvider) { }

            protected override Boolean MetaraceIsAllowed(String metarace, Alignment alignment)
            {
                return true;
            }
        }
    }
}