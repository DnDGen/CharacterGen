using D20Dice;
using Moq;
using NPCGen.Core.Generators.Randomizers.CharacterClasses.Levels;
using NUnit.Framework;

namespace NPCGen.Tests.Unit.Generators.Randomizers.CharacterClasses.Levels
{
    [TestFixture]
    public class VeryHighLevelRandomizerTests
    {
        [Test]
        public void Add15ToRoll()
        {
            var mockDice = new Mock<IDice>();
            var randomizer = new VeryHighLevelRandomizer(mockDice.Object);

            var level = randomizer.Randomize();
            Assert.That(level, Is.EqualTo(15));
        }
    }
}