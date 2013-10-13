using D20Dice.Dice;
using Moq;
using NPCGen.Core.Data.Alignments;
using NPCGen.Core.Generation.Providers.Interfaces;
using NPCGen.Core.Generation.Randomizers.Alignments;
using NPCGen.Core.Generation.Randomizers.Alignments.Interfaces;
using NUnit.Framework;
using System;

namespace NPCGen.Tests.Generation.Randomizers.Alignments
{
    [TestFixture]
    public class NonNeutralAlignmentRandomizerTests
    {
        private IAlignmentRandomizer alignmentRandomizer;
        private Mock<IDice> mockDice;
        private Mock<IPercentileResultProvider> mockPercentileResultProvider;

        [SetUp]
        public void Setup()
        {
            mockDice = new Mock<IDice>();
            mockPercentileResultProvider = new Mock<IPercentileResultProvider>();
            alignmentRandomizer = new NonNeutralAlignmentRandomizer(mockDice.Object,
                mockPercentileResultProvider.Object);
        }

        [Test]
        public void ForcedNotNeutralGoodness()
        {
            mockPercentileResultProvider.SetupSequence(p => p.GetPercentileResult(It.IsAny<String>()))
                .Returns(AlignmentConstants.Neutral.ToString()).Returns(AlignmentConstants.Good.ToString());
            mockDice.Setup(d => d.d3(1, 0)).Returns(1);

            var alignment = alignmentRandomizer.Randomize();
            Assert.That(alignment.Goodness, Is.EqualTo(AlignmentConstants.Good));
            mockPercentileResultProvider.Verify(p => p.GetPercentileResult(It.IsAny<String>()), Times.Exactly(2));
        }

        [Test]
        public void ForcedNotNeutralLawfulness()
        {
            mockDice.SetupSequence(d => d.d3(1, 0)).Returns(2).Returns(1);
            mockPercentileResultProvider.Setup(p => p.GetPercentileResult(It.IsAny<String>()))
                .Returns(AlignmentConstants.Good.ToString());

            var alignment = alignmentRandomizer.Randomize();
            Assert.That(alignment.Lawfulness, Is.EqualTo(AlignmentConstants.Chaotic));
            mockDice.Verify(d => d.d3(1, 0), Times.Exactly(2));
        }
    }
}