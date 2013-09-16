using D20Dice.Dice;
using Moq;
using NPCGen.Core.Data.Alignments;
using NPCGen.Core.Generation.Randomizers.Alignments;
using NUnit.Framework;

namespace NPCGen.Tests.Generation.Randomizers.Alignments
{
    [TestFixture]
    public class NonEvilAlignmentTests
    {
        private IAlignmentRandomizer alignmentRandomizer;
        private Mock<IDice> mockDice;

        [SetUp]
        public void Setup()
        {
            mockDice = new Mock<IDice>();
            alignmentRandomizer = new NonEvilAlignment(mockDice.Object);
        }

        [Test]
        public void ForcedNotEvil()
        {
            mockDice.SetupSequence(d => d.Percentile(1, 0)).Returns(100).Returns(50);

            var alignment = alignmentRandomizer.Randomize();
            Assert.That(alignment.Goodness, Is.EqualTo(AlignmentConstants.Neutral));
            mockDice.Verify(d => d.Percentile(1, 0), Times.Exactly(2));
        }
    }
}