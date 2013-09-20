using NPCGen.Core.Data.Alignments;
using NPCGen.Core.Data.CharacterClasses;
using NPCGen.Core.Generation.Randomizers.ClassNames;
using NUnit.Framework;

namespace NPCGen.Tests.Generation.Randomizers.ClassNames
{
    [TestFixture]
    public class StealthClassNameRandomizerTests : ClassNameRandomizerTests
    {
        [SetUp]
        public void Setup()
        {
            randomizer = new StealthClassNameRandomizer(mockPercentileResultProvider.Object);
            controlCase = CharacterClassConstants.Rogue;
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
        public void SorcererNeverAllowed()
        {
            AssertClassIsNotAllowed(CharacterClassConstants.Sorcerer);
        }

        [Test]
        public void RogueAlwaysAllowed()
        {
            AssertControlIsAllowed(CharacterClassConstants.Ranger);
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