using System;
using System.Collections.Generic;
using Moq;
using NPCGen.Core.Data.Races;
using NPCGen.Core.Generation.Randomizers.Races.Interfaces;
using NUnit.Framework;

namespace NPCGen.Tests.Generation.Randomizers.Races.BaseRaces
{
    [TestFixture]
    public abstract class BaseRaceRandomizerTests : RaceRandomizerTests
    {
        protected IBaseRaceRandomizer randomizer;

        [SetUp]
        public void Setup()
        {
            mockPercentileResultProvider.Setup(p => p.GetAllResults(It.IsAny<String>())).Returns(RaceConstants.BaseRaces.GetBaseRaces());
        }

        protected override IEnumerable<String> GetResults()
        {
            return randomizer.GetAllPossibleResults(String.Empty, String.Empty);
        }
    }
}