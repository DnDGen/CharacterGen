using Ninject;
using NPCGen.Generators.Randomizers.CharacterClasses.Levels;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Stress.Randomizers.CharacterClasses.Levels
{
    [TestFixture]
    public class VeryHighLevelRandomizerTests : StressTest
    {
        [Inject]
        public VeryHighLevelRandomizer LevelRandomizer { get; set; }

        [Test]
        public void VeryHighLevelRandomizerReturnsLevelGreaterThanFifteenAndLessThanOrEqualToTwenty()
        {
            while (TestShouldKeepRunning())
            {
                var level = LevelRandomizer.Randomize();
                Assert.That(level, Is.GreaterThan(15));
                Assert.That(level, Is.LessThanOrEqualTo(20));
            }

            AssertIterations();
        }
    }
}