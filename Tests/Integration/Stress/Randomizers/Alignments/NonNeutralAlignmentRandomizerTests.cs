using System;
using System.Collections.Generic;
using Ninject;
using NPCGen.Common.Alignments;
using NPCGen.Generators.Interfaces.Randomizers.Alignments;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Stress.Randomizers.Alignments
{
    [TestFixture]
    public class NonNeutralAlignmentRandomizerTests : StressTests
    {
        [Inject, Named(AlignmentRandomizerTypeConstants.NonNeutral)]
        public override IAlignmentRandomizer AlignmentRandomizer { get; set; }

        private IEnumerable<String> goodnesses;
        private IEnumerable<String> lawfulnesses;

        [SetUp]
        public void Setup()
        {
            goodnesses = AlignmentConstants.GetGoodnesses();
            lawfulnesses = AlignmentConstants.GetLawfulnesses();
        }

        protected override void MakeAssertions()
        {
            var alignment = AlignmentRandomizer.Randomize();
            Assert.That(goodnesses, Contains.Item(alignment.Goodness));
            Assert.That(lawfulnesses, Contains.Item(alignment.Lawfulness));
            Assert.That(alignment.Lawfulness, Is.Not.EqualTo(AlignmentConstants.Neutral));
            Assert.That(alignment.Goodness, Is.Not.EqualTo(AlignmentConstants.Neutral));
        }
    }
}