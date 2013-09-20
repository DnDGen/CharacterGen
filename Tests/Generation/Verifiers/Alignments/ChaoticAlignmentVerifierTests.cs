using Moq;
using NPCGen.Core.Data.CharacterClasses;
using NPCGen.Core.Generation.Providers.Interfaces;
using NPCGen.Core.Generation.Randomizers.ClassNames;
using NPCGen.Core.Generation.Verifiers.Alignments;
using NUnit.Framework;

namespace NPCGen.Tests.Generation.Verifiers.Alignments
{
    [TestFixture]
    public class ChaoticAlignmentVerifierTests
    {
        private IAlignmentVerifier verifier;
        private Mock<IPercentileResultProvider> mockPercentileResultProvider;

        [SetUp]
        public void Setup()
        {
            mockPercentileResultProvider = new Mock<IPercentileResultProvider>();
            verifier = new ChaoticAlignmentVerifier();
        }

        [Test]
        public void AnyClassNameRandomizerIsAllowed()
        {
            var randomizer = new AnyClassNameRandomizer(mockPercentileResultProvider.Object);
            var allowed = verifier.VerifyCompatiblity(randomizer);
            Assert.That(allowed, Is.True);
        }

        [Test]
        public void HealerClassRandomizerIsAllowed()
        {
            var randomizer = new HealerClassNameRandomizer(mockPercentileResultProvider.Object);
            var allowed = verifier.VerifyCompatiblity(randomizer);
            Assert.That(allowed, Is.True);
        }

        [Test]
        public void MageClassRandomizerIsAllowed()
        {
            var randomizer = new MageClassNameRandomizer(mockPercentileResultProvider.Object);
            var allowed = verifier.VerifyCompatiblity(randomizer);
            Assert.That(allowed, Is.True);
        }

        [Test]
        public void NonSpellcasterClassRandomizerIsAllowed()
        {
            var randomizer = new NonSpellcasterClassNameRandomizer(mockPercentileResultProvider.Object);
            var allowed = verifier.VerifyCompatiblity(randomizer);
            Assert.That(allowed, Is.True);
        }

        [Test]
        public void SpellcasterClassRandomizerIsAllowed()
        {
            var randomizer = new SpellcasterClassNameRandomizer(mockPercentileResultProvider.Object);
            var allowed = verifier.VerifyCompatiblity(randomizer);
            Assert.That(allowed, Is.True);
        }

        [Test]
        public void StealthClassRandomizerIsAllowed()
        {
            var randomizer = new StealthClassNameRandomizer(mockPercentileResultProvider.Object);
            var allowed = verifier.VerifyCompatiblity(randomizer);
            Assert.That(allowed, Is.True);
        }

        [Test]
        public void WarriorClassRandomizerIsAllowed()
        {
            var randomizer = new WarriorClassNameRandomizer(mockPercentileResultProvider.Object);
            var allowed = verifier.VerifyCompatiblity(randomizer);
            Assert.That(allowed, Is.True);
        }

        [Test]
        public void BarbarianIsAllowed()
        {
            var randomizer = new SetClassNameRandomizer();
            randomizer.ClassName = CharacterClassConstants.Barbarian;

            var allowed = verifier.VerifyCompatiblity(randomizer);
            Assert.That(allowed, Is.True);
        }

        [Test]
        public void BardIsAllowed()
        {
            var randomizer = new SetClassNameRandomizer();
            randomizer.ClassName = CharacterClassConstants.Bard;

            var allowed = verifier.VerifyCompatiblity(randomizer);
            Assert.That(allowed, Is.True);
        }

        [Test]
        public void ClericIsAllowed()
        {
            var randomizer = new SetClassNameRandomizer();
            randomizer.ClassName = CharacterClassConstants.Cleric;

            var allowed = verifier.VerifyCompatiblity(randomizer);
            Assert.That(allowed, Is.True);
        }

        [Test]
        public void DruidIsAllowed()
        {
            var randomizer = new SetClassNameRandomizer();
            randomizer.ClassName = CharacterClassConstants.Druid;

            var allowed = verifier.VerifyCompatiblity(randomizer);
            Assert.That(allowed, Is.True);
        }

        [Test]
        public void FighterIsAllowed()
        {
            var randomizer = new SetClassNameRandomizer();
            randomizer.ClassName = CharacterClassConstants.Fighter;

            var allowed = verifier.VerifyCompatiblity(randomizer);
            Assert.That(allowed, Is.True);
        }

        [Test]
        public void MonkIsNotAllowed()
        {
            var randomizer = new SetClassNameRandomizer();
            randomizer.ClassName = CharacterClassConstants.Monk;

            var allowed = verifier.VerifyCompatiblity(randomizer);
            Assert.That(allowed, Is.False);
        }

        [Test]
        public void PaladinIsNotAllowed()
        {
            var randomizer = new SetClassNameRandomizer();
            randomizer.ClassName = CharacterClassConstants.Paladin;

            var allowed = verifier.VerifyCompatiblity(randomizer);
            Assert.That(allowed, Is.False);
        }

        [Test]
        public void RangerIsAllowed()
        {
            var randomizer = new SetClassNameRandomizer();
            randomizer.ClassName = CharacterClassConstants.Ranger;

            var allowed = verifier.VerifyCompatiblity(randomizer);
            Assert.That(allowed, Is.True);
        }

        [Test]
        public void RogueIsAllowed()
        {
            var randomizer = new SetClassNameRandomizer();
            randomizer.ClassName = CharacterClassConstants.Rogue;

            var allowed = verifier.VerifyCompatiblity(randomizer);
            Assert.That(allowed, Is.True);
        }

        [Test]
        public void SorcererIsAllowed()
        {
            var randomizer = new SetClassNameRandomizer();
            randomizer.ClassName = CharacterClassConstants.Sorcerer;

            var allowed = verifier.VerifyCompatiblity(randomizer);
            Assert.That(allowed, Is.True);
        }

        [Test]
        public void WizardIsAllowed()
        {
            var randomizer = new SetClassNameRandomizer();
            randomizer.ClassName = CharacterClassConstants.Wizard;

            var allowed = verifier.VerifyCompatiblity(randomizer);
            Assert.That(allowed, Is.True);
        }
    }
}