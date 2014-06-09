using D20Dice;
using Moq;
using NPCGen.Core.Generators.Randomizers.CharacterClasses.Levels;
using NUnit.Framework;

namespace NPCGen.Tests.Unit.Generators.Randomizers.CharacterClasses.Levels
{
    [TestFixture]
    public class MediumLevelRandomizerTests
    {
        [Test]
        public void Add5ToRoll()
        {
            var mockDice = new Mock<IDice>();
            var randomizer = new MediumLevelRandomizer(mockDice.Object);

            var level = randomizer.Randomize();
            Assert.That(level, Is.EqualTo(5));
        }
    }
}