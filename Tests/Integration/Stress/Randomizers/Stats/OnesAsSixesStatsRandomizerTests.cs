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

        [Test]
        public void NonDefaultStatsOccur()
        {
            var stats = GenerateOrFail(OnesAsSixesStatsRandomizer.Randomize, ss => ss.Values.Any(s => s.Value != 10));
            var allStatsAreDefault = stats.Values.All(s => s.Value == 10);
            Assert.That(allStatsAreDefault, Is.False);
        }
    }
}