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
        [Inject]
        public IAbilitiesGenerator AbilitiesGenerator { get; set; }
        [Inject, Named(StatsRandomizerTypeConstants.Raw)]
        public IStatsRandomizer StatsRandomizer { get; set; }
        [Inject]
        public IEquipmentGenerator EquipmentGenerator { get; set; }
        [Inject]
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

            return EquipmentGenerator.GenerateWith(ability.Feats, characterClass);
        }

        [Test]
        public void CoinHappens()
        {
            var equipment = new Equipment();

            do equipment = GetEquipment();
            while (TestShouldKeepRunning() && equipment.Treasure.Coin.Quantity == 0);

            Assert.That(equipment.Treasure.Coin.Quantity, Is.Positive);
        }

        [Test]
        public void GoodsHappen()
        {
            var equipment = new Equipment();

            do equipment = GetEquipment();
            while (TestShouldKeepRunning() && !equipment.Treasure.Goods.Any());

            Assert.That(equipment.Treasure.Goods, Is.Not.Empty);
        }

        [Test]
        public void ItemsHappen()
        {
            var equipment = new Equipment();

            do equipment = GetEquipment();
            while (TestShouldKeepRunning() && !equipment.Treasure.Items.Any());

            Assert.That(equipment.Treasure.Items, Is.Not.Empty);
        }

        [Test]
        public void TreasureDoesNotHappen()
        {
            var equipment = new Equipment();

            do equipment = GetEquipment();
            while (TestShouldKeepRunning() &&
                (equipment.Treasure.Items.Any() || equipment.Treasure.Goods.Any() || equipment.Treasure.Coin.Quantity > 0));

            Assert.That(equipment.Treasure.Items, Is.Empty);
            Assert.That(equipment.Treasure.Goods, Is.Empty);
            Assert.That(equipment.Treasure.Coin.Quantity, Is.EqualTo(0));
        }

        [Test]
        public void OffHandShieldHappens()
        {
            var equipment = new Equipment();

            do equipment = GetEquipment();
            while (TestShouldKeepRunning() && (equipment.OffHand == null || equipment.OffHand == equipment.PrimaryHand));

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
            while (TestShouldKeepRunning() && (equipment.OffHand == null || equipment.OffHand == equipment.PrimaryHand));

            Assert.That(equipment.OffHand.Name, Is.Not.Empty);
            Assert.That(equipment.OffHand, Is.Not.EqualTo(equipment.PrimaryHand));
            Assert.That(equipment.OffHand.ItemType, Is.EqualTo(ItemTypeConstants.Weapon));
        }

        [Test]
        public void TwoHandedHappens()
        {
            var equipment = new Equipment();

            do equipment = GetEquipment();
            while (TestShouldKeepRunning() && equipment.OffHand != equipment.PrimaryHand);

            Assert.That(equipment.OffHand, Is.EqualTo(equipment.PrimaryHand));
            Assert.That(equipment.OffHand.ItemType, Is.EqualTo(ItemTypeConstants.Weapon));
        }

        [Test]
        public void OffHandDoesNotHappen()
        {
            var equipment = new Equipment();

            do equipment = GetEquipment();
            while (TestShouldKeepRunning() && equipment.OffHand != null);

            Assert.That(equipment.OffHand, Is.Null);
        }

        [Test]
        public void ArmorHappens()
        {
            var equipment = new Equipment();

            do equipment = GetEquipment();
            while (TestShouldKeepRunning() && equipment.Armor == null);

            Assert.That(equipment.Armor.ItemType, Is.EqualTo(ItemTypeConstants.Armor));
            Assert.That(equipment.Armor.Name, Is.Not.Empty);
            Assert.That(equipment.Armor.Attributes, Is.Not.Contains(AttributeConstants.Shield));
        }

        [Test]
        public void ArmorDoesNotHappen()
        {
            var equipment = new Equipment();

            do equipment = GetEquipment();
            while (TestShouldKeepRunning() && equipment.Armor != null);

            Assert.That(equipment.Armor, Is.Null);
        }
    }
}