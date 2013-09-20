using NPCGen.Core.Data.Alignments;
using NPCGen.Core.Data.CharacterClasses;
using NPCGen.Core.Generation.Randomizers.ClassNames;
using NUnit.Framework;

namespace NPCGen.Tests.Generation.Randomizers.ClassNames
{
    [TestFixture]
    public class NonSpellcasterClassNameRandomizerTests : ClassNameRandomizerTests
    {
        [SetUp]
        public void Setup()
        {
            randomizer = new NonSpellcasterClassNameRandomizer(mockPercentileResultProvider.Object);
            controlCase = CharacterClassConstants.Fighter;
        }

        [Test]
        public void FighterAlwaysAllowed()
        {
            AssertControlIsAllowed(CharacterClassConstants.Rogue);
        }

        [Test]
        public void ClericNeverAllowed()
        {
            AssertClassIsNotAllowed(CharacterClassConstants.Cleric);
        }

        [Test]
        public void RangerNeverAllowed()
        {
            AssertClassIsNotAllowed(CharacterClassConstants.Ranger);
        }

        [Test]
        public void SorcererNeverAllowed()
        {
            AssertClassIsNotAllowed(CharacterClassConstants.Sorcerer);
        }

        [Test]
        public void RogueAlwaysAllowed()
        {
            AssertClassIsAllowed(CharacterClassConstants.Rogue);
        }

        [Test]
        public void WizardNeverAllowed()
        {
            AssertClassIsNotAllowed(CharacterClassConstants.Wizard);
        }

        [Test]
        public void BarbarianNotAllowedIfAlignmentIsLawful()
        {
            alignment.Lawfulness = AlignmentConstants.Lawful;
            AssertClassIsNotAllowed(CharacterClassConstants.Barbarian);
        }

        [Test]
        public void BarbarianAllowedIfAlignmentIsNeutral()
        {
            alignment.Lawfulness = AlignmentConstants.Neutral;
            AssertClassIsAllowed(CharacterClassConstants.Barbarian);
        }

        [Test]
        public void BarbarianAllowedIfAlignmentIsChaotic()
        {
            alignment.Lawfulness = AlignmentConstants.Chaotic;
            AssertClassIsAllowed(CharacterClassConstants.Barbarian);
        }

        [Test]
        public void BardNeverAllowed()
        {
            alignment.Lawfulness = AlignmentConstants.Chaotic;
            AssertClassIsNotAllowed(CharacterClassConstants.Bard);
        }

        [Test]
        public void DruidNeverAllowed()
        {
            alignment.Lawfulness = AlignmentConstants.Neutral;
            AssertClassIsNotAllowed(CharacterClassConstants.Druid);
        }

        [Test]
        public void MonkNotAllowedIfAlignmentIsChaotic()
        {
            alignment.Lawfulness = AlignmentConstants.Chaotic;
            AssertClassIsNotAllowed(CharacterClassConstants.Monk);
        }

        [Test]
        public void MonkNotAllowedIfAlignmentIsNeutral()
        {
            alignment.Lawfulness = AlignmentConstants.Neutral;
            AssertClassIsNotAllowed(CharacterClassConstants.Monk);
        }

        [Test]
        public void MonkAllowedIfAlignmentIsLawful()
        {
            alignment.Lawfulness = AlignmentConstants.Lawful;
            AssertClassIsAllowed(CharacterClassConstants.Monk);
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