using D20Dice.Dice;
using Moq;
using NPCGen.Core.Generation.Randomizers.Level;
using NUnit.Framework;
using System;

namespace NPCGen.Tests.Generation.Randomizers.Level
{
    [TestFixture]
    public class RangedLevelRandomizerTests
    {
        private ILevelRandomizer randomizer;
        private Mock<IDice> mockDice;

        [SetUp]
        public void Setup()
        {
            mockDice = new Mock<IDice>();
            randomizer = new TestRangedLevelRandomizer(mockDice.Object, 9266);
        }

        [Test]
        public void IgnoreSixes()
        {
            mockDice.SetupSequence(d => d.d6(1, 0)).Returns(6).Returns(1);
            randomizer.Randomize();
            mockDice.Verify(d => d.d6(1, 0), Times.Exactly(2));
        }

        [Test]
        public void AddRollBonusToRoll()
        {
            mockDice.Setup(d => d.d6(1, 0)).Returns(1);
            var level = randomizer.Randomize();
            Assert.That(level, Is.EqualTo(9267));
        }

        private class TestRangedLevelRandomizer : RangedLevelRandomizer
        {
            public TestRangedLevelRandomizer(IDice dice, Int32 rollBonus)
                : base(dice)
            {
                this.rollBonus = rollBonus;
            }
        }
    }
}