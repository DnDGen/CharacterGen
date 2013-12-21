using System;
using Ninject;
using NPCGen.Core.Data.CharacterClasses;
using NPCGen.Core.Data.Races;
using NPCGen.Core.Data.Stats;
using NPCGen.Core.Generation.Factories.Interfaces;
using NPCGen.Core.Generation.Randomizers.Stats;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Stress.Generation.Factories
{
    [TestFixture]
    public class StatsFactoryTests : StressTest
    {
        [Inject]
        public IStatsFactory StatsFactory { get; set; }
        [Inject]
        public RawStatsRandomizer StatsRandomizer { get; set; }
        [Inject]
        public CharacterClass CharacterClass { get; set; }
        [Inject]
        public Race Race { get; set; }

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
                var stats = StatsFactory.CreateWith(StatsRandomizer, CharacterClass, Race);
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
                var stats = StatsFactory.CreateWith(StatsRandomizer, CharacterClass, Race);

                foreach (var statName in statNames)
                {
                    Assert.That(stats.ContainsKey(statName), Is.True);
                    Assert.That(stats[statName], Is.Not.Null);
                    Assert.That(stats[statName].Value, Is.GreaterThan(0), GetErrorMessage(statName));
                }
            }

            AssertIterations();
        }

        private String GetErrorMessage(String statName)
        {
            return String.Format("Stat: {0}\nClass: {1} {2}\nRace: {3} {4}", statName, CharacterClass.ClassName, CharacterClass.Level,
                Race.Metarace, Race.BaseRace);
        }
    }
}