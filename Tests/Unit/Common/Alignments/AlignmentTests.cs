using CharacterGen.Common.Alignments;
using NUnit.Framework;
using System;

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
        public void CanBuildAlignmentByString()
        {
            alignment = new Alignment("lawfulness goodness");
            Assert.That(alignment.Goodness, Is.EqualTo("goodness"));
            Assert.That(alignment.Lawfulness, Is.EqualTo("lawfulness"));
        }

        [Test]
        public void TrueNeutralBecomesNeutralNeutral()
        {
            alignment = new Alignment("True Neutral");
            Assert.That(alignment.Goodness, Is.EqualTo(AlignmentConstants.Neutral));
            Assert.That(alignment.Lawfulness, Is.EqualTo(AlignmentConstants.Neutral));
        }

        [Test]
        public void TooShortAlignmentDefaultsToEmpty()
        {
            alignment = new Alignment("good");
            Assert.That(alignment.Goodness, Is.Empty);
            Assert.That(alignment.Lawfulness, Is.Empty);
        }

        [Test]
        public void TooLongAlignmentDefaultsToEmpty()
        {
            alignment = new Alignment("much too good");
            Assert.That(alignment.Goodness, Is.Empty);
            Assert.That(alignment.Lawfulness, Is.Empty);
        }

        [Test]
        public void AlignmentInitialized()
        {
            Assert.That(alignment.Goodness, Is.Empty);
            Assert.That(alignment.Lawfulness, Is.Empty);
        }

        [Test]
        public void FullIsLawfulnessPlusGoodness()
        {
            alignment.Lawfulness = "lawfulness";
            alignment.Goodness = "goodness";
            Assert.That(alignment.Full, Is.EqualTo("lawfulness goodness"));
        }

        [Test]
        public void FullNeutralNeutralIsTrueNeutral()
        {
            alignment.Lawfulness = AlignmentConstants.Neutral;
            alignment.Goodness = AlignmentConstants.Neutral;
            Assert.That(alignment.Full, Is.EqualTo("True Neutral"));
        }

        [Test]
        public void ToStringIsFull()
        {
            alignment.Lawfulness = "lawfulness";
            alignment.Goodness = "goodness";

            var alignmentString = alignment.ToString();
            Assert.That(alignmentString, Is.EqualTo("lawfulness goodness"));
        }

        [Test]
        public void ConvertingToStringUsesFull()
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
        public void HashCodeIsHashOfFull()
        {
            alignment.Lawfulness = "lawfulness";
            alignment.Goodness = "goodness";

            var alignmentHash = alignment.GetHashCode();
            var alignmentToStringHash = alignment.ToString().GetHashCode();

            Assert.That(alignmentHash, Is.EqualTo(alignmentToStringHash));
        }
    }
}