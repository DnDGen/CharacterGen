using NUnit.Framework;
using System.Linq;
using NPCGen.Generators.Randomizers.Alignments;
using NPCGen.Common.Alignments;

namespace NPCGen.Tests.Unit.Generators.Randomizers.Alignments
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