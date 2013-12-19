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

        [SetUp]
        public void Setup()
        {
            LevelRandomizer.Level = 9266;
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
                var level = LevelRandomizer.Randomize();
                Assert.That(level, Is.EqualTo(9266));
            }
        }
    }
}