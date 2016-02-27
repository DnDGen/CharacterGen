using CharacterGen.Generators.Domain.Randomizers.Stats;
using CharacterGen.Generators.Randomizers.Stats;
using Moq;
using NUnit.Framework;
using RollGen;
using System.Linq;

namespace CharacterGen.Tests.Unit.Generators.Randomizers.Stats
{
    [TestFixture]
    public class BestOfFourStatsRandomizerTests
    {
        private IStatsRandomizer randomizer;
        private Mock<Dice> mockDice;

        [SetUp]
        public void Setup()
        {
            mockDice = new Mock<Dice>();
            var generator = new ConfigurableIterationGenerator(2);
            randomizer = new BestOfFourStatsRandomizer(mockDice.Object, generator);

            mockDice.Setup(d => d.Roll(4).IndividualRolls(6)).Returns(new[] { 1, 1, 1, 1 });
        }

        [Test]
        public void BestOfFourStatsCalls4d6OncePerStat()
        {
            var stats = randomizer.Randomize();
            mockDice.Verify(d => d.Roll(4).IndividualRolls(6), Times.Exactly(stats.Count));
        }

        [Test]
        public void BestOfFourIgnoresLowestRollPerStat()
        {
            mockDice.SetupSequence(d => d.Roll(4).IndividualRolls(6)).Returns(new[] { 2, 1, 3, 4 });

            var stats = randomizer.Randomize();
            var stat = stats.Values.First();
            Assert.That(stat.Value, Is.EqualTo(9));
        }

        [Test]
        public void BestOfFourIgnoresFirstInstanceOfLowestRoll()
        {
            mockDice.SetupSequence(d => d.Roll(4).IndividualRolls(6)).Returns(new[] { 2, 2, 3, 4 });

            var stats = randomizer.Randomize();
            var stat = stats.Values.First();
            Assert.That(stat.Value, Is.EqualTo(9));
        }
    }
}