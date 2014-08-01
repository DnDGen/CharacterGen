using Ninject;
using NPCGen.Generators.Interfaces.Abilities;
using NPCGen.Generators.Interfaces.Combats;
using NPCGen.Generators.Interfaces.Items;
using NPCGen.Generators.Interfaces.Randomizers.Stats;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Stress.Combats
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
        public IEquipmentGenerator EquipmentGenerator { get; set; }

        protected override void MakeAssertions()
        {
            var alignment = GetNewAlignment();
            var characterClass = GetNewCharacterClass(alignment);
            var race = GetNewRace(alignment, characterClass);

            var baseAttack = CombatGenerator.GenerateBaseAttackWith(characterClass);
            Assert.That(baseAttack.Bonus, Is.Not.Negative);

            var ability = AbilitiesGenerator.GenerateWith(characterClass, race, StatsRandomizer, baseAttack);
            var equipment = EquipmentGenerator.GenerateWith(ability.Feats, characterClass);

            var combat = CombatGenerator.GenerateWith(baseAttack, ability.Feats, ability.Stats, equipment);
            Assert.That(combat.ArmorClass.FlatFooted, Is.Positive);
            Assert.That(combat.ArmorClass.Full, Is.Positive);
            Assert.That(combat.ArmorClass.Touch, Is.Positive);
            Assert.That(combat.HitPoints, Is.AtLeast(characterClass.Level));
            Assert.That(combat.SavingThrows.Fortitude, Is.Positive);
            Assert.That(combat.SavingThrows.Reflex, Is.Positive);
            Assert.That(combat.SavingThrows.Will, Is.Positive);
            Assert.That(combat.BaseAttack, Is.EqualTo(baseAttack));
        }
    }
}