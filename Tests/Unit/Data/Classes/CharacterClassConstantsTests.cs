using System.Linq;
using NPCGen.Core.Data.CharacterClasses;
using NUnit.Framework;

namespace NPCGen.Tests.Unit.Data.Classes
{
    [TestFixture]
    public class CharacterClassConstantsTests
    {
        [Test]
        public void BarbarianConstant()
        {
            Assert.That(CharacterClassConstants.Barbarian, Is.EqualTo("Barbarian"));
        }

        [Test]
        public void BardConstant()
        {
            Assert.That(CharacterClassConstants.Bard, Is.EqualTo("Bard"));
        }

        [Test]
        public void ClericConstant()
        {
            Assert.That(CharacterClassConstants.Cleric, Is.EqualTo("Cleric"));
        }

        [Test]
        public void DruidConstant()
        {
            Assert.That(CharacterClassConstants.Druid, Is.EqualTo("Druid"));
        }

        [Test]
        public void FighterConstant()
        {
            Assert.That(CharacterClassConstants.Fighter, Is.EqualTo("Fighter"));
        }

        [Test]
        public void MonkConstant()
        {
            Assert.That(CharacterClassConstants.Monk, Is.EqualTo("Monk"));
        }

        [Test]
        public void PaladinConstant()
        {
            Assert.That(CharacterClassConstants.Paladin, Is.EqualTo("Paladin"));
        }

        [Test]
        public void RangerConstant()
        {
            Assert.That(CharacterClassConstants.Ranger, Is.EqualTo("Ranger"));
        }

        [Test]
        public void RogueConstant()
        {
            Assert.That(CharacterClassConstants.Rogue, Is.EqualTo("Rogue"));
        }

        [Test]
        public void SorcererConstant()
        {
            Assert.That(CharacterClassConstants.Sorcerer, Is.EqualTo("Sorcerer"));
        }

        [Test]
        public void WizardConstant()
        {
            Assert.That(CharacterClassConstants.Wizard, Is.EqualTo("Wizard"));
        }

        [Test]
        public void ClassNames()
        {
            var classNames = CharacterClassConstants.GetClassNames();

            Assert.That(classNames.Contains(CharacterClassConstants.Barbarian), Is.True);
            Assert.That(classNames.Contains(CharacterClassConstants.Bard), Is.True);
            Assert.That(classNames.Contains(CharacterClassConstants.Cleric), Is.True);
            Assert.That(classNames.Contains(CharacterClassConstants.Druid), Is.True);
            Assert.That(classNames.Contains(CharacterClassConstants.Fighter), Is.True);
            Assert.That(classNames.Contains(CharacterClassConstants.Monk), Is.True);
            Assert.That(classNames.Contains(CharacterClassConstants.Paladin), Is.True);
            Assert.That(classNames.Contains(CharacterClassConstants.Ranger), Is.True);
            Assert.That(classNames.Contains(CharacterClassConstants.Rogue), Is.True);
            Assert.That(classNames.Contains(CharacterClassConstants.Sorcerer), Is.True);
            Assert.That(classNames.Contains(CharacterClassConstants.Wizard), Is.True);
            Assert.That(classNames.Count(), Is.EqualTo(11));
        }
    }
}