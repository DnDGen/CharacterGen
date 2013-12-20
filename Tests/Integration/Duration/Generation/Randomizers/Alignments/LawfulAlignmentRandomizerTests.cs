using Ninject;
using NPCGen.Core.Generation.Randomizers.Alignments;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Duration.Generation.Randomizers.Alignments
{
    [TestFixture]
    public class LawfulAlignmentRandomizerTests : DurationTest
    {
        public LawfulAlignmentRandomizerTests()
        {
            AlignmentRandomizer = kernel.Get<LawfulAlignmentRandomizer>();
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
        public void LawfulAlignmentRandomization()
        {
            AlignmentRandomizer.Randomize();
            AssertDuration();
        }
    }
}