using D20Dice.Dice;
using Moq;
using NPCGen.Core.Generation.Randomizers.CharacterClasses.Levels;
using NUnit.Framework;

namespace NPCGen.Tests.Generation.Randomizers.CharacterClasses.Levels
{
    [TestFixture]
    public class LowLevelRandomizerTests
    {
        [Test]
        public void Add0ToRoll()
        {
            var mockDice = new Mock<IDice>();
            var randomizer = new LowLevelRandomizer(mockDice.Object);

            var level = randomizer.Randomize();
            Assert.That(level, Is.EqualTo(0));
        }
    }
}