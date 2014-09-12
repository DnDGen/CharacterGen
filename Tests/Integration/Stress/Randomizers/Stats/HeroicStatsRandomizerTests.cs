using System;
using System.Collections.Generic;
using System.Linq;
using Ninject;
using NPCGen.Common.Abilities;
using NPCGen.Common.Abilities.Stats;
using NPCGen.Generators.Interfaces.Randomizers.Stats;
using NPCGen.Generators.Randomizers.Stats;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Stress.Randomizers.Stats
{
    [TestFixture]
    public class HeroicStatsRandomizerTests : StressTests
    {
        [Inject, Named(StatsRandomizerTypeConstants.Heroic)]
        public IStatsRandomizer HeroicStatsRandomizer { get; set; }

        private IEnumerable<String> statNames;

        [SetUp]
        public void Setup()
        {
            statNames = StatConstants.GetStats();
        }

        [Test]
        public override void Stress()
        {
            do MakeAssertions();
            while (TestShouldKeepRunning());

            AssertIterations();
        }

        private void MakeAssertions()
        {
            var stats = HeroicStatsRandomizer.Randomize();

            foreach (var name in statNames)
            {
                Assert.That(stats.Keys, Contains.Item(name));
                Assert.That(stats[name].Value, Is.InRange<Int32>(3, 18));
            }

            Assert.That(stats.Count, Is.EqualTo(6));
            var average = stats.Values.Average(s => s.Value);
            Assert.That(average, Is.InRange<Double>(16, 18));
        }
    }
}