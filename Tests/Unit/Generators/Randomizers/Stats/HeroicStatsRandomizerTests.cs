using CharacterGen.Generators.Domain.Randomizers.Stats;
using CharacterGen.Generators.Randomizers.Stats;
using Moq;
using NUnit.Framework;
using RollGen;
using System.Linq;

namespace CharacterGen.Tests.Unit.Generators.Randomizers.Stats
{
    [TestFixture]
    public class HeroicStatsRandomizerTests
    {
        private const int min = 16;
        private const int max = 18;
        private const int middle = (max + min) / 2;

        private IStatsRandomizer randomizer;
        private Mock<Dice> mockDice;

        [SetUp]
        public void Setup()
        {
            mockDice = new Mock<Dice>();
            var generator = new ConfigurableIterationGenerator(2);
            mockDice.Setup(d => d.Roll(3).IndividualRolls(6)).Returns(new[] { 1, 1, 1 });

            randomizer = new HeroicStatsRandomizer(mockDice.Object, generator);
        }

        [Test]
        public void HeroicStatsCalls3d6OnceTimesPerStat()
        {
            var stats = randomizer.Randomize();
            mockDice.Verify(d => d.Roll(3).IndividualRolls(6), Times.Exactly(stats.Count));
        }

        [Test]
        public void HeroicStatRollsTreatsOnesAsSixes()
        {
            var sequence = mockDice.SetupSequence(d => d.Roll(3).IndividualRolls(6));
            for (var i = 0; i < 6; i++)
                sequence = sequence.Returns(new[] { 1, 5, 6 });

            var stats = randomizer.Randomize();
            var stat = stats.Values.First();
            Assert.That(stat.Value, Is.EqualTo(17));
        }

        [Test]
        public void AllowIfStatAverageIsInRange()
        {
            var stats = randomizer.Randomize();
            var average = stats.Values.Average(s => s.Value);
            Assert.That(average, Is.InRange(min, max));
        }

        [Test]
        public void RerollIfStatAverageIsLessThanSixteen()
        {
            var sequence = mockDice.SetupSequence(d => d.Roll(3).IndividualRolls(6));
            for (var i = 0; i < 6; i++)
                sequence = sequence.Returns(new[] { 5, 5, 5 }); //invalid average

            for (var i = 0; i < 6; i++)
                sequence = sequence.Returns(new[] { 1, 6, 5 }); //valid average

            var stats = randomizer.Randomize();
            var average = stats.Average(s => s.Value.Value);
            Assert.That(average, Is.EqualTo(middle));
        }

        [Test]
        public void DefaultValueIs16()
        {
            mockDice.Setup(d => d.Roll(3).IndividualRolls(6)).Returns(new[] { 2, 2, 2 });

            var stats = randomizer.Randomize();

            foreach (var stat in stats.Values)
                Assert.That(stat.Value, Is.EqualTo(16));
        }
    }
}