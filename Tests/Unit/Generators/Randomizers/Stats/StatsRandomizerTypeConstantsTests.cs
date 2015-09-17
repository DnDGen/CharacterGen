using CharacterGen.Generators.Randomizers.Stats;
using NUnit.Framework;
using System;

namespace CharacterGen.Tests.Unit.Generators.Randomizers.Stats
{
    [TestFixture]
    public class StatsRandomizerTypeConstantsTests
    {
        [TestCase(StatsRandomizerTypeConstants.Average, "Average")]
        [TestCase(StatsRandomizerTypeConstants.BestOfFour, "Best of four")]
        [TestCase(StatsRandomizerTypeConstants.Good, "Good")]
        [TestCase(StatsRandomizerTypeConstants.Heroic, "Heroic")]
        [TestCase(StatsRandomizerTypeConstants.OnesAsSixes, "Ones as sixes")]
        [TestCase(StatsRandomizerTypeConstants.Poor, "Poor")]
        [TestCase(StatsRandomizerTypeConstants.Raw, "Raw")]
        [TestCase(StatsRandomizerTypeConstants.TwoTenSidedDice, "2d10")]
        public void Constant(String constant, String value)
        {
            Assert.That(constant, Is.EqualTo(value));
        }
    }
}
