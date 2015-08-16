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
        [Inject]
        public IAbilitiesGenerator AbilitiesGenerator { get; set; }
        [Inject, Named(StatsRandomizerTypeConstants.Raw)]
        public IStatsRandomizer StatsRandomizer { get; set; }
        [Inject, Named(ItemTypeConstants.Weapon)]
        public GearGenerator WeaponGenerator { get; set; }
        [Inject]
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
            var race = GetNewRace(alignment, characterClass);
            var baseAttack = CombatGenerator.GenerateBaseAttackWith(characterClass, race);
            var ability = AbilitiesGenerator.GenerateWith(characterClass, race, StatsRandomizer, baseAttack);

            return WeaponGenerator.GenerateFrom(ability.Feats, characterClass);
        }

        [Test]
        public void MeleeWeaponHappens()
        {
            var weapon = new Item();

            do weapon = GetWeapon();
            while (TestShouldKeepRunning() && weapon.Attributes.Contains(AttributeConstants.Melee) == false);

            Assert.That(weapon.Attributes, Contains.Item(AttributeConstants.Melee));
        }

        [Test]
        public void RangedWeaponHappens()
        {
            var weapon = new Item();

            do weapon = GetWeapon();
            while (TestShouldKeepRunning() && weapon.Attributes.Contains(AttributeConstants.Melee));

            Assert.That(weapon.Attributes, Contains.Item(AttributeConstants.Ranged));
        }

        [Test]
        public void MundaneWeaponHappens()
        {
            var weapon = new Item();

            do weapon = GetWeapon();
            while (TestShouldKeepRunning() && weapon.IsMagical);

            Assert.That(weapon.IsMagical, Is.False);
        }

        [Test]
        public void MagicalWeaponHappens()
        {
            var weapon = new Item();

            do weapon = GetWeapon();
            while (TestShouldKeepRunning() && weapon.IsMagical == false);

            Assert.That(weapon.IsMagical, Is.True);
        }

        [Test]
        public void UncursedWeaponHappens()
        {
            var weapon = new Item();

            do weapon = GetWeapon();
            while (TestShouldKeepRunning() && weapon.Magic.Curse.Length > 0);

            Assert.That(weapon.Magic.Curse, Is.Empty);
        }

        [Test]
        public void CursedWeaponHappens()
        {
            var weapon = new Item();

            do weapon = GetWeapon();
            while (TestShouldKeepRunning() && weapon.Magic.Curse.Length == 0);

            Assert.That(weapon.Magic.Curse, Is.Not.Empty);
        }

        [Test]
        public void SpecificCursedWeaponHappens()
        {
            var weapon = new Item();

            do weapon = GetWeapon();
            while (TestShouldKeepRunning() && (weapon.Magic.Curse.Length == 0 || weapon.Attributes.Contains(AttributeConstants.Specific) == false));

            Assert.That(weapon.Magic.Curse, Is.Not.Empty);
            Assert.That(weapon.Magic.Curse, Contains.Item(AttributeConstants.Specific));
        }

        [Test]
        public void IntelligentWeaponHappens()
        {
            var weapon = new Item();

            do weapon = GetWeapon();
            while (TestShouldKeepRunning() && weapon.Magic.Intelligence.Ego == 0);

            Assert.That(weapon.Magic.Intelligence.Ego, Is.Positive);
        }

        [Test]
        public void NonIntelligentWeaponHappens()
        {
            var weapon = new Item();

            do weapon = GetWeapon();
            while (TestShouldKeepRunning() && weapon.Magic.Intelligence.Ego > 0);

            Assert.That(weapon.Magic.Intelligence.Ego, Is.EqualTo(0));
        }
    }
}
