﻿using DnDGen.CharacterGen.Alignments;
using DnDGen.CharacterGen.Randomizers.Alignments;
using NUnit.Framework;

namespace DnDGen.CharacterGen.Tests.Integration.Stress.Randomizers.Alignments
{
    [TestFixture]
    public class ChaoticAlignmentRandomizerTests : StressTests
    {
        [SetUp]
        public void Setup()
        {
            alignmentRandomizer = GetNewInstanceOf<IAlignmentRandomizer>(AlignmentRandomizerTypeConstants.Chaotic);
        }

        [Test]
        public void StressChaoticAlignment()
        {
            stressor.Stress(AssertAlignment);
        }

        protected void AssertAlignment()
        {
            var alignment = alignmentRandomizer.Randomize();
            Assert.That(alignment.Lawfulness, Is.EqualTo(AlignmentConstants.Chaotic));
            Assert.That(alignment.Goodness, Is.EqualTo(AlignmentConstants.Good)
                .Or.EqualTo(AlignmentConstants.Neutral)
                .Or.EqualTo(AlignmentConstants.Evil));
        }
    }
}