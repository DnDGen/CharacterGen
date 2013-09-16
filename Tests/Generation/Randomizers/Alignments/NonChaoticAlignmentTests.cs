using D20Dice.Dice;
using Moq;
using NPCGen.Core.Data.Alignments;
using NPCGen.Core.Generation.Randomizers.Alignments;
using NUnit.Framework;

namespace NPCGen.Tests.Generation.Randomizers.Alignments
{
    [TestFixture]
    public class NonChaoticAlignmentTests
    {
        private IAlignmentRandomizer alignmentRandomizer;
        private Mock<IDice> mockDice;

        [SetUp]
        public void Setup()
        {
            mockDice = new Mock<IDice>();
            alignmentRandomizer = new NonChaoticAlignment(mockDice.Object);
        }

        [Test]
        public void ForcedNotChaotic()
        {
            mockDice.SetupSequence(d => d.d3(1, 0)).Returns(1).Returns(2);

            var alignment = alignmentRandomizer.Randomize();
            Assert.That(alignment.Lawfulness, Is.EqualTo(AlignmentConstants.Neutral));
            mockDice.Verify(d => d.d3(1, 0), Times.Exactly(2));
        }
    }
}