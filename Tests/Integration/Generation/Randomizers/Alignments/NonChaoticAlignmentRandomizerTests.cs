using System.Linq;
using Ninject;
using NPCGen.Core.Data.Alignments;
using NPCGen.Core.Generation.Randomizers.Alignments;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Generation.Randomizers.Alignments
{
    [TestFixture]
    public class NonChaoticAlignmentRandomizerTests : IntegrationTest
    {
        [Inject]
        public NonChaoticAlignmentRandomizer AlignmentRandomizer { get; set; }

        [SetUp]
        public void Setup()
        {
            StartTest();
        }

        [Test]
        public void NonChaoticAlignmentRandomizerReturnsAlignment()
        {
            while (TestShouldKeepRunning())
            {
                var alignment = AlignmentRandomizer.Randomize();
                Assert.That(alignment, Is.Not.Null);
            }
        }

        [Test]
        public void NonChaoticAlignmentRandomizerReturnsAlignmentWithGoodness()
        {
            var goodnesses = AlignmentConstants.GetGoodnesses();

            while (TestShouldKeepRunning())
            {
                var alignment = AlignmentRandomizer.Randomize();
                Assert.That(goodnesses.Contains(alignment.Goodness), Is.True);
            }
        }

        [Test]
        public void NonChaoticAlignmentRandomizerReturnsAlignmentWithNeutralness()
        {
            var lawfulnesses = AlignmentConstants.GetLawfulnesses();

            while (TestShouldKeepRunning())
            {
                var alignment = AlignmentRandomizer.Randomize();
                Assert.That(lawfulnesses.Contains(alignment.Lawfulness), Is.True);
            }
        }

        [Test]
        public void NonChaoticAlignmentRandomizerAlwaysReturnsNonChaoticAlignment()
        {
            while (TestShouldKeepRunning())
            {
                var alignment = AlignmentRandomizer.Randomize();
                Assert.That(alignment.IsChaotic(), Is.False);
            }
        }
    }
}