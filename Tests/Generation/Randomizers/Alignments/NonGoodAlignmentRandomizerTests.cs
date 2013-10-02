using D20Dice.Dice;
using Moq;
using NPCGen.Core.Data.Alignments;
using NPCGen.Core.Generation.Randomizers.Alignments;
using NPCGen.Core.Generation.Randomizers.Alignments.Interfaces;
using NUnit.Framework;

namespace NPCGen.Tests.Generation.Randomizers.Alignments
{
    [TestFixture]
    public class NonGoodAlignmentRandomizerTests
    {
        private IAlignmentRandomizer alignmentRandomizer;
        private Mock<IDice> mockDice;

        [SetUp]
        public void Setup()
        {
            mockDice = new Mock<IDice>();
            alignmentRandomizer = new NonGoodAlignmentRandomizer(mockDice.Object);
        }

        [Test]
        public void ForcedNotGood()
        {
            mockDice.SetupSequence(d => d.Percentile(1, 0)).Returns(1).Returns(50);

            var alignment = alignmentRandomizer.Randomize();
            Assert.That(alignment.Goodness, Is.EqualTo(AlignmentConstants.Neutral));
            mockDice.Verify(d => d.Percentile(1, 0), Times.Exactly(2));
        }
    }
}