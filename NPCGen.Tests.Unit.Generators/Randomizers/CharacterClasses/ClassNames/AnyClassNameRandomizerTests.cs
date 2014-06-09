using NPCGen.Core.Data.CharacterClasses;
using NPCGen.Core.Generators.Randomizers.CharacterClasses.ClassNames;
using NUnit.Framework;

namespace NPCGen.Tests.Unit.Generators.Randomizers.CharacterClasses.ClassNames
{
    [TestFixture]
    public class AnyClassNameRandomizerTests : ClassNameRandomizerTests
    {
        [SetUp]
        public void Setup()
        {
            randomizer = new AnyClassNameRandomizer(mockPercentileResultProvider.Object);
        }

        [Test]
        public void FighterAlwaysAllowed()
        {
            AssertClassIsAlwaysAllowed(CharacterClassConstants.Fighter);
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
        public void RogueAlwaysAllowed()
        {
            AssertClassIsAlwaysAllowed(CharacterClassConstants.Rogue);
        }

        [Test]
        public void WizardAlwaysAllowed()
        {
            AssertClassIsAlwaysAllowed(CharacterClassConstants.Wizard);
        }

        [Test]
        public void BarbarianIsAllowedIfAlignmentIsNotLawful()
        {
            AssertClassMustNotBeLawful(CharacterClassConstants.Barbarian);
        }

        [Test]
        public void BardIsAllowedIfAlignmentIsNotLawful()
        {
            AssertClassMustNotBeLawful(CharacterClassConstants.Bard);
        }

        [Test]
        public void DruidIsAllowedIfAlignmentIsNeutral()
        {
            AssertDruidIsAllowed();
        }

        [Test]
        public void MonkIsAllowedIfAlignmentIsLawful()
        {
            AssertMonkIsAllowed();
        }

        [Test]
        public void PaladinNotAllowedIfAlignmentIsNotLawful()
        {
            AssertPaladinIsAllowed();
        }
    }
}