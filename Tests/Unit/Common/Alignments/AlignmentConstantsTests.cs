using System;
using System.Linq;
using CharacterGen.Common.Alignments;
using NUnit.Framework;

namespace CharacterGen.Tests.Unit.Common.Alignments
{
    [TestFixture]
    public class AlignmentConstantsTests
    {
        [TestCase(AlignmentConstants.Neutral, "Neutral")]
        [TestCase(AlignmentConstants.Chaotic, "Chaotic")]
        [TestCase(AlignmentConstants.Good, "Good")]
        [TestCase(AlignmentConstants.Evil, "Evil")]
        [TestCase(AlignmentConstants.Lawful, "Lawful")]
        public void Constant(String constant, String value)
        {
            Assert.That(constant, Is.EqualTo(value));
        }

        [Test]
        public void Lawfulnesses()
        {
            var lawfulnesses = AlignmentConstants.GetLawfulnesses();

            Assert.That(lawfulnesses, Contains.Item(AlignmentConstants.Lawful));
            Assert.That(lawfulnesses, Contains.Item(AlignmentConstants.Neutral));
            Assert.That(lawfulnesses, Contains.Item(AlignmentConstants.Chaotic));
            Assert.That(lawfulnesses.Count(), Is.EqualTo(3));
        }

        [Test]
        public void Goodnesses()
        {
            var goodnesses = AlignmentConstants.GetGoodnesses();

            Assert.That(goodnesses, Contains.Item(AlignmentConstants.Good));
            Assert.That(goodnesses, Contains.Item(AlignmentConstants.Neutral));
            Assert.That(goodnesses, Contains.Item(AlignmentConstants.Evil));
            Assert.That(goodnesses.Count(), Is.EqualTo(3));
        }
    }
}