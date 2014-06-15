using System.Linq;
using Ninject;
using NPCGen.Common.Alignments;
using NPCGen.Generators.Randomizers.Alignments;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Stress.Randomizers.Alignments
{
    [TestFixture]
    public class LawfulAlignmentRandomizerTests : StressTests
    {
        [Inject]
        public LawfulAlignmentRandomizer AlignmentRandomizer { get; set; }

        [Test]
        public void LawfulAlignmentRandomizerReturnsAlignment()
        {
            while (TestShouldKeepRunning())
            {
                var alignment = AlignmentRandomizer.Randomize();
                Assert.That(alignment, Is.Not.Null);
            }

            AssertIterations();
        }

        [Test]
        public void LawfulAlignmentRandomizerReturnsAlignmentWithGoodness()
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
        public void LawfulAlignmentRandomizerReturnsAlignmentWithLawfulness()
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
        public void LawfulAlignmentRandomizerAlwaysReturnsLawfulAlignment()
        {
            while (TestShouldKeepRunning())
            {
                var alignment = AlignmentRandomizer.Randomize();
                Assert.That(alignment.IsLawful(), Is.True);
            }

            AssertIterations();
        }
    }
}