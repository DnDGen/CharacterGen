using CharacterGen.Abilities.Stats;
using CharacterGen.Randomizers.Stats;
using Ninject;
using NUnit.Framework;
using System;
using System.Linq;

namespace CharacterGen.Tests.Integration.Stress.Randomizers.Stats
{
    [TestFixture]
    public class RawStatsRandomizerTests : StressTests
    {
        [Inject, Named(StatsRandomizerTypeConstants.Raw)]
        public IStatsRandomizer RawStatsRandomizer { get; set; }

        [TestCase("RawStatsRandomizer")]
        public override void Stress(string stressSubject)
        {
            Stress();
        }

        protected override void MakeAssertions()
        {
            var stats = RawStatsRandomizer.Randomize();

            Assert.That(stats.Count, Is.EqualTo(6));
            Assert.That(stats.Keys, Contains.Item(StatConstants.Charisma));
            Assert.That(stats.Keys, Contains.Item(StatConstants.Constitution));
            Assert.That(stats.Keys, Contains.Item(StatConstants.Dexterity));
            Assert.That(stats.Keys, Contains.Item(StatConstants.Intelligence));
            Assert.That(stats.Keys, Contains.Item(StatConstants.Strength));
            Assert.That(stats.Keys, Contains.Item(StatConstants.Wisdom));
            Assert.That(stats[StatConstants.Charisma].Value, Is.InRange(3, 18));
            Assert.That(stats[StatConstants.Constitution].Value, Is.InRange(3, 18));
            Assert.That(stats[StatConstants.Dexterity].Value, Is.InRange(3, 18));
            Assert.That(stats[StatConstants.Intelligence].Value, Is.InRange(3, 18));
            Assert.That(stats[StatConstants.Strength].Value, Is.InRange(3, 18));
            Assert.That(stats[StatConstants.Wisdom].Value, Is.InRange(3, 18));

            Assert.That(stats.Count, Is.EqualTo(6));
        }

        [Test]
        public void NonDefaultStatsOccur()
        {
            var stats = GenerateOrFail(RawStatsRandomizer.Randomize, ss => ss.Values.Any(s => s.Value != 10));
            var allStatsAreDefault = stats.Values.All(s => s.Value == 10);
            Assert.That(allStatsAreDefault, Is.False);
        }
    }
}