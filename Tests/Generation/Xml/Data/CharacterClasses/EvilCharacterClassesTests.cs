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
        public void EvilBarbarianPercentile()
        {
            AssertContentIsInRange(CharacterClassConstants.Barbarian, 1, 10);
        }

        [Test]
        public void EvilBardPercentile()
        {
            AssertContentIsInRange(CharacterClassConstants.Bard, 11, 15);
        }

        [Test]
        public void EvilClericPercentile()
        {
            AssertContentIsInRange(CharacterClassConstants.Cleric, 16, 35);
        }

        [Test]
        public void EvilDruidPercentile()
        {
            AssertContentIsInRange(CharacterClassConstants.Druid, 36, 40);
        }

        [Test]
        public void EvilFighterPercentile()
        {
            AssertContentIsInRange(CharacterClassConstants.Fighter, 41, 50);
        }

        [Test]
        public void EvilMonkPercentile()
        {
            AssertContentIsInRange(CharacterClassConstants.Monk, 51, 55);
        }

        [Test]
        public void EvilRangerPercentile()
        {
            AssertContentIsInRange(CharacterClassConstants.Ranger, 56, 60);
        }

        [Test]
        public void EvilRoguePercentile()
        {
            AssertContentIsInRange(CharacterClassConstants.Rogue, 61, 80);
        }

        [Test]
        public void EvilSorcererPercentile()
        {
            AssertContentIsInRange(CharacterClassConstants.Sorcerer, 81, 85);
        }

        [Test]
        public void EvilWizardPercentile()
        {
            AssertContentIsInRange(CharacterClassConstants.Wizard, 86, 100);
        }
    }
}