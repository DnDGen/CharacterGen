using NPCGen.Core.Data.CharacterClasses;
using NUnit.Framework;

namespace NPCGen.Tests.Generation.Xml.Data.CharacterClasses
{
    [TestFixture]
    public class NeutralCharacterClassesTests : PercentileTests
    {
        [SetUp]
        public void Setup()
        {
            tableName = "NeutralCharacterClasses";
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
            AssertContentIsInRange(CharacterClassConstants.Cleric, 11, 15);
        }

        [Test]
        public void Druid()
        {
            AssertContentIsInRange(CharacterClassConstants.Druid, 16, 25);
        }

        [Test]
        public void Fighter()
        {
            AssertContentIsInRange(CharacterClassConstants.Fighter, 26, 45);
        }

        [Test]
        public void Monk()
        {
            AssertContentIsInRange(CharacterClassConstants.Monk, 46, 50);
        }

        [Test]
        public void Ranger()
        {
            AssertContentIsInRange(CharacterClassConstants.Ranger, 51, 55);
        }

        [Test]
        public void Rogue()
        {
            AssertContentIsInRange(CharacterClassConstants.Rogue, 56, 75);
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