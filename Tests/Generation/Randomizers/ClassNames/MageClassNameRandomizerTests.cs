using System;
using Moq;
using NPCGen.Core.Data.Alignments;
using NPCGen.Core.Data.CharacterClasses;
using NPCGen.Core.Generation.Providers.Interfaces;
using NPCGen.Core.Generation.Randomizers.ClassNames;
using NUnit.Framework;

namespace NPCGen.Tests.Generation.Randomizers.ClassNames
{
    [TestFixture]
    public class MageClassNameRandomizerTests
    {
        private IClassNameRandomizer classNameRandomizer;
        private Mock<IPercentileResultProvider> mockPercentileResultProvider;
        private Alignment alignment;

        [SetUp]
        public void Setup()
        {
            mockPercentileResultProvider = new Mock<IPercentileResultProvider>();
            classNameRandomizer = new MageClassNameRandomizer(mockPercentileResultProvider.Object);

            alignment = new Alignment();
        }

        [Test]
        public void FighterNeverAllowed()
        {
            mockPercentileResultProvider.SetupSequence(p => p.GetPercentileResult(It.IsAny<String>()))
                .Returns(CharacterClassConstants.Fighter)
                .Returns(CharacterClassConstants.Wizard);
            var className = classNameRandomizer.Randomize(alignment);
            Assert.That(className, Is.EqualTo(CharacterClassConstants.Wizard));
        }

        [Test]
        public void ClericNeverAllowed()
        {
            mockPercentileResultProvider.SetupSequence(p => p.GetPercentileResult(It.IsAny<String>()))
                .Returns(CharacterClassConstants.Cleric)
                .Returns(CharacterClassConstants.Wizard);
            var className = classNameRandomizer.Randomize(alignment);
            Assert.That(className, Is.EqualTo(CharacterClassConstants.Wizard));
        }

        [Test]
        public void RangerAlwaysAllowed()
        {
            mockPercentileResultProvider.SetupSequence(p => p.GetPercentileResult(It.IsAny<String>()))
                .Returns(CharacterClassConstants.Ranger)
                .Returns(CharacterClassConstants.Wizard);
            var className = classNameRandomizer.Randomize(alignment);
            Assert.That(className, Is.EqualTo(CharacterClassConstants.Ranger));
        }

        [Test]
        public void SorcererAlwaysAllowed()
        {
            mockPercentileResultProvider.SetupSequence(p => p.GetPercentileResult(It.IsAny<String>()))
                .Returns(CharacterClassConstants.Sorcerer)
                .Returns(CharacterClassConstants.Wizard);
            var className = classNameRandomizer.Randomize(alignment);
            Assert.That(className, Is.EqualTo(CharacterClassConstants.Sorcerer));
        }

        [Test]
        public void RogueNeverAllowed()
        {
            mockPercentileResultProvider.SetupSequence(p => p.GetPercentileResult(It.IsAny<String>()))
                .Returns(CharacterClassConstants.Rogue)
                .Returns(CharacterClassConstants.Wizard);
            var className = classNameRandomizer.Randomize(alignment);
            Assert.That(className, Is.EqualTo(CharacterClassConstants.Wizard));
        }

        [Test]
        public void WizardAlwaysAllowed()
        {
            mockPercentileResultProvider.SetupSequence(p => p.GetPercentileResult(It.IsAny<String>()))
                .Returns(CharacterClassConstants.Wizard)
                .Returns(CharacterClassConstants.Sorcerer);
            var className = classNameRandomizer.Randomize(alignment);
            Assert.That(className, Is.EqualTo(CharacterClassConstants.Wizard));
        }

        [Test]
        public void BarbarianNeverAllowed()
        {
            mockPercentileResultProvider.SetupSequence(p => p.GetPercentileResult(It.IsAny<String>()))
                .Returns(CharacterClassConstants.Barbarian)
                .Returns(CharacterClassConstants.Wizard);
            var className = classNameRandomizer.Randomize(alignment);
            Assert.That(className, Is.EqualTo(CharacterClassConstants.Wizard));
        }

        [Test]
        public void BardNotAllowedIfAlignmentIsLawful()
        {
            alignment.Lawfulness = AlignmentConstants.Lawful;
            mockPercentileResultProvider.SetupSequence(p => p.GetPercentileResult(It.IsAny<String>()))
                .Returns(CharacterClassConstants.Bard)
                .Returns(CharacterClassConstants.Wizard);
            var className = classNameRandomizer.Randomize(alignment);
            Assert.That(className, Is.EqualTo(CharacterClassConstants.Wizard));
        }

        [Test]
        public void BardAllowedIfAlignmentIsNeutral()
        {
            alignment.Lawfulness = AlignmentConstants.Neutral;
            mockPercentileResultProvider.SetupSequence(p => p.GetPercentileResult(It.IsAny<String>()))
                .Returns(CharacterClassConstants.Bard)
                .Returns(CharacterClassConstants.Wizard);
            var className = classNameRandomizer.Randomize(alignment);
            Assert.That(className, Is.EqualTo(CharacterClassConstants.Bard));
        }

        [Test]
        public void BardAllowedIfAlignmentIsChaotic()
        {
            alignment.Lawfulness = AlignmentConstants.Neutral;
            mockPercentileResultProvider.SetupSequence(p => p.GetPercentileResult(It.IsAny<String>()))
                .Returns(CharacterClassConstants.Bard)
                .Returns(CharacterClassConstants.Wizard);
            var className = classNameRandomizer.Randomize(alignment);
            Assert.That(className, Is.EqualTo(CharacterClassConstants.Bard));
        }

        [Test]
        public void DruidNeverAllowed()
        {
            mockPercentileResultProvider.SetupSequence(p => p.GetPercentileResult(It.IsAny<String>()))
                .Returns(CharacterClassConstants.Druid)
                .Returns(CharacterClassConstants.Wizard);
            var className = classNameRandomizer.Randomize(alignment);
            Assert.That(className, Is.EqualTo(CharacterClassConstants.Wizard));
        }

        [Test]
        public void MonkNeverAllowed()
        {
            mockPercentileResultProvider.SetupSequence(p => p.GetPercentileResult(It.IsAny<String>()))
                .Returns(CharacterClassConstants.Monk)
                .Returns(CharacterClassConstants.Wizard);
            var className = classNameRandomizer.Randomize(alignment);
            Assert.That(className, Is.EqualTo(CharacterClassConstants.Wizard));
        }

        [Test]
        public void PaladinNeverAllowed()
        {
            mockPercentileResultProvider.SetupSequence(p => p.GetPercentileResult(It.IsAny<String>()))
                .Returns(CharacterClassConstants.Paladin)
                .Returns(CharacterClassConstants.Wizard);
            var className = classNameRandomizer.Randomize(alignment);
            Assert.That(className, Is.EqualTo(CharacterClassConstants.Wizard));
        }
    }
}