using Moq;
using NPCGen.Core.Data.Alignments;
using NPCGen.Core.Data.CharacterClasses;
using NPCGen.Core.Generation.Providers.Interfaces;
using NPCGen.Core.Generation.Randomizers.Races.BaseRaces;
using NUnit.Framework;
using System;

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
            randomizer.Allowed = false;
            mockPercentileResultProvider.Setup(p => p.GetPercentileResult("GoodBarbarianBaseRaces")).Returns("result");

            randomizer.Randomize(alignment, CharacterClassConstants.Barbarian);
            mockPercentileResultProvider.Verify(p => p.GetPercentileResult("GoodBarbarianBaseRaces"), Times.Exactly(2));
        }

        [Test]
        public void ReturnBaseRaceFromPercentileResultProvider()
        {
            randomizer.Allowed = true;
            mockPercentileResultProvider.Setup(p => p.GetPercentileResult("GoodBarbarianBaseRaces")).Returns("result");

            var result = randomizer.Randomize(alignment, CharacterClassConstants.Barbarian);
            Assert.That(result, Is.EqualTo("result"));
        }

        [Test]
        public void NoBaseRaceIsNotAllowed()
        {
            randomizer.Allowed = true;
            mockPercentileResultProvider.SetupSequence(p => p.GetPercentileResult("GoodBarbarianBaseRaces")).Returns(String.Empty).Returns("result");

            randomizer.Randomize(alignment, CharacterClassConstants.Barbarian);
            mockPercentileResultProvider.Verify(p => p.GetPercentileResult("GoodBarbarianBaseRaces"), Times.Exactly(2));
        }

        private class TestBaseRaceRandomizer : BaseBaseRaceRandomizer
        {
            public Boolean Allowed { get; set; }

            public TestBaseRaceRandomizer(IPercentileResultProvider percentileResultProvider) : base(percentileResultProvider) { }

            protected override Boolean BaseRaceIsAllowed(String baseRace)
            {
                var toReturn = Allowed;
                Allowed = !Allowed;
                return toReturn;
            }
        }
    }
}