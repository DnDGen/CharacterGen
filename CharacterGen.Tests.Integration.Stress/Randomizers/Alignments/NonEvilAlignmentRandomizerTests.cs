using CharacterGen.Alignments;
using CharacterGen.Randomizers.Alignments;
using NUnit.Framework;

namespace CharacterGen.Tests.Integration.Stress.Randomizers.Alignments
{
    [TestFixture]
    public class NonEvilAlignmentRandomizerTests : StressTests
    {
        [SetUp]
        public void Setup()
        {
            AlignmentRandomizer = GetNewInstanceOf<IAlignmentRandomizer>(AlignmentRandomizerTypeConstants.NonEvil);
        }

        [TestCase("Non-Evil Alignment Randomizer")]
        public override void Stress(string stressSubject)
        {
            Stress();
        }

        protected override void MakeAssertions()
        {
            var alignment = AlignmentRandomizer.Randomize();
            Assert.That(alignment.Goodness, Is.EqualTo(AlignmentConstants.Good)
                .Or.EqualTo(AlignmentConstants.Neutral));
            Assert.That(alignment.Lawfulness, Is.EqualTo(AlignmentConstants.Lawful)
                .Or.EqualTo(AlignmentConstants.Neutral)
                .Or.EqualTo(AlignmentConstants.Chaotic));
        }
    }
}