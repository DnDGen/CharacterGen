using System;
using NPCGen.Common.Abilities.Stats;
using NPCGen.Generators.Interfaces.Randomizers.Stats;
using NPCGen.Generators.Randomizers.Stats;
using NUnit.Framework;

namespace NPCGen.Tests.Unit.Generators.Randomizers.Stats
{
    [TestFixture]
    public class SetStatsRandomizerTests
    {
        private ISetStatsRandomizer randomizer;

        [SetUp]
        public void Setup()
        {
            randomizer = new SetStatsRandomizer();
        }

        [Test]
        public void SetStatsRandomizerIsAStatsRandomizer()
        {
            Assert.That(randomizer, Is.InstanceOf<IStatsRandomizer>());
        }

        [Test]
        public void SetStatsContainAllStats()
        {
            var statNames = StatConstants.GetStats();
            var stats = randomizer.Randomize();

            foreach (var stat in statNames)
                Assert.That(stats.Keys, Contains.Item(stat));

            Assert.That(stats.Count, Is.EqualTo(6));
        }

        [Test]
        public void StatsAreNotNull()
        {
            var stats = randomizer.Randomize();

            foreach (var stat in stats)
                Assert.That(stat, Is.Not.Null);
        }

        [Test]
        public void ReturnSetStats()
        {
            randomizer.SetStrength = 9266;
            randomizer.SetCharisma = 90210;
            randomizer.SetConstitution = 42;
            randomizer.SetDexterity = -3;
            randomizer.SetIntelligence = Int32.MaxValue;
            randomizer.SetWisdom = Int32.MinValue;

            var stats = randomizer.Randomize();
            Assert.That(stats[StatConstants.Charisma].Value, Is.EqualTo(90210));
            Assert.That(stats[StatConstants.Constitution].Value, Is.EqualTo(42));
            Assert.That(stats[StatConstants.Dexterity].Value, Is.EqualTo(-3));
            Assert.That(stats[StatConstants.Intelligence].Value, Is.EqualTo(Int32.MaxValue));
            Assert.That(stats[StatConstants.Strength].Value, Is.EqualTo(9266));
            Assert.That(stats[StatConstants.Wisdom].Value, Is.EqualTo(Int32.MinValue));
        }
    }
}