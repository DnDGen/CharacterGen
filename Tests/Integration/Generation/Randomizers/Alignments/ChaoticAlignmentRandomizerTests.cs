using System.Linq;
using Ninject;
using NPCGen.Core.Data.Alignments;
using NPCGen.Core.Generation.Randomizers.Alignments;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Generation.Randomizers.Alignments
{
    [TestFixture]
    public class ChaoticAlignmentRandomizerTests : IntegrationTest
    {
        [Inject]
        public ChaoticAlignmentRandomizer AlignmentRandomizer { get; set; }

        [SetUp]
        public void Setup()
        {
            StartTest();
        }

        [Test]
        public void ChaoticAlignmentRandomizerReturnsAlignment()
        {
            while (TestShouldKeepRunning())
            {
                var alignment = AlignmentRandomizer.Randomize();
                Assert.That(alignment, Is.Not.Null);
            }
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
        }

        [Test]
        public void ChaoticAlignmentRandomizerAlwaysReturnsChaoticAlignment()
        {
            while (TestShouldKeepRunning())
            {
                var alignment = AlignmentRandomizer.Randomize();
                Assert.That(alignment.IsChaotic(), Is.True);
            }
        }
    }
}