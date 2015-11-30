using CharacterGen.Common.Abilities.Stats;
using CharacterGen.Generators.Randomizers.Stats;
using Ninject;
using NUnit.Framework;
using System;
using System.Linq;

namespace CharacterGen.Tests.Integration.Stress.Randomizers.Stats
{
    [TestFixture]
    public class AverageStatsRandomizerTests : StressTests
    {
        [Inject, Named(StatsRandomizerTypeConstants.Average)]
        public IStatsRandomizer AverageStatsRandomizer { get; set; }

        [TestCase("AverageStatsRandomizer")]
        public override void Stress(String stressSubject)
        {
            Stress();
        }

        protected override void MakeAssertions()
        {
            var stats = AverageStatsRandomizer.Randomize();

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
            var average = stats.Values.Average(s => s.Value);
            Assert.That(average, Is.InRange(10, 12));
        }

        [Test]
        public void NonDefaultStatsOccur()
        {
            var stats = GenerateOrFail(AverageStatsRandomizer.Randomize, ss => ss.Values.Any(s => s.Value != 10));
            var allStatsAreDefault = stats.Values.All(s => s.Value == 10);
            Assert.That(allStatsAreDefault, Is.False);
        }
    }
}