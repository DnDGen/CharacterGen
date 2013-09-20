using System;
using Moq;
using NPCGen.Core.Data.Alignments;
using NPCGen.Core.Data.Races;
using NPCGen.Core.Generation.Providers.Interfaces;
using NPCGen.Core.Generation.Randomizers.Races.BaseRaces;
using NUnit.Framework;

namespace NPCGen.Tests.Generation.Randomizers.Races.BaseRaces
{
    [TestFixture]
    public class BaseRaceRandomizerTests
    {
        protected Mock<IPercentileResultProvider> mockPercentileResultProvider;
        protected IBaseRaceRandomizer randomizer;

        [SetUp]
        public void Setup()
        {
            mockPercentileResultProvider = new Mock<IPercentileResultProvider>();
        }

        protected void AssertBaseRaceIsAllowed(String baseRace)
        {
            var controlCase = RaceConstants.BaseRaces.ForestGnome;
            if (baseRace == controlCase)
                controlCase = RaceConstants.BaseRaces.Human;

            var result = GetResult(baseRace, controlCase);
            Assert.That(result, Is.EqualTo(baseRace));
        }

        protected void AssertBaseRaceIsNotAllowed(String baseRace)
        {
            var controlCase = RaceConstants.BaseRaces.ForestGnome;
            var result = GetResult(baseRace, controlCase);
            Assert.That(result, Is.EqualTo(controlCase));
        }

        private String GetResult(String baseRace, String controlCase)
        {
            mockPercentileResultProvider.SetupSequence(p => p.GetPercentileResult(It.IsAny<String>()))
                .Returns(baseRace)
                .Returns(controlCase);

            return randomizer.Randomize(new Alignment(), String.Empty);
        }
    }
}