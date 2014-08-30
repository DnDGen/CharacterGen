using System;
using System.Collections.Generic;
using Moq;
using NPCGen.Common.CharacterClasses;
using NPCGen.Common.Races;
using NPCGen.Generators.Interfaces.Randomizers.Races;
using NUnit.Framework;

namespace NPCGen.Tests.Unit.Generators.Randomizers.Races.BaseRaces
{
    [TestFixture]
    public abstract class BaseRaceRandomizerTests : RaceRandomizerTests
    {
        protected IBaseRaceRandomizer randomizer;

        [SetUp]
        public void BaseRaceRandomizerSetup()
        {
            var baseRaces = RaceConstants.BaseRaces.GetBaseRaces();
            mockPercentileResultSelector.Setup(p => p.SelectAllFrom(It.IsAny<String>())).Returns(baseRaces);

            foreach (var baseRace in baseRaces)
                adjustments.Add(baseRace, 0);
        }

        protected override IEnumerable<String> GetResults()
        {
            return randomizer.GetAllPossibleResults(String.Empty, characterClass);
        }
    }
}