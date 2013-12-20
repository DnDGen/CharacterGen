using Ninject;
using NPCGen.Core.Generation.Randomizers.CharacterClasses.Levels;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Duration.Generation.Randomizers.CharacterClasses.Levels
{
    [TestFixture]
    public class LowLevelRandomizerTests : DurationTest
    {
        public LowLevelRandomizerTests()
        {
            LevelRandomizer = kernel.Get<LowLevelRandomizer>();
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
        public void LowLevelRandomization()
        {
            LevelRandomizer.Randomize();
            AssertDuration();
        }
    }
}