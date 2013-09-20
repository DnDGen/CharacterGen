using NPCGen.Core.Generation.Randomizers.Level;
using NUnit.Framework;

namespace NPCGen.Tests.Generation.Randomizers.Level
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