using CharacterGen.Randomizers.CharacterClasses;
using Ninject;
using NUnit.Framework;

namespace CharacterGen.Tests.Integration.Stress.Randomizers.CharacterClasses.Levels
{
    [TestFixture]
    public class VeryHighLevelRandomizerTests : StressTests
    {
        [Inject, Named(LevelRandomizerTypeConstants.VeryHigh)]
        public ILevelRandomizer VeryHighLevelRandomizer { get; set; }

        [Test]
        public void StressLevel()
        {
            stressor.Stress(AssertLevel);
        }

        protected void AssertLevel()
        {
            var level = VeryHighLevelRandomizer.Randomize();
            Assert.That(level, Is.InRange(16, 20));
        }
    }
}