using NPCGen.Common;
using NUnit.Framework;

namespace NPCGen.Tests.Unit.Common
{
    [TestFixture]
    public class CharacterTests
    {
        private Character character;

        [SetUp]
        public void Setup()
        {
            character = new Character();
        }

        [Test]
        public void CharacterInitialized()
        {
            Assert.That(character.Alignment, Is.Not.Null);
            Assert.That(character.Armor, Is.Not.Null);
            Assert.That(character.ArmorClass, Is.Not.Null);
            Assert.That(character.Class, Is.Not.Null);
            Assert.That(character.Familiar, Is.Not.Null);
            Assert.That(character.Feats, Is.Empty);
            Assert.That(character.HitPoints, Is.EqualTo(0));
            Assert.That(character.InterestingTrait, Is.Empty);
            Assert.That(character.Languages, Is.Empty);
            Assert.That(character.OffHand, Is.Not.Null);
            Assert.That(character.PrimaryHand, Is.Not.Null);
            Assert.That(character.Race, Is.Not.Null);
            Assert.That(character.Skills, Is.Empty);
            Assert.That(character.Spells, Is.Empty);
            Assert.That(character.Stats, Is.Empty);
            Assert.That(character.Treasure, Is.Not.Null);
            Assert.That(character.SavingThrows, Is.Not.Null);
        }
    }
}