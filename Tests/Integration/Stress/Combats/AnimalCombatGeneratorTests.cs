using CharacterGen.Common.Abilities.Stats;
using CharacterGen.Common.Items;
using CharacterGen.Generators.Abilities;
using CharacterGen.Generators.Combats;
using CharacterGen.Generators.Randomizers.Stats;
using Ninject;
using NUnit.Framework;
using System;

namespace CharacterGen.Tests.Integration.Stress.Combats
{
    [TestFixture]
    public class AnimalCombatGeneratorTests : StressTests
    {
        [Inject, Named(CombatGeneratorTypeConstants.Animal)]
        public ICombatGenerator AnimalCombatGenerator { get; set; }
        [Inject, Named(AbilitiesGeneratorTypeConstants.Animal)]
        public IAbilitiesGenerator AnimalAbilitiesGenerator { get; set; }
        [Inject, Named(StatsRandomizerTypeConstants.Raw)]
        public IStatsRandomizer StatsRandomizer { get; set; }

        [TestCase("AnimalCombatGenerator")]
        public override void Stress(String stressSubject)
        {
            Stress();
        }

        protected override void MakeAssertions()
        {
            var alignment = GetNewAlignment();
            var characterClass = GetNewCharacterClass(alignment);
            var race = GetNewRace(alignment, characterClass);

            var baseAttack = AnimalCombatGenerator.GenerateBaseAttackWith(characterClass, race);
            Assert.That(baseAttack.Bonus, Is.Not.Negative);

            var ability = AnimalAbilitiesGenerator.GenerateWith(characterClass, race, StatsRandomizer, baseAttack);
            var equipment = new Equipment();

            var combat = AnimalCombatGenerator.GenerateWith(baseAttack, characterClass, race, ability.Feats, ability.Stats, equipment);
            Assert.That(combat.ArmorClass.FlatFooted, Is.Positive);
            Assert.That(combat.ArmorClass.Full, Is.Positive);
            Assert.That(combat.ArmorClass.Touch, Is.Positive);
            Assert.That(combat.HitPoints, Is.Positive);
            Assert.That(combat.SavingThrows.Fortitude, Is.Positive);
            Assert.That(combat.SavingThrows.Reflex, Is.Positive);
            Assert.That(combat.SavingThrows.Will, Is.Positive);
            Assert.That(combat.BaseAttack, Is.EqualTo(baseAttack));
            Assert.That(combat.AdjustedDexterityBonus, Is.EqualTo(ability.Stats[StatConstants.Dexterity].Bonus));
        }
    }
}
