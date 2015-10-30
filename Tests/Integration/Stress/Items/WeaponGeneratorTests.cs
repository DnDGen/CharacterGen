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
            var race = GetNewRace(alignment, characterClass);
            var baseAttack = CombatGenerator.GenerateBaseAttackWith(characterClass, race);
            var ability = AbilitiesGenerator.GenerateWith(characterClass, race, StatsRandomizer, baseAttack);

            return WeaponGenerator.GenerateFrom(ability.Feats, characterClass, race);
        }

        [Test]
        public void MeleeWeaponHappens()
        {
            var weapon = Generate<Item>(GetWeapon,
                w => w != null && w.Attributes.Contains(AttributeConstants.Melee));

            Assert.That(weapon.Attributes, Contains.Item(AttributeConstants.Melee));
        }

        [Test]
        public void RangedWeaponHappens()
        {
            var weapon = Generate<Item>(GetWeapon,
                w => w != null && w.Attributes.Contains(AttributeConstants.Melee) == false);

            Assert.That(weapon.Attributes, Contains.Item(AttributeConstants.Ranged));
        }

        [Test]
        public void MundaneWeaponHappens()
        {
            var weapon = Generate<Item>(GetWeapon,
                w => w != null && w.IsMagical == false);

            Assert.That(weapon.IsMagical, Is.False);
        }

        [Test]
        public void MagicalWeaponHappens()
        {
            var weapon = Generate<Item>(GetWeapon,
                w => w != null && w.IsMagical);

            Assert.That(weapon.IsMagical, Is.True);
        }

        [Test]
        public void UncursedWeaponHappens()
        {
            var weapon = Generate<Item>(GetWeapon,
                w => w != null && String.IsNullOrEmpty(w.Magic.Curse));

            Assert.That(weapon.Magic.Curse, Is.Empty);
        }

        [Test]
        public void CursedWeaponHappens()
        {
            var weapon = Generate<Item>(GetWeapon,
                w => w != null && String.IsNullOrEmpty(w.Magic.Curse) == false);

            Assert.That(weapon.Magic.Curse, Is.Not.Empty);
        }

        [Test]
        public void SpecificCursedWeaponHappens()
        {
            var weapon = Generate<Item>(GetWeapon,
                w => w != null && String.IsNullOrEmpty(w.Magic.Curse) == false && w.Attributes.Contains(AttributeConstants.Specific));

            Assert.That(weapon.Magic.Curse, Is.Not.Empty);
            Assert.That(weapon.Attributes, Contains.Item(AttributeConstants.Specific));
        }

        [Test]
        public void IntelligentWeaponHappens()
        {
            var weapon = Generate<Item>(GetWeapon,
                w => w != null && w.Magic.Intelligence.Ego > 0);

            Assert.That(weapon.Magic.Intelligence.Ego, Is.Positive);
        }

        [Test]
        public void NonIntelligentWeaponHappens()
        {
            var weapon = Generate<Item>(GetWeapon,
                w => w != null && w.Magic.Intelligence.Ego == 0);

            Assert.That(weapon.Magic.Intelligence.Ego, Is.EqualTo(0));
        }

        [Test]
        public void AmmunitionHappens()
        {
            var weapon = Generate<Item>(GetWeapon,
                w => w != null && w.Attributes.Contains(AttributeConstants.Ammunition));

            Assert.That(weapon.Attributes, Contains.Item(AttributeConstants.Ammunition));
        }

        [Test]
        public void NoWeaponHappens()
        {
            var weapon = Generate<Item>(GetWeapon,
                w => w == null);

            Assert.That(weapon, Is.Null);
        }
    }
}
