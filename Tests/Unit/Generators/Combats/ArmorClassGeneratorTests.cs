using CharacterGen.Common.Abilities.Feats;
using CharacterGen.Common.Items;
using CharacterGen.Common.Races;
using CharacterGen.Generators.Combats;
using CharacterGen.Generators.Domain.Combats;
using CharacterGen.Selectors;
using CharacterGen.Tables;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using TreasureGen.Common.Items;

namespace CharacterGen.Tests.Unit.Generators.Combats
{
    [TestFixture]
    public class ArmorClassGeneratorTests
    {
        private IArmorClassGenerator armorClassGenerator;
        private Equipment equipment;
        private List<Feat> feats;
        private Int32 adjustedDexterityBonus;
        private Mock<ICollectionsSelector> mockCollectionsSelector;
        private Mock<IAdjustmentsSelector> mockAdjustmentsSelector;
        private Dictionary<String, Int32> armorBonuses;
        private Race race;

        [SetUp]
        public void Setup()
        {
            mockAdjustmentsSelector = new Mock<IAdjustmentsSelector>();
            mockCollectionsSelector = new Mock<ICollectionsSelector>();
            armorClassGenerator = new ArmorClassGenerator(mockCollectionsSelector.Object, mockAdjustmentsSelector.Object);
            equipment = new Equipment();
            feats = new List<Feat>();
            armorBonuses = new Dictionary<String, Int32>();
            adjustedDexterityBonus = 0;
            race = new Race();

            armorBonuses[String.Empty] = 0;
            mockAdjustmentsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Adjustments.ArmorBonuses)).Returns(armorBonuses);
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.ArmorClassModifiers, GroupConstants.Size))
                .Returns(Enumerable.Empty<String>());
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.ArmorClassModifiers, GroupConstants.NaturalArmor))
                .Returns(Enumerable.Empty<String>());
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.ArmorClassModifiers, GroupConstants.Deflection))
                .Returns(Enumerable.Empty<String>());
        }

        [Test]
        public void ArmorClassesStartsAt10()
        {
            AssertArmorClass(10, 10, 10);
        }

        private void AssertArmorClass(Int32 flatFooted, Int32 full, Int32 touch)
        {
            var armorClass = armorClassGenerator.GenerateWith(equipment, adjustedDexterityBonus, feats, race);
            Assert.That(armorClass.FlatFooted, Is.EqualTo(flatFooted), "flat-footed");
            Assert.That(armorClass.Full, Is.EqualTo(full), "full");
            Assert.That(armorClass.Touch, Is.EqualTo(touch), "touch");
        }

        [Test]
        public void ArmorBonusesApplied()
        {
            armorBonuses["armor"] = 1;
            armorBonuses["other armor"] = -1;
            equipment.Armor = new Item { Name = "armor" };

            AssertArmorClass(11, 11, 10);
        }

        [Test]
        public void ShieldBonusesApplied()
        {
            armorBonuses["shield"] = 1;
            armorBonuses["other shield"] = -1;
            equipment.OffHand = new Item { Name = "shield" };
            equipment.OffHand.ItemType = ItemTypeConstants.Armor;
            equipment.OffHand.Attributes = new[] { AttributeConstants.Shield };

            AssertArmorClass(11, 11, 10);
        }

        [Test]
        public void PrimaryHandBonusNotApplied()
        {
            armorBonuses["shield"] = 1;
            equipment.PrimaryHand = new Item();
            equipment.PrimaryHand.Name = "shield";
            equipment.PrimaryHand.ItemType = ItemTypeConstants.Armor;
            equipment.PrimaryHand.Attributes = new[] { AttributeConstants.Shield };

            AssertArmorClass(10, 10, 10);
        }

        [Test]
        public void DexterityBonusApplied()
        {
            adjustedDexterityBonus = 1;
            AssertArmorClass(10, 11, 11);
        }

        [Test]
        public void NegativeDexterityBonusApplied()
        {
            adjustedDexterityBonus = -1;
            AssertArmorClass(10, 9, 9);
        }

        [Test]
        public void ArmorEnhancementBonusApplied()
        {
            armorBonuses["armor"] = 0;
            equipment.Armor = new Item { Name = "armor" };
            equipment.Armor.Magic.Bonus = 1;

            AssertArmorClass(11, 11, 10);
        }

        [Test]
        public void ShieldEnhancementBonusApplied()
        {
            armorBonuses["shield"] = 0;
            equipment.OffHand = new Item { Name = "shield" };
            equipment.OffHand.ItemType = ItemTypeConstants.Armor;
            equipment.OffHand.Attributes = new[] { AttributeConstants.Shield };
            equipment.OffHand.Magic.Bonus = 1;

            AssertArmorClass(11, 11, 10);
        }

        [Test]
        public void OffhandNonArmorEnhancementBonusNotApplied()
        {
            equipment.OffHand = new Item { Name = "item" };
            equipment.OffHand.ItemType = "not armor";
            equipment.OffHand.Magic.Bonus = 1;

            AssertArmorClass(10, 10, 10);
        }

        [Test]
        public void PrimaryHandEnhancementBonusNotApplied()
        {
            equipment.PrimaryHand = new Item();
            equipment.PrimaryHand.Magic.Bonus = 1;

            AssertArmorClass(10, 10, 10);
        }

        [Test]
        public void DeflectionBonusApplied()
        {
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.ArmorClassModifiers, GroupConstants.Deflection))
                .Returns(new[] { "ring", "other ring" });

            var ring = new Item();
            ring.Name = "ring";
            ring.Magic.Bonus = 1;

            var thing = new Item();
            thing.Name = "thing";
            thing.Magic.Bonus = -1;

            equipment.Treasure.Items = new[] { ring, thing };

            AssertArmorClass(11, 11, 11);
        }

        [Test]
        public void OnlyHighestDeflectionBonusApplies()
        {
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.ArmorClassModifiers, GroupConstants.Deflection))
                .Returns(new[] { "ring", "other ring" });

            var ring = new Item();
            ring.Name = "ring";
            ring.Magic.Bonus = 1;

            var otherRing = new Item();
            otherRing.Name = "other ring";
            otherRing.Magic.Bonus = 2;

            equipment.Treasure.Items = new[] { ring, otherRing };

            AssertArmorClass(12, 12, 12);
        }

        [Test]
        public void DeflectionBonusesDoNotStack()
        {
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.ArmorClassModifiers, GroupConstants.Deflection))
                .Returns(new[] { "ring", "other ring" });

            var ring = new Item();
            ring.Name = "ring";
            ring.Magic.Bonus = 1;

            var otherRing = new Item();
            otherRing.Name = "other ring";
            otherRing.Magic.Bonus = 1;

            equipment.Treasure.Items = new[] { ring, otherRing };

            AssertArmorClass(11, 11, 11);
        }

        [Test]
        public void TotalNaturalArmorBonusFromFeatApplied()
        {
            feats.Add(new Feat());
            feats.Add(new Feat());
            feats[0].Name = "feat 1";
            feats[0].Strength = 1;
            feats[1].Name = "feat 2";
            feats[1].Strength = 1;

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.ArmorClassModifiers, GroupConstants.NaturalArmor))
                .Returns(new[] { "feat 1", "feat 2", "other feat" });

            AssertArmorClass(12, 12, 10);
        }

        [Test]
        public void DoNotApplyNaturalArmorBonusesIfTheyHaveQualifications()
        {
            feats.Add(new Feat());
            feats.Add(new Feat());
            feats[0].Name = "feat 1";
            feats[0].Strength = 1;
            feats[1].Name = "feat 2";
            feats[1].Strength = 1;
            feats[1].Focus = "focus";

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.ArmorClassModifiers, GroupConstants.NaturalArmor))
                .Returns(new[] { "feat 1", "feat 2", "other feat" });

            AssertArmorClass(11, 11, 10);
        }

        [Test]
        public void NaturalArmorBonusFromItemsApplied()
        {
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.ArmorClassModifiers, GroupConstants.NaturalArmor))
                .Returns(new[] { "ring", "other ring" });

            var ring = new Item();
            ring.Name = "ring";
            ring.Magic.Bonus = 1;

            var thing = new Item();
            thing.Name = "thing";
            thing.Magic.Bonus = -1;

            equipment.Treasure.Items = new[] { ring, thing };

            AssertArmorClass(11, 11, 10);
        }

        [Test]
        public void OnlyHighestNaturalArmorBonusFromItemsApplies()
        {
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.ArmorClassModifiers, GroupConstants.NaturalArmor))
                .Returns(new[] { "ring", "other ring" });

            var ring = new Item();
            ring.Name = "ring";
            ring.Magic.Bonus = 1;

            var otherRing = new Item();
            otherRing.Name = "other ring";
            otherRing.Magic.Bonus = 2;

            equipment.Treasure.Items = new[] { ring, otherRing };

            AssertArmorClass(12, 12, 10);
        }

        [Test]
        public void TotalNaturalArmorBonusFromFeatAndItemsApplied()
        {
            feats.Add(new Feat());
            feats.Add(new Feat());
            feats[0].Name = "feat 1";
            feats[0].Strength = 1;
            feats[1].Name = "feat 2";
            feats[1].Strength = 1;

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.ArmorClassModifiers, GroupConstants.NaturalArmor))
                .Returns(new[] { "feat 1", "feat 2", "ring", "other ring" });

            var ring = new Item();
            ring.Name = "ring";
            ring.Magic.Bonus = 2;

            var otherRing = new Item();
            otherRing.Name = "other ring";
            otherRing.Magic.Bonus = 1;

            equipment.Treasure.Items = new[] { ring, otherRing };

            AssertArmorClass(14, 14, 10);
        }

        [Test]
        public void LargeCreaturesAreMinusOneOnArmorClass()
        {
            race.Size = RaceConstants.Sizes.Large;
            AssertArmorClass(9, 9, 9);
        }

        [Test]
        public void SmallCreaturesArePlusOneOnArmorClass()
        {
            race.Size = RaceConstants.Sizes.Small;
            AssertArmorClass(11, 11, 11);
        }

        [Test]
        public void ArmorClassesAreSummed()
        {
            armorBonuses["shield"] = 1;
            armorBonuses["armor"] = 1;
            adjustedDexterityBonus = 1;

            var feat = new Feat();
            feat.Name = "feat 1";
            feat.Strength = 1;
            feats.Add(feat);

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.ArmorClassModifiers, GroupConstants.Deflection))
                .Returns(new[] { "ring" });
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.ArmorClassModifiers, GroupConstants.NaturalArmor))
                .Returns(new[] { "feat 1" });

            equipment.Armor = new Item { Name = "armor" };
            equipment.Armor.Magic.Bonus = 1;
            equipment.OffHand = new Item { Name = "shield" };
            equipment.OffHand.Magic.Bonus = 1;
            equipment.OffHand.ItemType = ItemTypeConstants.Armor;
            equipment.OffHand.Attributes = new[] { AttributeConstants.Shield };

            var ring = new Item();
            ring.Name = "ring";
            ring.Magic.Bonus = 1;

            equipment.Treasure.Items = new[] { ring };

            race.Size = RaceConstants.Sizes.Small;

            AssertArmorClass(17, 18, 13);
        }
    }
}