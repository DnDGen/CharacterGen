using System;
using System.Linq;
using D20Dice.Dice;
using Moq;
using NPCGen.Core.Generation.Randomizers.Stats;
using NPCGen.Core.Generation.Randomizers.Stats.Interfaces;
using NUnit.Framework;

namespace NPCGen.Tests.Generation.Randomizers.Stats
{
    [TestFixture]
    public class AverageStatsRandomizerTests
    {
        private IStatsRandomizer randomizer;
        private Mock<IDice> mockDice;

        private const Int32 min = 10;
        private const Int32 max = 12;
        private Int32 middle { get { return (min + max) / 2; } }

        [SetUp]
        public void Setup()
        {
            mockDice = new Mock<IDice>();
            mockDice.SetupSequence(d => d.d6(3, 0)).Returns(min).Returns(max).Returns(middle).Returns(min - 1).Returns(max + 1).Returns(middle);

            randomizer = new AverageStatsRandomizer(mockDice.Object);
        }

        [Test]
        public void AverageCalls3d6PerStat()
        {
            var stats = randomizer.Randomize();
            mockDice.Verify(d => d.d6(3, 0), Times.Exactly(stats.Count));
        }

        [Test]
        public void AllowIfStatAverageIsInRange()
        {
            var stats = randomizer.Randomize();
            var average = stats.Values.Average(s => s.Value);
            Assert.That(average, Is.LessThanOrEqualTo(max));
            Assert.That(average, Is.GreaterThanOrEqualTo(min));
        }

        [Test]
        public void RerollIfStatAverageIsGreaterThanTwelve()
        {
            mockDice.SetupSequence(d => d.d6(3, 0))
                .Returns(min).Returns(max).Returns(middle).Returns(min - 1).Returns(max + 1).Returns(18) //invalid average
                .Returns(min).Returns(max).Returns(middle).Returns(min - 1).Returns(max + 1).Returns(middle); //valid average

            var stats = randomizer.Randomize();
            mockDice.Verify(d => d.d6(3, 0), Times.Exactly(stats.Count * 2));
        }

        [Test]
        public void RerollIfStatAverageIsLessThanTen()
        {
            mockDice.SetupSequence(d => d.d6(3, 0))
                .Returns(min).Returns(max).Returns(middle).Returns(min - 1).Returns(max + 1).Returns(3) //invalid average
                .Returns(min).Returns(max).Returns(middle).Returns(min - 1).Returns(max + 1).Returns(middle); //valid average

            var stats = randomizer.Randomize();
            mockDice.Verify(d => d.d6(3, 0), Times.Exactly(stats.Count * 2));
        }
    }
}