using CharacterGen.Generators.Randomizers.Races;
using NUnit.Framework;
using System;

namespace CharacterGen.Tests.Unit.Generators.Randomizers.Races.Metaraces
{
    [TestFixture]
    public class MetaraceRandomizerTypeConstantsTests
    {
        [TestCase(MetaraceRandomizerTypeConstants.Any, "Any")]
        [TestCase(MetaraceRandomizerTypeConstants.Evil, "Evil")]
        [TestCase(MetaraceRandomizerTypeConstants.Good, "Good")]
        [TestCase(MetaraceRandomizerTypeConstants.Neutral, "Neutral")]
        [TestCase(MetaraceRandomizerTypeConstants.NonEvil, "Non-evil")]
        [TestCase(MetaraceRandomizerTypeConstants.NonGood, "Non-good")]
        [TestCase(MetaraceRandomizerTypeConstants.NonNeutral, "Non-neutral")]
        [TestCase(MetaraceRandomizerTypeConstants.Genetic, "Genetic")]
        [TestCase(MetaraceRandomizerTypeConstants.Lycanthrope, "Lycanthrope")]
        [TestCase(MetaraceRandomizerTypeConstants.None, "None")]
        public void Constant(String constant, String value)
        {
            Assert.That(constant, Is.EqualTo(value));
        }
    }
}
