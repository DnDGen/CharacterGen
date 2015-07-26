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
        protected IBaseRaceRandomizer randomizer;
        protected abstract IEnumerable<String> baseRaceIds { get; }

        [SetUp]
        public void BaseRaceRandomizerSetup()
        {
            mockPercentileResultSelector.Setup(p => p.SelectAllFrom(It.IsAny<String>())).Returns(baseRaceIds);

            foreach (var baseRace in baseRaceIds)
                adjustments.Add(baseRace, 0);
        }
    }
}