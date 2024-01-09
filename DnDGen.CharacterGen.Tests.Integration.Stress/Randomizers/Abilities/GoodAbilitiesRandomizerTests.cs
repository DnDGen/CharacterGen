using DnDGen.CharacterGen.Abilities;
using DnDGen.CharacterGen.Randomizers.Abilities;
using NUnit.Framework;
using System.Linq;

namespace DnDGen.CharacterGen.Tests.Integration.Stress.Randomizers.Abilities
{
    [TestFixture]
    public class GoodAbilitiesRandomizerTests : StressTests
    {
        private IAbilitiesRandomizer goodAbilitiesRandomizer;

        [SetUp]
        public void Setup()
        {
            goodAbilitiesRandomizer = GetNewInstanceOf<IAbilitiesRandomizer>(AbilitiesRandomizerTypeConstants.Good);
        }

        [Test]
        public void StressGoodAbilities()
        {
            stressor.Stress(AssertAbilities);
        }

        protected void AssertAbilities()
        {
            var stats = goodAbilitiesRandomizer.Randomize();

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

            var average = stats.Values.Average(s => s.Value);
            Assert.That(average, Is.InRange(13, 16));
        }

        [Test]
        public void NonDefaultGoodAbilitiesOccur()
        {
            var stats = stressor.GenerateOrFail(goodAbilitiesRandomizer.Randomize, ss => ss.Values.Any(s => s.Value != 13));
            var allAbilitiesAreDefault = stats.Values.All(s => s.Value == 13);
            Assert.That(allAbilitiesAreDefault, Is.False);
        }
    }
}