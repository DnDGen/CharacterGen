using CharacterGen.Randomizers.Races;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;

namespace CharacterGen.Tests.Unit.Generators.Randomizers.Races.BaseRaces
{
    [TestFixture]
    public abstract class BaseRaceRandomizerTests : RaceRandomizerTests
    {
        protected RaceRandomizer randomizer;
        protected abstract IEnumerable<string> baseRaces { get; }

        [SetUp]
        public void BaseRaceRandomizerSetup()
        {
            mockPercentileResultSelector.Setup(p => p.SelectAllFrom(It.IsAny<string>())).Returns(baseRaces);

            foreach (var baseRace in baseRaces)
                adjustments.Add(baseRace, 0);
        }
    }
}