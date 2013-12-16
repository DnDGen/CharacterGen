using System;
using System.Linq;
using D20Dice;
using Moq;
using NPCGen.Core.Generation.Randomizers.CharacterClasses.Levels;
using NUnit.Framework;

namespace NPCGen.Tests.Generation.Randomizers.CharacterClasses.Levels
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
        public void RollOf1Returns1()
        {
            mockDice.Setup(d => d.d10(1)).Returns(1);
            var level = randomizer.Randomize();
            Assert.That(level, Is.EqualTo(1));
        }

        [Test]
        public void RollOf2Returns1()
        {
            mockDice.Setup(d => d.d10(1)).Returns(2);
            var level = randomizer.Randomize();
            Assert.That(level, Is.EqualTo(1));
        }

        [Test]
        public void RollOf3Returns2()
        {
            mockDice.Setup(d => d.d10(1)).Returns(3);
            var level = randomizer.Randomize();
            Assert.That(level, Is.EqualTo(2));
        }

        [Test]
        public void RollOf4Returns2()
        {
            mockDice.Setup(d => d.d10(1)).Returns(4);
            var level = randomizer.Randomize();
            Assert.That(level, Is.EqualTo(2));
        }

        [Test]
        public void RollOf5Returns3()
        {
            mockDice.Setup(d => d.d10(1)).Returns(5);
            var level = randomizer.Randomize();
            Assert.That(level, Is.EqualTo(3));
        }

        [Test]
        public void RollOf6Returns3()
        {
            mockDice.Setup(d => d.d10(1)).Returns(6);
            var level = randomizer.Randomize();
            Assert.That(level, Is.EqualTo(3));
        }

        [Test]
        public void RollOf7Returns4()
        {
            mockDice.Setup(d => d.d10(1)).Returns(7);
            var level = randomizer.Randomize();
            Assert.That(level, Is.EqualTo(4));
        }

        [Test]
        public void RollOf8Returns4()
        {
            mockDice.Setup(d => d.d10(1)).Returns(8);
            var level = randomizer.Randomize();
            Assert.That(level, Is.EqualTo(4));
        }

        [Test]
        public void RollOf9Returns5()
        {
            mockDice.Setup(d => d.d10(1)).Returns(9);
            var level = randomizer.Randomize();
            Assert.That(level, Is.EqualTo(5));
        }

        [Test]
        public void RollOf10Returns5()
        {
            mockDice.Setup(d => d.d10(1)).Returns(10);
            var level = randomizer.Randomize();
            Assert.That(level, Is.EqualTo(5));
        }

        [Test]
        public void AddRollBonusToRoll()
        {
            var roll = 1;
            var bonus = 9266;
            mockDice.Setup(d => d.d10(1)).Returns(roll);
            randomizer.RollBonus = bonus;

            var level = randomizer.Randomize();
            Assert.That(level, Is.EqualTo(roll + bonus));
        }

        [Test]
        public void GetAllPossibleResultsReturnsAllPossibleRolls()
        {
            randomizer.RollBonus = 9266;

            var levels = randomizer.GetAllPossibleResults();

            for (var level = randomizer.RollBonus + 1; level <= randomizer.RollBonus + 5; level++)
                Assert.That(levels.Contains(level), Is.True);

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