﻿using CharacterGen.Common.Abilities.Stats;
using CharacterGen.Generators.Abilities;
using CharacterGen.Generators.Combats;
using CharacterGen.Generators.Randomizers.Stats;
using Ninject;
using NUnit.Framework;
using System;

namespace CharacterGen.Tests.Integration.Stress.Abilities
{
    [TestFixture]
    public class AbilitiesGeneratorTests : StressTests
    {
        [Inject]
        public IAbilitiesGenerator AbilitiesGenerator { get; set; }
        [Inject, Named(StatsRandomizerTypeConstants.Raw)]
        public IStatsRandomizer StatsRandomizer { get; set; }
        [Inject]
        public ICombatGenerator CombatGenerator { get; set; }

        [TestCase("Abilities Generator")]
        public override void Stress(String stressSubject)
        {
            Stress();
        }

        protected override void MakeAssertions()
        {
            var alignment = GetNewAlignment();
            var characterClass = GetNewCharacterClass(alignment);
            var race = RaceGenerator.GenerateWith(alignment, characterClass, BaseRaceRandomizer, MetaraceRandomizer);
            var baseAttack = CombatGenerator.GenerateBaseAttackWith(characterClass, race);

            var ability = AbilitiesGenerator.GenerateWith(characterClass, race, StatsRandomizer, baseAttack);
            Assert.That(ability.Feats, Is.Not.Empty);
            Assert.That(ability.Languages, Is.Not.Empty);

            Assert.That(ability.Stats.Count, Is.EqualTo(6));
            Assert.That(ability.Stats.Keys, Contains.Item(StatConstants.Charisma));
            Assert.That(ability.Stats.Keys, Contains.Item(StatConstants.Constitution));
            Assert.That(ability.Stats.Keys, Contains.Item(StatConstants.Dexterity));
            Assert.That(ability.Stats.Keys, Contains.Item(StatConstants.Intelligence));
            Assert.That(ability.Stats.Keys, Contains.Item(StatConstants.Strength));
            Assert.That(ability.Stats.Keys, Contains.Item(StatConstants.Wisdom));
            Assert.That(ability.Stats[StatConstants.Charisma].Value, Is.Positive);
            Assert.That(ability.Stats[StatConstants.Constitution].Value, Is.Not.Negative);
            Assert.That(ability.Stats[StatConstants.Dexterity].Value, Is.Positive);
            Assert.That(ability.Stats[StatConstants.Intelligence].Value, Is.Positive);
            Assert.That(ability.Stats[StatConstants.Strength].Value, Is.Positive);
            Assert.That(ability.Stats[StatConstants.Wisdom].Value, Is.Positive);

            Assert.That(ability.Skills, Is.Not.Empty);

            foreach (var skill in ability.Skills.Values)
            {
                Assert.That(skill.BaseStat, Is.Not.Null);
                Assert.That(ability.Stats.Values, Contains.Item(skill.BaseStat));
            }
        }
    }
}