using System;
using System.Collections.Generic;
using Ninject;
using NPCGen.Common.Alignments;
using NPCGen.Generators.Interfaces.Randomizers.Alignments;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Stress.Randomizers.Alignments
{
    [TestFixture]
    public class GoodAlignmentRandomizerTests : StressTests
    {
        [Inject, Named(AlignmentRandomizerTypeConstants.Good)]
        public override IAlignmentRandomizer AlignmentRandomizer { get; set; }

        private IEnumerable<String> lawfulnesses;

        [SetUp]
        public void Setup()
        {
            lawfulnesses = AlignmentConstants.GetLawfulnesses();
        }

        protected override void MakeAssertions()
        {
            var alignment = AlignmentRandomizer.Randomize();
            Assert.That(alignment.Goodness, Is.EqualTo(AlignmentConstants.Good));
            Assert.That(lawfulnesses, Contains.Item(alignment.Lawfulness));
        }
    }
}