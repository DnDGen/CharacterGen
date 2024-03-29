﻿using DnDGen.CharacterGen.Abilities;
using DnDGen.CharacterGen.Randomizers.Abilities;
using NUnit.Framework;
using System.Linq;

namespace DnDGen.CharacterGen.Tests.Integration.Stress.Randomizers.Abilities
{
    [TestFixture]
    public class OnesAsSixesAbilitiesRandomizerTests : StressTests
    {
        private IAbilitiesRandomizer onesAsSixesAbilitiesRandomizer;

        [SetUp]
        public void Setup()
        {
            onesAsSixesAbilitiesRandomizer = GetNewInstanceOf<IAbilitiesRandomizer>(AbilitiesRandomizerTypeConstants.OnesAsSixes);
        }

        [Test]
        public void StressOnesAsSixesAbilities()
        {
            stressor.Stress(AssertAbilities);
        }

        protected void AssertAbilities()
        {
            var stats = onesAsSixesAbilitiesRandomizer.Randomize();

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
        }

        [Test]
        public void NonDefaultOnesAsSixesAbilitiesOccur()
        {
            var stats = stressor.GenerateOrFail(onesAsSixesAbilitiesRandomizer.Randomize, ss => ss.Values.Any(s => s.Value != 10));
            var allAbilitiesAreDefault = stats.Values.All(s => s.Value == 10);
            Assert.That(allAbilitiesAreDefault, Is.False);
        }
    }
}