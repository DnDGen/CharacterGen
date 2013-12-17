using System.Linq;
using NPCGen.Core.Data.Alignments;
using NUnit.Framework;

namespace NPCGen.Tests.Unit.Data.Alignments
{
    [TestFixture]
    public class AlignmentConstantsTests
    {
        [Test]
        public void LawfulConstant()
        {
            Assert.That(AlignmentConstants.Lawful, Is.EqualTo("Lawful"));
        }

        [Test]
        public void NeutralConstant()
        {
            Assert.That(AlignmentConstants.Neutral, Is.EqualTo("Neutral"));
        }

        [Test]
        public void ChaoticConstant()
        {
            Assert.That(AlignmentConstants.Chaotic, Is.EqualTo("Chaotic"));
        }

        [Test]
        public void GoodConstant()
        {
            Assert.That(AlignmentConstants.Good, Is.EqualTo("Good"));
        }

        [Test]
        public void EvilConstant()
        {
            Assert.That(AlignmentConstants.Evil, Is.EqualTo("Evil"));
        }

        [Test]
        public void Lawfulnesses()
        {
            var lawfulnesses = AlignmentConstants.GetLawfulnesses();

            Assert.That(lawfulnesses.Contains(AlignmentConstants.Lawful), Is.True);
            Assert.That(lawfulnesses.Contains(AlignmentConstants.Neutral), Is.True);
            Assert.That(lawfulnesses.Contains(AlignmentConstants.Chaotic), Is.True);
            Assert.That(lawfulnesses.Count(), Is.EqualTo(3));
        }

        [Test]
        public void Goodnesses()
        {
            var goodnesses = AlignmentConstants.GetGoodnesses();

            Assert.That(goodnesses.Contains(AlignmentConstants.Good), Is.True);
            Assert.That(goodnesses.Contains(AlignmentConstants.Neutral), Is.True);
            Assert.That(goodnesses.Contains(AlignmentConstants.Evil), Is.True);
            Assert.That(goodnesses.Count(), Is.EqualTo(3));
        }
    }
}