using System.Linq;
using D20Dice;
using Moq;
using NPCGen.Core.Generation.Randomizers.CharacterClasses.Interfaces;
using NPCGen.Core.Generation.Randomizers.CharacterClasses.Levels;
using NUnit.Framework;

namespace NPCGen.Tests.Generation.Randomizers.CharacterClasses.Levels
{
    [TestFixture]
    public class AnyLevelRandomizerTests
    {
        private ILevelRandomizer randomizer;
        private Mock<IDice> mockDice;

        [SetUp]
        public void Setup()
        {
            mockDice = new Mock<IDice>();
            randomizer = new AnyLevelRandomizer(mockDice.Object);
        }

        [Test]
        public void RandomizeReturnD20Result()
        {
            var roll = 1;
            mockDice.Setup(d => d.d20(1)).Returns(roll);

            var level = randomizer.Randomize();
            Assert.That(level, Is.EqualTo(roll));
        }

        [Test]
        public void GetAllPossibleResultsReturnsLevelsOneThroughTwenty()
        {
            var levels = randomizer.GetAllPossibleResults();

            for (var level = 1; level <= 20; level++)
                Assert.That(levels.Contains(level), Is.True);

            Assert.That(levels.Count(), Is.EqualTo(20));
        }
    }
}