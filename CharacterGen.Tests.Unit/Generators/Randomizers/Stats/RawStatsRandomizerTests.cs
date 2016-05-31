using CharacterGen.Abilities.Stats;
using CharacterGen.Domain.Generators.Randomizers.Stats;
using CharacterGen.Randomizers.Stats;
using Moq;
using NUnit.Framework;
using RollGen;

namespace CharacterGen.Tests.Unit.Generators.Randomizers.Stats
{
    [TestFixture]
    public class RawStatsRandomizerTests
    {
        private IStatsRandomizer randomizer;
        private Mock<Dice> mockDice;

        [SetUp]
        public void Setup()
        {
            mockDice = new Mock<Dice>();
            var generator = new ConfigurableIterationGenerator(2);
            randomizer = new RawStatsRandomizer(mockDice.Object, generator);

            mockDice.Setup(d => d.Roll(It.IsAny<int>()).IndividualRolls(It.IsAny<int>())).Returns(new[] { 1 });
        }

        [Test]
        public void RawStatsCalls3d6PerStat()
        {
            var stats = randomizer.Randomize();
            mockDice.Verify(d => d.Roll(3).IndividualRolls(6), Times.Exactly(stats.Count));
        }

        [Test]
        public void RawStatsReturnsUnmodified3d6PerStat()
        {
            mockDice.Setup(d => d.Roll(3).IndividualRolls(6)).Returns(new[] { 4, 5, 3 });
            var stats = randomizer.Randomize();

            foreach (var stat in stats.Values)
                Assert.That(stat.Value, Is.EqualTo(12));
        }

        [Test]
        public void RolledStatsAreAlwaysAllowed()
        {
            mockDice.SetupSequence(d => d.Roll(3).IndividualRolls(6))
                .Returns(new[] { 9266 }).Returns(new[] { -42 }).Returns(new[] { int.MaxValue })
                .Returns(new[] { int.MinValue }).Returns(new[] { 0 }).Returns(new[] { 1337 });

            var stats = randomizer.Randomize();

            Assert.That(stats[StatConstants.Charisma].Value, Is.EqualTo(9266));
            Assert.That(stats[StatConstants.Constitution].Value, Is.EqualTo(-42));
            Assert.That(stats[StatConstants.Dexterity].Value, Is.EqualTo(int.MaxValue));
            Assert.That(stats[StatConstants.Intelligence].Value, Is.EqualTo(int.MinValue));
            Assert.That(stats[StatConstants.Strength].Value, Is.EqualTo(0));
            Assert.That(stats[StatConstants.Wisdom].Value, Is.EqualTo(1337));
        }
    }
}