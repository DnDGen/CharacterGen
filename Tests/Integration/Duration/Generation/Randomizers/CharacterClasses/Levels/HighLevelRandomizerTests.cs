using Ninject;
using NPCGen.Core.Generation.Randomizers.CharacterClasses.Levels;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Duration.Generation.Randomizers.CharacterClasses.Levels
{
    [TestFixture]
    public class HighLevelRandomizerTests : DurationTest
    {
        public HighLevelRandomizerTests()
        {
            LevelRandomizer = kernel.Get<HighLevelRandomizer>();
        }

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
        public void HighLevelRandomization()
        {
            LevelRandomizer.Randomize();
            AssertDuration();
        }
    }
}