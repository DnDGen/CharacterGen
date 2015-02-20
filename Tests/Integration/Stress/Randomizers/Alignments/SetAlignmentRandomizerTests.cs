using System;
using Ninject;
using NPCGen.Generators.Interfaces.Randomizers.Alignments;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Stress.Randomizers.Alignments
{
    [TestFixture]
    public class SetAlignmentRandomizerTests : StressTests
    {
        [Inject]
        public ISetAlignmentRandomizer SetAlignmentRandomizer { get; set; }

        [TestCase("SetAlignmentRandomizer")]
        public override void Stress(String stressSubject)
        {
            Stress();
        }

        protected override void MakeAssertions()
        {
            SetAlignmentRandomizer.SetAlignment.Goodness = Guid.NewGuid().ToString();
            SetAlignmentRandomizer.SetAlignment.Lawfulness = Guid.NewGuid().ToString();

            var alignment = SetAlignmentRandomizer.Randomize();
            Assert.That(alignment, Is.EqualTo(SetAlignmentRandomizer.SetAlignment));
        }
    }
}