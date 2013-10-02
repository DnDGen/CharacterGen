using D20Dice.Dice;
using Moq;
using NPCGen.Core.Generation.Randomizers.Levels;
using NUnit.Framework;

namespace NPCGen.Tests.Generation.Randomizers.CharacterClasses.Levels
{
    [TestFixture]
    public class AnyLevelRandomizerTests
    {
        [Test]
        public void ReturnD20Result()
        {
            var mockDice = new Mock<IDice>();
            var randomizer = new AnyLevelRandomizer(mockDice.Object);

            randomizer.Randomize();
            mockDice.Verify(d => d.d20(1, 0), Times.Once());
        }
    }
}