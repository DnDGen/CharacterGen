using NPCGen.Core.Data.CharacterClasses;
using NUnit.Framework;

namespace NPCGen.Tests.Generation.Xml.Data.CharacterClasses
{
    [TestFixture]
    public class GoodCharacterClassesTests : PercentileTests
    {
        [SetUp]
        public void Setup()
        {
            tableName = "GoodCharacterClasses";
        }

        [Test]
        public void Barbarian()
        {
            AssertContentIsInRange(CharacterClassConstants.Barbarian, 1, 5);
        }

        [Test]
        public void Bard()
        {
            AssertContentIsInRange(CharacterClassConstants.Bard, 6, 10);
        }

        [Test]
        public void Cleric()
        {
            AssertContentIsInRange(CharacterClassConstants.Cleric, 11, 30);
        }

        [Test]
        public void Druid()
        {
            AssertContentIsInRange(CharacterClassConstants.Druid, 31, 35);
        }

        [Test]
        public void Fighter()
        {
            AssertContentIsInRange(CharacterClassConstants.Fighter, 36, 45);
        }

        [Test]
        public void Monk()
        {
            AssertContentIsInRange(CharacterClassConstants.Monk, 46, 50);
        }

        [Test]
        public void Paladin()
        {
            AssertContentIsInRange(CharacterClassConstants.Paladin, 51, 55);
        }

        [Test]
        public void Ranger()
        {
            AssertContentIsInRange(CharacterClassConstants.Ranger, 56, 65);
        }

        [Test]
        public void Rogue()
        {
            AssertContentIsInRange(CharacterClassConstants.Rogue, 66, 75);
        }

        [Test]
        public void Sorcerer()
        {
            AssertContentIsInRange(CharacterClassConstants.Sorcerer, 76, 80);
        }

        [Test]
        public void Wizard()
        {
            AssertContentIsInRange(CharacterClassConstants.Wizard, 81, 100);
        }
    }
}