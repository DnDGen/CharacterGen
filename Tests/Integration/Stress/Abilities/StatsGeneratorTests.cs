using CharacterGen.Common.Abilities.Stats;
using CharacterGen.Generators.Abilities;
using CharacterGen.Generators.Randomizers.Stats;
using Ninject;
using NUnit.Framework;
using System;

namespace CharacterGen.Tests.Integration.Stress.Abilities
{
    [TestFixture]
    public class StatsGeneratorTests : StressTests
    {
        [Inject]
        public IStatsGenerator StatsGenerator { get; set; }
        [Inject, Named(StatsRandomizerTypeConstants.Raw)]
        public IStatsRandomizer StatsRandomizer { get; set; }

        [TestCase("StatsGenerator")]
        public override void Stress(String stressSubject)
        {
            Stress();
        }

        protected override void MakeAssertions()
        {
            var alignment = GetNewAlignment();
            var characterClass = GetNewCharacterClass(alignment);
            var race = RaceGenerator.GenerateWith(alignment, characterClass, BaseRaceRandomizer, MetaraceRandomizer);
            var stats = StatsGenerator.GenerateWith(StatsRandomizer, characterClass, race);

            Assert.That(stats.Count, Is.EqualTo(6));
            Assert.That(stats.Keys, Contains.Item(StatConstants.Charisma));
            Assert.That(stats.Keys, Contains.Item(StatConstants.Constitution));
            Assert.That(stats.Keys, Contains.Item(StatConstants.Dexterity));
            Assert.That(stats.Keys, Contains.Item(StatConstants.Intelligence));
            Assert.That(stats.Keys, Contains.Item(StatConstants.Strength));
            Assert.That(stats.Keys, Contains.Item(StatConstants.Wisdom));
            Assert.That(stats[StatConstants.Charisma].Value, Is.Positive);
            Assert.That(stats[StatConstants.Constitution].Value, Is.Not.Negative);
            Assert.That(stats[StatConstants.Dexterity].Value, Is.Positive);
            Assert.That(stats[StatConstants.Intelligence].Value, Is.Positive);
            Assert.That(stats[StatConstants.Strength].Value, Is.Positive);
            Assert.That(stats[StatConstants.Wisdom].Value, Is.Positive);
        }
    }
}