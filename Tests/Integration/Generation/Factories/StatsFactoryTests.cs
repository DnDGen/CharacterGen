using Ninject;
using NPCGen.Core.Data.CharacterClasses;
using NPCGen.Core.Data.Races;
using NPCGen.Core.Data.Stats;
using NPCGen.Core.Generation.Factories.Interfaces;
using NPCGen.Core.Generation.Randomizers.Stats;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Generation.Factories
{
    [TestFixture]
    public class StatsFactoryTests : IntegrationTest
    {
        [Inject]
        public IStatsFactory StatsFactory { get; set; }
        [Inject]
        public RawStatsRandomizer StatsRandomizer { get; set; }
        [Inject]
        public CharacterClass CharacterClass { get; set; }
        [Inject]
        public Race Race { get; set; }

        [Test]
        public void StatsFactoryReturnsStats()
        {
            for (var i = 0; i < ConfidenceLevel; i++)
            {
                var stats = StatsFactory.CreateWith(StatsRandomizer, CharacterClass, Race);
                Assert.That(stats, Is.Not.Null);
            }
        }

        [Test]
        public void StatsFactoryReturnsStatsWithAllStats()
        {
            var statNames = StatConstants.GetStats();

            for (var i = 0; i < ConfidenceLevel; i++)
            {
                var stats = StatsFactory.CreateWith(StatsRandomizer, CharacterClass, Race);

                foreach (var statName in statNames)
                {
                    Assert.That(stats.ContainsKey(statName), Is.True);
                    Assert.That(stats[statName], Is.Not.Null);
                    Assert.That(stats[statName].Value, Is.GreaterThan(0));
                }
            }
        }
    }
}