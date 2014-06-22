using System;
using System.Linq;
using NPCGen.Common.CharacterClasses;
using NUnit.Framework;

namespace NPCGen.Tests.Unit.Common.Classes
{
    [TestFixture]
    public class CharacterClassConstantsTests
    {
        [TestCase(CharacterClassConstants.Barbarian, "Barbarian")]
        [TestCase(CharacterClassConstants.Bard, "Bard")]
        [TestCase(CharacterClassConstants.Cleric, "Cleric")]
        [TestCase(CharacterClassConstants.Druid, "Druid")]
        [TestCase(CharacterClassConstants.Fighter, "Fighter")]
        [TestCase(CharacterClassConstants.Monk, "Monk")]
        [TestCase(CharacterClassConstants.Paladin, "Paladin")]
        [TestCase(CharacterClassConstants.Ranger, "Ranger")]
        [TestCase(CharacterClassConstants.Rogue, "Rogue")]
        [TestCase(CharacterClassConstants.Sorcerer, "Sorcerer")]
        [TestCase(CharacterClassConstants.Wizard, "Wizard")]
        public void Constant(String constant, String value)
        {
            Assert.That(constant, Is.EqualTo(value));
        }

        [Test]
        public void ClassNames()
        {
            var classNames = CharacterClassConstants.GetClassNames();

            Assert.That(classNames, Contains.Item(CharacterClassConstants.Barbarian));
            Assert.That(classNames, Contains.Item(CharacterClassConstants.Bard));
            Assert.That(classNames, Contains.Item(CharacterClassConstants.Cleric));
            Assert.That(classNames, Contains.Item(CharacterClassConstants.Druid));
            Assert.That(classNames, Contains.Item(CharacterClassConstants.Fighter));
            Assert.That(classNames, Contains.Item(CharacterClassConstants.Monk));
            Assert.That(classNames, Contains.Item(CharacterClassConstants.Paladin));
            Assert.That(classNames, Contains.Item(CharacterClassConstants.Ranger));
            Assert.That(classNames, Contains.Item(CharacterClassConstants.Rogue));
            Assert.That(classNames, Contains.Item(CharacterClassConstants.Sorcerer));
            Assert.That(classNames, Contains.Item(CharacterClassConstants.Wizard));
            Assert.That(classNames.Count(), Is.EqualTo(11));
        }
    }
}