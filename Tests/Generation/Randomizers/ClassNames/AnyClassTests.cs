﻿using System;
using Moq;
using NPCGen.Core.Data.Alignments;
using NPCGen.Core.Data.CharacterClasses;
using NPCGen.Core.Generation.Providers.Interfaces;
using NPCGen.Core.Generation.Randomizers.ClassNames;
using NUnit.Framework;

namespace NPCGen.Tests.Generation.Randomizers.ClassNames
{
    [TestFixture]
    public class AnyClassTests
    {
        private IClassNameRandomizer classNameRandomizer;
        private Mock<IPercentileResultProvider> mockPercentileResultProvider;
        private Alignment alignment;

        [SetUp]
        public void Setup()
        {
            mockPercentileResultProvider = new Mock<IPercentileResultProvider>();
            classNameRandomizer = new AnyClass(mockPercentileResultProvider.Object);

            alignment = new Alignment();
        }

        [Test]
        public void FighterAlwaysAllowed()
        {
            mockPercentileResultProvider.SetupSequence(p => p.GetPercentileResult(It.IsAny<String>()))
                .Returns(CharacterClassConstants.Fighter)
                .Returns(CharacterClassConstants.Barbarian);
            var className = classNameRandomizer.Randomize(alignment);
            Assert.That(className, Is.EqualTo(CharacterClassConstants.Fighter));
        }

        [Test]
        public void ClericAlwaysAllowed()
        {
            mockPercentileResultProvider.SetupSequence(p => p.GetPercentileResult(It.IsAny<String>()))
                .Returns(CharacterClassConstants.Cleric)
                .Returns(CharacterClassConstants.Barbarian);
            var className = classNameRandomizer.Randomize(alignment);
            Assert.That(className, Is.EqualTo(CharacterClassConstants.Cleric));
        }

        [Test]
        public void RangerAlwaysAllowed()
        {
            mockPercentileResultProvider.SetupSequence(p => p.GetPercentileResult(It.IsAny<String>()))
                .Returns(CharacterClassConstants.Ranger)
                .Returns(CharacterClassConstants.Barbarian);
            var className = classNameRandomizer.Randomize(alignment);
            Assert.That(className, Is.EqualTo(CharacterClassConstants.Ranger));
        }

        [Test]
        public void SorcererAlwaysAllowed()
        {
            mockPercentileResultProvider.SetupSequence(p => p.GetPercentileResult(It.IsAny<String>()))
                .Returns(CharacterClassConstants.Sorcerer)
                .Returns(CharacterClassConstants.Barbarian);
            var className = classNameRandomizer.Randomize(alignment);
            Assert.That(className, Is.EqualTo(CharacterClassConstants.Sorcerer));
        }

        [Test]
        public void RogueAlwaysAllowed()
        {
            mockPercentileResultProvider.SetupSequence(p => p.GetPercentileResult(It.IsAny<String>()))
                .Returns(CharacterClassConstants.Rogue)
                .Returns(CharacterClassConstants.Barbarian);
            var className = classNameRandomizer.Randomize(alignment);
            Assert.That(className, Is.EqualTo(CharacterClassConstants.Rogue));
        }

        [Test]
        public void WizardAlwaysAllowed()
        {
            mockPercentileResultProvider.SetupSequence(p => p.GetPercentileResult(It.IsAny<String>()))
                .Returns(CharacterClassConstants.Wizard)
                .Returns(CharacterClassConstants.Barbarian);
            var className = classNameRandomizer.Randomize(alignment);
            Assert.That(className, Is.EqualTo(CharacterClassConstants.Wizard));
        }

        [Test]
        public void BarbarianNotAllowedIfAlignmentIsLawful()
        {
            alignment.Lawfulness = AlignmentConstants.Lawful;
            mockPercentileResultProvider.SetupSequence(p => p.GetPercentileResult(It.IsAny<String>()))
                .Returns(CharacterClassConstants.Barbarian)
                .Returns(CharacterClassConstants.Fighter);
            var className = classNameRandomizer.Randomize(alignment);
            Assert.That(className, Is.EqualTo(CharacterClassConstants.Fighter));
        }

        [Test]
        public void BarbarianAllowedIfAlignmentIsNeutral()
        {
            alignment.Lawfulness = AlignmentConstants.Neutral;
            mockPercentileResultProvider.SetupSequence(p => p.GetPercentileResult(It.IsAny<String>()))
                .Returns(CharacterClassConstants.Barbarian)
                .Returns(CharacterClassConstants.Fighter);
            var className = classNameRandomizer.Randomize(alignment);
            Assert.That(className, Is.EqualTo(CharacterClassConstants.Barbarian));
        }

        [Test]
        public void BarbarianAllowedIfAlignmentIsChaotic()
        {
            alignment.Lawfulness = AlignmentConstants.Neutral;
            mockPercentileResultProvider.SetupSequence(p => p.GetPercentileResult(It.IsAny<String>()))
                .Returns(CharacterClassConstants.Barbarian)
                .Returns(CharacterClassConstants.Fighter);
            var className = classNameRandomizer.Randomize(alignment);
            Assert.That(className, Is.EqualTo(CharacterClassConstants.Barbarian));
        }

        [Test]
        public void BardNotAllowedIfAlignmentIsLawful()
        {
            alignment.Lawfulness = AlignmentConstants.Lawful;
            mockPercentileResultProvider.SetupSequence(p => p.GetPercentileResult(It.IsAny<String>()))
                .Returns(CharacterClassConstants.Bard)
                .Returns(CharacterClassConstants.Fighter);
            var className = classNameRandomizer.Randomize(alignment);
            Assert.That(className, Is.EqualTo(CharacterClassConstants.Fighter));
        }

        [Test]
        public void BardAllowedIfAlignmentIsNeutral()
        {
            alignment.Lawfulness = AlignmentConstants.Neutral;
            mockPercentileResultProvider.SetupSequence(p => p.GetPercentileResult(It.IsAny<String>()))
                .Returns(CharacterClassConstants.Bard)
                .Returns(CharacterClassConstants.Fighter);
            var className = classNameRandomizer.Randomize(alignment);
            Assert.That(className, Is.EqualTo(CharacterClassConstants.Bard));
        }

        [Test]
        public void BardAllowedIfAlignmentIsChaotic()
        {
            alignment.Lawfulness = AlignmentConstants.Neutral;
            mockPercentileResultProvider.SetupSequence(p => p.GetPercentileResult(It.IsAny<String>()))
                .Returns(CharacterClassConstants.Bard)
                .Returns(CharacterClassConstants.Fighter);
            var className = classNameRandomizer.Randomize(alignment);
            Assert.That(className, Is.EqualTo(CharacterClassConstants.Bard));
        }

        [Test]
        public void DruidNotAllowedIfAlignmentIsNotNeutral()
        {
            alignment.Lawfulness = AlignmentConstants.Lawful;
            alignment.Goodness = AlignmentConstants.Good;
            mockPercentileResultProvider.SetupSequence(p => p.GetPercentileResult(It.IsAny<String>()))
                .Returns(CharacterClassConstants.Druid)
                .Returns(CharacterClassConstants.Fighter);
            var className = classNameRandomizer.Randomize(alignment);
            Assert.That(className, Is.EqualTo(CharacterClassConstants.Fighter));
        }

        [Test]
        public void DruidAllowedIfLawfulnessIsNeutral()
        {
            alignment.Lawfulness = AlignmentConstants.Neutral;
            alignment.Goodness = AlignmentConstants.Good;
            mockPercentileResultProvider.SetupSequence(p => p.GetPercentileResult(It.IsAny<String>()))
                .Returns(CharacterClassConstants.Druid)
                .Returns(CharacterClassConstants.Fighter);
            var className = classNameRandomizer.Randomize(alignment);
            Assert.That(className, Is.EqualTo(CharacterClassConstants.Druid));
        }

        [Test]
        public void DruidAllowedIfGoodnessIsNeutral()
        {
            alignment.Lawfulness = AlignmentConstants.Lawful;
            alignment.Goodness = AlignmentConstants.Neutral;
            mockPercentileResultProvider.SetupSequence(p => p.GetPercentileResult(It.IsAny<String>()))
                .Returns(CharacterClassConstants.Druid)
                .Returns(CharacterClassConstants.Fighter);
            var className = classNameRandomizer.Randomize(alignment);
            Assert.That(className, Is.EqualTo(CharacterClassConstants.Druid));
        }

        [Test]
        public void MonkNotAllowedIfAlignmentIsNotLawful()
        {
            alignment.Lawfulness = AlignmentConstants.Neutral;
            mockPercentileResultProvider.SetupSequence(p => p.GetPercentileResult(It.IsAny<String>()))
                .Returns(CharacterClassConstants.Monk)
                .Returns(CharacterClassConstants.Fighter);
            var className = classNameRandomizer.Randomize(alignment);
            Assert.That(className, Is.EqualTo(CharacterClassConstants.Fighter));
        }

        [Test]
        public void MonkAllowedIfAlignmentIsLawful()
        {
            alignment.Lawfulness = AlignmentConstants.Lawful;
            mockPercentileResultProvider.SetupSequence(p => p.GetPercentileResult(It.IsAny<String>()))
                .Returns(CharacterClassConstants.Monk)
                .Returns(CharacterClassConstants.Fighter);
            var className = classNameRandomizer.Randomize(alignment);
            Assert.That(className, Is.EqualTo(CharacterClassConstants.Monk));
        }

        [Test]
        public void PaladinNotAllowedIfAlignmentIsNotLawful()
        {
            alignment.Lawfulness = AlignmentConstants.Neutral;
            alignment.Goodness = AlignmentConstants.Good;
            mockPercentileResultProvider.SetupSequence(p => p.GetPercentileResult(It.IsAny<String>()))
                .Returns(CharacterClassConstants.Paladin)
                .Returns(CharacterClassConstants.Fighter);
            var className = classNameRandomizer.Randomize(alignment);
            Assert.That(className, Is.EqualTo(CharacterClassConstants.Fighter));
        }

        [Test]
        public void PaladinNotAllowedIfAlignmentIsNotGood()
        {
            alignment.Goodness = AlignmentConstants.Neutral;
            alignment.Lawfulness = AlignmentConstants.Lawful;
            mockPercentileResultProvider.SetupSequence(p => p.GetPercentileResult(It.IsAny<String>()))
                .Returns(CharacterClassConstants.Paladin)
                .Returns(CharacterClassConstants.Fighter);
            var className = classNameRandomizer.Randomize(alignment);
            Assert.That(className, Is.EqualTo(CharacterClassConstants.Fighter));
        }

        [Test]
        public void PaladinAllowedIfAlignmentIsLawfulGood()
        {
            alignment.Goodness = AlignmentConstants.Good;
            alignment.Lawfulness = AlignmentConstants.Lawful;
            mockPercentileResultProvider.SetupSequence(p => p.GetPercentileResult(It.IsAny<String>()))
                .Returns(CharacterClassConstants.Paladin)
                .Returns(CharacterClassConstants.Fighter);
            var className = classNameRandomizer.Randomize(alignment);
            Assert.That(className, Is.EqualTo(CharacterClassConstants.Paladin));
        }
    }
}