using CharacterGen.Generators.Randomizers.Races;
using NUnit.Framework;
using System;

namespace CharacterGen.Tests.Unit.Generators.Randomizers.Races.BaseRaces
{
    [TestFixture]
    public class BaseRaceRandomizerTypeConstantsTests
    {
        [TestCase(BaseRaceRandomizerTypeConstants.Animal, "Animal")]
        [TestCase(BaseRaceRandomizerTypeConstants.Any, "Any")]
        [TestCase(BaseRaceRandomizerTypeConstants.Evil, "Evil")]
        [TestCase(BaseRaceRandomizerTypeConstants.Good, "Good")]
        [TestCase(BaseRaceRandomizerTypeConstants.Neutral, "Neutral")]
        [TestCase(BaseRaceRandomizerTypeConstants.NonEvil, "Non-evil")]
        [TestCase(BaseRaceRandomizerTypeConstants.NonGood, "Non-good")]
        [TestCase(BaseRaceRandomizerTypeConstants.NonNeutral, "Non-neutral")]
        [TestCase(BaseRaceRandomizerTypeConstants.NonStandard, "Non-standard")]
        [TestCase(BaseRaceRandomizerTypeConstants.Standard, "Standard")]
        public void Constant(String constant, String value)
        {
            Assert.That(constant, Is.EqualTo(value));
        }
    }
}
