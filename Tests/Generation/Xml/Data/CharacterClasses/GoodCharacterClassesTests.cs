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
        public void GoodBarbarianPercentile()
        {
            AssertContentIsInRange(CharacterClassConstants.Barbarian, 1, 5);
        }

        [Test]
        public void GoodBardPercentile()
        {
            AssertContentIsInRange(CharacterClassConstants.Bard, 6, 10);
        }

        [Test]
        public void GoodClericPercentile()
        {
            AssertContentIsInRange(CharacterClassConstants.Cleric, 11, 30);
        }

        [Test]
        public void GoodDruidPercentile()
        {
            AssertContentIsInRange(CharacterClassConstants.Druid, 31, 35);
        }

        [Test]
        public void GoodFighterPercentile()
        {
            AssertContentIsInRange(CharacterClassConstants.Fighter, 36, 45);
        }

        [Test]
        public void GoodMonkPercentile()
        {
            AssertContentIsInRange(CharacterClassConstants.Monk, 46, 50);
        }

        [Test]
        public void PaladinPercentile()
        {
            AssertContentIsInRange(CharacterClassConstants.Paladin, 51, 55);
        }

        [Test]
        public void GoodRangerPercentile()
        {
            AssertContentIsInRange(CharacterClassConstants.Ranger, 56, 65);
        }

        [Test]
        public void GoodRoguePercentile()
        {
            AssertContentIsInRange(CharacterClassConstants.Rogue, 66, 75);
        }

        [Test]
        public void GoodSorcererPercentile()
        {
            AssertContentIsInRange(CharacterClassConstants.Sorcerer, 76, 80);
        }

        [Test]
        public void GoodWizardPercentile()
        {
            AssertContentIsInRange(CharacterClassConstants.Wizard, 81, 100);
        }
    }
}