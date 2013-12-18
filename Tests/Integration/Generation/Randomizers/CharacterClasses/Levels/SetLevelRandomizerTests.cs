using Ninject;
using NPCGen.Core.Generation.Randomizers.CharacterClasses.Levels;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Generation.Randomizers.CharacterClasses.Levels
{
    [TestFixture]
    public class SetLevelRandomizerTests : IntegrationTest
    {
        [Inject]
        public SetLevelRandomizer LevelRandomizer { get; set; }

        [SetUp]
        public void Setup()
        {
            LevelRandomizer.Level = 9266;
            StartTest();
        }

        [Test]
        public void SetLevelSingleRandomization()
        {
            LevelRandomizer.Randomize();
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