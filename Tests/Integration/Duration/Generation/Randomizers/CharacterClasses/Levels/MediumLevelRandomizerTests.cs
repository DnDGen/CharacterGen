using Ninject;
using NPCGen.Core.Generation.Randomizers.CharacterClasses.Levels;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Duration.Generation.Randomizers.CharacterClasses.Levels
{
    [TestFixture]
    public class MediumLevelRandomizerTests : DurationTest
    {
        public MediumLevelRandomizerTests()
        {
            LevelRandomizer = kernel.Get<MediumLevelRandomizer>();
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
        public void MediumLevelRandomization()
        {
            LevelRandomizer.Randomize();
            AssertDuration();
        }
    }
}