using NUnit.Framework;
using System.Collections.Generic;

namespace CharacterGen.Tests.Integration.Stress.Randomizers.Races.BaseRaces
{
    [TestFixture]
    public abstract class BaseRaceRandomizerTests : StressTests
    {
        protected abstract IEnumerable<string> allowedBaseRaces { get; }

        protected void AssertBaseRace()
        {
            var alignment = GetNewAlignment();
            var characterClass = GetNewCharacterClass(alignment);

            var baseRace = BaseRaceRandomizer.Randomize(alignment, characterClass);
            Assert.That(allowedBaseRaces, Contains.Item(baseRace));
        }
    }
}