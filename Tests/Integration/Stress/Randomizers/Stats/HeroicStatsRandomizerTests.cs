using CharacterGen.Common.Abilities.Stats;
using CharacterGen.Generators.Randomizers.Stats;
using Ninject;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CharacterGen.Tests.Integration.Stress.Randomizers.Stats
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

        [TestCase("HeroicStatsRandomizer")]
        public override void Stress(String stressSubject)
        {
            Stress();
        }

        protected override void MakeAssertions()
        {
            var stats = HeroicStatsRandomizer.Randomize();
            Assert.That(stats, Is.Not.Null);

            foreach (var name in statNames)
            {
                Assert.That(stats.Keys, Contains.Item(name), name);
                Assert.That(stats[name].Value, Is.InRange<Int32>(1, 18), name);
            }

            Assert.That(stats.Count, Is.EqualTo(6));
            var average = stats.Values.Average(s => s.Value);
            Assert.That(average, Is.InRange<Double>(16, 18));
        }

        [Test]
        public void NonDefaultStatsOccur()
        {
            var stats = Generate(HeroicStatsRandomizer.Randomize, ss => ss.Values.Any(s => s.Value != 16));
            var allStatsAreDefault = stats.Values.All(s => s.Value == 16);
            Assert.That(allStatsAreDefault, Is.False);
        }
    }
}