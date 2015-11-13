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
    public class PoorStatsRandomizerTests : StressTests
    {
        [Inject, Named(StatsRandomizerTypeConstants.Poor)]
        public IStatsRandomizer PoorStatsRandomizer { get; set; }

        private IEnumerable<String> statNames;

        [SetUp]
        public void Setup()
        {
            statNames = StatConstants.GetStats();
        }

        [TestCase("PoorStatsRandomizer")]
        public override void Stress(String stressSubject)
        {
            Stress();
        }

        protected override void MakeAssertions()
        {
            var stats = PoorStatsRandomizer.Randomize();

            foreach (var name in statNames)
            {
                Assert.That(stats.Keys, Contains.Item(name));
                Assert.That(stats[name].Value, Is.InRange<Int32>(3, 18));
            }

            Assert.That(stats.Count, Is.EqualTo(6));
            var average = stats.Values.Average(s => s.Value);
            Assert.That(average, Is.LessThan(10));
        }

        [Test]
        public void NonDefaultStatsOccur()
        {
            var stats = GenerateOrFail(PoorStatsRandomizer.Randomize, ss => ss.Values.Any(s => s.Value != 9));
            var allStatsAreDefault = stats.Values.All(s => s.Value == 9);
            Assert.That(allStatsAreDefault, Is.False);
        }
    }
}