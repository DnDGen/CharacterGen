using CharacterGen.Randomizers.CharacterClasses;
using Ninject;
using NUnit.Framework;

namespace CharacterGen.Tests.Integration.Stress.Randomizers.CharacterClasses.Levels
{
    [TestFixture]
    public class HighLevelRandomizerTests : StressTests
    {
        [Inject, Named(LevelRandomizerTypeConstants.High)]
        public ILevelRandomizer HighLevelRandomizer { get; set; }

        [Test]
        public void StressLevel()
        {
            Stress(AssertLevel);
        }

        protected void AssertLevel()
        {
            var level = HighLevelRandomizer.Randomize();
            Assert.That(level, Is.InRange(11, 15));
        }
    }
}