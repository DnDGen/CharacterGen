using System;
using Ninject;
using CharacterGen.Common.Abilities.Stats;
using CharacterGen.Generators.Abilities;
using CharacterGen.Generators.Combats;
using CharacterGen.Generators.Items;
using CharacterGen.Generators.Randomizers.Stats;
using NUnit.Framework;

namespace CharacterGen.Tests.Integration.Stress.Combats
{
    [TestFixture]
    public class CombatGeneratorTests : StressTests
    {
        [Inject]
        public ICombatGenerator CombatGenerator { get; set; }
        [Inject]
        public IAbilitiesGenerator AbilitiesGenerator { get; set; }
        [Inject, Named(StatsRandomizerTypeConstants.Raw)]
        public IStatsRandomizer StatsRandomizer { get; set; }
        [Inject]
        public IEquipmentGenerator TreasureGenerator { get; set; }

        [TestCase("CombatGenerator")]
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
            var equipment = TreasureGenerator.GenerateWith(ability.Feats, characterClass);

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