﻿using DnDGen.CharacterGen.Abilities;
using DnDGen.CharacterGen.Randomizers.Abilities;
using NUnit.Framework;
using System.Linq;

namespace DnDGen.CharacterGen.Tests.Integration.Stress.Randomizers.Abilities
{
    [TestFixture]
    public class TwoTenSidedDiceAbilitiesRandomizerTests : StressTests
    {
        private IAbilitiesRandomizer twoTenSidedDiceAbilitiesRandomizer;

        [SetUp]
        public void Setup()
        {
            twoTenSidedDiceAbilitiesRandomizer = GetNewInstanceOf<IAbilitiesRandomizer>(AbilitiesRandomizerTypeConstants.TwoTenSidedDice);
        }

        [Test]
        public void StressTwoTenSidedDiceAbilities()
        {
            stressor.Stress(AssertAbilities);
        }

        protected void AssertAbilities()
        {
            var stats = twoTenSidedDiceAbilitiesRandomizer.Randomize();

            Assert.That(stats.Count, Is.EqualTo(6));
            Assert.That(stats.Keys, Contains.Item(AbilityConstants.Charisma));
            Assert.That(stats.Keys, Contains.Item(AbilityConstants.Constitution));
            Assert.That(stats.Keys, Contains.Item(AbilityConstants.Dexterity));
            Assert.That(stats.Keys, Contains.Item(AbilityConstants.Intelligence));
            Assert.That(stats.Keys, Contains.Item(AbilityConstants.Strength));
            Assert.That(stats.Keys, Contains.Item(AbilityConstants.Wisdom));

            foreach (var kvp in stats)
            {
                Assert.That(kvp.Value.Name, Is.EqualTo(kvp.Key));
                Assert.That(kvp.Value.Value, Is.InRange(2, 20), kvp.Key);
            }
        }

        [Test]
        public void NonDefaultTwoTenSidedDiceAbilitiesOccur()
        {
            var stats = stressor.GenerateOrFail(twoTenSidedDiceAbilitiesRandomizer.Randomize, ss => ss.Values.Any(s => s.Value != 10));
            var allAbilitiesAreDefault = stats.Values.All(s => s.Value == 10);
            Assert.That(allAbilitiesAreDefault, Is.False);
        }
    }
}