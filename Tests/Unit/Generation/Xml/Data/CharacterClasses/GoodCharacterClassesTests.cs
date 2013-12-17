using NPCGen.Core.Data.CharacterClasses;
using NUnit.Framework;

namespace NPCGen.Tests.Unit.Generation.Xml.Data.CharacterClasses
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
        public void GoodBarbarianPercentile()
        {
            AssertContent(CharacterClassConstants.Barbarian, 1, 5);
        }

        [Test]
        public void GoodBardPercentile()
        {
            AssertContent(CharacterClassConstants.Bard, 6, 10);
        }

        [Test]
        public void GoodClericPercentile()
        {
            AssertContent(CharacterClassConstants.Cleric, 11, 30);
        }

        [Test]
        public void GoodDruidPercentile()
        {
            AssertContent(CharacterClassConstants.Druid, 31, 35);
        }

        [Test]
        public void GoodFighterPercentile()
        {
            AssertContent(CharacterClassConstants.Fighter, 36, 45);
        }

        [Test]
        public void GoodMonkPercentile()
        {
            AssertContent(CharacterClassConstants.Monk, 46, 50);
        }

        [Test]
        public void PaladinPercentile()
        {
            AssertContent(CharacterClassConstants.Paladin, 51, 55);
        }

        [Test]
        public void GoodRangerPercentile()
        {
            AssertContent(CharacterClassConstants.Ranger, 56, 65);
        }

        [Test]
        public void GoodRoguePercentile()
        {
            AssertContent(CharacterClassConstants.Rogue, 66, 75);
        }

        [Test]
        public void GoodSorcererPercentile()
        {
            AssertContent(CharacterClassConstants.Sorcerer, 76, 80);
        }

        [Test]
        public void GoodWizardPercentile()
        {
            AssertContent(CharacterClassConstants.Wizard, 81, 100);
        }
    }
}