using System;
using System.Collections.Generic;
using System.Linq;
using Ninject;
using NPCGen.Core.Data.Stats;
using NPCGen.Core.Generation.Randomizers.Stats;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Stress.Generation.Randomizers.Stats
{
    [TestFixture]
    public class TwoTenSidedDiceStatsRandomizerTests : StressTest
    {
        [Inject]
        public TwoTenSidedDiceStatsRandomizer StatsRandomizer { get; set; }

        private IEnumerable<String> statNames;

        [SetUp]
        public void Setup()
        {
            statNames = StatConstants.GetStats();
            StartTest();
        }

        [TearDown]
        public void TearDown()
        {
            StopTest();
        }

        [Test]
        public void TwoTenSidedDiceStatsRandomizerReturnsTwoTenSidedDiceStats()
        {
            while (TestShouldKeepRunning())
            {
                var stats = StatsRandomizer.Randomize();
                Assert.That(stats, Is.Not.Null);

                foreach (var name in statNames)
                {
                    Assert.That(stats.Keys.Contains(name), Is.True);
                    Assert.That(stats[name].Value, Is.InRange<Int32>(2, 20));
                }

                Assert.That(stats.Keys.Count, Is.EqualTo(statNames.Count()));
            }

            AssertIterations();
        }
    }
}