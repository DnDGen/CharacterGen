using NPCGen.Core.Data.Alignments;
using NPCGen.Core.Data.CharacterClasses;
using NPCGen.Core.Generation.Randomizers.ClassNames;
using NUnit.Framework;

namespace NPCGen.Tests.Generation.Randomizers.ClassNames
{
    [TestFixture]
    public class HealerClassNameRandomizerTests : ClassNameRandomizerTests
    {
        [SetUp]
        public void Setup()
        {
            randomizer = new HealerClassNameRandomizer(mockPercentileResultProvider.Object);
            controlCase = CharacterClassConstants.Cleric;
        }

        [Test]
        public void FighterNeverAllowed()
        {
            AssertClassIsNotAllowed(CharacterClassConstants.Fighter);
        }

        [Test]
        public void ClericAlwaysAllowed()
        {
            AssertControlIsAllowed(CharacterClassConstants.Ranger);
        }

        [Test]
        public void RangerAlwaysAllowed()
        {
            AssertClassIsAllowed(CharacterClassConstants.Ranger);
        }

        [Test]
        public void SorcererNeverAllowed()
        {
            AssertClassIsNotAllowed(CharacterClassConstants.Sorcerer);
        }

        [Test]
        public void RogueNeverAllowed()
        {
            AssertClassIsNotAllowed(CharacterClassConstants.Rogue);
        }

        [Test]
        public void WizardNeverAllowed()
        {
            AssertClassIsNotAllowed(CharacterClassConstants.Wizard);
        }

        [Test]
        public void BarbarianNeverAllowed()
        {
            alignment.Lawfulness = AlignmentConstants.Chaotic;
            AssertClassIsNotAllowed(CharacterClassConstants.Barbarian);
        }

        [Test]
        public void BardNotAllowedIfAlignmentIsLawful()
        {
            alignment.Lawfulness = AlignmentConstants.Lawful;
            AssertClassIsNotAllowed(CharacterClassConstants.Bard);
        }

        [Test]
        public void BardAllowedIfAlignmentIsNeutral()
        {
            alignment.Lawfulness = AlignmentConstants.Neutral;
            AssertClassIsAllowed(CharacterClassConstants.Bard);
        }

        [Test]
        public void BardAllowedIfAlignmentIsChaotic()
        {
            alignment.Lawfulness = AlignmentConstants.Chaotic;
            AssertClassIsAllowed(CharacterClassConstants.Bard);
        }

        [Test]
        public void DruidNotAllowedIfAlignmentIsLawfulGood()
        {
            alignment.Lawfulness = AlignmentConstants.Lawful;
            alignment.Goodness = AlignmentConstants.Good;
            AssertClassIsNotAllowed(CharacterClassConstants.Druid);
        }

        [Test]
        public void DruidNotAllowedIfAlignmentIsLawfulEvil()
        {
            alignment.Lawfulness = AlignmentConstants.Lawful;
            alignment.Goodness = AlignmentConstants.Evil;
            AssertClassIsNotAllowed(CharacterClassConstants.Druid);
        }

        [Test]
        public void DruidNotAllowedIfAlignmentIsChaoticGood()
        {
            alignment.Lawfulness = AlignmentConstants.Chaotic;
            alignment.Goodness = AlignmentConstants.Good;
            AssertClassIsNotAllowed(CharacterClassConstants.Druid);
        }

        [Test]
        public void DruidNotAllowedIfAlignmentIsChaoticEvil()
        {
            alignment.Lawfulness = AlignmentConstants.Chaotic;
            alignment.Goodness = AlignmentConstants.Evil;
            AssertClassIsNotAllowed(CharacterClassConstants.Druid);
        }

        [Test]
        public void DruidAllowedIfLawfulnessIsNeutral()
        {
            alignment.Lawfulness = AlignmentConstants.Neutral;
            alignment.Goodness = AlignmentConstants.Good;
            AssertClassIsAllowed(CharacterClassConstants.Druid);
        }

        [Test]
        public void DruidAllowedIfGoodnessIsNeutral()
        {
            alignment.Lawfulness = AlignmentConstants.Lawful;
            alignment.Goodness = AlignmentConstants.Neutral;
            AssertClassIsAllowed(CharacterClassConstants.Druid);
        }

        [Test]
        public void MonkNeverAllowed()
        {
            alignment.Lawfulness = AlignmentConstants.Lawful;
            AssertClassIsNotAllowed(CharacterClassConstants.Monk);
        }

        [Test]
        public void PaladinNotAllowedIfAlignmentIsNotLawful()
        {
            alignment.Lawfulness = AlignmentConstants.Neutral;
            alignment.Goodness = AlignmentConstants.Good;
            AssertClassIsNotAllowed(CharacterClassConstants.Paladin);
        }

        [Test]
        public void PaladinNotAllowedIfAlignmentIsNotGood()
        {
            alignment.Goodness = AlignmentConstants.Neutral;
            alignment.Lawfulness = AlignmentConstants.Lawful;
            AssertClassIsNotAllowed(CharacterClassConstants.Paladin);
        }

        [Test]
        public void PaladinAllowedIfAlignmentIsLawfulGood()
        {
            alignment.Goodness = AlignmentConstants.Good;
            alignment.Lawfulness = AlignmentConstants.Lawful;
            AssertClassIsAllowed(CharacterClassConstants.Paladin);
        }
    }
}