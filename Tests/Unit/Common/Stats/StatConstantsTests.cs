using System;
using System.Linq;
using NPCGen.Common.Abilities;
using NUnit.Framework;

namespace NPCGen.Tests.Unit.Common.Stats
{
    [TestFixture]
    public class StatConstantsTests
    {
        [TestCase(StatConstants.Strength, "Strength")]
        [TestCase(StatConstants.Charisma, "Charisma")]
        [TestCase(StatConstants.Constitution, "Constitution")]
        [TestCase(StatConstants.Dexterity, "Dexterity")]
        [TestCase(StatConstants.Intelligence, "Intelligence")]
        [TestCase(StatConstants.Wisdom, "Wisdom")]
        public void Constant(String constant, String value)
        {
            Assert.That(constant, Is.EqualTo(value));
        }

        [Test]
        public void AllStatConstants()
        {
            var stats = StatConstants.GetStats();

            Assert.That(stats, Contains.Item(StatConstants.Charisma));
            Assert.That(stats, Contains.Item(StatConstants.Constitution));
            Assert.That(stats, Contains.Item(StatConstants.Dexterity));
            Assert.That(stats, Contains.Item(StatConstants.Intelligence));
            Assert.That(stats, Contains.Item(StatConstants.Strength));
            Assert.That(stats, Contains.Item(StatConstants.Wisdom));
            Assert.That(stats.Count(), Is.EqualTo(6));
        }
    }
}