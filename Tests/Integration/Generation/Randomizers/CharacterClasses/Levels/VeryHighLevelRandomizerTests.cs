using Ninject;
using NPCGen.Core.Generation.Randomizers.CharacterClasses.Levels;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Generation.Randomizers.CharacterClasses.Levels
{
    [TestFixture]
    public class VeryHighLevelRandomizerTests : IntegrationTest
    {
        [Inject]
        public VeryHighLevelRandomizer LevelRandomizer { get; set; }

        [SetUp]
        public void Setup()
        {
            StartTest();
        }

        [Test]
        public void VeryHighLevelRandomizerReturnsLevelGreaterThanFifteenAndLessThanOrEqualToTwenty()
        {
            while (TestShouldKeepRunning())
            {
                var level = LevelRandomizer.Randomize();
                Assert.That(level, Is.GreaterThan(15));
                Assert.That(level, Is.LessThanOrEqualTo(20));
            }
        }
    }
}