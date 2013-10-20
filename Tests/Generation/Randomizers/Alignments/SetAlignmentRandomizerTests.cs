using NPCGen.Core.Data.Alignments;
using NPCGen.Core.Generation.Randomizers.Alignments;
using NUnit.Framework;
using System.Linq;

namespace NPCGen.Tests.Generation.Randomizers.Alignments
{
    [TestFixture]
    public class SetAlignmentRandomizerTests
    {
        private SetAlignmentRandomizer randomizer;

        [SetUp]
        public void Setup()
        {
            randomizer = new SetAlignmentRandomizer();
            randomizer.Alignment.Goodness = AlignmentConstants.Good;
            randomizer.Alignment.Lawfulness = AlignmentConstants.Lawful;
        }

        [Test]
        public void ReturnSetAlignment()
        {
            var alignment = randomizer.Randomize();
            Assert.That(alignment, Is.EqualTo(randomizer.Alignment));
        }

        [Test]
        public void GetAllPossibleResultsReturnsOnlyTheSetValue()
        {
            var alignments = randomizer.GetAllPossibleResults();
            Assert.That(alignments.Any(a => a == randomizer.Alignment), Is.True);
            Assert.That(alignments.Count(), Is.EqualTo(1));
        }
    }
}