using System;
using Ninject;
using NPCGen.Common.Alignments;
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

        private Alignment setAlignment;

        [SetUp]
        public void Setup()
        {
            setAlignment = new Alignment();
            SetAlignmentRandomizer.SetAlignment = setAlignment;
        }

        protected override void MakeAssertions()
        {
            setAlignment.Goodness = Random.Next().ToString();
            setAlignment.Lawfulness = Random.Next().ToString();

            var alignment = SetAlignmentRandomizer.Randomize();
            Assert.That(alignment, Is.EqualTo(setAlignment));
        }
    }
}