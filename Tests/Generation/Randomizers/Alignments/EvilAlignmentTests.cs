using D20Dice.Dice;
using Moq;
using NPCGen.Core.Data.Alignments;
using NPCGen.Core.Generation.Randomizers.Alignments;
using NUnit.Framework;

namespace NPCGen.Tests.Generation.Randomizers.Alignments
{
    [TestFixture]
    public class EvilAlignmentTests
    {
        private IAlignmentRandomizer alignmentRandomizer;
        private Mock<IDice> mockDice;

        [SetUp]
        public void Setup()
        {
            mockDice = new Mock<IDice>();
            alignmentRandomizer = new EvilAlignment(mockDice.Object);
        }

        [Test]
        public void ForcedEvil()
        {
            var alignment = alignmentRandomizer.Randomize();
            Assert.That(alignment.Goodness, Is.EqualTo(AlignmentConstants.Evil));
        }
    }
}