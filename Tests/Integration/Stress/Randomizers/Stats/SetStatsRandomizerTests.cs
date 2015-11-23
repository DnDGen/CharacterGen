using CharacterGen.Common.Abilities.Stats;
using CharacterGen.Generators.Randomizers.Stats;
using Ninject;
using NUnit.Framework;
using System;

namespace CharacterGen.Tests.Integration.Stress.Randomizers.Stats
{
    [TestFixture]
    public class SetStatsRandomizerTests : StressTests
    {
        [Inject]
        public ISetStatsRandomizer SetStatsRandomizer { get; set; }
        [Inject]
        public Random Random { get; set; }

        [TestCase("SetStatsRandomizer")]
        public override void Stress(String stressSubject)
        {
            Stress();
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

            Assert.That(stats.Count, Is.EqualTo(6));
            Assert.That(stats.Keys, Contains.Item(StatConstants.Charisma));
            Assert.That(stats.Keys, Contains.Item(StatConstants.Constitution));
            Assert.That(stats.Keys, Contains.Item(StatConstants.Dexterity));
            Assert.That(stats.Keys, Contains.Item(StatConstants.Intelligence));
            Assert.That(stats.Keys, Contains.Item(StatConstants.Strength));
            Assert.That(stats.Keys, Contains.Item(StatConstants.Wisdom));
            Assert.That(stats[StatConstants.Charisma].Value, Is.EqualTo(SetStatsRandomizer.SetCharisma));
            Assert.That(stats[StatConstants.Constitution].Value, Is.EqualTo(SetStatsRandomizer.SetConstitution));
            Assert.That(stats[StatConstants.Dexterity].Value, Is.EqualTo(SetStatsRandomizer.SetDexterity));
            Assert.That(stats[StatConstants.Intelligence].Value, Is.EqualTo(SetStatsRandomizer.SetIntelligence));
            Assert.That(stats[StatConstants.Strength].Value, Is.EqualTo(SetStatsRandomizer.SetStrength));
            Assert.That(stats[StatConstants.Wisdom].Value, Is.EqualTo(SetStatsRandomizer.SetWisdom));
        }
    }
}