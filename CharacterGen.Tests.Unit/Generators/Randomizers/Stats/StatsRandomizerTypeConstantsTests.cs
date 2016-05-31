using CharacterGen.Randomizers.Stats;
using NUnit.Framework;

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
        public void Constant(string constant, string value)
        {
            Assert.That(constant, Is.EqualTo(value));
        }
    }
}
