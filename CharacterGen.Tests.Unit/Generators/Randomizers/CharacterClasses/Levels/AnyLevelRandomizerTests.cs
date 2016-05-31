using CharacterGen.Domain.Generators.Randomizers.CharacterClasses.Levels;
using CharacterGen.Randomizers.CharacterClasses;
using Moq;
using NUnit.Framework;
using RollGen;
using System.Linq;

namespace CharacterGen.Tests.Unit.Generators.Randomizers.CharacterClasses.Levels
{
    [TestFixture]
    public class AnyLevelRandomizerTests
    {
        private ILevelRandomizer randomizer;
        private Mock<Dice> mockDice;

        [SetUp]
        public void Setup()
        {
            mockDice = new Mock<Dice>();
            randomizer = new AnyLevelRandomizer(mockDice.Object);
        }

        [Test]
        public void RandomizeReturnD20Result()
        {
            mockDice.Setup(d => d.Roll(1).IndividualRolls(20)).Returns(new[] { 9266 });
            var level = randomizer.Randomize();
            Assert.That(level, Is.EqualTo(9266));
        }

        [Test]
        public void GetAllPossibleResultsReturnsLevelsOneThroughTwenty()
        {
            var levels = randomizer.GetAllPossibleResults();

            for (var level = 1; level <= 20; level++)
                Assert.That(levels, Contains.Item(level));

            Assert.That(levels.Count(), Is.EqualTo(20));
        }
    }
}