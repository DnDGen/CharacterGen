using System;
using System.Collections.Generic;
using Moq;
using CharacterGen.Generators.Randomizers.Races;
using NUnit.Framework;

namespace CharacterGen.Tests.Unit.Generators.Randomizers.Races.BaseRaces
{
    [TestFixture]
    public abstract class BaseRaceRandomizerTests : RaceRandomizerTests
    {
        protected RaceRandomizer randomizer;
        protected abstract IEnumerable<String> baseRaces { get; }

        [SetUp]
        public void BaseRaceRandomizerSetup()
        {
            mockPercentileResultSelector.Setup(p => p.SelectAllFrom(It.IsAny<String>())).Returns(baseRaces);

            foreach (var baseRace in baseRaces)
                adjustments.Add(baseRace, 0);
        }
    }
}