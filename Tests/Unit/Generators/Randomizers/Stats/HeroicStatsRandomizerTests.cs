using CharacterGen.Generators.Domain.Randomizers.Stats;
using CharacterGen.Generators.Randomizers.Stats;
using Moq;
using NUnit.Framework;
using RollGen;
using System;
using System.Linq;

namespace CharacterGen.Tests.Unit.Generators.Randomizers.Stats
{
    [TestFixture]
    public class HeroicStatsRandomizerTests
    {
        private IStatsRandomizer randomizer;
        private Mock<IDice> mockDice;

        private const Int32 min = 16;
        private const Int32 max = 18;
        private Int32 middle;

        [SetUp]
        public void Setup()
        {
            middle = (max + min) / 2;

            mockDice = new Mock<IDice>();
            mockDice.SetupSequence(d => d.Roll(3).d6()).Returns(min).Returns(max).Returns(middle).Returns(min - 1).Returns(max + 1).Returns(middle);

            randomizer = new HeroicStatsRandomizer(mockDice.Object);
        }

        [Test]
        public void HeroicCalls3d6PerStat()
        {
            var stats = randomizer.Randomize();
            mockDice.Verify(d => d.Roll(3).d6(), Times.Exactly(stats.Count));
        }

        [Test]
        public void AllowIfStatAverageIsInRange()
        {
            var stats = randomizer.Randomize();
            var average = stats.Values.Average(s => s.Value);
            Assert.That(average, Is.InRange<Double>(min, max));
        }

        [Test]
        public void RerollIfStatAverageIsLessThanSixteen()
        {
            mockDice.SetupSequence(d => d.Roll(3).d6())
                .Returns(min).Returns(max).Returns(middle).Returns(min - 1).Returns(max + 1).Returns(3) //invalid average
                .Returns(min).Returns(max).Returns(middle).Returns(min - 1).Returns(max + 1).Returns(middle); //valid average

            var stats = randomizer.Randomize();
            mockDice.Verify(d => d.Roll(3).d6(), Times.Exactly(stats.Count * 2));
        }

        [Test]
        public void DefaultValueIs16()
        {
            mockDice.Setup(d => d.Roll(3).d6()).Returns(9);

            var stats = randomizer.Randomize();

            foreach (var stat in stats)
                Assert.That(stat.Value, Is.EqualTo(16));
        }
    }
}