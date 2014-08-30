using System;
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
    public class NonGoodAlignmentRandomizerTests
    {
        private IEnumerable<Alignment> alignments;

        [SetUp]
        public void Setup()
        {
            var mockDice = new Mock<IDice>();
            var mockPercentileResultSelector = new Mock<IPercentileSelector>();
            mockPercentileResultSelector.Setup(p => p.SelectAllFrom(It.IsAny<String>())).Returns(AlignmentConstants.GetGoodnesses());

            var randomizer = new NonGoodAlignmentRandomizer(mockDice.Object, mockPercentileResultSelector.Object);

            alignments = randomizer.GetAllPossibleResults();
        }

        [TestCase(AlignmentConstants.Lawful, AlignmentConstants.Neutral)]
        [TestCase(AlignmentConstants.Neutral, AlignmentConstants.Neutral)]
        [TestCase(AlignmentConstants.Chaotic, AlignmentConstants.Neutral)]
        [TestCase(AlignmentConstants.Lawful, AlignmentConstants.Evil)]
        [TestCase(AlignmentConstants.Neutral, AlignmentConstants.Evil)]
        [TestCase(AlignmentConstants.Chaotic, AlignmentConstants.Evil)]
        public void Allowed(String lawfulness, String goodness)
        {
            var expectedAlignment = new Alignment { Lawfulness = lawfulness, Goodness = goodness };
            Assert.That(alignments, Contains.Item(expectedAlignment));
        }

        [TestCase(AlignmentConstants.Lawful, AlignmentConstants.Good)]
        [TestCase(AlignmentConstants.Neutral, AlignmentConstants.Good)]
        [TestCase(AlignmentConstants.Chaotic, AlignmentConstants.Good)]
        public void NotAllowed(String lawfulness, String goodness)
        {
            var expectedAlignment = new Alignment { Lawfulness = lawfulness, Goodness = goodness };
            Assert.That(alignments, Is.Not.Contains(expectedAlignment));
        }
    }
}