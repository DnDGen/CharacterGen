using CharacterGen.Common.Abilities.Feats;
using CharacterGen.Common.CharacterClasses;
using CharacterGen.Common.Races;
using CharacterGen.Generators;
using CharacterGen.Generators.Domain.Items;
using CharacterGen.Generators.Items;
using CharacterGen.Selectors;
using CharacterGen.Tables;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using TreasureGen.Common;
using TreasureGen.Common.Items;
using TreasureGen.Generators;

namespace CharacterGen.Tests.Unit.Generators.Items
{
    [TestFixture]
    public class EquipmentGeneratorTests
    {
        private IEquipmentGenerator equipmentGenerator;
        private Mock<IWeaponGenerator> mockWeaponGenerator;
        private Mock<ICollectionsSelector> mockCollectionsSelector;
        private Mock<IArmorGenerator> mockArmorGenerator;
        private Mock<ITreasureGenerator> mockTreasureGenerator;
        private Generator generator;
        private List<Feat> feats;
        private CharacterClass characterClass;
        private Item meleeWeapon;
        private Item rangedWeapon;
        private List<String> baseRangedWeaponTypes;
        private Item armor;
        private Treasure treasure;
        private Race race;
        private List<String> shieldProficiencyFeats;
        private List<String> weaponProficiencyFeats;
        private Item treasureItem;

        [SetUp]
        public void Setup()
        {
            mockWeaponGenerator = new Mock<IWeaponGenerator>();
            mockCollectionsSelector = new Mock<ICollectionsSelector>();
            mockArmorGenerator = new Mock<IArmorGenerator>();
            mockTreasureGenerator = new Mock<ITreasureGenerator>();
            generator = new ConfigurableIterationGenerator(3);
            equipmentGenerator = new EquipmentGenerator(mockCollectionsSelector.Object, mockWeaponGenerator.Object,
                mockTreasureGenerator.Object, mockArmorGenerator.Object, generator);
            feats = new List<Feat>();
            characterClass = new CharacterClass();
            meleeWeapon = new Item();
            rangedWeapon = new Item();
            baseRangedWeaponTypes = new List<String>();
            armor = new Item();
            treasure = new Treasure();
            race = new Race();
            shieldProficiencyFeats = new List<String>();
            weaponProficiencyFeats = new List<String>();

            characterClass.Level = 9266;
            meleeWeapon.Name = "melee weapon";
            meleeWeapon.ItemType = ItemTypeConstants.Weapon;
            meleeWeapon.Attributes = new[] { AttributeConstants.Melee };
            rangedWeapon.Name = "ranged weapon";
            rangedWeapon.ItemType = ItemTypeConstants.Weapon;
            rangedWeapon.Attributes = new[] { "not melee" };
            armor.Name = "armor";
            armor.ItemType = ItemTypeConstants.Armor;
            baseRangedWeaponTypes.Add("base ranged weapon");
            treasureItem = new Item { Name = "treasure item" };
            treasure.Items = new[] { treasureItem };

            mockWeaponGenerator.Setup(g => g.GenerateFrom(feats, characterClass, race)).Returns(meleeWeapon);
            mockWeaponGenerator.Setup(g => g.GenerateRangedFrom(feats, characterClass, race)).Returns(rangedWeapon);
            mockWeaponGenerator.Setup(g => g.GenerateMeleeFrom(feats, characterClass, race)).Returns(meleeWeapon);
            mockArmorGenerator.Setup(g => g.GenerateArmorFrom(feats, characterClass, race)).Returns(armor);
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.ItemGroups, It.IsAny<String>())).Returns((String table, String name) => new[] { name });
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.ItemGroups, rangedWeapon.Name)).Returns(baseRangedWeaponTypes);
            mockTreasureGenerator.Setup(g => g.GenerateAtLevel(9266)).Returns(treasure);
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.FeatGroups, AttributeConstants.Shield + GroupConstants.Proficiency))
                .Returns(shieldProficiencyFeats);
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.FeatGroups, ItemTypeConstants.Weapon + GroupConstants.Proficiency))
                .Returns(weaponProficiencyFeats);
        }

        [Test]
        public void GeneratesWeaponForPrimaryHand()
        {
            var equipment = equipmentGenerator.GenerateWith(feats, characterClass, race);
            Assert.That(equipment.PrimaryHand, Is.EqualTo(meleeWeapon));
        }

        [Test]
        public void IfWeaponIsTwoHanded_PutInOffHandAsWell()
        {
            meleeWeapon.Attributes = meleeWeapon.Attributes.Union(new[] { AttributeConstants.TwoHanded });

            var equipment = equipmentGenerator.GenerateWith(feats, characterClass, race);
            Assert.That(equipment.PrimaryHand, Is.EqualTo(meleeWeapon));
            Assert.That(equipment.OffHand, Is.EqualTo(meleeWeapon));
        }

        [Test]
        public void IfWeaponIsNotTwoHanded_DoNotPutInOffHandAsWell()
        {
            var equipment = equipmentGenerator.GenerateWith(feats, characterClass, race);
            Assert.That(equipment.PrimaryHand, Is.EqualTo(meleeWeapon));
            Assert.That(equipment.OffHand, Is.Null);
        }

        [Test]
        public void IfCharacterHasTwoWeaponFeats_GenerateTwoOneHandedWeapons()
        {
            var offHandWeapon = new Item();
            offHandWeapon.Attributes = new[] { AttributeConstants.Melee };

            mockWeaponGenerator.SetupSequence(g => g.GenerateOneHandedMeleeFrom(feats, characterClass, race))
                .Returns(meleeWeapon).Returns(offHandWeapon);

            feats.Add(new Feat { Name = "other feat" });
            feats.Add(new Feat { Name = "two-weapon feat" });
            var twoWeaponFeats = new[] { "two-weapon feat", "two-handed feat" };
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.FeatGroups, GroupConstants.TwoHanded))
                .Returns(twoWeaponFeats);

            var equipment = equipmentGenerator.GenerateWith(feats, characterClass, race);
            Assert.That(equipment.PrimaryHand, Is.EqualTo(meleeWeapon));
            Assert.That(equipment.OffHand, Is.EqualTo(offHandWeapon));
        }

        [Test]
        public void IfOffHandIsEmptyButDoesNotHaveTwoWeaponFeats_DoNotGenerateSecondWeapon()
        {
            var offHandWeapon = new Item();
            mockWeaponGenerator.SetupSequence(g => g.GenerateFrom(feats, characterClass, race))
                .Returns(meleeWeapon).Returns(offHandWeapon);

            mockWeaponGenerator.Setup(g => g.GenerateOneHandedMeleeFrom(feats, characterClass, race)).Returns(offHandWeapon);

            feats.Add(new Feat { Name = "other feat" });
            feats.Add(new Feat { Name = "different feat" });
            var twoWeaponFeats = new[] { "two-weapon feat", "two-handed feat" };
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.FeatGroups, GroupConstants.TwoHanded))
                .Returns(twoWeaponFeats);

            var equipment = equipmentGenerator.GenerateWith(feats, characterClass, race);
            Assert.That(equipment.PrimaryHand, Is.EqualTo(meleeWeapon));
            Assert.That(equipment.OffHand, Is.Null);
        }

        [Test]
        public void GenerateArmor()
        {
            var equipment = equipmentGenerator.GenerateWith(feats, characterClass, race);
            Assert.That(equipment.Armor, Is.EqualTo(armor));
        }

        [Test]
        public void CanGenerateNoArmor()
        {
            Item noArmor = null;
            mockArmorGenerator.Setup(g => g.GenerateArmorFrom(feats, characterClass, race)).Returns(noArmor);

            var equipment = equipmentGenerator.GenerateWith(feats, characterClass, race);
            Assert.That(equipment.Armor, Is.Null);
        }

        [Test]
        public void IfOffHandIsEmptyAndProficientInShields_GenerateShield()
        {
            var shield = new Item();
            mockArmorGenerator.SetupSequence(g => g.GenerateShieldFrom(feats, characterClass, race)).Returns(shield);

            var equipment = equipmentGenerator.GenerateWith(feats, characterClass, race);
            Assert.That(equipment.OffHand, Is.EqualTo(shield));
        }

        [Test]
        public void IfOffHandIsEmptyAndProficientInShields_CanGenerateNoShield()
        {
            Item shield = null;
            mockArmorGenerator.SetupSequence(g => g.GenerateShieldFrom(feats, characterClass, race)).Returns(shield);

            var equipment = equipmentGenerator.GenerateWith(feats, characterClass, race);
            Assert.That(equipment.OffHand, Is.Null);
        }

        [Test]
        public void IfOffHandIsNotEmptyAndProficientInShields_DoNotGenerateShield()
        {
            meleeWeapon.Attributes = meleeWeapon.Attributes.Union(new[] { AttributeConstants.TwoHanded });

            var shield = new Item();
            mockArmorGenerator.SetupSequence(g => g.GenerateShieldFrom(feats, characterClass, race)).Returns(shield);

            var equipment = equipmentGenerator.GenerateWith(feats, characterClass, race);
            Assert.That(equipment.PrimaryHand, Is.EqualTo(meleeWeapon), equipment.PrimaryHand.Name);
            Assert.That(equipment.OffHand, Is.EqualTo(meleeWeapon), equipment.OffHand.Name);
        }

        [Test]
        public void GenerateTreasure()
        {
            var equipment = equipmentGenerator.GenerateWith(feats, characterClass, race);
            Assert.That(equipment.Treasure, Is.EqualTo(treasure));
            Assert.That(equipment.Treasure.Items, Contains.Item(treasureItem));
        }

        [Test]
        public void IfWeaponRequiresAmmunition_GenerateMatchingAmmunitionAndAddToTreasure()
        {
            var ammo = new Item();
            ammo.Name = "ammo";
            ammo.Attributes = new[] { AttributeConstants.Ammunition };

            baseRangedWeaponTypes.Add("ammunition");
            mockWeaponGenerator.Setup(g => g.GenerateAmmunition(feats, characterClass, race, "ammunition")).Returns(ammo);

            var equipment = equipmentGenerator.GenerateWith(feats, characterClass, race);
            Assert.That(equipment.PrimaryHand, Is.EqualTo(meleeWeapon));
            Assert.That(equipment.Treasure.Items, Contains.Item(treasureItem));
            Assert.That(equipment.Treasure.Items, Contains.Item(ammo));
        }

        [Test]
        public void IfWeaponDoesNotRequireAmmunition_DoNotGenerateMatchingAmmunitionOrAddToTreasure()
        {
            mockWeaponGenerator.Setup(g => g.GenerateFrom(feats, characterClass, race)).Returns(rangedWeapon);

            var ammo = new Item();
            ammo.Attributes = new[] { AttributeConstants.Ammunition };

            mockWeaponGenerator.Setup(g => g.GenerateAmmunition(feats, characterClass, race, It.IsAny<String>())).Returns(ammo);

            var equipment = equipmentGenerator.GenerateWith(feats, characterClass, race);
            Assert.That(equipment.PrimaryHand, Is.EqualTo(rangedWeapon));
            Assert.That(equipment.Treasure.Items, Contains.Item(treasureItem));
            Assert.That(equipment.Treasure.Items, Is.All.Not.EqualTo(ammo));
        }

        [Test]
        public void IfCannotGenerateMatchingAmmunition_NoAmmunition()
        {
            mockWeaponGenerator.Setup(g => g.GenerateFrom(feats, characterClass, race)).Returns(rangedWeapon);

            baseRangedWeaponTypes.Add("ammunition");

            var equipment = equipmentGenerator.GenerateWith(feats, characterClass, race);
            Assert.That(equipment.PrimaryHand, Is.EqualTo(rangedWeapon));
            Assert.That(equipment.Treasure.Items, Contains.Item(treasureItem));
            Assert.That(equipment.Treasure.Items, Contains.Item(meleeWeapon));
            Assert.That(equipment.Treasure.Items, Is.All.Not.Null);
        }

        [Test]
        public void IfPrimaryHandIsNotMelee_GenerateMeleeWeaponForTreasure()
        {
            mockWeaponGenerator.Setup(g => g.GenerateFrom(feats, characterClass, race)).Returns(rangedWeapon);

            var equipment = equipmentGenerator.GenerateWith(feats, characterClass, race);
            Assert.That(equipment.PrimaryHand, Is.EqualTo(rangedWeapon));
            Assert.That(equipment.Treasure.Items, Contains.Item(treasureItem));
            Assert.That(equipment.Treasure.Items, Contains.Item(meleeWeapon));
        }

        [Test]
        public void AllowTwoHandedMeleeIfCharacterDoesNotHaveTwoWeaponFeat()
        {
            mockWeaponGenerator.Setup(g => g.GenerateFrom(feats, characterClass, race)).Returns(rangedWeapon);

            var twoHandedWeapon = new Item();
            twoHandedWeapon.Attributes = new[] { AttributeConstants.Melee, AttributeConstants.TwoHanded };

            mockWeaponGenerator.Setup(g => g.GenerateMeleeFrom(feats, characterClass, race)).Returns(twoHandedWeapon);

            feats.Add(new Feat { Name = "other feat" });
            var twoWeaponFeats = new[] { "two-weapon feat", "two-handed feat" };
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.FeatGroups, GroupConstants.TwoHanded))
                .Returns(twoWeaponFeats);

            var equipment = equipmentGenerator.GenerateWith(feats, characterClass, race);
            Assert.That(equipment.PrimaryHand, Is.EqualTo(rangedWeapon));
            Assert.That(equipment.Treasure.Items, Contains.Item(treasureItem));
            Assert.That(equipment.Treasure.Items, Contains.Item(twoHandedWeapon), String.Join(",", equipment.Treasure.Items.Select(i => i.Name)));
            Assert.That(equipment.Treasure.Items, Is.All.Not.EqualTo(meleeWeapon));
        }

        [Test]
        public void IfMeleeWeaponIsOneHandedAndProficientInShields_GenerateShield()
        {
            mockWeaponGenerator.Setup(g => g.GenerateFrom(feats, characterClass, race)).Returns(rangedWeapon);

            feats.Add(new Feat { Name = "feat" });
            feats.Add(new Feat { Name = "other feat" });
            shieldProficiencyFeats.Add(feats[0].Name);

            var shield = new Item();
            shield.Attributes = new[] { AttributeConstants.Shield };
            mockArmorGenerator.Setup(g => g.GenerateShieldFrom(feats, characterClass, race)).Returns(shield);

            var equipment = equipmentGenerator.GenerateWith(feats, characterClass, race);
            Assert.That(equipment.PrimaryHand, Is.EqualTo(rangedWeapon));
            Assert.That(equipment.Treasure.Items, Contains.Item(treasureItem));
            Assert.That(equipment.Treasure.Items, Contains.Item(meleeWeapon));
            Assert.That(equipment.Treasure.Items, Contains.Item(shield));
        }

        [Test]
        public void IfMeleeWeaponIsTwoHandedAndProficientInShields_DoNotGenerateShield()
        {
            mockWeaponGenerator.Setup(g => g.GenerateFrom(feats, characterClass, race)).Returns(rangedWeapon);

            var twoHandedWeapon = new Item();
            twoHandedWeapon.Attributes = new[] { AttributeConstants.Melee, AttributeConstants.TwoHanded };

            mockWeaponGenerator.Setup(g => g.GenerateMeleeFrom(feats, characterClass, race)).Returns(twoHandedWeapon);

            feats.Add(new Feat { Name = "feat" });
            feats.Add(new Feat { Name = "other feat" });
            shieldProficiencyFeats.Add(feats[0].Name);

            var shield = new Item();
            shield.Attributes = new[] { AttributeConstants.Shield };
            mockArmorGenerator.Setup(g => g.GenerateShieldFrom(feats, characterClass, race)).Returns(shield);

            var equipment = equipmentGenerator.GenerateWith(feats, characterClass, race);
            Assert.That(equipment.PrimaryHand, Is.EqualTo(rangedWeapon));
            Assert.That(equipment.Treasure.Items, Contains.Item(treasureItem));
            Assert.That(equipment.Treasure.Items, Contains.Item(twoHandedWeapon));
            Assert.That(equipment.Treasure.Items, Is.All.Not.EqualTo(shield));
        }

        [Test]
        public void IfMeleeWeaponIsOneHandedAndNotProficientInShields_DoNotGenerateShield()
        {
            mockWeaponGenerator.Setup(g => g.GenerateFrom(feats, characterClass, race)).Returns(rangedWeapon);

            feats.Add(new Feat { Name = "feat" });
            feats.Add(new Feat { Name = "other feat" });

            Item noShield = null;
            mockArmorGenerator.Setup(g => g.GenerateShieldFrom(feats, characterClass, race)).Returns(noShield);

            var equipment = equipmentGenerator.GenerateWith(feats, characterClass, race);
            Assert.That(equipment.PrimaryHand, Is.EqualTo(rangedWeapon));
            Assert.That(equipment.Treasure.Items, Contains.Item(treasureItem));
            Assert.That(equipment.Treasure.Items, Contains.Item(meleeWeapon));
            Assert.That(equipment.Treasure.Items, Is.All.Not.Null);
        }

        [Test]
        public void IfPrimaryHandIsMelee_GenerateRangedToCarry()
        {
            var equipment = equipmentGenerator.GenerateWith(feats, characterClass, race);
            Assert.That(equipment.PrimaryHand, Is.EqualTo(meleeWeapon));
            Assert.That(equipment.Treasure.Items, Contains.Item(treasureItem));
            Assert.That(equipment.Treasure.Items, Contains.Item(rangedWeapon));
        }

        [Test]
        public void IfPrimaryHandIsMelee_GenerateRangedAndAmmunitionToCarry()
        {
            var ammo = new Item();
            ammo.Name = "ammunition";
            ammo.Attributes = new[] { AttributeConstants.Ammunition };

            baseRangedWeaponTypes.Add("ammunition");
            mockWeaponGenerator.Setup(g => g.GenerateAmmunition(feats, characterClass, race, "ammunition")).Returns(ammo);

            var equipment = equipmentGenerator.GenerateWith(feats, characterClass, race);
            Assert.That(equipment.PrimaryHand, Is.EqualTo(meleeWeapon));
            Assert.That(equipment.Treasure.Items, Contains.Item(treasureItem));
            Assert.That(equipment.Treasure.Items, Contains.Item(rangedWeapon));
            Assert.That(equipment.Treasure.Items, Contains.Item(ammo));
        }

        [Test]
        public void IfPrimaryHandIsMelee_GenerateRangedAndNoAmmunition()
        {
            Item noAmmo = null;

            baseRangedWeaponTypes.Add("ammunition");
            mockWeaponGenerator.Setup(g => g.GenerateAmmunition(feats, characterClass, race, "ammunition")).Returns(noAmmo);

            var equipment = equipmentGenerator.GenerateWith(feats, characterClass, race);
            Assert.That(equipment.PrimaryHand, Is.EqualTo(meleeWeapon));
            Assert.That(equipment.Treasure.Items, Contains.Item(treasureItem));
            Assert.That(equipment.Treasure.Items, Contains.Item(rangedWeapon));
            Assert.That(equipment.Treasure.Items, Is.All.Not.Null);
        }

        [Test]
        public void IfPrimaryHandIsOneHandedRangedWeaponAndProficientInShields_DoNotEquipShield()
        {
            mockWeaponGenerator.Setup(g => g.GenerateFrom(feats, characterClass, race)).Returns(rangedWeapon);

            feats.Add(new Feat { Name = "feat" });
            feats.Add(new Feat { Name = "other feat" });
            shieldProficiencyFeats.Add(feats[0].Name);

            var shield = new Item();
            shield.Attributes = new[] { AttributeConstants.Shield };
            mockArmorGenerator.Setup(g => g.GenerateShieldFrom(feats, characterClass, race)).Returns(shield);

            var equipment = equipmentGenerator.GenerateWith(feats, characterClass, race);
            Assert.That(equipment.OffHand, Is.Null);
            Assert.That(equipment.Treasure.Items, Contains.Item(treasureItem));
            Assert.That(equipment.Treasure.Items, Contains.Item(shield));
        }
    }
}
