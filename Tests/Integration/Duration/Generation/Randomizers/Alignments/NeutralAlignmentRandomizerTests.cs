using Ninject;
using NPCGen.Core.Generation.Randomizers.Alignments;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Duration.Generation.Randomizers.Alignments
{
    [TestFixture]
    public class NeutralAlignmentRandomizerTests : DurationTest
    {
        public NeutralAlignmentRandomizerTests()
        {
            AlignmentRandomizer = kernel.Get<NeutralAlignmentRandomizer>();
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
        public void NeutralAlignmentRandomization()
        {
            AlignmentRandomizer.Randomize();
            AssertDuration();
        }
    }
}