﻿using System;
using System.Collections.Generic;
using System.Linq;
using Ninject;
using CharacterGen.Common.Abilities.Stats;
using CharacterGen.Generators.Abilities;
using CharacterGen.Generators.Randomizers.Stats;
using NUnit.Framework;

namespace CharacterGen.Tests.Integration.Stress.Abilities
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

        [TestCase("StatsGenerator")]
        public override void Stress(String stressSubject)
        {
            Stress();
        }

        protected override void MakeAssertions()
        {
            var alignment = GetNewAlignment();
            var characterClass = GetNewCharacterClass(alignment);
            var race = GetNewRace(alignment, characterClass);
            var stats = StatsGenerator.GenerateWith(StatsRandomizer, characterClass, race);

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