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
        public void NeutralBarbarianPercentile()
        {
            AssertContentIsInRange(CharacterClassConstants.Barbarian, 1, 5);
        }

        [Test]
        public void NeutralBardPercentile()
        {
            AssertContentIsInRange(CharacterClassConstants.Bard, 6, 10);
        }

        [Test]
        public void NeutralClericPercentile()
        {
            AssertContentIsInRange(CharacterClassConstants.Cleric, 11, 15);
        }

        [Test]
        public void NeutralDruidPercentile()
        {
            AssertContentIsInRange(CharacterClassConstants.Druid, 16, 25);
        }

        [Test]
        public void NeutralFighterPercentile()
        {
            AssertContentIsInRange(CharacterClassConstants.Fighter, 26, 45);
        }

        [Test]
        public void NeutralMonkPercentile()
        {
            AssertContentIsInRange(CharacterClassConstants.Monk, 46, 50);
        }

        [Test]
        public void NeutralRangerPercentile()
        {
            AssertContentIsInRange(CharacterClassConstants.Ranger, 51, 55);
        }

        [Test]
        public void NeutralRoguePercentile()
        {
            AssertContentIsInRange(CharacterClassConstants.Rogue, 56, 75);
        }

        [Test]
        public void NeutralSorcererPercentile()
        {
            AssertContentIsInRange(CharacterClassConstants.Sorcerer, 76, 80);
        }

        [Test]
        public void NeutralWizardPercentile()
        {
            AssertContentIsInRange(CharacterClassConstants.Wizard, 81, 100);
        }
    }
}