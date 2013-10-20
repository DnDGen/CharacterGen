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
    public class NonNeutralAlignmentRandomizerTests
    {
        private IAlignmentRandomizer randomizer;
        private Mock<IDice> mockDice;
        private Mock<IPercentileResultProvider> mockPercentileResultProvider;

        [SetUp]
        public void Setup()
        {
            mockDice = new Mock<IDice>();
            mockPercentileResultProvider = new Mock<IPercentileResultProvider>();
            randomizer = new NonNeutralAlignmentRandomizer(mockDice.Object, mockPercentileResultProvider.Object);
        }

        [Test]
        public void ForcedNotNeutralGoodness()
        {
            mockPercentileResultProvider.SetupSequence(p => p.GetPercentileResult(It.IsAny<String>()))
                .Returns(AlignmentConstants.Neutral.ToString()).Returns(AlignmentConstants.Good.ToString());
            mockDice.Setup(d => d.d3(1, 0)).Returns(1);

            var alignment = randomizer.Randomize();
            Assert.That(alignment.Goodness, Is.EqualTo(AlignmentConstants.Good));
            mockPercentileResultProvider.Verify(p => p.GetPercentileResult(It.IsAny<String>()), Times.Exactly(2));
        }

        [Test]
        public void ForcedNotNeutralLawfulness()
        {
            mockDice.SetupSequence(d => d.d3(1, 0)).Returns(2).Returns(1);
            mockPercentileResultProvider.Setup(p => p.GetPercentileResult(It.IsAny<String>()))
                .Returns(AlignmentConstants.Good.ToString());

            var alignment = randomizer.Randomize();
            Assert.That(alignment.Lawfulness, Is.EqualTo(AlignmentConstants.Chaotic));
            mockDice.Verify(d => d.d3(1, 0), Times.Exactly(2));
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

            Assert.That(alignments.Any(a => a.Goodness == AlignmentConstants.Good && a.Lawfulness == AlignmentConstants.Lawful), Is.True);
            Assert.That(alignments.Any(a => a.Goodness == AlignmentConstants.Good && a.Lawfulness == AlignmentConstants.Chaotic), Is.True);
            Assert.That(alignments.Any(a => a.Goodness == AlignmentConstants.Evil && a.Lawfulness == AlignmentConstants.Lawful), Is.True);
            Assert.That(alignments.Any(a => a.Goodness == AlignmentConstants.Evil && a.Lawfulness == AlignmentConstants.Chaotic), Is.True);
            Assert.That(alignments.Count(), Is.EqualTo(4));
        }
    }
}