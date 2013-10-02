using NPCGen.Core.Data.CharacterClasses;
using NPCGen.Core.Generation.Randomizers.CharacterClasses.ClassNames;
using NUnit.Framework;

namespace NPCGen.Tests.Generation.Randomizers.CharacterClasses.ClassNames
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
            AssertClassIsNeverAllowed(CharacterClassConstants.Fighter);
        }

        [Test]
        public void ClericNeverAllowed()
        {
            AssertClassIsNeverAllowed(CharacterClassConstants.Cleric);
        }

        [Test]
        public void RangerAlwaysAllowed()
        {
            AssertClassIsAlwaysAllowed(CharacterClassConstants.Ranger);
        }

        [Test]
        public void SorcererNeverAllowed()
        {
            AssertClassIsNeverAllowed(CharacterClassConstants.Sorcerer);
        }

        [Test]
        public void RogueAlwaysAllowed()
        {
            AssertControlIsAlwaysAllowed(CharacterClassConstants.Ranger);
        }

        [Test]
        public void WizardNeverAllowed()
        {
            AssertClassIsNeverAllowed(CharacterClassConstants.Wizard);
        }

        [Test]
        public void BarbarianNeverAllowed()
        {
            AssertClassIsNeverAllowed(CharacterClassConstants.Barbarian);
        }

        [Test]
        public void BardAllowedIfAlignmentIsNotLawful()
        {
            AssertClassMustNotBeLawful(CharacterClassConstants.Bard);
        }

        [Test]
        public void DruidNeverAllowed()
        {
            AssertClassIsNeverAllowed(CharacterClassConstants.Druid);
        }

        [Test]
        public void MonkNeverAllowed()
        {
            AssertClassIsNeverAllowed(CharacterClassConstants.Monk);
        }

        [Test]
        public void PaladinNeverAllowed()
        {
            AssertClassIsNeverAllowed(CharacterClassConstants.Paladin);
        }
    }
}