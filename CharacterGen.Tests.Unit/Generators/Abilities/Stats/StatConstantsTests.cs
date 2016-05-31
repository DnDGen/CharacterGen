using CharacterGen.Abilities.Stats;
using NUnit.Framework;
using System;

namespace CharacterGen.Tests.Unit.Common.Abilities.Stats
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
    }
}