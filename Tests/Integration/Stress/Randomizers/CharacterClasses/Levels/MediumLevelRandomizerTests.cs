using Ninject;
using NPCGen.Generators.Randomizers.CharacterClasses.Levels;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Stress.Randomizers.CharacterClasses.Levels
{
    [TestFixture]
    public class MediumLevelRandomizerTests : StressTest
    {
        [Inject]
        public MediumLevelRandomizer LevelRandomizer { get; set; }

        [Test]
        public void MediumLevelRandomizerReturnsLevelGreaterThanFiveAndLessThanOrEqualToTen()
        {
            while (TestShouldKeepRunning())
            {
                var level = LevelRandomizer.Randomize();
                Assert.That(level, Is.GreaterThan(5));
                Assert.That(level, Is.LessThanOrEqualTo(10));
            }

            AssertIterations();
        }
    }
}