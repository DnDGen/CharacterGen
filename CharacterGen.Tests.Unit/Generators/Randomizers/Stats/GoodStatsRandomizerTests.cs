using CharacterGen.Domain.Generators.Randomizers.Stats;
using CharacterGen.Randomizers.Stats;
using Moq;
using NUnit.Framework;
using RollGen;
using System.Linq;

namespace CharacterGen.Tests.Unit.Generators.Randomizers.Stats
{
    [TestFixture]
    public class GoodStatsRandomizerTests
    {
        private const int min = 13;
        private const int max = 15;
        private const int middle = (max + min) / 2;

        private IStatsRandomizer randomizer;
        private Mock<Dice> mockDice;


        [SetUp]
        public void Setup()
        {
            mockDice = new Mock<Dice>();
            var generator = new ConfigurableIterationGenerator(2);
            mockDice.SetupSequence(d => d.Roll(3).IndividualRolls(6))
                .Returns(new[] { min }).Returns(new[] { max }).Returns(new[] { middle })
                .Returns(new[] { min - 1 }).Returns(new[] { max + 1 }).Returns(new[] { middle });

            randomizer = new GoodStatsRandomizer(mockDice.Object, generator);
        }

        [Test]
        public void GoodCalls3d6PerStat()
        {
            var stats = randomizer.Randomize();
            mockDice.Verify(d => d.Roll(3).IndividualRolls(6), Times.Exactly(stats.Count));
        }

        [Test]
        public void AllowIfStatAverageIsInRange()
        {
            var stats = randomizer.Randomize();
            var average = stats.Values.Average(s => s.Value);
            Assert.That(average, Is.InRange(min, max));
        }

        [Test]
        public void RerollIfStatAverageIsGreaterThanFifteen()
        {
            mockDice.SetupSequence(d => d.Roll(3).IndividualRolls(6))
                .Returns(new[] { min }).Returns(new[] { max }).Returns(new[] { middle })
                .Returns(new[] { min - 1 }).Returns(new[] { max + 1 }).Returns(new[] { 9000 }) //invalid average
                .Returns(new[] { min }).Returns(new[] { max }).Returns(new[] { middle })
                .Returns(new[] { min - 1 }).Returns(new[] { max + 1 }).Returns(new[] { middle }); //valid average

            var stats = randomizer.Randomize();
            mockDice.Verify(d => d.Roll(3).IndividualRolls(6), Times.Exactly(stats.Count * 2));
        }

        [Test]
        public void RerollIfStatAverageIsLessThanThirteen()
        {
            mockDice.SetupSequence(d => d.Roll(3).IndividualRolls(6))
                .Returns(new[] { min }).Returns(new[] { max }).Returns(new[] { middle })
                .Returns(new[] { min - 1 }).Returns(new[] { max + 1 }).Returns(new[] { 3 }) //invalid average
                .Returns(new[] { min }).Returns(new[] { max }).Returns(new[] { middle })
                .Returns(new[] { min - 1 }).Returns(new[] { max + 1 }).Returns(new[] { middle }); //valid average

            var stats = randomizer.Randomize();
            mockDice.Verify(d => d.Roll(3).IndividualRolls(6), Times.Exactly(stats.Count * 2));
        }

        [Test]
        public void DefaultValueIs13()
        {
            mockDice.Setup(d => d.Roll(3).IndividualRolls(6)).Returns(new[] { 9 });

            var stats = randomizer.Randomize();

            foreach (var stat in stats.Values)
                Assert.That(stat.Value, Is.EqualTo(13));
        }
    }
}