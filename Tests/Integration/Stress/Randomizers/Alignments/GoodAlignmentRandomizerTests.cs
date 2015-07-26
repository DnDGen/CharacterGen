using System;
using System.Collections.Generic;
using Ninject;
using CharacterGen.Common.Alignments;
using CharacterGen.Generators.Randomizers.Alignments;
using NUnit.Framework;

namespace CharacterGen.Tests.Integration.Stress.Randomizers.Alignments
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

        [TestCase("GoodAlignmentRandomizer")]
        public override void Stress(String stressSubject)
        {
            Stress();
        }

        protected override void MakeAssertions()
        {
            var alignment = AlignmentRandomizer.Randomize();
            Assert.That(alignment.Goodness, Is.EqualTo(AlignmentConstants.Good));
            Assert.That(lawfulnesses, Contains.Item(alignment.Lawfulness));
        }
    }
}