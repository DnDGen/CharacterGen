using Ninject;
using NPCGen.Core.Data.CharacterClasses;
using NPCGen.Core.Data.Races;
using NPCGen.Core.Data.Stats;
using NPCGen.Core.Generation.Factories.Interfaces;
using NPCGen.Core.Generation.Randomizers.Stats.Interfaces;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Stress.Generation.Factories
{
    [TestFixture]
    public class StatsFactoryTests : StressTest
    {
        [Inject]
        public IStatsFactory StatsFactory { get; set; }
        [Inject]
        public IStatsRandomizer StatsRandomizer { get; set; }

        [SetUp]
        public void Setup()
        {
            StartTest();
        }

        [TearDown]
        public void TearDown()
        {
            StopTest();
        }

        [Test]
        public void StatsFactoryReturnsStats()
        {
            while (TestShouldKeepRunning())
            {
                var characterClass = GetNewInstanceOf<CharacterClass>();
                var race = GetNewInstanceOf<Race>();
                var stats = StatsFactory.CreateWith(StatsRandomizer, characterClass, race);
                Assert.That(stats, Is.Not.Null);
            }

            AssertIterations();
        }

        [Test]
        public void StatsFactoryReturnsStatsWithAllStats()
        {
            var statNames = StatConstants.GetStats();

            while (TestShouldKeepRunning())
            {
                var characterClass = GetNewInstanceOf<CharacterClass>();
                var race = GetNewInstanceOf<Race>();
                var stats = StatsFactory.CreateWith(StatsRandomizer, characterClass, race);

                foreach (var statName in statNames)
                {
                    Assert.That(stats.ContainsKey(statName), Is.True);
                    Assert.That(stats[statName], Is.Not.Null);
                    Assert.That(stats[statName].Value, Is.GreaterThan(0));
                }
            }

            AssertIterations();
        }
    }
}