using NPCGen.Core.Data.CharacterClasses;
using NPCGen.Core.Generation.Randomizers.CharacterClasses.ClassNames;
using NUnit.Framework;

namespace NPCGen.Tests.Generation.Randomizers.CharacterClasses.ClassNames
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
            AssertClassIsNeverAllowed(CharacterClassConstants.Fighter);
        }

        [Test]
        public void ClericAlwaysAllowed()
        {
            AssertControlIsAlwaysAllowed(CharacterClassConstants.Ranger);
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
        public void RogueNeverAllowed()
        {
            AssertClassIsNeverAllowed(CharacterClassConstants.Rogue);
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
        public void PaladinNotAllowedIfAlignmentIsNotLawful()
        {
            AssertPaladinIsAllowed();
        }
    }
}