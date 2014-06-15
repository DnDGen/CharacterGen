using System.Linq;
using Ninject;
using NPCGen.Common.Alignments;
using NPCGen.Generators.Randomizers.Alignments;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Stress.Randomizers.Alignments
{
    [TestFixture]
    public class ChaoticAlignmentRandomizerTests : StressTests
    {
        [Inject]
        public ChaoticAlignmentRandomizer AlignmentRandomizer { get; set; }

        [Test]
        public void ChaoticAlignmentRandomizerReturnsAlignment()
        {
            while (TestShouldKeepRunning())
            {
                var alignment = AlignmentRandomizer.Randomize();
                Assert.That(alignment, Is.Not.Null);
            }

            AssertIterations();
        }

        [Test]
        public void ChaoticAlignmentRandomizerReturnsAlignmentWithGoodness()
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
        public void ChaoticAlignmentRandomizerReturnsAlignmentWithLawfulness()
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
        public void ChaoticAlignmentRandomizerAlwaysReturnsChaoticAlignment()
        {
            while (TestShouldKeepRunning())
            {
                var alignment = AlignmentRandomizer.Randomize();
                Assert.That(alignment.IsChaotic(), Is.True);
            }

            AssertIterations();
        }
    }
}