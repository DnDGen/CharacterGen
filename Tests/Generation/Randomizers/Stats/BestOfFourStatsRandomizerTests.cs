using System.Linq;
using D20Dice.Dice;
using Moq;
using NPCGen.Core.Generation.Randomizers.Stats;
using NPCGen.Core.Generation.Randomizers.Stats.Interfaces;
using NUnit.Framework;

namespace NPCGen.Tests.Generation.Randomizers.Stats
{
    [TestFixture]
    public class BestOfFourStatsRandomizerTests
    {
        private IStatsRandomizer randomizer;
        private Mock<IDice> mockDice;

        [SetUp]
        public void Setup()
        {
            mockDice = new Mock<IDice>();
            randomizer = new BestOfFourStatsRandomizer(mockDice.Object);
        }

        [Test]
        public void BestOfFourStatsCalls1d6FourTimesPerStat()
        {
            var stats = randomizer.Randomize();
            mockDice.Verify(d => d.d6(1, 0), Times.Exactly(stats.Count * 4));
        }

        [Test]
        public void BestOfFourIgnoresLowestRollPerStat()
        {
            mockDice.SetupSequence(d => d.d6(1, 0)).Returns(1).Returns(2).Returns(3).Returns(4);

            var stats = randomizer.Randomize();
            var stat = stats.Values.First();
            Assert.That(stat.Value, Is.EqualTo(9));
        }
    }
}