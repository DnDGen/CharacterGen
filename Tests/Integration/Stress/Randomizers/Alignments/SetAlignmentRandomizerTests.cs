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
        [Inject]
        public Random Random { get; set; }

        protected override void MakeAssertions()
        {
            SetAlignmentRandomizer.SetAlignment.Goodness = Random.Next().ToString();
            SetAlignmentRandomizer.SetAlignment.Lawfulness = Random.Next().ToString();

            var alignment = SetAlignmentRandomizer.Randomize();
            Assert.That(alignment, Is.EqualTo(SetAlignmentRandomizer.SetAlignment));
        }
    }
}