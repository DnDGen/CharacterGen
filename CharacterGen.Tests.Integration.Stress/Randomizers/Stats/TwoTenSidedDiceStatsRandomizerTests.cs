using CharacterGen.Abilities.Stats;
using CharacterGen.Randomizers.Stats;
using Ninject;
using NUnit.Framework;
using System.Linq;

namespace CharacterGen.Tests.Integration.Stress.Randomizers.Stats
{
    [TestFixture]
    public class TwoTenSidedDiceStatsRandomizerTests : StressTests
    {
        [Inject, Named(StatsRandomizerTypeConstants.TwoTenSidedDice)]
        public IStatsRandomizer TwoTenSidedDiceStatsRandomizer { get; set; }

        [Test]
        public void Stress()
        {
            Stress(AssertStats);
        }

        protected void AssertStats()
        {
            var stats = TwoTenSidedDiceStatsRandomizer.Randomize();

            Assert.That(stats.Count, Is.EqualTo(6));
            Assert.That(stats.Keys, Contains.Item(StatConstants.Charisma));
            Assert.That(stats.Keys, Contains.Item(StatConstants.Constitution));
            Assert.That(stats.Keys, Contains.Item(StatConstants.Dexterity));
            Assert.That(stats.Keys, Contains.Item(StatConstants.Intelligence));
            Assert.That(stats.Keys, Contains.Item(StatConstants.Strength));
            Assert.That(stats.Keys, Contains.Item(StatConstants.Wisdom));

            foreach (var kvp in stats)
            {
                Assert.That(kvp.Value.Name, Is.EqualTo(kvp.Key));
                Assert.That(kvp.Value.Value, Is.InRange(2, 20), kvp.Key);
            }
        }

        [Test]
        public void NonDefaultStatsOccur()
        {
            var stats = GenerateOrFail(TwoTenSidedDiceStatsRandomizer.Randomize, ss => ss.Values.Any(s => s.Value != 10));
            var allStatsAreDefault = stats.Values.All(s => s.Value == 10);
            Assert.That(allStatsAreDefault, Is.False);
        }
    }
}