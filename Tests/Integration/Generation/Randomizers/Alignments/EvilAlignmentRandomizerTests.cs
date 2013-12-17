using System.Linq;
using Ninject;
using NPCGen.Core.Data.Alignments;
using NPCGen.Core.Generation.Randomizers.Alignments;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Generation.Randomizers.Alignments
{
    [TestFixture]
    public class EvilAlignmentRandomizerTests : IntegrationTest
    {
        [Inject]
        public EvilAlignmentRandomizer AlignmentRandomizer { get; set; }

        [Test]
        public void EvilAlignmentRandomizerReturnsAlignment()
        {
            for (var i = 0; i < ConfidenceLevel; i++)
            {
                var alignment = AlignmentRandomizer.Randomize();
                Assert.That(alignment, Is.Not.Null);
            }
        }

        [Test]
        public void EvilAlignmentRandomizerReturnsAlignmentWithGoodness()
        {
            var goodnesses = AlignmentConstants.GetGoodnesses();

            for (var i = 0; i < ConfidenceLevel; i++)
            {
                var alignment = AlignmentRandomizer.Randomize();
                Assert.That(goodnesses.Contains(alignment.Goodness), Is.True);
            }
        }

        [Test]
        public void EvilAlignmentRandomizerReturnsAlignmentWithLawfulness()
        {
            var lawfulnesses = AlignmentConstants.GetLawfulnesses();

            for (var i = 0; i < ConfidenceLevel; i++)
            {
                var alignment = AlignmentRandomizer.Randomize();
                Assert.That(lawfulnesses.Contains(alignment.Lawfulness), Is.True);
            }
        }

        [Test]
        public void EvilAlignmentRandomizerAlwaysReturnsEvilAlignment()
        {
            for (var i = 0; i < ConfidenceLevel; i++)
            {
                var alignment = AlignmentRandomizer.Randomize();
                Assert.That(alignment.IsEvil(), Is.True);
            }
        }
    }
}