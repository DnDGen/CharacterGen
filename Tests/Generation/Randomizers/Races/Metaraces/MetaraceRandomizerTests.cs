using System;
using System.Collections.Generic;
using Moq;
using NPCGen.Core.Data.CharacterClasses;
using NPCGen.Core.Data.Races;
using NPCGen.Core.Generation.Randomizers.Races.Interfaces;
using NUnit.Framework;

namespace NPCGen.Tests.Generation.Randomizers.Races.Metaraces
{
    [TestFixture]
    public abstract class MetaraceRandomizerTests : RaceRandomizerTests
    {
        protected IMetaraceRandomizer randomizer;

        [SetUp]
        public void Setup()
        {
            mockPercentileResultProvider.Setup(p => p.GetAllResults(It.IsAny<String>())).Returns(RaceConstants.Metaraces.GetMetaraces());
        }

        protected override IEnumerable<String> GetResults()
        {
            return randomizer.GetAllPossibleResults(String.Empty, new CharacterClass());
        }
    }
}