using NPCGen.Core.Data.CharacterClasses;
using NUnit.Framework;

namespace NPCGen.Tests.Unit.Generation.Xml.Data.CharacterClasses
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
            AssertContent(CharacterClassConstants.Barbarian, 1, 10);
        }

        [Test]
        public void EvilBardPercentile()
        {
            AssertContent(CharacterClassConstants.Bard, 11, 15);
        }

        [Test]
        public void EvilClericPercentile()
        {
            AssertContent(CharacterClassConstants.Cleric, 16, 35);
        }

        [Test]
        public void EvilDruidPercentile()
        {
            AssertContent(CharacterClassConstants.Druid, 36, 40);
        }

        [Test]
        public void EvilFighterPercentile()
        {
            AssertContent(CharacterClassConstants.Fighter, 41, 50);
        }

        [Test]
        public void EvilMonkPercentile()
        {
            AssertContent(CharacterClassConstants.Monk, 51, 55);
        }

        [Test]
        public void EvilRangerPercentile()
        {
            AssertContent(CharacterClassConstants.Ranger, 56, 60);
        }

        [Test]
        public void EvilRoguePercentile()
        {
            AssertContent(CharacterClassConstants.Rogue, 61, 80);
        }

        [Test]
        public void EvilSorcererPercentile()
        {
            AssertContent(CharacterClassConstants.Sorcerer, 81, 85);
        }

        [Test]
        public void EvilWizardPercentile()
        {
            AssertContent(CharacterClassConstants.Wizard, 86, 100);
        }
    }
}