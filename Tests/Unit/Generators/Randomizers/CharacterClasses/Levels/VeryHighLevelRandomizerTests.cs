using CharacterGen.Generators.Domain.Randomizers.CharacterClasses.Levels;
using Moq;
using NUnit.Framework;
using RollGen;

namespace CharacterGen.Tests.Unit.Generators.Randomizers.CharacterClasses.Levels
{
    [TestFixture]
    public class VeryHighLevelRandomizerTests
    {
        [Test]
        public void Add15ToRoll()
        {
            var mockDice = new Mock<Dice>();
            mockDice.Setup(d => d.Roll(1).IndividualRolls(5)).Returns(new[] { 9266 });
            var randomizer = new VeryHighLevelRandomizer(mockDice.Object);

            var level = randomizer.Randomize();
            Assert.That(level, Is.EqualTo(9281));
        }
    }
}