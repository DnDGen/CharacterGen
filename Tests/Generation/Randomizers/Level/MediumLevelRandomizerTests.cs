using D20Dice.Dice;
using Moq;
using NPCGen.Core.Generation.Randomizers.Level;
using NUnit.Framework;

namespace NPCGen.Tests.Generation.Randomizers.Level
{
    [TestFixture]
    public class MediumLevelRandomizerTests
    {
        [Test]
        public void Add5ToRoll()
        {
            var mockDice = new Mock<IDice>();
            var randomizer = new MediumLevelRandomizer(mockDice.Object);

            mockDice.Setup(d => d.d6(1, 0)).Returns(1);
            var level = randomizer.Randomize();
            Assert.That(level, Is.EqualTo(6));
        }
    }
}