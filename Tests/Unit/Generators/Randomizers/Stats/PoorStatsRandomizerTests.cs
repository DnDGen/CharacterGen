using CharacterGen.Generators;
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
    public class PoorStatsRandomizerTests
    {
        private IStatsRandomizer randomizer;
        private Mock<IDice> mockDice;
        private Generator generator;

        private const Int32 min = 3;
        private const Int32 max = 9;
        private Int32 middle;

        [SetUp]
        public void Setup()
        {
            middle = (max + min) / 2;

            generator = new ConfigurableIterationGenerator(2);
            mockDice = new Mock<IDice>();
            mockDice.SetupSequence(d => d.Roll(3).d6()).Returns(min).Returns(max).Returns(middle).Returns(min - 1).Returns(max + 1).Returns(middle);

            randomizer = new PoorStatsRandomizer(mockDice.Object, generator);
        }

        [Test]
        public void PoorCalls3d6PerStat()
        {
            var stats = randomizer.Randomize();
            mockDice.Verify(d => d.Roll(3).d6(), Times.Exactly(stats.Count));
        }

        [Test]
        public void AllowIfStatAverageIsLessThanTen()
        {
            var stats = randomizer.Randomize();
            var average = stats.Values.Average(s => s.Value);
            Assert.That(average, Is.InRange(min, max));
        }

        [Test]
        public void RerollIfStatAverageIsNotLessThanTen()
        {
            mockDice.SetupSequence(d => d.Roll(3).d6())
                .Returns(min).Returns(max).Returns(middle).Returns(min - 1).Returns(max + 1).Returns(9000) //total is invalid average
                .Returns(min).Returns(max).Returns(middle).Returns(min - 1).Returns(max + 1).Returns(middle); //total is valid average

            var stats = randomizer.Randomize();
            mockDice.Verify(d => d.Roll(3).d6(), Times.Exactly(stats.Count * 2));
        }

        [Test]
        public void DefaultValueIs9()
        {
            mockDice.Setup(d => d.Roll(3).d6()).Returns(19);

            var stats = randomizer.Randomize();

            foreach (var stat in stats.Values)
                Assert.That(stat.Value, Is.EqualTo(9));
        }
    }
}