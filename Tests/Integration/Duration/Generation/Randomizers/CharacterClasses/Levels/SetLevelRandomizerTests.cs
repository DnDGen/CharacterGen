using Ninject;
using NPCGen.Core.Generation.Randomizers.CharacterClasses.Levels;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Duration.Generation.Randomizers.CharacterClasses.Levels
{
    [TestFixture]
    public class SetLevelRandomizerTests : DurationTest
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
        public void SetLevelRandomization()
        {
            LevelRandomizer.Randomize();
            AssertDuration();
        }
    }
}