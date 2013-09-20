using NPCGen.Core.Data.Alignments;
using NPCGen.Core.Data.CharacterClasses;
using NPCGen.Core.Generation.Randomizers.ClassNames;
using NUnit.Framework;

namespace NPCGen.Tests.Generation.Randomizers.ClassNames
{
    [TestFixture]
    public class MageClassNameRandomizerTests : ClassNameRandomizerTests
    {
        [SetUp]
        public void Setup()
        {
            randomizer = new MageClassNameRandomizer(mockPercentileResultProvider.Object);
            controlCase = CharacterClassConstants.Wizard;
        }

        [Test]
        public void FighterNeverAllowed()
        {
            AssertClassIsNotAllowed(CharacterClassConstants.Fighter);
        }

        [Test]
        public void ClericNeverAllowed()
        {
            AssertClassIsNotAllowed(CharacterClassConstants.Cleric);
        }

        [Test]
        public void RangerAlwaysAllowed()
        {
            AssertClassIsAllowed(CharacterClassConstants.Ranger);
        }

        [Test]
        public void SorcererAlwaysAllowed()
        {
            AssertClassIsAllowed(CharacterClassConstants.Sorcerer);
        }

        [Test]
        public void RogueNeverAllowed()
        {
            AssertClassIsNotAllowed(CharacterClassConstants.Rogue);
        }

        [Test]
        public void WizardAlwaysAllowed()
        {
            AssertControlIsAllowed(CharacterClassConstants.Sorcerer);
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
        public void DruidNeverAllowed()
        {
            alignment.Lawfulness = AlignmentConstants.Neutral;
            AssertClassIsNotAllowed(CharacterClassConstants.Druid);
        }

        [Test]
        public void MonkNeverAllowed()
        {
            alignment.Lawfulness = AlignmentConstants.Lawful;
            AssertClassIsNotAllowed(CharacterClassConstants.Monk);
        }

        [Test]
        public void PaladinNeverAllowed()
        {
            alignment.Lawfulness = AlignmentConstants.Lawful;
            alignment.Goodness = AlignmentConstants.Good;
            AssertClassIsNotAllowed(CharacterClassConstants.Paladin);
        }
    }
}