using System;
using Moq;
using NPCGen.Core.Data.Alignments;
using NPCGen.Core.Data.CharacterClasses;
using NPCGen.Core.Generation.Providers.Interfaces;
using NPCGen.Core.Generation.Randomizers.Races.BaseRaces;
using NUnit.Framework;

namespace NPCGen.Tests.Generation.Randomizers.Races.BaseRaces
{
    [TestFixture]
    public class BaseBaseRaceRandomizerTests
    {
        private TestBaseRaceRandomizer randomizer;
        private Mock<IPercentileResultProvider> mockPercentileResultProvider;
        private Alignment alignment;

        [SetUp]
        public void Setup()
        {
            alignment = new Alignment();
            alignment.Goodness = AlignmentConstants.Good;

            mockPercentileResultProvider = new Mock<IPercentileResultProvider>();
            randomizer = new TestBaseRaceRandomizer(mockPercentileResultProvider.Object);
        }

        [Test]
        public void LoopUntilBaseRaceIsAllowed()
        {
            randomizer.BaseRaceIsAllowed = false;
            randomizer.Randomize(alignment, CharacterClassConstants.Barbarian);
            mockPercentileResultProvider.Verify(p => p.GetPercentileResult("GoodBarbarianBaseRaces"), Times.Exactly(2));
        }

        [Test]
        public void ReturnBaseRaceFromPercentileResultProvider()
        {
            randomizer.BaseRaceIsAllowed = true;
            mockPercentileResultProvider.Setup(p => p.GetPercentileResult("GoodBarbarianBaseRaces")).Returns("result");

            var result = randomizer.Randomize(alignment, CharacterClassConstants.Barbarian);
            Assert.That(result, Is.EqualTo("result"));
        }

        private class TestBaseRaceRandomizer : BaseBaseRaceRandomizer
        {
            public Boolean BaseRaceIsAllowed { get; set; }

            public TestBaseRaceRandomizer(IPercentileResultProvider percentileResultProvider) : base(percentileResultProvider) { }

            protected override Boolean RaceIsAllowed(String baseRace, Alignment alignment)
            {
                var toReturn = BaseRaceIsAllowed;
                BaseRaceIsAllowed = !BaseRaceIsAllowed;
                return toReturn;
            }
        }
    }
}