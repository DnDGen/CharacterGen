using CharacterGen.Alignments;
using CharacterGen.Randomizers.Alignments;
using NUnit.Framework;

namespace CharacterGen.Tests.Integration.Stress.Randomizers.Alignments
{
    [TestFixture]
    public class NonChaoticAlignmentRandomizerTests : StressTests
    {
        [SetUp]
        public void Setup()
        {
            AlignmentRandomizer = GetNewInstanceOf<IAlignmentRandomizer>(AlignmentRandomizerTypeConstants.NonChaotic);
        }

        [Test]
        public void StressAlignment()
        {
            Stress(AssertAlignment);
        }

        protected void AssertAlignment()
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