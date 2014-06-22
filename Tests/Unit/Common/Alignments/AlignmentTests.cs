using System;
using NPCGen.Common.Alignments;
using NUnit.Framework;

namespace NPCGen.Tests.Unit.Common.Alignments
{
    [TestFixture]
    public class AlignmentTests
    {
        private Alignment alignment;

        [SetUp]
        public void Setup()
        {
            alignment = new Alignment();
        }

        [Test]
        public void AlignmentInitialized()
        {
            Assert.That(alignment.Goodness, Is.Empty);
            Assert.That(alignment.Lawfulness, Is.Empty);
        }

        [Test]
        public void AlignmentIsNeutralIfLawfulnessIsNeutral()
        {
            alignment.Lawfulness = AlignmentConstants.Neutral;
            alignment.Goodness = AlignmentConstants.Good;
            Assert.That(alignment.IsNeutral(), Is.True);

            alignment.Goodness = AlignmentConstants.Neutral;
            Assert.That(alignment.IsNeutral(), Is.True);

            alignment.Goodness = AlignmentConstants.Evil;
            Assert.That(alignment.IsNeutral(), Is.True);
        }

        [Test]
        public void AlignmentIsNeutralIfGoodnessIsNeutral()
        {
            alignment.Goodness = AlignmentConstants.Neutral;
            alignment.Lawfulness = AlignmentConstants.Lawful;
            Assert.That(alignment.IsNeutral(), Is.True);

            alignment.Lawfulness = AlignmentConstants.Neutral;
            Assert.That(alignment.IsNeutral(), Is.True);

            alignment.Lawfulness = AlignmentConstants.Chaotic;
            Assert.That(alignment.IsNeutral(), Is.True);
        }

        [Test]
        public void LawfulGoodIsNotNeutral()
        {
            alignment.Lawfulness = AlignmentConstants.Lawful;
            alignment.Goodness = AlignmentConstants.Good;
            Assert.That(alignment.IsNeutral(), Is.False);
        }

        [Test]
        public void ChaoticGoodIsNotNeutral()
        {
            alignment.Lawfulness = AlignmentConstants.Chaotic;
            alignment.Goodness = AlignmentConstants.Good;
            Assert.That(alignment.IsNeutral(), Is.False);
        }

        [Test]
        public void LawfulEvilIsNotNeutral()
        {
            alignment.Lawfulness = AlignmentConstants.Lawful;
            alignment.Goodness = AlignmentConstants.Evil;
            Assert.That(alignment.IsNeutral(), Is.False);
        }

        [Test]
        public void ChaoticEvilIsNotNeutral()
        {
            alignment.Lawfulness = AlignmentConstants.Chaotic;
            alignment.Goodness = AlignmentConstants.Evil;
            Assert.That(alignment.IsNeutral(), Is.False);
        }

        [Test]
        public void AlignmentIsLawfulIfLawfulnessIsLawful()
        {
            alignment.Lawfulness = AlignmentConstants.Lawful;
            Assert.That(alignment.IsLawful(), Is.True);
        }

        [Test]
        public void AlignmentIsNotLawfulIfLawfulnessIsNeutral()
        {
            alignment.Lawfulness = AlignmentConstants.Neutral;
            Assert.That(alignment.IsLawful(), Is.False);
        }

        [Test]
        public void AlignmentIsNotLawfulIfLawfulnessIsChaotic()
        {
            alignment.Lawfulness = AlignmentConstants.Chaotic;
            Assert.That(alignment.IsLawful(), Is.False);
        }

        [Test]
        public void AlignmentIsChaoticIfLawfulnessIsChaotic()
        {
            alignment.Lawfulness = AlignmentConstants.Chaotic;
            Assert.That(alignment.IsChaotic(), Is.True);
        }

        [Test]
        public void AlignmentIsNotChaoticIfLawfulnessIsNeutral()
        {
            alignment.Lawfulness = AlignmentConstants.Neutral;
            Assert.That(alignment.IsChaotic(), Is.False);
        }

        [Test]
        public void AlignmentIsNotChaoticIfLawfulnessIsLawful()
        {
            alignment.Lawfulness = AlignmentConstants.Lawful;
            Assert.That(alignment.IsChaotic(), Is.False);
        }

        [Test]
        public void AlignmentIsGoodIfGoodnessIsGood()
        {
            alignment.Goodness = AlignmentConstants.Good;
            Assert.That(alignment.IsGood(), Is.True);
        }

        [Test]
        public void AlignmentIsNotGoodIfGoodnessIsNeutral()
        {
            alignment.Goodness = AlignmentConstants.Neutral;
            Assert.That(alignment.IsGood(), Is.False);
        }

        [Test]
        public void AlignmentIsNotGoodIfGoodnessIsEvil()
        {
            alignment.Goodness = AlignmentConstants.Evil;
            Assert.That(alignment.IsGood(), Is.False);
        }

        [Test]
        public void AlignmentIsEvilIfGoodnessIsEvil()
        {
            alignment.Goodness = AlignmentConstants.Evil;
            Assert.That(alignment.IsEvil(), Is.True);
        }

        [Test]
        public void AlignmentIsNotEvilIfGoodnessIsNeutral()
        {
            alignment.Goodness = AlignmentConstants.Neutral;
            Assert.That(alignment.IsEvil(), Is.False);
        }

        [Test]
        public void AlignmentIsNotEvilIfGoodnessIsGood()
        {
            alignment.Goodness = AlignmentConstants.Good;
            Assert.That(alignment.IsEvil(), Is.False);
        }

        [Test]
        public void ToStringIsLawfulnessPlusGoodness()
        {
            alignment.Lawfulness = "lawfulness";
            alignment.Goodness = "goodness";
            Assert.That(alignment.ToString(), Is.EqualTo("lawfulness goodness"));
        }

        [Test]
        public void TrueNeutralToString()
        {
            alignment.Lawfulness = AlignmentConstants.Neutral;
            alignment.Goodness = AlignmentConstants.Neutral;
            Assert.That(alignment.ToString(), Is.EqualTo("True Neutral"));
        }

        [Test]
        public void AlignmentIsNotEqualIfOtherItemNotAlignment()
        {
            alignment.Lawfulness = "lawfulness";
            var otherAlignment = new Object();

            Assert.That(alignment, Is.Not.EqualTo(otherAlignment));
        }

        [Test]
        public void AlignmentIsNotEqualIfLawfulnessDiffers()
        {
            alignment.Lawfulness = "lawfulness";
            alignment.Goodness = "goodness";

            var otherAlignment = new Alignment();
            otherAlignment.Lawfulness = "other lawfulness";
            otherAlignment.Goodness = "goodness";

            Assert.That(alignment, Is.Not.EqualTo(otherAlignment));
        }

        [Test]
        public void AlignmentIsNotEqualIfGoodnessDiffers()
        {
            alignment.Lawfulness = "lawfulness";
            alignment.Goodness = "goodness";

            var otherAlignment = new Alignment();
            otherAlignment.Lawfulness = "lawfulness";
            otherAlignment.Goodness = "other goodness";

            Assert.That(alignment, Is.Not.EqualTo(otherAlignment));
        }

        [Test]
        public void AlignmentIsEqualIfGoodnessesAndLawfulnessesMatch()
        {
            alignment.Lawfulness = "lawfulness";
            alignment.Goodness = "goodness";

            var otherAlignment = new Alignment();
            otherAlignment.Lawfulness = "lawfulness";
            otherAlignment.Goodness = "goodness";

            Assert.That(alignment, Is.EqualTo(otherAlignment));
        }
    }
}