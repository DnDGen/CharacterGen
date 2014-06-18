using System;
using System.Collections.Generic;
using Ninject;
using NPCGen.Common.Stats;
using NPCGen.Generators.Interfaces.Randomizers.Stats;
using NPCGen.Generators.Randomizers.Stats;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Stress.Randomizers.Stats
{
    [TestFixture]
    public class TwoTenSidedDiceStatsRandomizerTests : StressTests
    {
        [Inject, Named(StatsRandomizerTypeConstants.TwoTenSidedDice)]
        public IStatsRandomizer TwoTenSidedDiceStatsRandomizer { get; set; }

        private IEnumerable<String> statNames;

        [SetUp]
        public void Setup()
        {
            statNames = StatConstants.GetStats();
        }

        protected override void MakeAssertions()
        {
            var stats = TwoTenSidedDiceStatsRandomizer.Randomize();

            foreach (var name in statNames)
            {
                Assert.That(stats.Keys, Contains.Item(name));
                Assert.That(stats[name].Value, Is.InRange<Int32>(2, 20));
            }

            Assert.That(stats.Count, Is.EqualTo(6));
        }
    }
}