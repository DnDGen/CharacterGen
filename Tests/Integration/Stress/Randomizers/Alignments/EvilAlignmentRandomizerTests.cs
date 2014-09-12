using System;
using System.Collections.Generic;
using Ninject;
using NPCGen.Common.Alignments;
using NPCGen.Generators.Interfaces.Randomizers.Alignments;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Stress.Randomizers.Alignments
{
    [TestFixture]
    public class EvilAlignmentRandomizerTests : StressTests
    {
        [Inject, Named(AlignmentRandomizerTypeConstants.Evil)]
        public override IAlignmentRandomizer AlignmentRandomizer { get; set; }

        private IEnumerable<String> lawfulnesses;

        [SetUp]
        public void Setup()
        {
            lawfulnesses = AlignmentConstants.GetLawfulnesses();
        }

        [Test]
        public override void Stress()
        {
            do MakeAssertions();
            while (TestShouldKeepRunning());

            AssertIterations();
        }

        private void MakeAssertions()
        {
            var alignment = AlignmentRandomizer.Randomize();
            Assert.That(alignment.Goodness, Is.EqualTo(AlignmentConstants.Evil));
            Assert.That(lawfulnesses, Contains.Item(alignment.Lawfulness));
        }
    }
}