using Moq;
using NPCGen.Core.Data.Alignments;
using NPCGen.Core.Data.CharacterClasses;
using NPCGen.Core.Generation.Providers.Interfaces;
using NPCGen.Core.Generation.Randomizers.Races.Metaraces;
using NUnit.Framework;
using System;

namespace NPCGen.Tests.Generation.Randomizers.Races.Metaraces
{
    [TestFixture]
    public class BaseMetaraceRandomizerTests
    {
        private TestMetaraceRandomizer randomizer;
        private Mock<IPercentileResultProvider> mockPercentileResultProvider;
        private Alignment alignment;

        [SetUp]
        public void Setup()
        {
            alignment = new Alignment();
            alignment.Goodness = AlignmentConstants.Good;

            mockPercentileResultProvider = new Mock<IPercentileResultProvider>();
            randomizer = new TestMetaraceRandomizer(mockPercentileResultProvider.Object);
        }

        [Test]
        public void LoopUntilMetaraceIsAllowed()
        {
            randomizer.MetaraceIsAllowed = false;
            randomizer.Randomize(alignment, CharacterClassConstants.Barbarian);
            mockPercentileResultProvider.Verify(p => p.GetPercentileResult("GoodBarbarianMetaraces"), Times.Exactly(2));
        }

        [Test]
        public void ReturnMetaraceFromPercentileResultProvider()
        {
            randomizer.MetaraceIsAllowed = true;
            mockPercentileResultProvider.Setup(p => p.GetPercentileResult("GoodBarbarianMetaraces")).Returns("result");

            var result = randomizer.Randomize(alignment, CharacterClassConstants.Barbarian);
            Assert.That(result, Is.EqualTo("result"));
        }

        private class TestMetaraceRandomizer : BaseMetaraceRandomizer
        {
            public Boolean MetaraceIsAllowed { get; set; }

            public TestMetaraceRandomizer(IPercentileResultProvider percentileResultProvider) : base(percentileResultProvider) { }

            protected override Boolean RaceIsAllowed(String metarace, Alignment alignment)
            {
                var toReturn = MetaraceIsAllowed;
                MetaraceIsAllowed = !MetaraceIsAllowed;
                return toReturn;
            }
        }
    }
}