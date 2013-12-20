using Ninject;
using NPCGen.Core.Generation.Randomizers.Alignments;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Duration.Generation.Randomizers.Alignments
{
    [TestFixture]
    public class NonGoodAlignmentRandomizerTests : DurationTest
    {
        public NonGoodAlignmentRandomizerTests()
        {
            AlignmentRandomizer = kernel.Get<NonGoodAlignmentRandomizer>();
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
        public void NonGoodAlignmentRandomization()
        {
            AlignmentRandomizer.Randomize();
            AssertDuration();
        }
    }
}