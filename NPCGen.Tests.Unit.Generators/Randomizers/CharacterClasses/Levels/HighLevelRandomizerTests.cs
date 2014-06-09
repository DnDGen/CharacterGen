using D20Dice;
using Moq;
using NPCGen.Core.Generators.Randomizers.CharacterClasses.Levels;
using NUnit.Framework;

namespace NPCGen.Tests.Unit.Generators.Randomizers.CharacterClasses.Levels
{
    [TestFixture]
    public class HighLevelRandomizerTests
    {
        [Test]
        public void Add10ToRoll()
        {
            var mockDice = new Mock<IDice>();
            var randomizer = new HighLevelRandomizer(mockDice.Object);

            var level = randomizer.Randomize();
            Assert.That(level, Is.EqualTo(10));
        }
    }
}