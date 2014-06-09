using Ninject;
using NPCGen.Generators.Randomizers.CharacterClasses.Levels;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Stress.Randomizers.CharacterClasses.Levels
{
    [TestFixture]
    public class AnyLevelRandomizerTests : StressTest
    {
        [Inject]
        public AnyLevelRandomizer LevelRandomizer { get; set; }

        [Test]
        public void AnyLevelRandomizerReturnsLevel()
        {
            while (TestShouldKeepRunning())
            {
                var level = LevelRandomizer.Randomize();
                Assert.That(level, Is.GreaterThan(0));
                Assert.That(level, Is.LessThanOrEqualTo(20));
            }

            AssertIterations();
        }
    }
}