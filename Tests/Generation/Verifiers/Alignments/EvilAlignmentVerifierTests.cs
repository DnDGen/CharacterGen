using NPCGen.Core.Data.CharacterClasses;
using NPCGen.Core.Generation.Randomizers.ClassNames;
using NPCGen.Core.Generation.Verifiers.Alignments;
using NUnit.Framework;

namespace NPCGen.Tests.Generation.Verifiers.Alignments
{
    [TestFixture]
    public class EvilAlignmentVerifierTests : AlignmentVerifierTests
    {
        [SetUp]
        public void Setup()
        {
            verifier = new EvilAlignmentVerifier();
        }

        [Test]
        public void AnyClassNameRandomizerIsAllowed()
        {
            var randomizer = new AnyClassNameRandomizer(mockPercentileResultProvider.Object);
            AssertRandomizerIsAllowed(randomizer);
        }

        [Test]
        public void HealerClassRandomizerIsAllowed()
        {
            var randomizer = new HealerClassNameRandomizer(mockPercentileResultProvider.Object);
            AssertRandomizerIsAllowed(randomizer);
        }

        [Test]
        public void MageClassRandomizerIsAllowed()
        {
            var randomizer = new MageClassNameRandomizer(mockPercentileResultProvider.Object);
            AssertRandomizerIsAllowed(randomizer);
        }

        [Test]
        public void NonSpellcasterClassRandomizerIsAllowed()
        {
            var randomizer = new NonSpellcasterClassNameRandomizer(mockPercentileResultProvider.Object);
            AssertRandomizerIsAllowed(randomizer);
        }

        [Test]
        public void SpellcasterClassRandomizerIsAllowed()
        {
            var randomizer = new SpellcasterClassNameRandomizer(mockPercentileResultProvider.Object);
            AssertRandomizerIsAllowed(randomizer);
        }

        [Test]
        public void StealthClassRandomizerIsAllowed()
        {
            var randomizer = new StealthClassNameRandomizer(mockPercentileResultProvider.Object);
            AssertRandomizerIsAllowed(randomizer);
        }

        [Test]
        public void WarriorClassRandomizerIsAllowed()
        {
            var randomizer = new WarriorClassNameRandomizer(mockPercentileResultProvider.Object);
            AssertRandomizerIsAllowed(randomizer);
        }

        [Test]
        public void BarbarianIsAllowed()
        {
            AssertClassNameIsAllowed(CharacterClassConstants.Barbarian);
        }

        [Test]
        public void BardIsAllowed()
        {
            AssertClassNameIsAllowed(CharacterClassConstants.Bard);
        }

        [Test]
        public void ClericIsAllowed()
        {
            AssertClassNameIsAllowed(CharacterClassConstants.Cleric);
        }

        [Test]
        public void DruidIsAllowed()
        {
            AssertClassNameIsAllowed(CharacterClassConstants.Druid);
        }

        [Test]
        public void FighterIsAllowed()
        {
            AssertClassNameIsAllowed(CharacterClassConstants.Fighter);
        }

        [Test]
        public void MonkIsAllowed()
        {
            AssertClassNameIsAllowed(CharacterClassConstants.Monk);
        }

        [Test]
        public void PaladinIsNotAllowed()
        {
            AssertClassNameIsNotAllowed(CharacterClassConstants.Paladin);
        }

        [Test]
        public void RangerIsAllowed()
        {
            AssertClassNameIsAllowed(CharacterClassConstants.Ranger);
        }

        [Test]
        public void RogueIsAllowed()
        {
            AssertClassNameIsAllowed(CharacterClassConstants.Rogue);
        }

        [Test]
        public void SorcererIsAllowed()
        {
            AssertClassNameIsAllowed(CharacterClassConstants.Sorcerer);
        }

        [Test]
        public void WizardIsAllowed()
        {
            AssertClassNameIsAllowed(CharacterClassConstants.Wizard);
        }
    }
}