﻿using Ninject;
using NPCGen.Core.Data.Alignments;
using NPCGen.Core.Generation.Randomizers.Alignments;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Stress.Generation.Randomizers.Alignments
{
    [TestFixture]
    public class SetAlignmentRandomizerTests : StressTest
    {
        [Inject]
        public SetAlignmentRandomizer AlignmentRandomizer { get; set; }
        [Inject]
        public Alignment Alignment { get; set; }

        [SetUp]
        public void Setup()
        {
            AlignmentRandomizer.Alignment = Alignment;
            StartTest();
        }

        [TearDown]
        public void TearDown()
        {
            StopTest();
        }

        [Test]
        public void SetAlignmentRandomizerAlwaysReturnsSetAlignment()
        {
            while (TestShouldKeepRunning())
            {
                var alignment = AlignmentRandomizer.Randomize();
                Assert.That(alignment, Is.EqualTo(Alignment));
            }
        }
    }
}