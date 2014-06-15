using Ninject;
using NPCGen.Common.Alignments;
using NPCGen.Generators.Randomizers.Alignments;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Stress.Randomizers.Alignments
{
    [TestFixture]
    public class SetAlignmentRandomizerTests : StressTests
    {
        [Inject]
        public SetAlignmentRandomizer AlignmentRandomizer { get; set; }

        [Test]
        public void SetAlignmentRandomizerAlwaysReturnsSetAlignment()
        {
            while (TestShouldKeepRunning())
            {
                AlignmentRandomizer.Alignment = GetNewInstanceOf<Alignment>();
                var alignment = AlignmentRandomizer.Randomize();
                Assert.That(alignment, Is.EqualTo(AlignmentRandomizer.Alignment));
            }

            AssertIterations();
        }
    }
}