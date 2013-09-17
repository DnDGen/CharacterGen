using NPCGen.Core.Data.CharacterClasses;
using NUnit.Framework;

namespace NPCGen.Tests.Generation.Xml.Data.CharacterClasses
{
    [TestFixture]
    public class EvilCharacterClassesTests : PercentileTests
    {
        [SetUp]
        public void Setup()
        {
            tableName = "EvilCharacterClasses";
        }

        [Test]
        public void Barbarian()
        {
            AssertContentIsInRange(CharacterClassConstants.Barbarian, 1, 10);
        }

        [Test]
        public void Bard()
        {
            AssertContentIsInRange(CharacterClassConstants.Bard, 11, 15);
        }

        [Test]
        public void Cleric()
        {
            AssertContentIsInRange(CharacterClassConstants.Cleric, 16, 35);
        }

        [Test]
        public void Druid()
        {
            AssertContentIsInRange(CharacterClassConstants.Druid, 36, 40);
        }

        [Test]
        public void Fighter()
        {
            AssertContentIsInRange(CharacterClassConstants.Fighter, 41, 50);
        }

        [Test]
        public void Monk()
        {
            AssertContentIsInRange(CharacterClassConstants.Monk, 51, 55);
        }

        [Test]
        public void Ranger()
        {
            AssertContentIsInRange(CharacterClassConstants.Ranger, 56, 60);
        }

        [Test]
        public void Rogue()
        {
            AssertContentIsInRange(CharacterClassConstants.Rogue, 61, 80);
        }

        [Test]
        public void Sorcerer()
        {
            AssertContentIsInRange(CharacterClassConstants.Sorcerer, 81, 85);
        }

        [Test]
        public void Wizard()
        {
            AssertContentIsInRange(CharacterClassConstants.Wizard, 86, 100);
        }
    }
}