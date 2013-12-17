using System.Linq;
using Ninject;
using NPCGen.Core.Data.Alignments;
using NPCGen.Core.Generation.Randomizers.Alignments;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Generation.Randomizers.Alignments
{
    [TestFixture]
    public class GoodAlignmentRandomizerTests : IntegrationTest
    {
        [Inject]
        public GoodAlignmentRandomizer AlignmentRandomizer { get; set; }

        [SetUp]
        public void Setup()
        {
            StartTest();
        }

        [Test]
        public void GoodAlignmentRandomizerReturnsAlignment()
        {
            while (TestShouldKeepRunning())
            {
                var alignment = AlignmentRandomizer.Randomize();
                Assert.That(alignment, Is.Not.Null);
            }
        }

        [Test]
        public void GoodAlignmentRandomizerReturnsAlignmentWithGoodness()
        {
            var goodnesses = AlignmentConstants.GetGoodnesses();

            while (TestShouldKeepRunning())
            {
                var alignment = AlignmentRandomizer.Randomize();
                Assert.That(goodnesses.Contains(alignment.Goodness), Is.True);
            }
        }

        [Test]
        public void GoodAlignmentRandomizerReturnsAlignmentWithLawfulness()
        {
            var lawfulnesses = AlignmentConstants.GetLawfulnesses();

            while (TestShouldKeepRunning())
            {
                var alignment = AlignmentRandomizer.Randomize();
                Assert.That(lawfulnesses.Contains(alignment.Lawfulness), Is.True);
            }
        }

        [Test]
        public void GoodAlignmentRandomizerAlwaysReturnsGoodAlignment()
        {
            while (TestShouldKeepRunning())
            {
                var alignment = AlignmentRandomizer.Randomize();
                Assert.That(alignment.IsGood(), Is.True);
            }
        }
    }
}