﻿using CharacterGen.Abilities.Stats;
using CharacterGen.Randomizers.Stats;
using Ninject;
using NUnit.Framework;
using System.Linq;

namespace CharacterGen.Tests.Integration.Stress.Randomizers.Stats
{
    [TestFixture]
    public class HeroicStatsRandomizerTests : StressTests
    {
        [Inject, Named(StatsRandomizerTypeConstants.Heroic)]
        public IStatsRandomizer HeroicStatsRandomizer { get; set; }

        [Test]
        public void Stress()
        {
            Stress(AssertStats);
        }

        protected void AssertStats()
        {
            var stats = HeroicStatsRandomizer.Randomize();
            Assert.That(stats, Is.Not.Null);

            Assert.That(stats.Count, Is.EqualTo(6));
            Assert.That(stats.Keys, Contains.Item(StatConstants.Charisma));
            Assert.That(stats.Keys, Contains.Item(StatConstants.Constitution));
            Assert.That(stats.Keys, Contains.Item(StatConstants.Dexterity));
            Assert.That(stats.Keys, Contains.Item(StatConstants.Intelligence));
            Assert.That(stats.Keys, Contains.Item(StatConstants.Strength));
            Assert.That(stats.Keys, Contains.Item(StatConstants.Wisdom));
            Assert.That(stats[StatConstants.Charisma].Value, Is.InRange(6, 18));
            Assert.That(stats[StatConstants.Constitution].Value, Is.InRange(6, 18));
            Assert.That(stats[StatConstants.Dexterity].Value, Is.InRange(6, 18));
            Assert.That(stats[StatConstants.Intelligence].Value, Is.InRange(6, 18));
            Assert.That(stats[StatConstants.Strength].Value, Is.InRange(6, 18));
            Assert.That(stats[StatConstants.Wisdom].Value, Is.InRange(6, 18));

            Assert.That(stats.Count, Is.EqualTo(6));
            var average = stats.Values.Average(s => s.Value);
            Assert.That(average, Is.InRange(16, 18));
        }

        [Test]
        public void NonDefaultStatsOccur()
        {
            var stats = GenerateOrFail(HeroicStatsRandomizer.Randomize, ss => ss.Values.Any(s => s.Value != 16));
            var allStatsAreDefault = stats.Values.All(s => s.Value == 16);
            Assert.That(allStatsAreDefault, Is.False);
        }
    }
}