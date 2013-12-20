using Ninject;
using NPCGen.Core.Generation.Randomizers.CharacterClasses.Levels;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Duration.Generation.Randomizers.CharacterClasses.Levels
{
    [TestFixture]
    public class VeryHighLevelRandomizerTests : DurationTest
    {
        public VeryHighLevelRandomizerTests()
        {
            LevelRandomizer = kernel.Get<VeryHighLevelRandomizer>();
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
        public void VeryHighLevelRandomization()
        {
            LevelRandomizer.Randomize();
            AssertDuration();
        }
    }
}