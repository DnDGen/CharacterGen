using System;
using System.Collections.Generic;
using Ninject;
using NPCGen.Common.Alignments;
using NPCGen.Generators.Interfaces.Randomizers.Alignments;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Stress.Randomizers.Alignments
{
    [TestFixture]
    public class ChaoticAlignmentRandomizerTests : StressTests
    {
        [Inject, Named(AlignmentRandomizerTypeConstants.Chaotic)]
        public override IAlignmentRandomizer AlignmentRandomizer { get; set; }

        private IEnumerable<String> goodnesses;

        [SetUp]
        public void Setup()
        {
            goodnesses = AlignmentConstants.GetGoodnesses();
        }

        protected override void MakeAssertions()
        {
            var alignment = AlignmentRandomizer.Randomize();
            Assert.That(goodnesses, Contains.Item(alignment.Goodness));
            Assert.That(alignment.Lawfulness, Is.EqualTo(AlignmentConstants.Chaotic));
        }
    }
}