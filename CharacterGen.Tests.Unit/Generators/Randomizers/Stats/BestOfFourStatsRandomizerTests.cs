using CharacterGen.Domain.Generators.Randomizers.Stats;
using CharacterGen.Randomizers.Stats;
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

            mockDice.Setup(d => d.Roll("4d6k3").AsSum()).Returns(3);
        }

        [Test]
        public void BestOfFourStatsCalls4d6k3OncePerStat()
        {
            var stats = randomizer.Randomize();
            mockDice.Verify(d => d.Roll("4d6k3").AsSum(), Times.Exactly(stats.Count));
        }

        [Test]
        public void BestOfFourIgnoresLowestRollPerStat()
        {
            mockDice.SetupSequence(d => d.Roll("4d6k3").AsSum()).Returns(9);

            var stats = randomizer.Randomize();
            var stat = stats.Values.First();
            Assert.That(stat.Value, Is.EqualTo(9));
        }
    }
}