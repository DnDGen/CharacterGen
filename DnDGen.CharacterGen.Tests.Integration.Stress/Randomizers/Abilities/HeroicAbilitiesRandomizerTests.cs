using DnDGen.CharacterGen.Abilities;
using DnDGen.CharacterGen.Randomizers.Abilities;
using Ninject;
using NUnit.Framework;
using System.Linq;

namespace DnDGen.CharacterGen.Tests.Integration.Stress.Randomizers.Abilities
{
    [TestFixture]
    public class HeroicAbilitiesRandomizerTests : StressTests
    {
        [Inject, Named(AbilitiesRandomizerTypeConstants.Heroic)]
        public IAbilitiesRandomizer HeroicAbilitiesRandomizer { get; set; }

        [Test]
        public void StressHeroicAbilities()
        {
            stressor.Stress(AssertAbilities);
        }

        protected void AssertAbilities()
        {
            var stats = HeroicAbilitiesRandomizer.Randomize();
            Assert.That(stats, Is.Not.Null);

            Assert.That(stats.Count, Is.EqualTo(6));
            Assert.That(stats.Keys, Contains.Item(AbilityConstants.Charisma));
            Assert.That(stats.Keys, Contains.Item(AbilityConstants.Constitution));
            Assert.That(stats.Keys, Contains.Item(AbilityConstants.Dexterity));
            Assert.That(stats.Keys, Contains.Item(AbilityConstants.Intelligence));
            Assert.That(stats.Keys, Contains.Item(AbilityConstants.Strength));
            Assert.That(stats.Keys, Contains.Item(AbilityConstants.Wisdom));
            Assert.That(stats[AbilityConstants.Charisma].Value, Is.InRange(6, 18));
            Assert.That(stats[AbilityConstants.Constitution].Value, Is.InRange(6, 18));
            Assert.That(stats[AbilityConstants.Dexterity].Value, Is.InRange(6, 18));
            Assert.That(stats[AbilityConstants.Intelligence].Value, Is.InRange(6, 18));
            Assert.That(stats[AbilityConstants.Strength].Value, Is.InRange(6, 18));
            Assert.That(stats[AbilityConstants.Wisdom].Value, Is.InRange(6, 18));

            var average = stats.Values.Average(s => s.Value);
            Assert.That(average, Is.InRange(16, 18));
        }

        [Test]
        public void NonDefaultHeroicAbilitiesOccur()
        {
            var stats = stressor.GenerateOrFail(HeroicAbilitiesRandomizer.Randomize, ss => ss.Values.Any(s => s.Value != 16));
            var allAbilitiesAreDefault = stats.Values.All(s => s.Value == 16);
            Assert.That(allAbilitiesAreDefault, Is.False);
        }
    }
}