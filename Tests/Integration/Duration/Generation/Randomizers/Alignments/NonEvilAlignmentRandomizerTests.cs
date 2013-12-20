using Ninject;
using NPCGen.Core.Generation.Randomizers.Alignments;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Duration.Generation.Randomizers.Alignments
{
    [TestFixture]
    public class NonEvilAlignmentRandomizerTests : DurationTest
    {
        public NonEvilAlignmentRandomizerTests()
        {
            AlignmentRandomizer = kernel.Get<NonEvilAlignmentRandomizer>();
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
        public void NonEvilAlignmentRandomization()
        {
            AlignmentRandomizer.Randomize();
            AssertDuration();
        }
    }
}