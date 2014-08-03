using System;
using System.Collections.Generic;
using System.Linq;
using EquipmentGen.Common.Items;
using Moq;
using NPCGen.Common.Abilities.Feats;
using NPCGen.Common.Items;
using NPCGen.Generators.Combats;
using NPCGen.Generators.Interfaces.Combats;
using NPCGen.Selectors.Interfaces;
using NUnit.Framework;

namespace NPCGen.Tests.Unit.Generators.Combats
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
        private Dictionary<String, Int32> featAdjustments;

        [SetUp]
        public void Setup()
        {
            mockAdjustmentsSelector = new Mock<IAdjustmentsSelector>();
            mockCollectionsSelector = new Mock<ICollectionsSelector>();
            armorClassGenerator = new ArmorClassGenerator(mockCollectionsSelector.Object, mockAdjustmentsSelector.Object);
            equipment = new Equipment();
            feats = new List<Feat>();
            armorBonuses = new Dictionary<String, Int32>();
            featAdjustments = new Dictionary<String, Int32>();
            adjustedDexterityBonus = 0;

            armorBonuses[String.Empty] = 0;
            mockAdjustmentsSelector.Setup(s => s.SelectAdjustmentsFrom("ArmorBonuses")).Returns(armorBonuses);
            mockAdjustmentsSelector.Setup(s => s.SelectAdjustmentsFrom("FeatArmorAdjustments")).Returns(featAdjustments);
            mockCollectionsSelector.Setup(s => s.SelectFrom("SizeModifiers")).Returns(Enumerable.Empty<String>());
            mockCollectionsSelector.Setup(s => s.SelectFrom("NaturalArmorBonuses")).Returns(Enumerable.Empty<String>());
            mockCollectionsSelector.Setup(s => s.SelectFrom("DodgeBonuses")).Returns(Enumerable.Empty<String>());
            mockCollectionsSelector.Setup(s => s.SelectFrom("DeflectionBonuses")).Returns(Enumerable.Empty<String>());

        }

        [Test]
        public void ArmorClassesStartsAt10()
        {
            AssertArmorClass(10, 10, 10);
        }

        private void AssertArmorClass(Int32 flatFooted, Int32 full, Int32 touch)
        {
            var armorClass = armorClassGenerator.GenerateWith(equipment, adjustedDexterityBonus, feats);
            Assert.That(armorClass.FlatFooted, Is.EqualTo(flatFooted));
            Assert.That(armorClass.Full, Is.EqualTo(full));
            Assert.That(armorClass.Touch, Is.EqualTo(touch));
        }

        [Test]
        public void ArmorBonusesApplied()
        {
            armorBonuses["armor"] = 1;
            armorBonuses["other armor"] = -1;
            equipment.Armor.Name = "armor";

            AssertArmorClass(11, 11, 10);
        }

        [Test]
        public void ShieldBonusesApplied()
        {
            armorBonuses["shield"] = 1;
            armorBonuses["other shield"] = -1;
            equipment.OffHand.Name = "shield";
            equipment.OffHand.ItemType = ItemTypeConstants.Armor;
            equipment.OffHand.Attributes = new[] { AttributeConstants.Shield };

            AssertArmorClass(11, 11, 10);
        }

        [Test]
        public void PrimaryHandBonusNotApplied()
        {
            armorBonuses["shield"] = 1;
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
        public void SizeModifierApplied()
        {
            feats.Add(new Feat { Name = "feat 1" });
            featAdjustments["feat 1"] = 1;
            featAdjustments["feat 2"] = -1;
            mockCollectionsSelector.Setup(s => s.SelectFrom("SizeModifiers")).Returns(new[] { "feat 1", "feat 2", "other feat" });

            AssertArmorClass(11, 11, 11);
        }

        [Test]
        public void SizeModifersStack()
        {
            feats.Add(new Feat { Name = "feat 1" });
            feats.Add(new Feat { Name = "feat 2" });
            featAdjustments["feat 1"] = 1;
            featAdjustments["feat 2"] = 1;
            mockCollectionsSelector.Setup(s => s.SelectFrom("SizeModifiers")).Returns(new[] { "feat 1", "feat 2", "other feat" });

            AssertArmorClass(12, 12, 12);
        }

        [Test]
        public void NegativeSizeModierApplied()
        {
            feats.Add(new Feat { Name = "feat 2" });
            featAdjustments["feat 1"] = 1;
            featAdjustments["feat 2"] = -1;
            mockCollectionsSelector.Setup(s => s.SelectFrom("SizeModifiers")).Returns(new[] { "feat 1", "feat 2", "other feat" });

            AssertArmorClass(9, 9, 9);
        }

        [Test]
        public void ArmorEnhancementBonusApplied()
        {
            equipment.Armor.Magic.Bonus = 1;
            AssertArmorClass(11, 11, 10);
        }

        [Test]
        public void ShieldEnhancementBonusApplied()
        {
            equipment.OffHand.ItemType = ItemTypeConstants.Armor;
            equipment.OffHand.Attributes = new[] { AttributeConstants.Shield };
            equipment.OffHand.Magic.Bonus = 1;
            AssertArmorClass(11, 11, 10);
        }

        [Test]
        public void OffhandNonArmorEnhancementBonusNotApplied()
        {
            equipment.OffHand.ItemType = "not armor";
            equipment.OffHand.Magic.Bonus = 1;
            AssertArmorClass(10, 10, 10);
        }

        [Test]
        public void PrimaryHandEnhancementBonusNotApplied()
        {
            equipment.PrimaryHand.Magic.Bonus = 1;
            AssertArmorClass(10, 10, 10);
        }

        [Test]
        public void DeflectionBonusApplied()
        {
            mockCollectionsSelector.Setup(s => s.SelectFrom("DeflectionBonuses")).Returns(new[] { "ring", "other ring" });

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
            mockCollectionsSelector.Setup(s => s.SelectFrom("DeflectionBonuses")).Returns(new[] { "ring", "other ring" });

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
            mockCollectionsSelector.Setup(s => s.SelectFrom("DeflectionBonuses")).Returns(new[] { "ring", "other ring" });

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
        public void NaturalArmorBonusFromFeatApplied()
        {
            feats.Add(new Feat { Name = "feat 1" });
            featAdjustments["feat 1"] = 1;
            featAdjustments["feat 2"] = -1;
            mockCollectionsSelector.Setup(s => s.SelectFrom("NaturalArmorBonuses")).Returns(new[] { "feat 1", "feat 2", "other feat" });

            AssertArmorClass(11, 11, 10);
        }

        [Test]
        public void NaturalArmorBonusFromItemsApplied()
        {
            mockCollectionsSelector.Setup(s => s.SelectFrom("NaturalArmorBonuses")).Returns(new[] { "ring", "other ring" });

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
        public void OnlyHighestNaturalArmorBonusAppliesWhenHighestIsFeat()
        {
            feats.Add(new Feat { Name = "feat 1" });
            feats.Add(new Feat { Name = "feat 2" });
            featAdjustments["feat 1"] = 1;
            featAdjustments["feat 2"] = 2;
            mockCollectionsSelector.Setup(s => s.SelectFrom("NaturalArmorBonuses")).Returns(new[] { "feat 1", "feat 2", "ring", "other ring" });

            var ring = new Item();
            ring.Name = "ring";
            ring.Magic.Bonus = 1;

            var otherRing = new Item();
            otherRing.Name = "other ring";
            otherRing.Magic.Bonus = 1;

            equipment.Treasure.Items = new[] { ring, otherRing };

            AssertArmorClass(12, 12, 10);
        }

        [Test]
        public void OnlyHighestNaturalArmorBonusAppliesWhenHighestIsItem()
        {
            feats.Add(new Feat { Name = "feat 1" });
            feats.Add(new Feat { Name = "feat 2" });
            featAdjustments["feat 1"] = 1;
            featAdjustments["feat 2"] = 1;
            mockCollectionsSelector.Setup(s => s.SelectFrom("NaturalArmorBonuses")).Returns(new[] { "feat 1", "feat 2", "ring", "other ring" });

            var ring = new Item();
            ring.Name = "ring";
            ring.Magic.Bonus = 2;

            var otherRing = new Item();
            otherRing.Name = "other ring";
            otherRing.Magic.Bonus = 1;

            equipment.Treasure.Items = new[] { ring, otherRing };

            AssertArmorClass(12, 12, 10);
        }

        [Test]
        public void NaturalArmorBonusesDoNotStack()
        {
            feats.Add(new Feat { Name = "feat 1" });
            feats.Add(new Feat { Name = "feat 2" });
            featAdjustments["feat 1"] = 1;
            featAdjustments["feat 2"] = 1;
            mockCollectionsSelector.Setup(s => s.SelectFrom("NaturalArmorBonuses")).Returns(new[] { "feat 1", "feat 2", "ring", "other ring" });

            var ring = new Item();
            ring.Name = "ring";
            ring.Magic.Bonus = 1;

            var otherRing = new Item();
            otherRing.Name = "other ring";
            otherRing.Magic.Bonus = 1;

            equipment.Treasure.Items = new[] { ring, otherRing };

            AssertArmorClass(11, 11, 10);
        }

        [Test]
        public void DodgeBonusApplied()
        {
            feats.Add(new Feat { Name = "feat 1" });
            featAdjustments["feat 1"] = 1;
            featAdjustments["feat 2"] = -1;
            mockCollectionsSelector.Setup(s => s.SelectFrom("DodgeBonuses")).Returns(new[] { "feat 1", "feat 2", "other feat" });

            AssertArmorClass(10, 11, 11);
        }

        [Test]
        public void DodgeBonusesStack()
        {
            feats.Add(new Feat { Name = "feat 1" });
            feats.Add(new Feat { Name = "feat 2" });
            featAdjustments["feat 1"] = 1;
            featAdjustments["feat 2"] = 1;
            mockCollectionsSelector.Setup(s => s.SelectFrom("DodgeBonuses")).Returns(new[] { "feat 1", "feat 2", "other feat" });

            AssertArmorClass(10, 12, 12);
        }

        [Test]
        public void ArmorClassesAreSummed()
        {
            armorBonuses["shield"] = 1;
            armorBonuses["armor"] = 1;
            adjustedDexterityBonus = 1;

            feats.Add(new Feat { Name = "feat 1" });
            featAdjustments["feat 1"] = 1;

            mockCollectionsSelector.Setup(s => s.SelectFrom("DeflectionBonuses")).Returns(new[] { "ring" });
            mockCollectionsSelector.Setup(s => s.SelectFrom("NaturalArmorBonuses")).Returns(new[] { "feat 1" });
            mockCollectionsSelector.Setup(s => s.SelectFrom("DodgeBonuses")).Returns(new[] { "feat 1" });
            mockCollectionsSelector.Setup(s => s.SelectFrom("SizeModifiers")).Returns(new[] { "feat 1" });

            equipment.Armor.Name = "armor";
            equipment.Armor.Magic.Bonus = 1;
            equipment.OffHand.Name = "shield";
            equipment.OffHand.Magic.Bonus = 1;
            equipment.OffHand.ItemType = ItemTypeConstants.Armor;
            equipment.OffHand.Attributes = new[] { AttributeConstants.Shield };

            var ring = new Item();
            ring.Name = "ring";
            ring.Magic.Bonus = 1;

            equipment.Treasure.Items = new[] { ring };

            AssertArmorClass(17, 19, 14);
        }
    }
}