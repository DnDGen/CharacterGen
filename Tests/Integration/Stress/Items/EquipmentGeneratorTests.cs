using CharacterGen.Common.Items;
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
    public class TreasureGeneratorTests : StressTests
    {
        [Inject, Named(AbilitiesGeneratorTypeConstants.Character)]
        public IAbilitiesGenerator AbilitiesGenerator { get; set; }
        [Inject, Named(StatsRandomizerTypeConstants.Raw)]
        public IStatsRandomizer StatsRandomizer { get; set; }
        [Inject]
        public IEquipmentGenerator EquipmentGenerator { get; set; }
        [Inject, Named(AbilitiesGeneratorTypeConstants.Character)]
        public ICombatGenerator CombatGenerator { get; set; }

        [TestCase("EquipmentGenerator")]
        public override void Stress(String stressSubject)
        {
            Stress();
        }

        protected override void MakeAssertions()
        {
            var equipment = GetEquipment();
            Assert.That(equipment.Treasure, Is.Not.Null);
            Assert.That(equipment.PrimaryHand.Name, Is.Not.Empty);
            Assert.That(equipment.PrimaryHand.ItemType, Is.EqualTo(ItemTypeConstants.Weapon));
        }

        private Equipment GetEquipment()
        {
            var alignment = GetNewAlignment();
            var characterClass = GetNewCharacterClass(alignment);
            var race = GetNewRace(alignment, characterClass);
            var baseAttack = CombatGenerator.GenerateBaseAttackWith(characterClass, race);
            var ability = AbilitiesGenerator.GenerateWith(characterClass, race, StatsRandomizer, baseAttack);

            return EquipmentGenerator.GenerateWith(ability.Feats, characterClass, race);
        }

        [Test]
        public void CoinHappens()
        {
            var equipment = Generate(GetEquipment,
                e => e.Treasure.Coin.Quantity > 0);

            Assert.That(equipment.Treasure.Coin.Quantity, Is.Positive);
        }

        [Test]
        public void GoodsHappen()
        {
            var equipment = Generate(GetEquipment,
                e => e.Treasure.Goods.Any());

            Assert.That(equipment.Treasure.Goods, Is.Not.Empty);
        }

        [Test]
        public void ItemsHappen()
        {
            var equipment = Generate(GetEquipment,
                e => e.Treasure.Items.Any());

            Assert.That(equipment.Treasure.Items, Is.Not.Empty);
        }

        [Test]
        public void TreasureDoesNotHappen()
        {
            var equipment = Generate(GetEquipment,
                e => e.Treasure.Goods.Any() == false && e.Treasure.Items.Any() == false && e.Treasure.Coin.Quantity == 0);

            Assert.That(equipment.Treasure.Items, Is.Empty);
            Assert.That(equipment.Treasure.Goods, Is.Empty);
            Assert.That(equipment.Treasure.Coin.Quantity, Is.EqualTo(0));
        }

        [Test]
        public void OffHandShieldHappens()
        {
            var equipment = Generate(GetEquipment,
                e => e.OffHand != null && e.OffHand.Attributes.Contains(AttributeConstants.Shield));

            Assert.That(equipment.OffHand.Name, Is.Not.Empty);
            Assert.That(equipment.OffHand, Is.Not.EqualTo(equipment.PrimaryHand));
            Assert.That(equipment.OffHand.ItemType, Is.EqualTo(ItemTypeConstants.Armor));
            Assert.That(equipment.OffHand.Attributes, Contains.Item(AttributeConstants.Shield));
        }

        [Test]
        public void OffHandWeaponHappens()
        {
            var equipment = Generate(GetEquipment,
                e => e.OffHand != null && e.OffHand.ItemType == ItemTypeConstants.Weapon && e.OffHand != e.PrimaryHand);

            Assert.That(equipment.OffHand.Name, Is.Not.Empty);
            Assert.That(equipment.OffHand, Is.Not.EqualTo(equipment.PrimaryHand));
            Assert.That(equipment.OffHand.ItemType, Is.EqualTo(ItemTypeConstants.Weapon));
        }

        [Test]
        public void TwoHandedHappens()
        {
            var equipment = Generate(GetEquipment,
                e => e.OffHand != null && e.OffHand.ItemType == ItemTypeConstants.Weapon && e.OffHand == e.PrimaryHand);

            Assert.That(equipment.OffHand.Name, Is.Not.Empty);
            Assert.That(equipment.OffHand, Is.EqualTo(equipment.PrimaryHand));
            Assert.That(equipment.OffHand.ItemType, Is.EqualTo(ItemTypeConstants.Weapon));
        }

        [Test]
        public void OffHandDoesNotHappen()
        {
            var equipment = Generate(GetEquipment,
                e => e.OffHand == null);

            Assert.That(equipment.OffHand, Is.Null);
        }

        [Test]
        public void RangedWeaponWithAmmunitionHappens()
        {
            var equipment = Generate(GetEquipment,
                e => e.PrimaryHand.Attributes.Contains(AttributeConstants.Ranged) && e.Treasure.Items.Any(i => i.Attributes.Contains(AttributeConstants.Ammunition)));

            Assert.That(equipment.PrimaryHand.Attributes, Contains.Item(AttributeConstants.Ranged));

            var hasAmmunition = equipment.Treasure.Items.Any(i => i.Attributes.Contains(AttributeConstants.Ammunition));
            Assert.That(hasAmmunition, Is.True);
        }
    }
}