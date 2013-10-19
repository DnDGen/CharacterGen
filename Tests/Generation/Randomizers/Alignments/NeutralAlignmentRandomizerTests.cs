using D20Dice.Dice;
using Moq;
using NPCGen.Core.Data.Alignments;
using NPCGen.Core.Generation.Providers.Interfaces;
using NPCGen.Core.Generation.Randomizers.Alignments;
using NPCGen.Core.Generation.Randomizers.Alignments.Interfaces;
using NUnit.Framework;
using System;
using System.Linq;

namespace NPCGen.Tests.Generation.Randomizers.Alignments
{
    [TestFixture]
    public class NeutralAlignmentRandomizerTests
    {
        private IAlignmentRandomizer randomizer;
        private Mock<IDice> mockDice;
        private Mock<IPercentileResultProvider> mockPercentileResultProvider;

        [SetUp]
        public void Setup()
        {
            mockDice = new Mock<IDice>();
            mockPercentileResultProvider = new Mock<IPercentileResultProvider>();
            randomizer = new NeutralAlignmentRandomizer(mockDice.Object, mockPercentileResultProvider.Object);
        }

        [Test]
        public void ForcedNeutralLawfulness()
        {
            mockDice.SetupSequence(d => d.d3(1, 0)).Returns(1).Returns(2);
            mockPercentileResultProvider.Setup(p => p.GetPercentileResult(It.IsAny<String>()))
                .Returns(AlignmentConstants.Good.ToString());

            var alignment = randomizer.Randomize();
            Assert.That(alignment.Lawfulness, Is.EqualTo(AlignmentConstants.Neutral));
            mockDice.Verify(d => d.d3(1, 0), Times.Exactly(2));
        }

        [Test]
        public void ForcedNeutralGoodness()
        {
            mockDice.Setup(d => d.d3(1, 0)).Returns(1);
            mockPercentileResultProvider.SetupSequence(p => p.GetPercentileResult(It.IsAny<String>()))
                .Returns(AlignmentConstants.Good.ToString()).Returns(AlignmentConstants.Neutral.ToString());

            var alignment = randomizer.Randomize();
            Assert.That(alignment.Goodness, Is.EqualTo(AlignmentConstants.Neutral));
            mockPercentileResultProvider.Verify(p => p.GetPercentileResult(It.IsAny<String>()), Times.Exactly(2));
        }

        [Test]
        public void ReturnFilteredAlignments()
        {
            var goodnesses = new[] 
            { 
                AlignmentConstants.Good.ToString(),
                AlignmentConstants.Neutral.ToString(), 
                AlignmentConstants.Evil.ToString() 
            };

            mockPercentileResultProvider.Setup(p => p.GetAllResults(It.IsAny<String>())).Returns(goodnesses);

            var alignments = randomizer.GetAllPossibleResults();

            Assert.That(alignments.Count(a => a.Goodness == AlignmentConstants.Good && a.Lawfulness == AlignmentConstants.Neutral), Is.EqualTo(1));
            Assert.That(alignments.Count(a => a.Goodness == AlignmentConstants.Neutral && a.Lawfulness == AlignmentConstants.Good), Is.EqualTo(1));
            Assert.That(alignments.Count(a => a.Goodness == AlignmentConstants.Neutral && a.Lawfulness == AlignmentConstants.Neutral), Is.EqualTo(1));
            Assert.That(alignments.Count(a => a.Goodness == AlignmentConstants.Neutral && a.Lawfulness == AlignmentConstants.Evil), Is.EqualTo(1));
            Assert.That(alignments.Count(a => a.Goodness == AlignmentConstants.Evil && a.Lawfulness == AlignmentConstants.Neutral), Is.EqualTo(1));
            Assert.That(alignments.Count(), Is.EqualTo(5));
        }
    }
}