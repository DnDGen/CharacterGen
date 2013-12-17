using NPCGen.Core.Data.CharacterClasses;
using NUnit.Framework;

namespace NPCGen.Tests.Unit.Generation.Xml.Data.CharacterClasses
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
        public void NeutralBarbarianPercentile()
        {
            AssertContent(CharacterClassConstants.Barbarian, 1, 5);
        }

        [Test]
        public void NeutralBardPercentile()
        {
            AssertContent(CharacterClassConstants.Bard, 6, 10);
        }

        [Test]
        public void NeutralClericPercentile()
        {
            AssertContent(CharacterClassConstants.Cleric, 11, 15);
        }

        [Test]
        public void NeutralDruidPercentile()
        {
            AssertContent(CharacterClassConstants.Druid, 16, 25);
        }

        [Test]
        public void NeutralFighterPercentile()
        {
            AssertContent(CharacterClassConstants.Fighter, 26, 45);
        }

        [Test]
        public void NeutralMonkPercentile()
        {
            AssertContent(CharacterClassConstants.Monk, 46, 50);
        }

        [Test]
        public void NeutralRangerPercentile()
        {
            AssertContent(CharacterClassConstants.Ranger, 51, 55);
        }

        [Test]
        public void NeutralRoguePercentile()
        {
            AssertContent(CharacterClassConstants.Rogue, 56, 75);
        }

        [Test]
        public void NeutralSorcererPercentile()
        {
            AssertContent(CharacterClassConstants.Sorcerer, 76, 80);
        }

        [Test]
        public void NeutralWizardPercentile()
        {
            AssertContent(CharacterClassConstants.Wizard, 81, 100);
        }
    }
}