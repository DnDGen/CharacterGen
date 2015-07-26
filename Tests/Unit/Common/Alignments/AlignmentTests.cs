using System;
using CharacterGen.Common.Alignments;
using NUnit.Framework;

namespace CharacterGen.Tests.Unit.Common.Alignments
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
        public void ConvertingToStringUsesToString()
        {
            alignment.Lawfulness = "lawfulness";
            alignment.Goodness = "goodness";

            var alignmentString = Convert.ToString(alignment);
            Assert.That(alignmentString, Is.EqualTo("lawfulness goodness"));
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

        [Test]
        public void HashCodeIsHashOfToString()
        {
            alignment.Lawfulness = "lawfulness";
            alignment.Goodness = "goodness";

            var alignmentHash = alignment.GetHashCode();
            var alignmentToStringHash = alignment.ToString().GetHashCode();

            Assert.That(alignmentHash, Is.EqualTo(alignmentToStringHash));
        }
    }
}