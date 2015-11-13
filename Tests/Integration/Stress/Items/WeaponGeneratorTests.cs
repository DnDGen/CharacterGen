using CharacterGen.Generators.Abilities;
using CharacterGen.Generators.Combats;
using CharacterGen.Generators.Items;
using CharacterGen.Generators.Randomizers.Stats;
using Ninject;
using NUnit.Framework;
using System;
using TreasureGen.Common.Items;

namespace CharacterGen.Tests.Integration.Stress.Items
{
    [TestFixture]
    public class WeaponGeneratorTests : StressTests
    {
        [Inject, Named(AbilitiesGeneratorTypeConstants.Character)]
        public IAbilitiesGenerator AbilitiesGenerator { get; set; }
        [Inject, Named(StatsRandomizerTypeConstants.Raw)]
        public IStatsRandomizer StatsRandomizer { get; set; }
        [Inject, Named(ItemTypeConstants.Weapon)]
        public GearGenerator WeaponGenerator { get; set; }
        [Inject, Named(AbilitiesGeneratorTypeConstants.Character)]
        public ICombatGenerator CombatGenerator { get; set; }

        [TestCase("WeaponGenerator")]
        public override void Stress(String stressSubject)
        {
            Stress();
        }

        protected override void MakeAssertions()
        {
            var weapon = GetWeapon();
            Assert.That(weapon.Name, Is.Not.Empty);
            Assert.That(weapon.ItemType, Is.EqualTo(ItemTypeConstants.Weapon), weapon.Name);
        }

        private Item GetWeapon()
        {
            var alignment = GetNewAlignment();
            var characterClass = GetNewCharacterClass(alignment);
            var race = RaceGenerator.GenerateWith(alignment, characterClass, BaseRaceRandomizer, MetaraceRandomizer);
            var baseAttack = CombatGenerator.GenerateBaseAttackWith(characterClass, race);
            var ability = AbilitiesGenerator.GenerateWith(characterClass, race, StatsRandomizer, baseAttack);

            return WeaponGenerator.GenerateFrom(ability.Feats, characterClass, race);
        }
    }
}
