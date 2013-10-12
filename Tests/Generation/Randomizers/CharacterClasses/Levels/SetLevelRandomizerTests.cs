using NPCGen.Core.Generation.Randomizers.CharacterClasses.Levels;
using NUnit.Framework;

namespace NPCGen.Tests.Generation.Randomizers.CharacterClasses.Levels
{
    [TestFixture]
    public class SetLevelRandomizerTests
    {
        [Test]
        public void ReturnSetLevel()
        {
            var randomizer = new SetLevelRandomizer();
            randomizer.Level = 9266;

            var level = randomizer.Randomize();
            Assert.That(level, Is.EqualTo(9266));
        }
    }
}