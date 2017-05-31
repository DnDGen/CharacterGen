using CharacterGen.Abilities;
using CharacterGen.Randomizers.Abilities;
using Ninject;
using NUnit.Framework;
using System.Linq;

namespace CharacterGen.Tests.Integration.Stress.Randomizers.Abilities
{
    [TestFixture]
    public class RawAbilitiesRandomizerTests : StressTests
    {
        [Inject, Named(AbilitiesRandomizerTypeConstants.Raw)]
        public IAbilitiesRandomizer RawAbilitiesRandomizer { get; set; }

        [Test]
        public void Stress()
        {
            Stress(AssertAbilities);
        }

        protected void AssertAbilities()
        {
            var stats = RawAbilitiesRandomizer.Randomize();

            Assert.That(stats.Count, Is.EqualTo(6));
            Assert.That(stats.Keys, Contains.Item(AbilityConstants.Charisma));
            Assert.That(stats.Keys, Contains.Item(AbilityConstants.Constitution));
            Assert.That(stats.Keys, Contains.Item(AbilityConstants.Dexterity));
            Assert.That(stats.Keys, Contains.Item(AbilityConstants.Intelligence));
            Assert.That(stats.Keys, Contains.Item(AbilityConstants.Strength));
            Assert.That(stats.Keys, Contains.Item(AbilityConstants.Wisdom));
            Assert.That(stats[AbilityConstants.Charisma].Value, Is.InRange(3, 18));
            Assert.That(stats[AbilityConstants.Constitution].Value, Is.InRange(3, 18));
            Assert.That(stats[AbilityConstants.Dexterity].Value, Is.InRange(3, 18));
            Assert.That(stats[AbilityConstants.Intelligence].Value, Is.InRange(3, 18));
            Assert.That(stats[AbilityConstants.Strength].Value, Is.InRange(3, 18));
            Assert.That(stats[AbilityConstants.Wisdom].Value, Is.InRange(3, 18));
        }

        [Test]
        public void NonDefaultAbilitiesOccur()
        {
            var stats = GenerateOrFail(RawAbilitiesRandomizer.Randomize, ss => ss.Values.Any(s => s.Value != 10));
            var allAbilitiesAreDefault = stats.Values.All(s => s.Value == 10);
            Assert.That(allAbilitiesAreDefault, Is.False);
        }
    }
}