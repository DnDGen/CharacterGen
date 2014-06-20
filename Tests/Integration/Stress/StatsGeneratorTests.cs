using System;
using System.Collections.Generic;
using System.Linq;
using Ninject;
using NPCGen.Common.Stats;
using NPCGen.Generators.Interfaces;
using NPCGen.Generators.Interfaces.Randomizers.Stats;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Stress
{
    [TestFixture]
    public class StatsGeneratorTests : StressTests
    {
        [Inject]
        public IStatsGenerator StatsGenerator { get; set; }
        [Inject, Named(StatsRandomizerTypeConstants.Raw)]
        public IStatsRandomizer StatsRandomizer { get; set; }

        private IEnumerable<String> statNames;

        [SetUp]
        public void Setup()
        {
            statNames = StatConstants.GetStats();
        }

        protected override void MakeAssertions()
        {
            var data = GetNewDependentData();
            var stats = StatsGenerator.CreateWith(StatsRandomizer, data.CharacterClass, data.Race);

            foreach (var statName in statNames)
            {
                Assert.That(stats.Keys, Contains.Item(statName));
                Assert.That(stats[statName].Value, Is.Positive);
            }

            var extras = stats.Keys.Except(statNames);
            Assert.That(extras, Is.Empty);
        }
    }
}