﻿using System;
using System.Collections.Generic;
using System.Linq;
using D20Dice;
using Moq;
using NPCGen.Common.Alignments;
using NPCGen.Generators.Randomizers.Alignments;
using NPCGen.Selectors.Interfaces;
using NUnit.Framework;

namespace NPCGen.Tests.Unit.Generators.Randomizers.Alignments
{
    [TestFixture]
    public class NonChaoticAlignmentRandomizerTests
    {
        private IEnumerable<Alignment> alignments;

        [SetUp]
        public void Setup()
        {
            var mockDice = new Mock<IDice>();
            var mockPercentileResultProvider = new Mock<IPercentileSelector>();
            mockPercentileResultProvider.Setup(p => p.GetAllResults(It.IsAny<String>())).Returns(AlignmentConstants.GetGoodnesses());

            var randomizer = new NonChaoticAlignmentRandomizer(mockDice.Object, mockPercentileResultProvider.Object);

            alignments = randomizer.GetAllPossibleResults();
        }

        [Test]
        public void LawfulGoodIsAllowed()
        {
            Assert.That(alignments.Any(a => a.Lawfulness == AlignmentConstants.Lawful && a.Goodness == AlignmentConstants.Good), Is.True);
        }

        [Test]
        public void NeutralGoodIsAllowed()
        {
            Assert.That(alignments.Any(a => a.Lawfulness == AlignmentConstants.Neutral && a.Goodness == AlignmentConstants.Good), Is.True);
        }

        [Test]
        public void ChaoticGoodIsNotAllowed()
        {
            Assert.That(alignments.Any(a => a.Lawfulness == AlignmentConstants.Chaotic && a.Goodness == AlignmentConstants.Good), Is.False);
        }

        [Test]
        public void LawfulNeutralIsAllowed()
        {
            Assert.That(alignments.Any(a => a.Lawfulness == AlignmentConstants.Lawful && a.Goodness == AlignmentConstants.Neutral), Is.True);
        }

        [Test]
        public void TrueNeutralIsAllowed()
        {
            Assert.That(alignments.Any(a => a.Lawfulness == AlignmentConstants.Neutral && a.Goodness == AlignmentConstants.Neutral), Is.True);
        }

        [Test]
        public void ChaoticNeutralIsNotAllowed()
        {
            Assert.That(alignments.Any(a => a.Lawfulness == AlignmentConstants.Chaotic && a.Goodness == AlignmentConstants.Neutral), Is.False);
        }

        [Test]
        public void LawfulEvilIsAllowed()
        {
            Assert.That(alignments.Any(a => a.Lawfulness == AlignmentConstants.Lawful && a.Goodness == AlignmentConstants.Evil), Is.True);
        }

        [Test]
        public void NeutralEvilIsAllowed()
        {
            Assert.That(alignments.Any(a => a.Lawfulness == AlignmentConstants.Neutral && a.Goodness == AlignmentConstants.Evil), Is.True);
        }

        [Test]
        public void ChaoticEvilIsNotAllowed()
        {
            Assert.That(alignments.Any(a => a.Lawfulness == AlignmentConstants.Chaotic && a.Goodness == AlignmentConstants.Evil), Is.False);
        }
    }
}