using System;
using System.Collections.Generic;
using Ninject;
using NPCGen.Common.Abilities.Stats;
using NPCGen.Generators.Interfaces.Randomizers.Stats;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Stress.Randomizers.Stats
{
    [TestFixture]
    public class SetStatsRandomizerTests : StressTests
    {
        [Inject]
        public ISetStatsRandomizer SetStatsRandomizer { get; set; }
        [Inject]
        public Random Random { get; set; }

        private IEnumerable<String> statNames;

        [SetUp]
        public void Setup()
        {
            statNames = StatConstants.GetStats();
        }

        protected override void MakeAssertions()
        {
            SetStatsRandomizer.SetCharisma = Random.Next();
            SetStatsRandomizer.SetConstitution = Random.Next();
            SetStatsRandomizer.SetDexterity = Random.Next();
            SetStatsRandomizer.SetIntelligence = Random.Next();
            SetStatsRandomizer.SetStrength = Random.Next();
            SetStatsRandomizer.SetWisdom = Random.Next();

            var stats = SetStatsRandomizer.Randomize();

            foreach (var name in statNames)
                Assert.That(stats.Keys, Contains.Item(name));

            Assert.That(stats.Count, Is.EqualTo(6));
            Assert.That(stats[StatConstants.Charisma].Value, Is.EqualTo(SetStatsRandomizer.SetCharisma));
            Assert.That(stats[StatConstants.Constitution].Value, Is.EqualTo(SetStatsRandomizer.SetConstitution));
            Assert.That(stats[StatConstants.Dexterity].Value, Is.EqualTo(SetStatsRandomizer.SetDexterity));
            Assert.That(stats[StatConstants.Intelligence].Value, Is.EqualTo(SetStatsRandomizer.SetIntelligence));
            Assert.That(stats[StatConstants.Strength].Value, Is.EqualTo(SetStatsRandomizer.SetStrength));
            Assert.That(stats[StatConstants.Wisdom].Value, Is.EqualTo(SetStatsRandomizer.SetWisdom));
        }
    }
}