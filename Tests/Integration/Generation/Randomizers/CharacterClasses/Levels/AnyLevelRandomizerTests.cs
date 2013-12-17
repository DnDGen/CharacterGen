using Ninject;
using NPCGen.Core.Generation.Randomizers.CharacterClasses.Levels;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Generation.Randomizers.CharacterClasses.Levels
{
    [TestFixture]
    public class AnyLevelRandomizerTests : IntegrationTest
    {
        [Inject]
        public AnyLevelRandomizer LevelRandomizer { get; set; }

        [SetUp]
        public void Setup()
        {
            StartTest();
        }

        [Test]
        public void AnyLevelRandomizerReturnsLevelGreaterThanZero()
        {
            while (TestShouldKeepRunning())
            {
                var level = LevelRandomizer.Randomize();
                Assert.That(level, Is.GreaterThan(0));
            }
        }
    }
}