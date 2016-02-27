using CharacterGen.Generators.Domain.Randomizers.Stats;
using CharacterGen.Generators.Randomizers.Stats;
using Moq;
using NUnit.Framework;
using RollGen;
using System.Linq;

namespace CharacterGen.Tests.Unit.Generators.Randomizers.Stats
{
    [TestFixture]
    public class OnesAsSixesStatsRandomizerTests
    {
        private IStatsRandomizer randomizer;
        private Mock<Dice> mockDice;

        [SetUp]
        public void Setup()
        {
            mockDice = new Mock<Dice>();
            var generator = new ConfigurableIterationGenerator(2);
            randomizer = new OnesAsSixesStatsRandomizer(mockDice.Object, generator);

            mockDice.Setup(d => d.Roll(3).IndividualRolls(6)).Returns(new[] { 2, 3, 4 });
        }

        [Test]
        public void OnesAsSixesStatsCalls3d6OncePerStat()
        {
            var stats = randomizer.Randomize();
            mockDice.Verify(d => d.Roll(3).IndividualRolls(6), Times.Exactly(stats.Count));
        }

        [Test]
        public void OnesAsSixesTreatsOnesAsSixes()
        {
            mockDice.Setup(d => d.Roll(3).IndividualRolls(6)).Returns(new[] { 2, 3, 1 });

            var stats = randomizer.Randomize();
            var stat = stats.Values.First();
            Assert.That(stat.Value, Is.EqualTo(11));
        }
    }
}