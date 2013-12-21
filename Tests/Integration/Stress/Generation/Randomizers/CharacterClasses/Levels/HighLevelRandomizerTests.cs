using Ninject;
using NPCGen.Core.Generation.Randomizers.CharacterClasses.Levels;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Stress.Generation.Randomizers.CharacterClasses.Levels
{
    [TestFixture]
    public class HighLevelRandomizerTests : StressTest
    {
        [Inject]
        public HighLevelRandomizer LevelRandomizer { get; set; }

        [SetUp]
        public void Setup()
        {
            StartTest();
        }

        [TearDown]
        public void TearDown()
        {
            StopTest();
        }

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