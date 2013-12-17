using Ninject;
using NPCGen.Core.Generation.Randomizers.CharacterClasses.Levels;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Generation.Randomizers.CharacterClasses.Levels
{
    [TestFixture]
    public class LowLevelRandomizerTests : IntegrationTest
    {
        [Inject]
        public LowLevelRandomizer LevelRandomizer { get; set; }

        [SetUp]
        public void Setup()
        {
            StartTest();
        }

        [Test]
        public void LowLevelRandomizerReturnsLevelGreaterThanZeroAndLessThanOrEqualToFive()
        {
            while (TestShouldKeepRunning())
            {
                var level = LevelRandomizer.Randomize();
                Assert.That(level, Is.GreaterThan(0));
                Assert.That(level, Is.LessThanOrEqualTo(5));
            }
        }
    }
}