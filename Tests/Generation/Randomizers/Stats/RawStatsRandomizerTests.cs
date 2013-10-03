using System;
using D20Dice.Dice;
using Moq;
using NPCGen.Core.Data.Stats;
using NPCGen.Core.Generation.Randomizers.Stats;
using NPCGen.Core.Generation.Randomizers.Stats.Interfaces;
using NUnit.Framework;

namespace NPCGen.Tests.Generation.Randomizers.Stats
{
    [TestFixture]
    public class RawStatsRandomizerTests
    {
        private IStatsRandomizer randomizer;
        private Mock<IDice> mockDice;

        [SetUp]
        public void Setup()
        {
            mockDice = new Mock<IDice>();
            randomizer = new RawStatsRandomizer(mockDice.Object);
        }

        [Test]
        public void RawStatsCalls3d6PerStat()
        {
            var stats = randomizer.Randomize();
            mockDice.Verify(d => d.d6(3, 0), Times.Exactly(stats.Count));
        }

        [Test]
        public void RawStatsReturnsUnmodified3d6PerStat()
        {
            mockDice.Setup(d => d.d6(3, 0)).Returns(12);
            var stats = randomizer.Randomize();

            foreach (var stat in stats.Values)
                Assert.That(stat.Value, Is.EqualTo(12));
        }

        [Test]
        public void RolledStatsAreAlwaysAllowed()
        {
            var minStatValue = 3;
            var maxStatValue = 18;

            for (var strength = minStatValue; strength <= maxStatValue; strength++)
                for (var constitution = minStatValue; constitution <= maxStatValue; constitution++)
                    for (var dexterity = minStatValue; dexterity <= maxStatValue; dexterity++)
                        for (var intelligence = minStatValue; intelligence <= maxStatValue; intelligence++)
                            for (var wisdom = minStatValue; wisdom <= maxStatValue; wisdom++)
                                for (var charisma = minStatValue; charisma <= maxStatValue; charisma++)
                                    AssertStatValues(strength, constitution, dexterity, intelligence, wisdom, charisma);
        }

        private void AssertStatValues(Int32 strength, Int32 constitution, Int32 dexterity, Int32 intelligence, Int32 wisdom, Int32 charisma)
        {
            mockDice.SetupSequence(d => d.d6(3, 0)).Returns(strength).Returns(constitution).Returns(dexterity).Returns(intelligence)
                .Returns(wisdom).Returns(charisma);

            var stats = randomizer.Randomize();

            Assert.That(stats[StatConstants.Strength].Value, Is.EqualTo(strength));
            Assert.That(stats[StatConstants.Constitution].Value, Is.EqualTo(constitution));
            Assert.That(stats[StatConstants.Dexterity].Value, Is.EqualTo(dexterity));
            Assert.That(stats[StatConstants.Intelligence].Value, Is.EqualTo(intelligence));
            Assert.That(stats[StatConstants.Wisdom].Value, Is.EqualTo(wisdom));
            Assert.That(stats[StatConstants.Charisma].Value, Is.EqualTo(charisma));
        }
    }
}