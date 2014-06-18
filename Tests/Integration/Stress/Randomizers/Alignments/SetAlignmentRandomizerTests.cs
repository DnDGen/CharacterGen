using Ninject;
using NPCGen.Generators.Randomizers.Alignments;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Stress.Randomizers.Alignments
{
    [TestFixture]
    public class SetAlignmentRandomizerTests : StressTests
    {
        [Inject]
        public SetAlignmentRandomizer SetAlignmentRandomizer { get; set; }

        protected override void MakeAssertions()
        {
            var dependentData = GetNewDependentData();
            SetAlignmentRandomizer.Alignment = dependentData.Alignment;

            var alignment = AlignmentRandomizer.Randomize();
            Assert.That(alignment, Is.EqualTo(dependentData.Alignment));
        }
    }
}