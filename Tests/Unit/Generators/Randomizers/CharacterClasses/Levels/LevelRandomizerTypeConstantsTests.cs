using CharacterGen.Generators.Randomizers.CharacterClasses;
using NUnit.Framework;
using System;

namespace CharacterGen.Tests.Unit.Generators.Randomizers.CharacterClasses.Levels
{
    [TestFixture]
    public class LevelRandomizerTypeConstantsTests
    {
        [TestCase(LevelRandomizerTypeConstants.Any, "Any")]
        [TestCase(LevelRandomizerTypeConstants.High, "High")]
        [TestCase(LevelRandomizerTypeConstants.Low, "Low")]
        [TestCase(LevelRandomizerTypeConstants.Medium, "Medium")]
        [TestCase(LevelRandomizerTypeConstants.VeryHigh, "Very high")]
        public void Constant(String constant, String value)
        {
            Assert.That(constant, Is.EqualTo(value));
        }
    }
}
