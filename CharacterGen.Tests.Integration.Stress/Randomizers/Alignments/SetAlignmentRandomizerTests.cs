using CharacterGen.Randomizers.Alignments;
using Ninject;
using NUnit.Framework;
using System;

namespace CharacterGen.Tests.Integration.Stress.Randomizers.Alignments
{
    [TestFixture]
    public class SetAlignmentRandomizerTests : StressTests
    {
        [Inject]
        public ISetAlignmentRandomizer SetAlignmentRandomizer { get; set; }

        [Test]
        public void StressAlignment()
        {
            Stress(AssertAlignment);
        }

        protected void AssertAlignment()
        {
            SetAlignmentRandomizer.SetAlignment.Goodness = Guid.NewGuid().ToString();
            SetAlignmentRandomizer.SetAlignment.Lawfulness = Guid.NewGuid().ToString();

            var alignment = SetAlignmentRandomizer.Randomize();
            Assert.That(alignment, Is.EqualTo(SetAlignmentRandomizer.SetAlignment));
        }
    }
}