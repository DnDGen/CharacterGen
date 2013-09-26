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
        [Test]
        public void NoMetaraceIsNotAllowed()
        {
            var mockPercentileResultProvider = new Mock<IPercentileResultProvider>();
            mockPercentileResultProvider.SetupSequence(p => p.GetPercentileResult(It.IsAny<String>())).Returns(String.Empty).Returns("metarace");

            var randomizer = new TestForcedMetaraceRandomizer(mockPercentileResultProvider.Object);

            randomizer.Randomize(new Alignment(), String.Empty);
            mockPercentileResultProvider.Verify(p => p.GetPercentileResult(It.IsAny<String>()), Times.Exactly(2));
        }

        private class TestForcedMetaraceRandomizer : ForcedMetaraceRandomizer
        {
            public TestForcedMetaraceRandomizer(IPercentileResultProvider percentileResultProvider) : base(percentileResultProvider) { }

            protected override Boolean RaceIsAllowed(String metarace, Alignment alignment)
            {
                return base.RaceIsAllowed(metarace, alignment);
            }
        }
    }
}