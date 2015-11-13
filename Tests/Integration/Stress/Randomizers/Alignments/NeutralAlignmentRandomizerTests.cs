using CharacterGen.Common.Alignments;
using CharacterGen.Generators.Randomizers.Alignments;
using Ninject;
using NUnit.Framework;
using System;

namespace CharacterGen.Tests.Integration.Stress.Randomizers.Alignments
{
    [TestFixture]
    public class NeutralAlignmentRandomizerTests : StressTests
    {
        [Inject, Named(AlignmentRandomizerTypeConstants.Neutral)]
        public override IAlignmentRandomizer AlignmentRandomizer { get; set; }

        [TestCase("NeutralAlignmentRandomizer")]
        public override void Stress(String stressSubject)
        {
            Stress();
        }

        protected override void MakeAssertions()
        {
            var alignment = AlignmentRandomizer.Randomize();
            Assert.That(alignment.Goodness, Is.EqualTo(AlignmentConstants.Good)
                .Or.EqualTo(AlignmentConstants.Neutral)
                .Or.EqualTo(AlignmentConstants.Evil));
            Assert.That(alignment.Lawfulness, Is.EqualTo(AlignmentConstants.Lawful)
                .Or.EqualTo(AlignmentConstants.Neutral)
                .Or.EqualTo(AlignmentConstants.Chaotic));
        }

        [Test]
        public void NeutralGoodnessHappens()
        {
            var alignment = GenerateOrFail(
                () => AlignmentRandomizer.Randomize(),
                a => a.Goodness == AlignmentConstants.Neutral);

            Assert.That(alignment.Goodness, Is.EqualTo(AlignmentConstants.Neutral));
        }

        [Test]
        public void NeutralLawfulnessHappens()
        {
            var alignment = GenerateOrFail(
                () => AlignmentRandomizer.Randomize(),
                a => a.Lawfulness == AlignmentConstants.Neutral);

            Assert.That(alignment.Lawfulness, Is.EqualTo(AlignmentConstants.Neutral));
        }

        [Test]
        public void TrueNeutralHappens()
        {
            var alignment = GenerateOrFail(
                () => AlignmentRandomizer.Randomize(),
                a => a.Goodness == AlignmentConstants.Neutral && a.Lawfulness == AlignmentConstants.Neutral);

            Assert.That(alignment.Lawfulness, Is.EqualTo(AlignmentConstants.Neutral));
            Assert.That(alignment.Goodness, Is.EqualTo(AlignmentConstants.Neutral));
        }
    }
}