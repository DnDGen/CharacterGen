﻿using Ninject;
using NPCGen.Core.Data.Alignments;
using NPCGen.Core.Generation.Randomizers.CharacterClasses.ClassNames;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Stress.Generation.Randomizers.CharacterClasses.ClassNames
{
    [TestFixture]
    public class SetClassNameRandomizerTests : StressTest
    {
        [Inject]
        public SetClassNameRandomizer ClassNameRandomizer { get; set; }
        [Inject]
        public Alignment Alignment { get; set; }

        [SetUp]
        public void Setup()
        {
            ClassNameRandomizer.ClassName = "class name";
            StartTest();
        }

        [TearDown]
        public void TearDown()
        {
            StopTest();
        }

        [Test]
        public void SetClassNameRandomizerReturnsClassName()
        {
            while (TestShouldKeepRunning())
            {
                var className = ClassNameRandomizer.Randomize(Alignment);
                Assert.That(className, Is.Not.Null);
                Assert.That(className, Is.Not.Empty);
            }
        }

        [Test]
        public void SetClassNameRandomizerAlwaysReturnsSetClassName()
        {
            while (TestShouldKeepRunning())
            {
                var className = ClassNameRandomizer.Randomize(Alignment);
                Assert.That(className, Is.EqualTo("class name"));
            }
        }
    }
}