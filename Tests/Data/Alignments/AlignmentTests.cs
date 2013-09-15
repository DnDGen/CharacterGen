using NPCGen.Core.Data.Alignments;
using NUnit.Framework;
using System;

namespace NPCGen.Tests.Data.Alignments
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
        public void ChaoticLawfulnessShowsChaotic()
        {
            alignment.Lawfulness = AlignmentConstants.Chaotic;
            Assert.That(alignment.GetLawfulnessString(), Is.EqualTo("Chaotic"));
        }

        [Test]
        public void NeutralLawfulnessShowsNeutral()
        {
            alignment.Lawfulness = AlignmentConstants.Neutral;
            Assert.That(alignment.GetLawfulnessString(), Is.EqualTo("Neutral"));
        }

        [Test]
        public void LawfulLawfulnessShowsLawful()
        {
            alignment.Lawfulness = AlignmentConstants.Lawful;
            Assert.That(alignment.GetLawfulnessString(), Is.EqualTo("Lawful"));
        }

        [Test]
        public void GoodGoodnessShowsGood()
        {
            alignment.Goodness = AlignmentConstants.Good;
            Assert.That(alignment.GetGoodnessString(), Is.EqualTo("Good"));
        }

        [Test]
        public void NeutralGoodnessShowsNeutral()
        {
            alignment.Goodness = AlignmentConstants.Neutral;
            Assert.That(alignment.GetGoodnessString(), Is.EqualTo("Neutral"));
        }

        [Test]
        public void EvilGoodnessShowsEvil()
        {
            alignment.Goodness = AlignmentConstants.Evil;
            Assert.That(alignment.GetGoodnessString(), Is.EqualTo("Evil"));
        }

        [Test]
        public void LawfulGoodToString()
        {
            alignment.Lawfulness = AlignmentConstants.Lawful;
            alignment.Goodness = AlignmentConstants.Good;
            Assert.That(alignment.ToString(), Is.EqualTo("Lawful Good"));
        }

        [Test]
        public void NeutralGoodToString()
        {
            alignment.Lawfulness = AlignmentConstants.Neutral;
            alignment.Goodness = AlignmentConstants.Good;
            Assert.That(alignment.ToString(), Is.EqualTo("Neutral Good"));
        }

        [Test]
        public void ChaoticGoodToString()
        {
            alignment.Lawfulness = AlignmentConstants.Chaotic;
            alignment.Goodness = AlignmentConstants.Good;
            Assert.That(alignment.ToString(), Is.EqualTo("Chaotic Good"));
        }

        [Test]
        public void LawfulNeutralToString()
        {
            alignment.Lawfulness = AlignmentConstants.Lawful;
            alignment.Goodness = AlignmentConstants.Neutral;
            Assert.That(alignment.ToString(), Is.EqualTo("Lawful Neutral"));
        }

        [Test]
        public void TrueNeutralToString()
        {
            alignment.Lawfulness = AlignmentConstants.Neutral;
            alignment.Goodness = AlignmentConstants.Neutral;
            Assert.That(alignment.ToString(), Is.EqualTo("True Neutral"));
        }

        [Test]
        public void ChaoticNeutralToString()
        {
            alignment.Lawfulness = AlignmentConstants.Chaotic;
            alignment.Goodness = AlignmentConstants.Neutral;
            Assert.That(alignment.ToString(), Is.EqualTo("Chaotic Neutral"));
        }

        [Test]
        public void LawfulEvilToString()
        {
            alignment.Lawfulness = AlignmentConstants.Lawful;
            alignment.Goodness = AlignmentConstants.Evil;
            Assert.That(alignment.ToString(), Is.EqualTo("Lawful Evil"));
        }

        [Test]
        public void NeutralEvilToString()
        {
            alignment.Lawfulness = AlignmentConstants.Neutral;
            alignment.Goodness = AlignmentConstants.Evil;
            Assert.That(alignment.ToString(), Is.EqualTo("Neutral Evil"));
        }

        [Test]
        public void ChaoticEvilToString()
        {
            alignment.Lawfulness = AlignmentConstants.Chaotic;
            alignment.Goodness = AlignmentConstants.Evil;
            Assert.That(alignment.ToString(), Is.EqualTo("Chaotic Evil"));
        }

        [Test, ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void InvalidLawfulnessThrowsErrorOnGetLawfulnessString()
        {
            alignment.Lawfulness = 2;
            alignment.GetLawfulnessString();
        }

        [Test, ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void InvalidGoodnessThrowsErrorOnGetGoodnessString()
        {
            alignment.Goodness = 2;
            alignment.GetGoodnessString();
        }

        [Test, ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void InvalidLawfulnessThrowsErrorOnToString()
        {
            alignment.Lawfulness = 2;
            alignment.ToString();
        }

        [Test, ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void InvalidGoodnessThrowsErrorOnToString()
        {
            alignment.Goodness = 2;
            alignment.ToString();
        }
    }
}