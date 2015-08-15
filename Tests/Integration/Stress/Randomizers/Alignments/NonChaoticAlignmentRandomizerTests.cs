using CharacterGen.Common.Alignments;
using CharacterGen.Generators.Randomizers.Alignments;
using Ninject;
using NUnit.Framework;
using System;

namespace CharacterGen.Tests.Integration.Stress.Randomizers.Alignments
{
    [TestFixture]
    public class NonChaoticAlignmentRandomizerTests : StressTests
    {
        [Inject, Named(AlignmentRandomizerTypeConstants.NonChaotic)]
        public override IAlignmentRandomizer AlignmentRandomizer { get; set; }

        [TestCase("NonChaoticAlignmentRandomizer")]
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
                .Or.EqualTo(AlignmentConstants.Neutral));
        }
    }
}