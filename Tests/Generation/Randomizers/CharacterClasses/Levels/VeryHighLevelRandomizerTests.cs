using D20Dice.Dice;
using Moq;
using NPCGen.Core.Generation.Randomizers.Levels;
using NUnit.Framework;

namespace NPCGen.Tests.Generation.Randomizers.CharacterClasses.Levels
{
    [TestFixture]
    public class VeryHighLevelRandomizerTests
    {
        [Test]
        public void Add15ToRoll()
        {
            var mockDice = new Mock<IDice>();
            var randomizer = new VeryHighLevelRandomizer(mockDice.Object);

            mockDice.Setup(d => d.d6(1, 0)).Returns(1);
            var level = randomizer.Randomize();
            Assert.That(level, Is.EqualTo(16));
        }
    }
}