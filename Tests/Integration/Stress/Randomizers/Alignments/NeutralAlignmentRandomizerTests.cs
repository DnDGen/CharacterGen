using System;
using System.Collections.Generic;
using Ninject;
using NPCGen.Common.Alignments;
using NPCGen.Generators.Interfaces.Randomizers.Alignments;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Stress.Randomizers.Alignments
{
    [TestFixture]
    public class NeutralAlignmentRandomizerTests : StressTests
    {
        [Inject, Named(AlignmentRandomizerTypeConstants.Neutral)]
        public override IAlignmentRandomizer AlignmentRandomizer { get; set; }

        private IEnumerable<String> goodnesses;
        private IEnumerable<String> lawfulnesses;

        [SetUp]
        public void Setup()
        {
            goodnesses = AlignmentConstants.GetGoodnesses();
            lawfulnesses = AlignmentConstants.GetLawfulnesses();
        }

        [TestCase("NeutralAlignmentRandomizer")]
        public override void Stress(String stressSubject)
        {
            Stress();
        }

        protected override void MakeAssertions()
        {
            var alignment = AlignmentRandomizer.Randomize();
            Assert.That(goodnesses, Contains.Item(alignment.Goodness));
            Assert.That(lawfulnesses, Contains.Item(alignment.Lawfulness));
        }

        [Test]
        public void NeutralGoodnessHappens()
        {
            var alignment = new Alignment();

            do alignment = AlignmentRandomizer.Randomize();
            while (TestShouldKeepRunning() && alignment.Goodness != AlignmentConstants.Neutral);

            Assert.That(alignment.Goodness, Is.EqualTo(AlignmentConstants.Neutral));
        }

        [Test]
        public void NeutralLawfulnessHappens()
        {
            var alignment = new Alignment();

            do alignment = AlignmentRandomizer.Randomize();
            while (TestShouldKeepRunning() && alignment.Lawfulness != AlignmentConstants.Neutral);

            Assert.That(alignment.Lawfulness, Is.EqualTo(AlignmentConstants.Neutral));
        }

        [Test]
        public void TrueNeutralHappens()
        {
            var alignment = new Alignment();

            do alignment = AlignmentRandomizer.Randomize();
            while (TestShouldKeepRunning() && (alignment.Lawfulness != AlignmentConstants.Neutral || alignment.Goodness != AlignmentConstants.Neutral));

            Assert.That(alignment.Lawfulness, Is.EqualTo(AlignmentConstants.Neutral));
            Assert.That(alignment.Goodness, Is.EqualTo(AlignmentConstants.Neutral));
        }
    }
}