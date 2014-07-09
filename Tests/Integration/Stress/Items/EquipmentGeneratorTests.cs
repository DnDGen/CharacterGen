using System;
using EquipmentGen.Common.Items;
using Ninject;
using NPCGen.Common.Items;
using NPCGen.Generators.Interfaces.Abilities;
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

        protected override void MakeAssertions()
        {
            var alignment = GetNewAlignment();
            var characterClass = GetNewCharacterClass(alignment);
            var race = GetNewRace(alignment, characterClass);
            var ability = AbilitiesGenerator.GenerateWith(characterClass, race, StatsRandomizer);

            var equipment = EquipmentGenerator.GenerateWith(ability.Feats, characterClass);
            Assert.That(equipment.Armor, Is.Not.Null);
            Assert.That(equipment.OffHand, Is.Not.Null);
            Assert.That(equipment.PrimaryHand, Is.Not.Null);
            Assert.That(equipment.Treasure, Is.Not.Null);

            Assert.That(equipment.Armor.ItemType, Is.EqualTo(ItemTypeConstants.Armor));
            Assert.That(equipment.Armor.Name, Is.Not.Empty);
            Assert.That(equipment.PrimaryHand.ItemType, Is.EqualTo(ItemTypeConstants.Weapon));
            Assert.That(equipment.PrimaryHand.Name, Is.Not.Empty);
            Assert.That(equipment.Treasure, Is.Not.Null);
            Assert.That(equipment.OffHand, Is.Not.Null);

            if (equipment.OffHand.ItemType == ItemTypeConstants.Armor)
                Assert.That(equipment.OffHand.Attributes, Contains.Item(AttributeConstants.Shield));
            else if (equipment.OffHand.ItemType == ItemTypeConstants.Weapon && equipment.OffHand != equipment.PrimaryHand)
                Assert.That(equipment.OffHand.Attributes, Is.Not.Contains(WeaponAttributeConstants.TwoHanded));
            else if (equipment.OffHand != equipment.PrimaryHand)
                Assert.That(equipment.OffHand.Name, Is.Empty);
        }

        [Test]
        public void CoinHappens()
        {
            var equipment = new Equipment();

            do
            {
                var alignment = GetNewAlignment();
                var characterClass = GetNewCharacterClass(alignment);
                var race = GetNewRace(alignment, characterClass);
                var ability = AbilitiesGenerator.GenerateWith(characterClass, race, StatsRandomizer);

                equipment = EquipmentGenerator.GenerateWith(ability.Feats, characterClass);
            }
            while (TestShouldKeepRunning() && equipment.Treasure.Coin.Quantity == 0);

            Assert.That(equipment.Treasure.Coin.Quantity, Is.Positive);
            AssertIterations();
        }

        [Test]
        public void GoodsHappen()
        {
            var equipment = new Equipment();

            do
            {
                var alignment = GetNewAlignment();
                var characterClass = GetNewCharacterClass(alignment);
                var race = GetNewRace(alignment, characterClass);
                var ability = AbilitiesGenerator.GenerateWith(characterClass, race, StatsRandomizer);

                equipment = EquipmentGenerator.GenerateWith(ability.Feats, characterClass);
            }
            while (TestShouldKeepRunning() && !equipment.Treasure.Goods.Any());

            Assert.That(equipment.Treasure.Goods, Is.Not.Empty);
            AssertIterations();
        }

        [Test]
        public void ItemsHappen()
        {
            var equipment = new Equipment();

            do
            {
                var alignment = GetNewAlignment();
                var characterClass = GetNewCharacterClass(alignment);
                var race = GetNewRace(alignment, characterClass);
                var ability = AbilitiesGenerator.GenerateWith(characterClass, race, StatsRandomizer);

                equipment = EquipmentGenerator.GenerateWith(ability.Feats, characterClass);
            }
            while (TestShouldKeepRunning() && !equipment.Equipment.Treasure.Items.Any());

            Assert.That(equipment.Treasure.Items, Is.Not.Empty);
            AssertIterations();
        }

        [Test]
        public void TreasureDoesNotHappen()
        {
            var equipment = new Equipment();

            do
            {
                var alignment = GetNewAlignment();
                var characterClass = GetNewCharacterClass(alignment);
                var race = GetNewRace(alignment, characterClass);
                var ability = AbilitiesGenerator.GenerateWith(characterClass, race, StatsRandomizer);

                equipment = EquipmentGenerator.GenerateWith(ability.Feats, characterClass);
            }
            while (TestShouldKeepRunning() && (equipment.Treasure.Items.Any() || equipment.Treasure.Goods.Any()
                || equipment.Treasure.Coin.Quantity > 0));

            Assert.That(equipment.Treasure.Items, Is.Empty);
            Assert.That(equipment.Treasure.Goods, Is.Empty);
            Assert.That(equipment.Treasure.Coin.Quantity, Is.EqualTo(0));
            AssertIterations();
        }

        [Test]
        public void OffHandShieldHappens()
        {
            var equipment = new Equipment();

            do
            {
                var alignment = GetNewAlignment();
                var characterClass = GetNewCharacterClass(alignment);
                var race = GetNewRace(alignment, characterClass);
                var ability = AbilitiesGenerator.GenerateWith(characterClass, race, StatsRandomizer);

                equipment = EquipmentGenerator.GenerateWith(ability.Feats, characterClass);
            }
            while (TestShouldKeepRunning() && (String.IsNullOrEmpty(equipment.OffHand.Name) || equipment.OffHand == equipment.PrimaryHand));

            Assert.That(equipment.OffHand.Name, Is.Not.Empty);
            Assert.That(equipment.OffHand, Is.Not.EqualTo(equipment.PrimaryHand));
            Assert.That(equipment.OffHand.ItemType, Is.EqualTo(ItemTypeConstants.Armor));
            Assert.That(equipment.OffHand.Attributes, Contains.Item(AttributeConstants.Shield));
            AssertIterations();
        }

        [Test]
        public void OffHandWeaponHappens()
        {
            var equipment = new Equipment();

            do
            {
                var alignment = GetNewAlignment();
                var characterClass = GetNewCharacterClass(alignment);
                var race = GetNewRace(alignment, characterClass);
                var ability = AbilitiesGenerator.GenerateWith(characterClass, race, StatsRandomizer);

                equipment = EquipmentGenerator.GenerateWith(ability.Feats, characterClass);
            }
            while (TestShouldKeepRunning() && (String.IsNullOrEmpty(equipment.OffHand.Name) || equipment.OffHand == equipment.PrimaryHand));

            Assert.That(equipment.OffHand.Name, Is.Not.Empty);
            Assert.That(equipment.OffHand, Is.Not.EqualTo(equipment.PrimaryHand));
            Assert.That(equipment.OffHand.ItemType, Is.EqualTo(ItemTypeConstants.Weapon));
            AssertIterations();
        }

        [Test]
        public void TwoHandedHappens()
        {
            var equipment = new Equipment();

            do
            {
                var alignment = GetNewAlignment();
                var characterClass = GetNewCharacterClass(alignment);
                var race = GetNewRace(alignment, characterClass);
                var ability = AbilitiesGenerator.GenerateWith(characterClass, race, StatsRandomizer);

                equipment = EquipmentGenerator.GenerateWith(ability.Feats, characterClass);
            }
            while (TestShouldKeepRunning() && equipment.OffHand != equipment.PrimaryHand);

            Assert.That(equipment.OffHand, Is.EqualTo(equipment.PrimaryHand));
            AssertIterations();
        }

        [Test]
        public void OffHandDoesNotHappen()
        {
            var equipment = new Equipment();

            do
            {
                var alignment = GetNewAlignment();
                var characterClass = GetNewCharacterClass(alignment);
                var race = GetNewRace(alignment, characterClass);
                var ability = AbilitiesGenerator.GenerateWith(characterClass, race, StatsRandomizer);

                equipment = EquipmentGenerator.GenerateWith(ability.Feats, characterClass);
            }
            while (TestShouldKeepRunning() && !String.IsNullOrEmpty(equipment.OffHand.Name));

            Assert.That(equipment.OffHand.Name, Is.Empty);
            AssertIterations();
        }

        [Test]
        public void ArmorHappens()
        {
            Assert.Fail();
        }

        [Test]
        public void ArmorDoesNotHappen()
        {
            Assert.Fail();
        }
    }
}