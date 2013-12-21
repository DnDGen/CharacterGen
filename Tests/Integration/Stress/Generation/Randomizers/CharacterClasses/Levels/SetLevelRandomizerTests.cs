using System;
using Ninject;
using NPCGen.Core.Generation.Randomizers.CharacterClasses.Levels;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Stress.Generation.Randomizers.CharacterClasses.Levels
{
    [TestFixture]
    public class SetLevelRandomizerTests : StressTest
    {
        [Inject]
        public SetLevelRandomizer LevelRandomizer { get; set; }

        private Random random;

        [SetUp]
        public void Setup()
        {
            random = new Random();
            StartTest();
        }

        [TearDown]
        public void TearDown()
        {
            StopTest();
        }

        [Test]
        public void SetLevelRandomizerReturnsSetLevel()
        {
            while (TestShouldKeepRunning())
            {
                LevelRandomizer.Level = random.Next();
                var level = LevelRandomizer.Randomize();
                Assert.That(level, Is.EqualTo(LevelRandomizer.Level));
            }

            AssertIterations();
        }
    }
}