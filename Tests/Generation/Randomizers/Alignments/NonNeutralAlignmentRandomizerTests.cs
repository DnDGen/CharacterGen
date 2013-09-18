using D20Dice.Dice;
using Moq;
using NPCGen.Core.Data.Alignments;
using NPCGen.Core.Generation.Randomizers.Alignments;
using NUnit.Framework;

namespace NPCGen.Tests.Generation.Randomizers.Alignments
{
    [TestFixture]
    public class NonNeutralAlignmentRandomizerTests
    {
        private IAlignmentRandomizer alignmentRandomizer;
        private Mock<IDice> mockDice;

        [SetUp]
        public void Setup()
        {
            mockDice = new Mock<IDice>();
            alignmentRandomizer = new NonNeutralAlignmentRandomizer(mockDice.Object);
        }

        [Test]
        public void ForcedNotNeutralGoodness()
        {
            mockDice.SetupSequence(d => d.Percentile(1, 0)).Returns(50).Returns(1);
            mockDice.Setup(d => d.d3(1, 0)).Returns(1);

            var alignment = alignmentRandomizer.Randomize();
            Assert.That(alignment.Goodness, Is.EqualTo(AlignmentConstants.Good));
            mockDice.Verify(d => d.Percentile(1, 0), Times.Exactly(2));
        }

        [Test]
        public void ForcedNotNeutralLawfulness()
        {
            mockDice.SetupSequence(d => d.d3(1, 0)).Returns(2).Returns(1);
            mockDice.Setup(d => d.Percentile(1, 0)).Returns(1);

            var alignment = alignmentRandomizer.Randomize();
            Assert.That(alignment.Lawfulness, Is.EqualTo(AlignmentConstants.Chaotic));
            mockDice.Verify(d => d.d3(1, 0), Times.Exactly(2));
        }
    }
}