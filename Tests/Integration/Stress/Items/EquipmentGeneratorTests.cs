using System;
using System.Linq;
using EquipmentGen.Common.Items;
using Ninject;
using NPCGen.Common.Items;
using NPCGen.Generators.Interfaces.Abilities;
using NPCGen.Generators.Interfaces.Combats;
using NPCGen.Generators.Interfaces.Items;
using NPCGen.Generators.Interfaces.Randomizers.Stats;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Stress.Items
{
    [TestFixture]
    public class EquipmentGeneratorTests : StressTests
    {
        [Inject]
        public IAbilitiesGenerator AbilitiesGenerator { get; set; }
        [Inject, Named(StatsRandomizerTypeConstants.Raw)]
        public IStatsRandomizer StatsRandomizer { get; set; }
        [Inject]
        public IEquipmentGenerator EquipmentGenerator { get; set; }
        [Inject]
        public ICombatGenerator CombatGenerator { get; set; }

        [Test]
        public override void Stress()
        {
            do MakeAssertions();
            while (TestShouldKeepRunning());

            AssertIterations();
        }

        private void MakeAssertions()
        {
            var equipment = GetEquipment();
            Assert.That(equipment.Armor, Is.Not.Null);
            Assert.That(equipment.OffHand, Is.Not.Null);
            Assert.That(equipment.Treasure, Is.Not.Null);
            Assert.That(equipment.PrimaryHand.Name, Is.Not.Empty);
            Assert.That(equipment.PrimaryHand.ItemType, Is.EqualTo(ItemTypeConstants.Weapon));

            if (equipment.PrimaryHand.Attributes.Contains(WeaponAttributeConstants.TwoHanded))
                Assert.That(equipment.PrimaryHand, Is.EqualTo(equipment.OffHand));
        }

        private Equipment GetEquipment()
        {
            var alignment = GetNewAlignment();
            var characterClass = GetNewCharacterClass(alignment);
            var race = GetNewRace(alignment, characterClass);
            var baseAttack = CombatGenerator.GenerateBaseAttackWith(characterClass, race);
            var ability = AbilitiesGenerator.GenerateWith(characterClass, race, StatsRandomizer, baseAttack);

            return EquipmentGenerator.GenerateWith(ability.Feats, characterClass);
        }

        [Test]
        public void CoinHappens()
        {
            var equipment = new Equipment();

            do equipment = GetEquipment();
            while (TestShouldKeepRunning() && equipment.Treasure.Coin.Quantity == 0);

            AssertIterations();
            Assert.That(equipment.Treasure.Coin.Quantity, Is.Positive);
        }

        [Test]
        public void GoodsHappen()
        {
            var equipment = new Equipment();

            do equipment = GetEquipment();
            while (TestShouldKeepRunning() && !equipment.Treasure.Goods.Any());

            AssertIterations();
            Assert.That(equipment.Treasure.Goods, Is.Not.Empty);
        }

        [Test]
        public void ItemsHappen()
        {
            var equipment = new Equipment();

            do equipment = GetEquipment();
            while (TestShouldKeepRunning() && !equipment.Treasure.Items.Any());

            AssertIterations();
            Assert.That(equipment.Treasure.Items, Is.Not.Empty);
        }

        [Test]
        public void TreasureDoesNotHappen()
        {
            var equipment = new Equipment();

            do equipment = GetEquipment();
            while (TestShouldKeepRunning() && (equipment.Treasure.Items.Any() || equipment.Treasure.Goods.Any()
                || equipment.Treasure.Coin.Quantity > 0));

            AssertIterations();
            Assert.That(equipment.Treasure.Items, Is.Empty);
            Assert.That(equipment.Treasure.Goods, Is.Empty);
            Assert.That(equipment.Treasure.Coin.Quantity, Is.EqualTo(0));
        }

        [Test]
        public void OffHandShieldHappens()
        {
            var equipment = new Equipment();

            do equipment = GetEquipment();
            while (TestShouldKeepRunning() && (String.IsNullOrEmpty(equipment.OffHand.Name) || equipment.OffHand == equipment.PrimaryHand));

            AssertIterations();
            Assert.That(equipment.OffHand.Name, Is.Not.Empty);
            Assert.That(equipment.OffHand, Is.Not.EqualTo(equipment.PrimaryHand));
            Assert.That(equipment.OffHand.ItemType, Is.EqualTo(ItemTypeConstants.Armor));
            Assert.That(equipment.OffHand.Attributes, Contains.Item(AttributeConstants.Shield));
        }

        [Test]
        public void OffHandWeaponHappens()
        {
            var equipment = new Equipment();

            do equipment = GetEquipment();
            while (TestShouldKeepRunning() && (String.IsNullOrEmpty(equipment.OffHand.Name) || equipment.OffHand == equipment.PrimaryHand));

            AssertIterations();
            Assert.That(equipment.OffHand.Name, Is.Not.Empty);
            Assert.That(equipment.OffHand, Is.Not.EqualTo(equipment.PrimaryHand));
            Assert.That(equipment.OffHand.ItemType, Is.EqualTo(ItemTypeConstants.Weapon));
            Assert.That(equipment.OffHand.Attributes, Is.Not.Contains(WeaponAttributeConstants.TwoHanded));
        }

        [Test]
        public void TwoHandedHappens()
        {
            var equipment = new Equipment();

            do equipment = GetEquipment();
            while (TestShouldKeepRunning() && equipment.OffHand != equipment.PrimaryHand);

            AssertIterations();
            Assert.That(equipment.OffHand, Is.EqualTo(equipment.PrimaryHand));
            Assert.That(equipment.OffHand.Attributes, Contains.Item(WeaponAttributeConstants.TwoHanded));
        }

        [Test]
        public void OffHandDoesNotHappen()
        {
            var equipment = new Equipment();

            do equipment = GetEquipment();
            while (TestShouldKeepRunning() && !String.IsNullOrEmpty(equipment.OffHand.Name));

            AssertIterations();
            Assert.That(equipment.OffHand.Name, Is.Empty);
        }

        [Test]
        public void ArmorHappens()
        {
            var equipment = new Equipment();

            do equipment = GetEquipment();
            while (TestShouldKeepRunning() && String.IsNullOrEmpty(equipment.Armor.Name));

            AssertIterations();
            Assert.That(equipment.Armor.ItemType, Is.EqualTo(ItemTypeConstants.Armor));
            Assert.That(equipment.Armor.Name, Is.Not.Empty);
        }

        [Test]
        public void ArmorDoesNotHappen()
        {
            var equipment = new Equipment();

            do equipment = GetEquipment();
            while (TestShouldKeepRunning() && !String.IsNullOrEmpty(equipment.Armor.Name));

            AssertIterations();
            Assert.That(equipment.Armor.Name, Is.Empty);
        }
    }
}