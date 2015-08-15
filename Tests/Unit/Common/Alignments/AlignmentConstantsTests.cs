using CharacterGen.Common.Alignments;
using NUnit.Framework;
using System;

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
    }
}