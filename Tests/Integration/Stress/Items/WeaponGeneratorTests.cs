using CharacterGen.Common.Abilities;
using CharacterGen.Common.CharacterClasses;
using CharacterGen.Common.Races;
using CharacterGen.Generators.Abilities;
using CharacterGen.Generators.Combats;
using CharacterGen.Generators.Items;
using CharacterGen.Generators.Randomizers.Stats;
using CharacterGen.Selectors;
using CharacterGen.Tables;
using Ninject;
using NUnit.Framework;
using System.Linq;
using TreasureGen.Common.Items;

namespace CharacterGen.Tests.Integration.Stress.Items
{
    [TestFixture]
    public class WeaponGeneratorTests : StressTests
    {
        [Inject]
        public IWeaponGenerator WeaponGenerator { get; set; }
        [Inject]
        public IAbilitiesGenerator AbilitiesGenerator { get; set; }
        [Inject, Named(StatsRandomizerTypeConstants.Raw)]
        public IStatsRandomizer StatsRandomizer { get; set; }
        [Inject]
        public ICombatGenerator CombatGenerator { get; set; }
        [Inject]
        public ICollectionsSelector CollectionsSelector { get; set; }

        [TestCase("Weapon Generator")]
        public override void Stress(string stressSubject)
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
            Assert.That(weapon, Is.Not.Null);
            Assert.That(weapon.ItemType, Is.EqualTo(ItemTypeConstants.Weapon));
            Assert.That(weapon.Name, Is.Not.Empty);
        }

        [Test]
        public void StressMeleeWeapon()
        {
            Stress(MakeMeleeAssertions);
        }

        private void MakeMeleeAssertions()
        {
            var alignment = GetNewAlignment();
            var characterClass = GetNewCharacterClass(alignment);
            var race = RaceGenerator.GenerateWith(alignment, characterClass, BaseRaceRandomizer, MetaraceRandomizer);
            var baseAttack = CombatGenerator.GenerateBaseAttackWith(characterClass, race);
            var ability = AbilitiesGenerator.GenerateWith(characterClass, race, StatsRandomizer, baseAttack);

            var weapon = WeaponGenerator.GenerateMeleeFrom(ability.Feats, characterClass, race);
            Assert.That(weapon, Is.Not.Null);
            Assert.That(weapon.ItemType, Is.EqualTo(ItemTypeConstants.Weapon));
            Assert.That(weapon.Name, Is.Not.Empty);
            Assert.That(weapon.Attributes, Contains.Item(AttributeConstants.Melee));
        }

        [Test]
        public void StressRangedWeapon()
        {
            Stress(MakeRangedAssertions);
        }

        private void MakeRangedAssertions()
        {
            var alignment = GetNewAlignment();
            var characterClass = GetNewCharacterClass(alignment);
            var race = RaceGenerator.GenerateWith(alignment, characterClass, BaseRaceRandomizer, MetaraceRandomizer);
            var baseAttack = CombatGenerator.GenerateBaseAttackWith(characterClass, race);
            var ability = AbilitiesGenerator.GenerateWith(characterClass, race, StatsRandomizer, baseAttack);

            var weapon = WeaponGenerator.GenerateRangedFrom(ability.Feats, characterClass, race);
            Assert.That(weapon, Is.Not.Null);
            Assert.That(weapon.ItemType, Is.EqualTo(ItemTypeConstants.Weapon));
            Assert.That(weapon.Name, Is.Not.Empty);
            Assert.That(weapon.Attributes, Is.All.Not.EqualTo(AttributeConstants.Melee));
        }

        [Test]
        public void StressOneHandedMeleeWeapon()
        {
            Stress(MakeOneHandedMeleeAssertions);
        }

        private void MakeOneHandedMeleeAssertions()
        {
            var alignment = GetNewAlignment();
            var characterClass = GetNewCharacterClass(alignment);
            var race = RaceGenerator.GenerateWith(alignment, characterClass, BaseRaceRandomizer, MetaraceRandomizer);
            var baseAttack = CombatGenerator.GenerateBaseAttackWith(characterClass, race);
            var ability = AbilitiesGenerator.GenerateWith(characterClass, race, StatsRandomizer, baseAttack);

            var weapon = WeaponGenerator.GenerateOneHandedMeleeFrom(ability.Feats, characterClass, race);
            Assert.That(weapon, Is.Not.Null);
            Assert.That(weapon.ItemType, Is.EqualTo(ItemTypeConstants.Weapon));
            Assert.That(weapon.Name, Is.Not.Empty);
            Assert.That(weapon.Attributes, Contains.Item(AttributeConstants.Melee));
            Assert.That(weapon.Attributes, Is.All.Not.EqualTo(AttributeConstants.TwoHanded));
        }

        [Test]
        public void StressAmmunition()
        {
            Stress(MakeAmmunitionAssertions);
        }

        private void MakeAmmunitionAssertions()
        {
            CharacterClass characterClass;
            Race race;
            Ability ability;
            Item rangedWeapon;

            do
            {
                var alignment = GetNewAlignment();
                characterClass = GetNewCharacterClass(alignment);
                race = RaceGenerator.GenerateWith(alignment, characterClass, BaseRaceRandomizer, MetaraceRandomizer);
                var baseAttack = CombatGenerator.GenerateBaseAttackWith(characterClass, race);
                ability = AbilitiesGenerator.GenerateWith(characterClass, race, StatsRandomizer, baseAttack);
                rangedWeapon = WeaponGenerator.GenerateRangedFrom(ability.Feats, characterClass, race);
            }
            while (rangedWeapon == null || CollectionsSelector.SelectFrom(TableNameConstants.Set.Collection.ItemGroups, rangedWeapon.Name).Count() < 2);

            var ammunitionType = CollectionsSelector.SelectFrom(TableNameConstants.Set.Collection.ItemGroups, rangedWeapon.Name).Last();

            var ammunition = WeaponGenerator.GenerateAmmunition(ability.Feats, characterClass, race, ammunitionType);
            Assert.That(ammunition, Is.Not.Null, ammunitionType);
            Assert.That(ammunition.Name, Is.Not.Empty, ammunitionType);
            Assert.That(ammunition.ItemType, Is.EqualTo(ItemTypeConstants.Weapon), ammunition.Name);
            Assert.That(ammunition.Attributes, Is.All.Not.EqualTo(AttributeConstants.Melee), ammunition.Name);
            Assert.That(ammunition.Attributes, Contains.Item(AttributeConstants.Ammunition), ammunition.Name);
        }
    }
}
