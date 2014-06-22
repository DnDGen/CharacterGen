using System;
using System.Linq;
using D20Dice;
using Moq;
using NPCGen.Generators.Randomizers.CharacterClasses.Levels;
using NUnit.Framework;

namespace NPCGen.Tests.Unit.Generators.Randomizers.CharacterClasses.Levels
{
    [TestFixture]
    public class RangedLevelRandomizerTests
    {
        private TestRangedLevelRandomizer randomizer;
        private Mock<IDice> mockDice;

        [SetUp]
        public void Setup()
        {
            mockDice = new Mock<IDice>();
            randomizer = new TestRangedLevelRandomizer(mockDice.Object);
        }

        [Test]
        public void BaseIsRollOf1d5()
        {
            mockDice.Setup(d => d.Roll("1d5")).Returns(9266);
            var level = randomizer.Randomize();
            Assert.That(level, Is.EqualTo(9266));
        }

        [Test]
        public void AddRollBonusToRoll()
        {
            mockDice.Setup(d => d.Roll("1d5")).Returns(9200);
            randomizer.RollBonus = 66;

            var level = randomizer.Randomize();
            Assert.That(level, Is.EqualTo(9266));
        }

        [Test]
        public void GetAllPossibleResultsReturnsAllPossibleRolls()
        {
            randomizer.RollBonus = 9266;

            var levels = randomizer.GetAllPossibleResults();

            for (var level = randomizer.RollBonus + 1; level <= randomizer.RollBonus + 5; level++)
                Assert.That(levels, Contains.Item(level));

            Assert.That(levels.Count(), Is.EqualTo(5));
        }

        private class TestRangedLevelRandomizer : RangedLevel
        {
            public Int32 RollBonus
            {
                get { return rollBonus; }
                set { rollBonus = value; }
            }

            public TestRangedLevelRandomizer(IDice dice) : base(dice) { }
        }
    }
}