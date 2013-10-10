using System;
using D20Dice.Dice;
using Moq;
using NPCGen.Core.Data.Stats;
using NPCGen.Core.Generation.Randomizers.Stats;
using NPCGen.Core.Generation.Randomizers.Stats.Interfaces;
using NUnit.Framework;

namespace NPCGen.Tests.Generation.Randomizers.Stats
{
    [TestFixture]
    public class RawStatsRandomizerTests
    {
        private IStatsRandomizer randomizer;
        private Mock<IDice> mockDice;

        [SetUp]
        public void Setup()
        {
            mockDice = new Mock<IDice>();
            randomizer = new RawStatsRandomizer(mockDice.Object);
        }

        [Test]
        public void RawStatsCalls3d6PerStat()
        {
            var stats = randomizer.Randomize();
            mockDice.Verify(d => d.d6(3, 0), Times.Exactly(stats.Count));
        }

        [Test]
        public void RawStatsReturnsUnmodified3d6PerStat()
        {
            mockDice.Setup(d => d.d6(3, 0)).Returns(12);
            var stats = randomizer.Randomize();

            foreach (var stat in stats.Values)
                Assert.That(stat.Value, Is.EqualTo(12));
        }

        [Test]
        public void RolledStatsAreAlwaysAllowed()
        {
            mockDice.SetupSequence(d => d.d6(3, 0)).Returns(9266).Returns(-42).Returns(Int32.MaxValue).Returns(Int32.MinValue).Returns(0)
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