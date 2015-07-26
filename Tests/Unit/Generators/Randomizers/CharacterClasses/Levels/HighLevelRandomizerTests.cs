using CharacterGen.Generators.Domain.Randomizers.CharacterClasses.Levels;
using RollGen;
using Moq;
using NUnit.Framework;

namespace CharacterGen.Tests.Unit.Generators.Randomizers.CharacterClasses.Levels
{
    [TestFixture]
    public class HighLevelRandomizerTests
    {
        [Test]
        public void Add10ToRoll()
        {
            var mockDice = new Mock<IDice>();
            mockDice.Setup(d => d.Roll(1).d(5)).Returns(9266);
            var randomizer = new HighLevelRandomizer(mockDice.Object);

            var level = randomizer.Randomize();
            Assert.That(level, Is.EqualTo(9276));
        }
    }
}