using System.Linq;
using Ninject;
using NPCGen.Common.Alignments;
using NPCGen.Generators.Randomizers.Alignments;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Stress.Randomizers.Alignments
{
    [TestFixture]
    public class NonGoodAlignmentRandomizerTests : StressTests
    {
        [Inject]
        public NonGoodAlignmentRandomizer AlignmentRandomizer { get; set; }

        [Test]
        public void NonGoodAlignmentRandomizerReturnsAlignment()
        {
            while (TestShouldKeepRunning())
            {
                var alignment = AlignmentRandomizer.Randomize();
                Assert.That(alignment, Is.Not.Null);
            }

            AssertIterations();
        }

        [Test]
        public void NonGoodAlignmentRandomizerReturnsAlignmentWithGoodness()
        {
            var goodnesses = AlignmentConstants.GetGoodnesses();

            while (TestShouldKeepRunning())
            {
                var alignment = AlignmentRandomizer.Randomize();
                Assert.That(goodnesses.Contains(alignment.Goodness), Is.True);
            }

            AssertIterations();
        }

        [Test]
        public void NonGoodAlignmentRandomizerReturnsAlignmentWithNeutralness()
        {
            var lawfulnesses = AlignmentConstants.GetLawfulnesses();

            while (TestShouldKeepRunning())
            {
                var alignment = AlignmentRandomizer.Randomize();
                Assert.That(lawfulnesses.Contains(alignment.Lawfulness), Is.True);
            }

            AssertIterations();
        }

        [Test]
        public void NonGoodAlignmentRandomizerAlwaysReturnsNonGoodAlignment()
        {
            while (TestShouldKeepRunning())
            {
                var alignment = AlignmentRandomizer.Randomize();
                Assert.That(alignment.IsGood(), Is.False);
            }

            AssertIterations();
        }
    }
}