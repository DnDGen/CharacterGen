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
    public class NeutralAlignmentRandomizerTests
    {
        private IAlignmentRandomizer alignmentRandomizer;
        private Mock<IDice> mockDice;
        private Mock<IPercentileResultProvider> mockPercentileResultProvider;

        [SetUp]
        public void Setup()
        {
            mockDice = new Mock<IDice>();
            mockPercentileResultProvider = new Mock<IPercentileResultProvider>();
            alignmentRandomizer = new NeutralAlignmentRandomizer(mockDice.Object,
                mockPercentileResultProvider.Object);
        }

        [Test]
        public void ForcedNeutralLawfulness()
        {
            mockDice.SetupSequence(d => d.d3(1, 0)).Returns(1).Returns(2);
            mockPercentileResultProvider.Setup(p => p.GetPercentileResult(It.IsAny<String>()))
                .Returns(AlignmentConstants.Good.ToString());

            var alignment = alignmentRandomizer.Randomize();
            Assert.That(alignment.Lawfulness, Is.EqualTo(AlignmentConstants.Neutral));
            mockDice.Verify(d => d.d3(1, 0), Times.Exactly(2));
        }

        [Test]
        public void ForcedNeutralGoodness()
        {
            mockDice.Setup(d => d.d3(1, 0)).Returns(1);
            mockPercentileResultProvider.SetupSequence(p => p.GetPercentileResult(It.IsAny<String>()))
                .Returns(AlignmentConstants.Good.ToString()).Returns(AlignmentConstants.Neutral.ToString());

            var alignment = alignmentRandomizer.Randomize();
            Assert.That(alignment.Goodness, Is.EqualTo(AlignmentConstants.Neutral));
            mockPercentileResultProvider.Verify(p => p.GetPercentileResult(It.IsAny<String>()), Times.Exactly(2));
        }
    }
}