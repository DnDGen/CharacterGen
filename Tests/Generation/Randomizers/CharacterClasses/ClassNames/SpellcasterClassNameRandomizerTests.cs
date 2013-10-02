using NPCGen.Core.Data.CharacterClasses;
using NPCGen.Core.Generation.Randomizers.CharacterClasses.ClassNames;
using NUnit.Framework;

namespace NPCGen.Tests.Generation.Randomizers.CharacterClasses.ClassNames
{
    [TestFixture]
    public class SpellcasterClassNameRandomizerTests : ClassNameRandomizerTests
    {
        [SetUp]
        public void Setup()
        {
            randomizer = new SpellcasterClassNameRandomizer(mockPercentileResultProvider.Object);
            controlCase = CharacterClassConstants.Wizard;
        }

        [Test]
        public void FighterNeverAllowed()
        {
            AssertClassIsNeverAllowed(CharacterClassConstants.Fighter);
        }

        [Test]
        public void ClericAlwaysAllowed()
        {
            AssertClassIsAlwaysAllowed(CharacterClassConstants.Cleric);
        }

        [Test]
        public void RangerAlwaysAllowed()
        {
            AssertClassIsAlwaysAllowed(CharacterClassConstants.Ranger);
        }

        [Test]
        public void SorcererAlwaysAllowed()
        {
            AssertClassIsAlwaysAllowed(CharacterClassConstants.Sorcerer);
        }

        [Test]
        public void RogueNeverAllowed()
        {
            AssertClassIsNeverAllowed(CharacterClassConstants.Rogue);
        }

        [Test]
        public void WizardAlwaysAllowed()
        {
            AssertControlIsAlwaysAllowed(CharacterClassConstants.Sorcerer);
        }

        [Test]
        public void BarbarianNeverAllowed()
        {
            AssertClassIsNeverAllowed(CharacterClassConstants.Barbarian);
        }

        [Test]
        public void BardNotAllowedIfAlignmentIsLawful()
        {
            AssertClassMustNotBeLawful(CharacterClassConstants.Bard);
        }

        [Test]
        public void DruidAllowedIfAlignmentIsNeutral()
        {
            AssertDruidIsAllowed();
        }

        [Test]
        public void MonkNeverAllowed()
        {
            AssertClassIsNeverAllowed(CharacterClassConstants.Monk);
        }

        [Test]
        public void PaladinAllowedIfAlignmentIsLawfulGood()
        {
            AssertPaladinIsAllowed();
        }
    }
}