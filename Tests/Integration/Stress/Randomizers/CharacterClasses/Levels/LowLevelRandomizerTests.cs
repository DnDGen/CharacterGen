using Ninject;
using NPCGen.Generators.Randomizers.CharacterClasses.Levels;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Stress.Randomizers.CharacterClasses.Levels
{
    [TestFixture]
    public class LowLevelRandomizerTests : StressTest
    {
        [Inject]
        public LowLevelRandomizer LevelRandomizer { get; set; }

        [Test]
        public void LowLevelRandomizerReturnsLevelGreaterThanZeroAndLessThanOrEqualToFive()
        {
            while (TestShouldKeepRunning())
            {
                var level = LevelRandomizer.Randomize();
                Assert.That(level, Is.GreaterThan(0));
                Assert.That(level, Is.LessThanOrEqualTo(5));
            }

            AssertIterations();
        }
    }
}