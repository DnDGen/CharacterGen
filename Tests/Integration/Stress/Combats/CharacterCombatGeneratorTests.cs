using CharacterGen.Common.Abilities.Stats;
using CharacterGen.Generators.Abilities;
using CharacterGen.Generators.Combats;
using CharacterGen.Generators.Items;
using CharacterGen.Generators.Randomizers.Stats;
using Ninject;
using NUnit.Framework;
using System;

namespace CharacterGen.Tests.Integration.Stress.Combats
{
    [TestFixture]
    public class CharacterCombatGeneratorTests : StressTests
    {
        [Inject, Named(CombatGeneratorTypeConstants.Character)]
        public ICombatGenerator CombatGenerator { get; set; }
        [Inject, Named(AbilitiesGeneratorTypeConstants.Character)]
        public IAbilitiesGenerator AbilitiesGenerator { get; set; }
        [Inject, Named(StatsRandomizerTypeConstants.Raw)]
        public IStatsRandomizer StatsRandomizer { get; set; }
        [Inject]
        public IEquipmentGenerator TreasureGenerator { get; set; }

        [TestCase("CharacterCombatGenerator")]
        public override void Stress(String stressSubject)
        {
            Stress();
        }

        protected override void MakeAssertions()
        {
            var alignment = GetNewAlignment();
            var characterClass = GetNewCharacterClass(alignment);
            var race = GetNewRace(alignment, characterClass);

            var baseAttack = CombatGenerator.GenerateBaseAttackWith(characterClass, race);
            Assert.That(baseAttack.Bonus, Is.Not.Negative);

            var ability = AbilitiesGenerator.GenerateWith(characterClass, race, StatsRandomizer, baseAttack);
            var equipment = TreasureGenerator.GenerateWith(ability.Feats, characterClass, race);

            var combat = CombatGenerator.GenerateWith(baseAttack, characterClass, race, ability.Feats, ability.Stats, equipment);
            Assert.That(combat.ArmorClass.FlatFooted, Is.Positive);
            Assert.That(combat.ArmorClass.Full, Is.Positive);
            Assert.That(combat.ArmorClass.Touch, Is.Positive);
            Assert.That(combat.HitPoints, Is.AtLeast(characterClass.Level));
            Assert.That(combat.SavingThrows.Fortitude, Is.Positive);
            Assert.That(combat.SavingThrows.Reflex, Is.Positive);
            Assert.That(combat.SavingThrows.Will, Is.Positive);
            Assert.That(combat.BaseAttack, Is.EqualTo(baseAttack));
            Assert.That(combat.AdjustedDexterityBonus, Is.AtMost(ability.Stats[StatConstants.Dexterity].Bonus));
        }
    }
}