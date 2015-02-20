using System;
using System.Collections.Generic;
using Ninject;
using NPCGen.Common.Abilities.Stats;
using NPCGen.Generators.Interfaces.Randomizers.Stats;
using NPCGen.Generators.Randomizers.Stats;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Stress.Randomizers.Stats
{
    [TestFixture]
    public class OnesAsSixesStatsRandomizerTests : StressTests
    {
        [Inject, Named(StatsRandomizerTypeConstants.OnesAsSixes)]
        public IStatsRandomizer OnesAsSixesStatsRandomizer { get; set; }

        private IEnumerable<String> statNames;

        [SetUp]
        public void Setup()
        {
            statNames = StatConstants.GetStats();
        }

        [TestCase("OnesAsSixesStatsRandomizer")]
        public override void Stress(String stressSubject)
        {
            Stress();
        }

        protected override void MakeAssertions()
        {
            var stats = OnesAsSixesStatsRandomizer.Randomize();

            foreach (var name in statNames)
            {
                Assert.That(stats.Keys, Contains.Item(name));
                Assert.That(stats[name].Value, Is.InRange<Int32>(6, 18));
            }

            Assert.That(stats.Count, Is.EqualTo(6));
        }
    }
}