using NPCGen.Core.Data.Alignments;
using NPCGen.Core.Generation.Randomizers.Alignments;
using NUnit.Framework;

namespace NPCGen.Tests.Generation.Randomizers.Alignments
{
    [TestFixture]
    public class SetAlignmentRandomizerTests
    {
        [Test]
        public void ReturnSetAlignment()
        {
            var alignmentRandomizer = new SetAlignmentRandomizer();
            alignmentRandomizer.Alignment.Goodness = AlignmentConstants.Good;
            alignmentRandomizer.Alignment.Lawfulness = AlignmentConstants.Lawful;

            var alignment = alignmentRandomizer.Randomize();
            Assert.That(alignment, Is.EqualTo(alignmentRandomizer.Alignment));
        }
    }
}