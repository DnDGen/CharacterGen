using D20Dice.Dice;
using Moq;
using NPCGen.Core.Generation.Randomizers.CharacterClasses.Levels;
using NUnit.Framework;

namespace NPCGen.Tests.Generation.Randomizers.CharacterClasses.Levels
{
    [TestFixture]
    public class AnyLevelRandomizerTests
    {
        [Test]
        public void ReturnD20Result()
        {
            var roll = 1;
            var mockDice = new Mock<IDice>();
            mockDice.Setup(d => d.d20(1, 0)).Returns(roll);
            var randomizer = new AnyLevelRandomizer(mockDice.Object);

            var level = randomizer.Randomize();
            Assert.That(level, Is.EqualTo(roll));
        }
    }
}