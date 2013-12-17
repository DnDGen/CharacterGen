using System.Linq;
using Ninject;
using NPCGen.Core.Data.Alignments;
using NPCGen.Core.Generation.Randomizers.Alignments;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Generation.Randomizers.Alignments
{
    [TestFixture]
    public class NonNeutralAlignmentRandomizerTests : IntegrationTest
    {
        [Inject]
        public NonNeutralAlignmentRandomizer AlignmentRandomizer { get; set; }

        [Test]
        public void NonNeutralAlignmentRandomizerReturnsAlignment()
        {
            for (var i = 0; i < ConfidenceLevel; i++)
            {
                var alignment = AlignmentRandomizer.Randomize();
                Assert.That(alignment, Is.Not.Null);
            }
        }

        [Test]
        public void NonNeutralAlignmentRandomizerReturnsAlignmentWithGoodness()
        {
            var goodnesses = AlignmentConstants.GetGoodnesses();

            for (var i = 0; i < ConfidenceLevel; i++)
            {
                var alignment = AlignmentRandomizer.Randomize();
                Assert.That(goodnesses.Contains(alignment.Goodness), Is.True);
            }
        }

        [Test]
        public void NonNeutralAlignmentRandomizerReturnsAlignmentWithNeutralness()
        {
            var lawfulnesses = AlignmentConstants.GetLawfulnesses();

            for (var i = 0; i < ConfidenceLevel; i++)
            {
                var alignment = AlignmentRandomizer.Randomize();
                Assert.That(lawfulnesses.Contains(alignment.Lawfulness), Is.True);
            }
        }

        [Test]
        public void NonNeutralAlignmentRandomizerAlwaysReturnsNonNeutralAlignment()
        {
            for (var i = 0; i < ConfidenceLevel; i++)
            {
                var alignment = AlignmentRandomizer.Randomize();
                Assert.That(alignment.IsNeutral(), Is.False);
            }
        }
    }
}
