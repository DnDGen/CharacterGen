using NPCGen.Core.Data.CharacterClasses;
using NPCGen.Core.Generation.Randomizers.CharacterClasses.ClassNames;
using NUnit.Framework;

namespace NPCGen.Tests.Unit.Generation.Randomizers.CharacterClasses.ClassNames
{
    [TestFixture]
    public class NonSpellcasterClassNameRandomizerTests : ClassNameRandomizerTests
    {
        [SetUp]
        public void Setup()
        {
            randomizer = new NonSpellcasterClassNameRandomizer(mockPercentileResultProvider.Object);
        }

        [Test]
        public void FighterAlwaysAllowed()
        {
            AssertClassIsAlwaysAllowed(CharacterClassConstants.Fighter);
        }

        [Test]
        public void ClericNeverAllowed()
        {
            AssertClassIsNeverAllowed(CharacterClassConstants.Cleric);
        }

        [Test]
        public void RangerNeverAllowed()
        {
            AssertClassIsNeverAllowed(CharacterClassConstants.Ranger);
        }

        [Test]
        public void SorcererNeverAllowed()
        {
            AssertClassIsNeverAllowed(CharacterClassConstants.Sorcerer);
        }

        [Test]
        public void RogueAlwaysAllowed()
        {
            AssertClassIsAlwaysAllowed(CharacterClassConstants.Rogue);
        }

        [Test]
        public void WizardNeverAllowed()
        {
            AssertClassIsNeverAllowed(CharacterClassConstants.Wizard);
        }

        [Test]
        public void BarbarianNotAllowedIfAlignmentIsLawful()
        {
            AssertClassMustNotBeLawful(CharacterClassConstants.Barbarian);
        }

        [Test]
        public void BardNeverAllowed()
        {
            AssertClassIsNeverAllowed(CharacterClassConstants.Bard);
        }

        [Test]
        public void DruidNeverAllowed()
        {
            AssertClassIsNeverAllowed(CharacterClassConstants.Druid);
        }

        [Test]
        public void MonkNotAllowedIfAlignmentIsChaotic()
        {
            AssertMonkIsAllowed();
        }

        [Test]
        public void PaladinNeverAllowed()
        {
            AssertClassIsNeverAllowed(CharacterClassConstants.Paladin);
        }
    }
}