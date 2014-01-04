using System;
using System.Linq;
using D20Dice;
using Moq;
using NPCGen.Core.Generation.Randomizers.Stats;
using NPCGen.Core.Generation.Randomizers.Stats.Interfaces;
using NUnit.Framework;

namespace NPCGen.Tests.Unit.Generation.Randomizers.Stats
{
    [TestFixture]
    public class PoorStatsRandomizerTests
    {
        private IStatsRandomizer randomizer;
        private Mock<IDice> mockDice;

        private const Int32 min = 3;
        private const Int32 max = 9;
        private Int32 middle;

        [SetUp]
        public void Setup()
        {
            middle = (max + min) / 2;

            mockDice = new Mock<IDice>();
            mockDice.SetupSequence(d => d.d6(3)).Returns(min).Returns(max).Returns(middle).Returns(min - 1).Returns(max + 1).Returns(middle);

            randomizer = new PoorStatsRandomizer(mockDice.Object);
        }

        [Test]
        public void PoorCalls3d6PerStat()
        {
            var stats = randomizer.Randomize();
            mockDice.Verify(d => d.d6(3), Times.Exactly(stats.Count));
        }

        [Test]
        public void AllowIfStatAverageIsLessThanTen()
        {
            var stats = randomizer.Randomize();
            var average = stats.Values.Average(s => s.Value);
            Assert.That(average, Is.InRange<Double>(min, max));
        }

        [Test]
        public void RerollIfStatAverageIsNotLessThanTen()
        {
            mockDice.SetupSequence(d => d.d6(3))
                .Returns(min).Returns(max).Returns(middle).Returns(min - 1).Returns(max + 1).Returns(9000) //total is invalid average
                .Returns(min).Returns(max).Returns(middle).Returns(min - 1).Returns(max + 1).Returns(middle); //total is valid average

            var stats = randomizer.Randomize();
            mockDice.Verify(d => d.d6(3), Times.Exactly(stats.Count * 2));
        }
    }
}