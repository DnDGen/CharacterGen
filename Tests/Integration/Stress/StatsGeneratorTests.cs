﻿using Ninject;
using NPCGen.Common.Stats;
using NPCGen.Generators.Interfaces;
using NPCGen.Generators.Interfaces.Randomizers.Stats;
using NPCGen.Tests.Integration.Common;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Stress
{
    [TestFixture]
    public class StatsGeneratorTests : StressTests
    {
        [Inject]
        public IStatsGenerator StatsGenerator { get; set; }
        [Inject]
        public IStatsRandomizer StatsRandomizer { get; set; }

        [Test]
        public void StatsGeneratorReturnsStats()
        {
            while (TestShouldKeepRunning())
            {
                var data = GetNewInstanceOf<DependentDataCollection>();
                var stats = StatsGenerator.CreateWith(StatsRandomizer, data.CharacterClass, data.Race);
                Assert.That(stats, Is.Not.Null);
            }

            AssertIterations();
        }

        [Test]
        public void StatsGeneratorReturnsStatsWithAllStats()
        {
            var statNames = StatConstants.GetStats();

            while (TestShouldKeepRunning())
            {
                var data = GetNewInstanceOf<DependentDataCollection>();
                var stats = StatsGenerator.CreateWith(StatsRandomizer, data.CharacterClass, data.Race);

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