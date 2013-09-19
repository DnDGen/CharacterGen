using D20Dice.Dice;
using Moq;
using NPCGen.Core.Generation.Randomizers.Level;
using NUnit.Framework;

namespace NPCGen.Tests.Generation.Randomizers.Level
{
    [TestFixture]
    public class LowLevelRandomizerTests
    {
        [Test]
        public void AddZeroToRoll()
        {
            var mockDice = new Mock<IDice>();
            var randomizer = new LowLevelRandomizer(mockDice.Object);

            mockDice.Setup(d => d.d6(1, 0)).Returns(1);
            var level = randomizer.Randomize();
            Assert.That(level, Is.EqualTo(1));
        }
    }
}