using Ninject;
using NPCGen.Core.Generation.Randomizers.Alignments;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Duration.Generation.Randomizers.Alignments
{
    [TestFixture]
    public class NonChaoticAlignmentRandomizerTests : DurationTest
    {
        public NonChaoticAlignmentRandomizerTests()
        {
            AlignmentRandomizer = kernel.Get<NonChaoticAlignmentRandomizer>();
        }

        [SetUp]
        public void Setup()
        {
            StartTest();
        }

        [TearDown]
        public void TearDown()
        {
            StopTest();
        }

        [Test]
        public void NonChaoticAlignmentRandomization()
        {
            AlignmentRandomizer.Randomize();
            AssertDuration();
        }
    }
}