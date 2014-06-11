using System.Linq;
using D20Dice;
using Moq;
using NPCGen.Generators.Interfaces.Randomizers.Stats;
using NPCGen.Generators.Randomizers.Stats;
using NUnit.Framework;

namespace NPCGen.Tests.Unit.Generators.Randomizers.Stats
{
    [TestFixture]
    public class OnesAsSixesStatsRandomizerTests
    {
        private IStatsRandomizer randomizer;
        private Mock<IDice> mockDice;

        [SetUp]
        public void Setup()
        {
            mockDice = new Mock<IDice>();
            randomizer = new OnesAsSixesStatsRandomizer(mockDice.Object);
        }

        [Test]
        public void OnesAsSixesStatsCalls1d6ThreeTimesPerStat()
        {
            var stats = randomizer.Randomize();
            mockDice.Verify(d => d.d6(1), Times.Exactly(stats.Count * 3));
        }

        [Test]
        public void OnesAsSixesTreatsOnesAsSixes()
        {
            mockDice.SetupSequence(d => d.d6(1)).Returns(1).Returns(2).Returns(3);

            var stats = randomizer.Randomize();
            var stat = stats.Values.First();
            Assert.That(stat.Value, Is.EqualTo(11));
        }
    }
}