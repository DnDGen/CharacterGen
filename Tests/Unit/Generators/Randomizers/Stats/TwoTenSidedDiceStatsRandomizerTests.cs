using System;
using D20Dice;
using Moq;
using NPCGen.Common.Abilities;
using NPCGen.Generators.Interfaces.Randomizers.Stats;
using NPCGen.Generators.Randomizers.Stats;
using NUnit.Framework;

namespace NPCGen.Tests.Unit.Generators.Randomizers.Stats
{
    [TestFixture]
    public class TwoTenSidedDiceStatsRandomizerTests
    {
        private IStatsRandomizer randomizer;
        private Mock<IDice> mockDice;

        [SetUp]
        public void Setup()
        {
            mockDice = new Mock<IDice>();
            randomizer = new TwoTenSidedDiceStatsRandomizer(mockDice.Object);
        }

        [Test]
        public void TwoTenSidedDiceCalls2d10PerStat()
        {
            var stats = randomizer.Randomize();
            mockDice.Verify(d => d.d10(2), Times.Exactly(stats.Count));
        }

        [Test]
        public void TwoTenSidedDiceReturnsUnmodified2d10PerStat()
        {
            mockDice.Setup(d => d.d10(2)).Returns(10);

            var stats = randomizer.Randomize();
            foreach (var stat in stats.Values)
                Assert.That(stat.Value, Is.EqualTo(10));
        }

        [Test]
        public void RolledStatsAreAlwaysAllowed()
        {
            mockDice.SetupSequence(d => d.d10(2)).Returns(9266).Returns(-42).Returns(Int32.MaxValue).Returns(Int32.MinValue).Returns(0)
                .Returns(1337);

            var stats = randomizer.Randomize();

            Assert.That(stats[StatConstants.Strength].Value, Is.EqualTo(9266));
            Assert.That(stats[StatConstants.Constitution].Value, Is.EqualTo(-42));
            Assert.That(stats[StatConstants.Dexterity].Value, Is.EqualTo(Int32.MaxValue));
            Assert.That(stats[StatConstants.Intelligence].Value, Is.EqualTo(Int32.MinValue));
            Assert.That(stats[StatConstants.Wisdom].Value, Is.EqualTo(0));
            Assert.That(stats[StatConstants.Charisma].Value, Is.EqualTo(1337));
        }
    }
}