using CharacterGen.Generators.Abilities;
using CharacterGen.Generators.Combats;
using CharacterGen.Generators.Items;
using CharacterGen.Generators.Randomizers.Stats;
using Ninject;
using NUnit.Framework;
using System;
using System.Linq;
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
        [Inject]
        public IWeaponGenerator WeaponGenerator { get; set; }
        [Inject, Named(AbilitiesGeneratorTypeConstants.Character)]
        public ICombatGenerator CombatGenerator { get; set; }
        [Inject]
        public Random Random { get; set; }

        [TestCase("WeaponGenerator")]
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

            var weapon = WeaponGenerator.GenerateFrom(ability.Feats, characterClass, race);
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

        [Test]
        public void StressMeleeWeapon()
        {
            var alignment = GetNewAlignment();
            var characterClass = GetNewCharacterClass(alignment);
            var race = RaceGenerator.GenerateWith(alignment, characterClass, BaseRaceRandomizer, MetaraceRandomizer);
            var baseAttack = CombatGenerator.GenerateBaseAttackWith(characterClass, race);
            var ability = AbilitiesGenerator.GenerateWith(characterClass, race, StatsRandomizer, baseAttack);

            var weapon = WeaponGenerator.GenerateMeleeFrom(ability.Feats, characterClass, race);
            Assert.That(weapon.Name, Is.Not.Empty);
            Assert.That(weapon.ItemType, Is.EqualTo(ItemTypeConstants.Weapon), weapon.Name);
            Assert.That(weapon.Attributes, Contains.Item(AttributeConstants.Melee));
        }

        [Test]
        public void StressOneHandedMeleeWeapon()
        {
            var alignment = GetNewAlignment();
            var characterClass = GetNewCharacterClass(alignment);
            var race = RaceGenerator.GenerateWith(alignment, characterClass, BaseRaceRandomizer, MetaraceRandomizer);
            var baseAttack = CombatGenerator.GenerateBaseAttackWith(characterClass, race);
            var ability = AbilitiesGenerator.GenerateWith(characterClass, race, StatsRandomizer, baseAttack);

            var weapon = WeaponGenerator.GenerateOneHandedMeleeFrom(ability.Feats, characterClass, race);
            Assert.That(weapon.Name, Is.Not.Empty);
            Assert.That(weapon.ItemType, Is.EqualTo(ItemTypeConstants.Weapon), weapon.Name);
            Assert.That(weapon.Attributes, Contains.Item(AttributeConstants.Melee));
            Assert.That(weapon.Attributes, Is.All.Not.EqualTo(AttributeConstants.TwoHanded));
        }

        [Test]
        public void StressRangedWeapon()
        {
            var alignment = GetNewAlignment();
            var characterClass = GetNewCharacterClass(alignment);
            var race = RaceGenerator.GenerateWith(alignment, characterClass, BaseRaceRandomizer, MetaraceRandomizer);
            var baseAttack = CombatGenerator.GenerateBaseAttackWith(characterClass, race);
            var ability = AbilitiesGenerator.GenerateWith(characterClass, race, StatsRandomizer, baseAttack);

            var weapon = WeaponGenerator.GenerateRangedFrom(ability.Feats, characterClass, race);
            Assert.That(weapon.Name, Is.Not.Empty);
            Assert.That(weapon.ItemType, Is.EqualTo(ItemTypeConstants.Weapon), weapon.Name);
            Assert.That(weapon.Attributes, Is.All.Not.EqualTo(AttributeConstants.Melee));
        }

        [Test]
        public void StressAmmunition()
        {
            var ammunitionTypes = new[] { WeaponConstants.Arrow, WeaponConstants.CrossbowBolt, WeaponConstants.SlingBullet };
            var ammunitionType = ammunitionTypes.ElementAt(Random.Next(3));
            var alignment = GetNewAlignment();
            var characterClass = GetNewCharacterClass(alignment);
            var race = RaceGenerator.GenerateWith(alignment, characterClass, BaseRaceRandomizer, MetaraceRandomizer);
            var baseAttack = CombatGenerator.GenerateBaseAttackWith(characterClass, race);
            var ability = AbilitiesGenerator.GenerateWith(characterClass, race, StatsRandomizer, baseAttack);

            var ammunition = WeaponGenerator.GenerateAmmunition(ability.Feats, characterClass, race, ammunitionType);
            Assert.That(ammunition.Name, Is.Not.Empty);
            Assert.That(ammunition.ItemType, Is.EqualTo(ItemTypeConstants.Weapon), ammunition.Name);
            Assert.That(ammunition.Attributes, Contains.Item(AttributeConstants.Ammunition));
            Assert.That(ammunition.Attributes, Is.All.Not.EqualTo(AttributeConstants.Melee));
        }
    }
}
