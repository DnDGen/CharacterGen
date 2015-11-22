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
            var generator = new ConfigurableIterationGenerator(2);
            mockDice.Setup(d => d.Roll(1).d6()).Returns(1);

            randomizer = new HeroicStatsRandomizer(mockDice.Object, generator);
        }

        [Test]
        public void HeroicStatsCalls1d6ThreeTimesPerStat()
        {
            var stats = randomizer.Randomize();
            mockDice.Verify(d => d.Roll(1).d6(), Times.Exactly(stats.Count * 3));
        }

        [Test]
        public void HeroicStatRollsTreatsOnesAsSixes()
        {
            var sequence = mockDice.SetupSequence(d => d.Roll(1).d6());
            for (var i = 0; i < 6; i++)
                sequence = sequence.Returns(1).Returns(5).Returns(6);

            var stats = randomizer.Randomize();
            var stat = stats.Values.First();
            Assert.That(stat.Value, Is.EqualTo(17));
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
            var sequence = mockDice.SetupSequence(d => d.Roll(1).d6());
            for (var i = 0; i < 6; i++)
                sequence = sequence.Returns(5).Returns(5).Returns(5); //invalid average

            for (var i = 0; i < 6; i++)
                sequence = sequence.Returns(1).Returns(6).Returns(5); //valid average

            var stats = randomizer.Randomize();
            var average = stats.Average(s => s.Value.Value);
            Assert.That(average, Is.EqualTo(middle));
        }

        [Test]
        public void DefaultValueIs16()
        {
            mockDice.Setup(d => d.Roll(1).d6()).Returns(2);

            var stats = randomizer.Randomize();

            foreach (var stat in stats.Values)
                Assert.That(stat.Value, Is.EqualTo(16));
        }
    }
}