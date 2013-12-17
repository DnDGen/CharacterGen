using Ninject;
using NPCGen.Core.Generation.Randomizers.CharacterClasses.Levels;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Generation.Randomizers.CharacterClasses.Levels
{
    [TestFixture]
    public class MediumLevelRandomizerTests : IntegrationTest
    {
        [Inject]
        public MediumLevelRandomizer LevelRandomizer { get; set; }

        [SetUp]
        public void Setup()
        {
            StartTest();
        }

        [Test]
        public void MediumLevelRandomizerReturnsLevelGreaterThanFiveAndLessThanOrEqualToTen()
        {
            while (TestShouldKeepRunning())
            {
                var level = LevelRandomizer.Randomize();
                Assert.That(level, Is.GreaterThan(5));
                Assert.That(level, Is.LessThanOrEqualTo(10));
            }
        }
    }
}