using Ninject;
using NPCGen.Generators.Randomizers.CharacterClasses.Levels;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Stress.Randomizers.CharacterClasses.Levels
{
    [TestFixture]
    public class HighLevelRandomizerTests : StressTests
    {
        [Inject]
        public HighLevelRandomizer LevelRandomizer { get; set; }

        [Test]
        public void HighLevelRandomizerReturnsLevelGreaterThanTenAndLessThanOrEqualToFifteen()
        {
            while (TestShouldKeepRunning())
            {
                var level = LevelRandomizer.Randomize();
                Assert.That(level, Is.GreaterThan(10));
                Assert.That(level, Is.LessThanOrEqualTo(15));
            }

            AssertIterations();
        }
    }
}