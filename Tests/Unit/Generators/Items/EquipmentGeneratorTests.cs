using CharacterGen.Common.Abilities.Feats;
using CharacterGen.Common.CharacterClasses;
using CharacterGen.Generators.Domain.Items;
using CharacterGen.Generators.Items;
using CharacterGen.Selectors;
using CharacterGen.Tables;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using TreasureGen.Common;
using TreasureGen.Common.Items;
using TreasureGen.Generators;

namespace CharacterGen.Tests.Unit.Generators.Items
{
    [TestFixture]
    public class EquipmentGeneratorTests
    {
        private IEquipmentGenerator equipmentGenerator;
        private Mock<GearGenerator> mockWeaponGenerator;
        private Mock<ICollectionsSelector> mockCollectionsSelector;
        private Mock<GearGenerator> mockArmorGenerator;
        private Mock<ITreasureGenerator> mockTreasureGenerator;
        private List<Feat> feats;
        private CharacterClass characterClass;
        private Item weapon;
        private List<String> baseWeaponTypes;
        private Item armor;
        private Treasure treasure;

        [SetUp]
        public void Setup()
        {
            mockWeaponGenerator = new Mock<GearGenerator>();
            mockCollectionsSelector = new Mock<ICollectionsSelector>();
            mockArmorGenerator = new Mock<GearGenerator>();
            mockTreasureGenerator = new Mock<ITreasureGenerator>();
            equipmentGenerator = new EquipmentGenerator(mockCollectionsSelector.Object, mockWeaponGenerator.Object,
                mockTreasureGenerator.Object, mockArmorGenerator.Object);
            feats = new List<Feat>();
            characterClass = new CharacterClass();
            weapon = new Item();
            baseWeaponTypes = new List<String>();
            armor = new Item();
            treasure = new Treasure();

            characterClass.Level = 9266;
            weapon.Name = "weapon name";
            armor.Name = "armor name";
            baseWeaponTypes.Add("base weapon");

            mockWeaponGenerator.Setup(g => g.GenerateFrom(feats, characterClass)).Returns(weapon);
            mockArmorGenerator.Setup(g => g.GenerateFrom(feats, characterClass)).Returns(armor);
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.ItemGroups, It.IsAny<String>())).Returns((String table, String name) => new[] { name });
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.ItemGroups, weapon.Name)).Returns(baseWeaponTypes);
            mockTreasureGenerator.Setup(g => g.GenerateAtLevel(9266)).Returns(treasure);
        }

        [Test]
        public void GeneratesWeaponForPrimaryHand()
        {
            var equipment = equipmentGenerator.GenerateWith(feats, characterClass);
            Assert.That(equipment.PrimaryHand, Is.EqualTo(weapon));
            Assert.That(equipment.PrimaryHand.Name, Is.EqualTo("weapon name"));
        }

        [Test]
        public void IfWeaponIsTwoHanded_PutInOffHandAsWell()
        {
            var twoHandedWeapons = new[] { "weapon 1", baseWeaponTypes[0] };
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.ItemGroups, GroupConstants.TwoHanded))
                .Returns(twoHandedWeapons);

            var equipment = equipmentGenerator.GenerateWith(feats, characterClass);
            Assert.That(equipment.PrimaryHand, Is.EqualTo(weapon));
            Assert.That(equipment.OffHand, Is.EqualTo(weapon));
        }

        [Test]
        public void IfWeaponIsNotTwoHanded_DoNotPutInOffHandAsWell()
        {
            var twoHandedWeapons = new[] { "weapon 1", "weapon 2" };
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.ItemGroups, GroupConstants.TwoHanded))
                .Returns(twoHandedWeapons);

            var equipment = equipmentGenerator.GenerateWith(feats, characterClass);
            Assert.That(equipment.PrimaryHand, Is.EqualTo(weapon));
            Assert.That(equipment.OffHand, Is.Null);
        }

        [Test]
        public void IfOffHandIsEmptyAndHasTwoWeaponFeats_GenerateSecondWeapon()
        {
            var offHandWeapon = new Item();
            offHandWeapon.Name = "offhand";
            mockWeaponGenerator.SetupSequence(g => g.GenerateFrom(feats, characterClass))
                .Returns(weapon).Returns(offHandWeapon);

            feats.Add(new Feat { Name = "other feat" });
            feats.Add(new Feat { Name = "two-weapon feat" });
            var twoWeaponFeats = new[] { "two-weapon feat", "two-handed feat" };
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.FeatGroups, GroupConstants.TwoHanded))
                .Returns(twoWeaponFeats);

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.ItemGroups, offHandWeapon.Name)).Returns(new[] { offHandWeapon.Name });

            var equipment = equipmentGenerator.GenerateWith(feats, characterClass);
            Assert.That(equipment.PrimaryHand, Is.EqualTo(weapon));
            Assert.That(equipment.OffHand, Is.EqualTo(offHandWeapon));
        }

        [Test]
        public void IfOffHandIsNotEmptyAndHasTwoWeaponFeats_DoNotGenerateSecondWeapon()
        {
            var twoHandedWeapons = new[] { "weapon 1", baseWeaponTypes[0] };
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.ItemGroups, GroupConstants.TwoHanded))
                .Returns(twoHandedWeapons);

            var offHandWeapon = new Item();
            mockWeaponGenerator.SetupSequence(g => g.GenerateFrom(feats, characterClass))
                .Returns(weapon).Returns(offHandWeapon);

            feats.Add(new Feat { Name = "other feat" });
            feats.Add(new Feat { Name = "two-weapon feat" });
            var twoWeaponFeats = new[] { "two-weapon feat", "two-handed feat" };
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.FeatGroups, GroupConstants.TwoHanded))
                .Returns(twoWeaponFeats);

            var equipment = equipmentGenerator.GenerateWith(feats, characterClass);
            Assert.That(equipment.PrimaryHand, Is.EqualTo(weapon));
            Assert.That(equipment.OffHand, Is.EqualTo(weapon));
        }

        [Test]
        public void IfOffHandIsEmptyButDoesNotHaveTwoWeaponFeats_DoNotGenerateSecondWeapon()
        {
            var offHandWeapon = new Item();
            mockWeaponGenerator.SetupSequence(g => g.GenerateFrom(feats, characterClass))
                .Returns(weapon).Returns(offHandWeapon);

            feats.Add(new Feat { Name = "other feat" });
            feats.Add(new Feat { Name = "different feat" });
            var twoWeaponFeats = new[] { "two-weapon feat", "two-handed feat" };
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.FeatGroups, GroupConstants.TwoHanded))
                .Returns(twoWeaponFeats);

            var equipment = equipmentGenerator.GenerateWith(feats, characterClass);
            Assert.That(equipment.PrimaryHand, Is.EqualTo(weapon));
            Assert.That(equipment.OffHand, Is.Null);
        }

        [Test]
        public void OffHandWeaponCannotBeTwoHanded()
        {
            var twoHandedWeapon = new Item();
            twoHandedWeapon.Name = "two-handed";

            var offHandWeapon = new Item();
            offHandWeapon.Name = "offhand";
            mockWeaponGenerator.SetupSequence(g => g.GenerateFrom(feats, characterClass))
                .Returns(weapon).Returns(twoHandedWeapon).Returns(offHandWeapon);

            feats.Add(new Feat { Name = "other feat" });
            feats.Add(new Feat { Name = "two-weapon feat" });
            var twoWeaponFeats = new[] { "two-weapon feat", "two-handed feat" };
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.FeatGroups, GroupConstants.TwoHanded))
                .Returns(twoWeaponFeats);

            var twoHandedWeapons = new[] { "two-handed", "weapon 2" };
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.ItemGroups, GroupConstants.TwoHanded))
                .Returns(twoHandedWeapons);

            var equipment = equipmentGenerator.GenerateWith(feats, characterClass);
            Assert.That(equipment.PrimaryHand, Is.EqualTo(weapon));
            Assert.That(equipment.OffHand, Is.EqualTo(offHandWeapon));
        }

        [Test]
        public void OffHandWeaponCannotBeAmmunition()
        {
            var ammo = new Item();
            ammo.Name = "ammo";
            ammo.Attributes = new[] { AttributeConstants.Ammunition };
            var offHandWeapon = new Item();
            offHandWeapon.Name = "offhand";
            mockWeaponGenerator.SetupSequence(g => g.GenerateFrom(feats, characterClass))
                .Returns(weapon).Returns(ammo).Returns(offHandWeapon);

            feats.Add(new Feat { Name = "other feat" });
            feats.Add(new Feat { Name = "two-weapon feat" });
            var twoWeaponFeats = new[] { "two-weapon feat", "two-handed feat" };
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.FeatGroups, GroupConstants.TwoHanded))
                .Returns(twoWeaponFeats);

            var twoHandedWeapons = new[] { "two-handed", "weapon 2" };
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.ItemGroups, GroupConstants.TwoHanded))
                .Returns(twoHandedWeapons);

            var equipment = equipmentGenerator.GenerateWith(feats, characterClass);
            Assert.That(equipment.PrimaryHand, Is.EqualTo(weapon));
            Assert.That(equipment.OffHand, Is.EqualTo(offHandWeapon));
        }

        [Test]
        public void GenerateArmor()
        {
            var equipment = equipmentGenerator.GenerateWith(feats, characterClass);
            Assert.That(equipment.Armor, Is.EqualTo(armor));
        }

        [Test]
        public void CanGenerateNoArmor()
        {
            Item noArmor = null;
            mockArmorGenerator.Setup(g => g.GenerateFrom(feats, characterClass)).Returns(noArmor);

            var equipment = equipmentGenerator.GenerateWith(feats, characterClass);
            Assert.That(equipment.Armor, Is.Null);
        }

        [Test]
        public void ArmorCannotBeShield()
        {
            var firstShield = new Item();
            firstShield.Attributes = new[] { AttributeConstants.Shield };
            var secondShield = new Item();
            secondShield.Attributes = new[] { AttributeConstants.Shield };
            mockArmorGenerator.SetupSequence(g => g.GenerateFrom(feats, characterClass))
                .Returns(firstShield).Returns(secondShield).Returns(armor);

            var equipment = equipmentGenerator.GenerateWith(feats, characterClass);
            Assert.That(equipment.Armor, Is.EqualTo(armor));
        }

        [Test]
        public void IfOffHandIsEmptyAndArmorIsShield_WearShield()
        {
            var firstShield = new Item();
            firstShield.Attributes = new[] { AttributeConstants.Shield };
            var secondShield = new Item();
            secondShield.Attributes = new[] { AttributeConstants.Shield };
            mockArmorGenerator.SetupSequence(g => g.GenerateFrom(feats, characterClass))
                .Returns(firstShield).Returns(secondShield).Returns(armor);

            var equipment = equipmentGenerator.GenerateWith(feats, characterClass);
            Assert.That(equipment.OffHand, Is.EqualTo(firstShield));
        }

        [Test]
        public void IfOffHandIsEmptyAndArmorIsNotShield_DoNotWearShield()
        {
            var equipment = equipmentGenerator.GenerateWith(feats, characterClass);
            Assert.That(equipment.OffHand, Is.Null);
        }

        [Test]
        public void IfOffHandIsNotEmptyAndArmorIsShield_DoNotWearShield()
        {
            var offHandWeapon = new Item();
            offHandWeapon.Name = "offhand";
            mockWeaponGenerator.SetupSequence(g => g.GenerateFrom(feats, characterClass))
                .Returns(weapon).Returns(offHandWeapon);

            feats.Add(new Feat { Name = "other feat" });
            feats.Add(new Feat { Name = "two-weapon feat" });
            var twoWeaponFeats = new[] { "two-weapon feat", "two-handed feat" };
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.FeatGroups, GroupConstants.TwoHanded))
                .Returns(twoWeaponFeats);

            var firstShield = new Item();
            firstShield.Attributes = new[] { AttributeConstants.Shield };
            var secondShield = new Item();
            secondShield.Attributes = new[] { AttributeConstants.Shield };
            mockArmorGenerator.SetupSequence(g => g.GenerateFrom(feats, characterClass))
                .Returns(firstShield).Returns(secondShield).Returns(armor);

            var equipment = equipmentGenerator.GenerateWith(feats, characterClass);
            Assert.That(equipment.OffHand, Is.EqualTo(offHandWeapon));
        }

        [Test]
        public void GenerateTreasure()
        {
            var equipment = equipmentGenerator.GenerateWith(feats, characterClass);
            Assert.That(equipment.Treasure, Is.EqualTo(treasure));
        }

        [Test]
        public void IfWeaponRequiresAmmunition_GenerateMatchingAmmunitionAndAddToTreasure()
        {
            var notAmmo = new Item();
            notAmmo.Name = "not ammo";
            var ammo = new Item();
            ammo.Name = "ammo";
            ammo.Attributes = new[] { AttributeConstants.Ammunition };
            var wrongAmmo = new Item();
            wrongAmmo.Name = "other ammo";
            wrongAmmo.Attributes = new[] { AttributeConstants.Ammunition };
            mockWeaponGenerator.SetupSequence(g => g.GenerateFrom(feats, characterClass))
                .Returns(weapon).Returns(notAmmo).Returns(wrongAmmo).Returns(ammo);

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.ItemGroups, "ammo"))
                .Returns(new[] { "ammunition" });

            baseWeaponTypes.Add("ammunition");

            var equipment = equipmentGenerator.GenerateWith(feats, characterClass);
            Assert.That(equipment.PrimaryHand, Is.EqualTo(weapon));
            Assert.That(equipment.Treasure.Items, Contains.Item(ammo));
        }

        [Test]
        public void IfWeaponIsAmmunition_AddAmmoToTreasureAndGenerateWeaponToFit()
        {
            weapon.Attributes = new[] { AttributeConstants.Ammunition };

            var rangedWeapon = new Item();
            rangedWeapon.Name = "ranged";
            var wrongRangedWeapon = new Item();
            wrongRangedWeapon.Name = "wrong ranged";
            mockWeaponGenerator.SetupSequence(g => g.GenerateFrom(feats, characterClass))
                .Returns(weapon).Returns(wrongRangedWeapon).Returns(rangedWeapon);

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.ItemGroups, "ranged"))
                .Returns(new[] { "ranged", baseWeaponTypes[0] });
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.ItemGroups, "wrong ranged"))
                .Returns(new[] { "wrong ranged", "wrong ammo" });

            var equipment = equipmentGenerator.GenerateWith(feats, characterClass);
            Assert.That(equipment.PrimaryHand, Is.EqualTo(rangedWeapon));
            Assert.That(equipment.Treasure.Items, Contains.Item(weapon));
        }

        [Test]
        public void IfWeaponIsAmmunitionAndMatchingWeaponIsTwoHanded_MarkAsTwoHanded()
        {
            weapon.Attributes = new[] { AttributeConstants.Ammunition };

            var rangedWeapon = new Item();
            rangedWeapon.Name = "ranged";
            var wrongRangedWeapon = new Item();
            wrongRangedWeapon.Name = "wrong ranged";
            mockWeaponGenerator.SetupSequence(g => g.GenerateFrom(feats, characterClass))
                .Returns(weapon).Returns(wrongRangedWeapon).Returns(rangedWeapon);

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.ItemGroups, "ranged"))
                .Returns(new[] { "ranged", baseWeaponTypes[0] });
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.ItemGroups, "wrong ranged"))
                .Returns(new[] { "wrong ranged", "wrong ammo" });

            var twoHandedWeapons = new[] { "two-handed", "ranged" };
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.ItemGroups, GroupConstants.TwoHanded))
                .Returns(twoHandedWeapons);

            var equipment = equipmentGenerator.GenerateWith(feats, characterClass);
            Assert.That(equipment.PrimaryHand, Is.EqualTo(rangedWeapon));
            Assert.That(equipment.OffHand, Is.EqualTo(rangedWeapon));
            Assert.That(equipment.Treasure.Items, Contains.Item(weapon));
        }
    }
}
