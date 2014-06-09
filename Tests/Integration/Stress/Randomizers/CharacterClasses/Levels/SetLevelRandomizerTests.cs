using System;
using Ninject;
using NPCGen.Generators.Randomizers.CharacterClasses.Levels;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Stress.Randomizers.CharacterClasses.Levels
{
    [TestFixture]
    public class SetLevelRandomizerTests : StressTest
    {
        [Inject]
        public SetLevelRandomizer LevelRandomizer { get; set; }
        [Inject]
        public Random Random { get; set; }

        [Test]
        public void SetLevelRandomizerReturnsSetLevel()
        {
            while (TestShouldKeepRunning())
            {
                LevelRandomizer.Level = Random.Next();
                var level = LevelRandomizer.Randomize();
                Assert.That(level, Is.EqualTo(LevelRandomizer.Level));
            }

            AssertIterations();
        }
    }
}