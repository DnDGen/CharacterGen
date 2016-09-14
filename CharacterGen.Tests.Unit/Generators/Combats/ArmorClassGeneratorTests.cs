using CharacterGen.Abilities.Feats;
using CharacterGen.Combats;
using CharacterGen.Domain.Generators.Combats;
using CharacterGen.Domain.Selectors.Collections;
using CharacterGen.Domain.Tables;
using CharacterGen.Items;
using CharacterGen.Races;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using TreasureGen.Items;
using TreasureGen.Items.Magical;

namespace CharacterGen.Tests.Unit.Generators.Combats
{
    [TestFixture]
    public class ArmorClassGeneratorTests
    {
        private IArmorClassGenerator armorClassGenerator;
        private Equipment equipment;
        private List<Feat> feats;
        private int adjustedDexterityBonus;
        private Mock<ICollectionsSelector> mockCollectionsSelector;
        private Mock<IAdjustmentsSelector> mockAdjustmentsSelector;
        private Dictionary<string, int> armorBonuses;
        private Race race;
        private Dictionary<string, int> sizeModifiers;

        [SetUp]
        public void Setup()
        {
            mockAdjustmentsSelector = new Mock<IAdjustmentsSelector>();
            mockCollectionsSelector = new Mock<ICollectionsSelector>();
            armorClassGenerator = new ArmorClassGenerator(mockCollectionsSelector.Object, mockAdjustmentsSelector.Object);
            equipment = new Equipment();
            feats = new List<Feat>();
            armorBonuses = new Dictionary<string, int>();
            sizeModifiers = new Dictionary<string, int>();
            adjustedDexterityBonus = 0;
            race = new Race();

            armorBonuses[string.Empty] = 0;
            mockAdjustmentsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Adjustments.ArmorBonuses)).Returns(armorBonuses);
            mockAdjustmentsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Adjustments.SizeModifiers)).Returns(sizeModifiers);
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.ArmorClassModifiers, GroupConstants.Size))
                .Returns(Enumerable.Empty<string>());
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.ArmorClassModifiers, GroupConstants.NaturalArmor))
                .Returns(Enumerable.Empty<string>());
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.ArmorClassModifiers, GroupConstants.Deflection))
                .Returns(Enumerable.Empty<string>());

            race.Size = "size";
            sizeModifiers["size"] = 0;
            sizeModifiers["other size"] = 0;
        }

        [Test]
        public void ArmorClassesStartsAtBase()
        {
            GenerateAndAssertArmorClass();
        }

        private ArmorClass GenerateAndAssertArmorClass(int full = ArmorClass.BaseArmorClass, int flatFooted = ArmorClass.BaseArmorClass, int touch = ArmorClass.BaseArmorClass, bool circumstantial = false)
        {
            var armorClass = armorClassGenerator.GenerateWith(equipment, adjustedDexterityBonus, feats, race);
            Assert.That(armorClass.Full, Is.EqualTo(full), "full");
            Assert.That(armorClass.FlatFooted, Is.EqualTo(flatFooted), "flat-footed");
            Assert.That(armorClass.Touch, Is.EqualTo(touch), "touch");
            Assert.That(armorClass.CircumstantialBonus, Is.EqualTo(circumstantial));
            Assert.That(armorClass.AdjustedDexterityBonus, Is.EqualTo(adjustedDexterityBonus));

            return armorClass;
        }

        [Test]
        public void ArmorBonusesApplied()
        {
            armorBonuses["armor"] = 1;
            armorBonuses["other armor"] = -1;
            equipment.Armor = new Item { Name = "armor" };

            var armorClass = GenerateAndAssertArmorClass(11, 11);
            Assert.That(armorClass.ArmorBonus, Is.EqualTo(1));
        }

        [Test]
        public void ShieldBonusesApplied()
        {
            armorBonuses["shield"] = 1;
            armorBonuses["other shield"] = -1;
            equipment.OffHand = new Item { Name = "shield" };
            equipment.OffHand.ItemType = ItemTypeConstants.Armor;
            equipment.OffHand.Attributes = new[] { AttributeConstants.Shield };

            var armorClass = GenerateAndAssertArmorClass(11, 11);
            Assert.That(armorClass.ShieldBonus, Is.EqualTo(1));
        }

        [Test]
        public void PrimaryHandBonusNotApplied()
        {
            armorBonuses["shield"] = 1;
            equipment.PrimaryHand = new Item();
            equipment.PrimaryHand.Name = "shield";
            equipment.PrimaryHand.ItemType = ItemTypeConstants.Armor;
            equipment.PrimaryHand.Attributes = new[] { AttributeConstants.Shield };

            GenerateAndAssertArmorClass();
        }

        [Test]
        public void ArmorFromItemApplied()
        {
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.ArmorClassModifiers, GroupConstants.ArmorBonus))
                .Returns(new[] { "bracers", "other item" });

            var bracers = new Item();
            bracers.Name = "bracers";
            bracers.Magic.Bonus = 1;

            var thing = new Item();
            thing.Name = "thing";
            thing.Magic.Bonus = -1;

            equipment.Treasure.Items = new[] { bracers, thing };

            var armorClass = GenerateAndAssertArmorClass(11, 11);
            Assert.That(armorClass.ArmorBonus, Is.EqualTo(1));
        }

        [Test]
        public void BestArmorBonusAppliedIfArmor()
        {
            armorBonuses["armor"] = 2;
            equipment.Armor = new Item { Name = "armor" };

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.ArmorClassModifiers, GroupConstants.ArmorBonus))
                .Returns(new[] { "bracers", "other item" });

            var bracers = new Item();
            bracers.Name = "bracers";
            bracers.Magic.Bonus = 1;

            var thing = new Item();
            thing.Name = "thing";
            thing.Magic.Bonus = -1;

            equipment.Treasure.Items = new[] { bracers, thing };

            var armorClass = GenerateAndAssertArmorClass(12, 12);
            Assert.That(armorClass.ArmorBonus, Is.EqualTo(2));
        }

        [Test]
        public void BestArmorBonusAppliedIfArmorWithBonus()
        {
            armorBonuses["armor"] = 1;
            equipment.Armor = new Item { Name = "armor" };
            equipment.Armor.Magic.Bonus = 1;

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.ArmorClassModifiers, GroupConstants.ArmorBonus))
                .Returns(new[] { "bracers", "other item" });

            var bracers = new Item();
            bracers.Name = "bracers";
            bracers.Magic.Bonus = 1;

            var thing = new Item();
            thing.Name = "thing";
            thing.Magic.Bonus = -1;

            equipment.Treasure.Items = new[] { bracers, thing };

            var armorClass = GenerateAndAssertArmorClass(12, 12);
            Assert.That(armorClass.ArmorBonus, Is.EqualTo(2));
        }

        [Test]
        public void BestArmorBonusAppliedIfItem()
        {
            armorBonuses["armor"] = 1;
            equipment.Armor = new Item { Name = "armor" };

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.ArmorClassModifiers, GroupConstants.ArmorBonus))
                .Returns(new[] { "bracers", "other item" });

            var bracers = new Item();
            bracers.Name = "bracers";
            bracers.Magic.Bonus = 2;

            var thing = new Item();
            thing.Name = "thing";
            thing.Magic.Bonus = -1;

            equipment.Treasure.Items = new[] { bracers, thing };

            var armorClass = GenerateAndAssertArmorClass(12, 12);
            Assert.That(armorClass.ArmorBonus, Is.EqualTo(2));
        }

        [Test]
        public void BestArmorBonusAppliedIfItemAndShield()
        {
            armorBonuses["armor"] = 1;
            equipment.Armor = new Item { Name = "armor" };

            armorBonuses["shield"] = 1;
            equipment.OffHand = new Item { Name = "shield" };
            equipment.OffHand.ItemType = ItemTypeConstants.Armor;
            equipment.OffHand.Attributes = new[] { AttributeConstants.Shield };

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.ArmorClassModifiers, GroupConstants.ArmorBonus))
                .Returns(new[] { "bracers", "other item" });

            var bracers = new Item();
            bracers.Name = "bracers";
            bracers.Magic.Bonus = 2;

            var thing = new Item();
            thing.Name = "thing";
            thing.Magic.Bonus = -1;

            equipment.Treasure.Items = new[] { bracers, thing };

            var armorClass = GenerateAndAssertArmorClass(13, 13);
            Assert.That(armorClass.ArmorBonus, Is.EqualTo(2));
            Assert.That(armorClass.ShieldBonus, Is.EqualTo(1));
        }

        [Test]
        public void DexterityBonusApplied()
        {
            adjustedDexterityBonus = 1;
            var armorClass = GenerateAndAssertArmorClass(11, touch: 11);
            Assert.That(armorClass.AdjustedDexterityBonus, Is.EqualTo(1));
        }

        [Test]
        public void NegativeDexterityBonusApplied()
        {
            adjustedDexterityBonus = -1;
            var armorClass = GenerateAndAssertArmorClass(9, touch: 9);
            Assert.That(armorClass.AdjustedDexterityBonus, Is.EqualTo(-1));
        }

        [Test]
        public void ArmorEnhancementBonusApplied()
        {
            armorBonuses["armor"] = 0;
            equipment.Armor = new Item { Name = "armor" };
            equipment.Armor.Magic.Bonus = 1;

            var armorClass = GenerateAndAssertArmorClass(11, 11);
            Assert.That(armorClass.ArmorBonus, Is.EqualTo(1));
        }

        [Test]
        public void ShieldEnhancementBonusApplied()
        {
            armorBonuses["shield"] = 0;
            equipment.OffHand = new Item { Name = "shield" };
            equipment.OffHand.ItemType = ItemTypeConstants.Armor;
            equipment.OffHand.Attributes = new[] { AttributeConstants.Shield };
            equipment.OffHand.Magic.Bonus = 1;

            var armorClass = GenerateAndAssertArmorClass(11, 11);
            Assert.That(armorClass.ShieldBonus, Is.EqualTo(1));
        }

        [Test]
        public void OffhandNonArmorEnhancementBonusNotApplied()
        {
            equipment.OffHand = new Item { Name = "item" };
            equipment.OffHand.ItemType = "not armor";
            equipment.OffHand.Magic.Bonus = 1;

            GenerateAndAssertArmorClass();
        }

        [Test]
        public void PrimaryHandEnhancementBonusNotApplied()
        {
            equipment.PrimaryHand = new Item();
            equipment.PrimaryHand.Magic.Bonus = 1;

            GenerateAndAssertArmorClass();
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

            var armorClass = GenerateAndAssertArmorClass(11, 11, 11);
            Assert.That(armorClass.DeflectionBonus, Is.EqualTo(1));
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

            var armorClass = GenerateAndAssertArmorClass(12, 12, 12);
            Assert.That(armorClass.DeflectionBonus, Is.EqualTo(2));
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

            var armorClass = GenerateAndAssertArmorClass(11, 11, 11);
            Assert.That(armorClass.DeflectionBonus, Is.EqualTo(1));
        }

        [Test]
        public void TotalNaturalArmorBonusFromFeatApplied()
        {
            feats.Add(new Feat());
            feats.Add(new Feat());
            feats[0].Name = "feat 1";
            feats[0].Power = 1;
            feats[1].Name = "feat 2";
            feats[1].Power = 1;

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.ArmorClassModifiers, GroupConstants.NaturalArmor))
                .Returns(new[] { "feat 1", "feat 2", "other feat" });

            var armorClass = GenerateAndAssertArmorClass(12, 12);
            Assert.That(armorClass.NaturalArmorBonus, Is.EqualTo(2));
        }

        [Test]
        public void IfNaturalArmorHasCircumstantialArmorBonus_MarkCircumstantialBonus()
        {
            feats.Add(new Feat());
            feats.Add(new Feat());
            feats[0].Name = "feat 1";
            feats[0].Power = 1;
            feats[1].Name = "feat 2";
            feats[1].Foci = new[] { "focus" };
            feats[1].Power = 1;

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.ArmorClassModifiers, GroupConstants.NaturalArmor))
                .Returns(new[] { "feat 1", "feat 2", "other feat" });

            var armorClass = GenerateAndAssertArmorClass(11, 11, circumstantial: true);
            Assert.That(armorClass.NaturalArmorBonus, Is.EqualTo(1));
        }

        [Test]
        public void DoNotOverwriteCircumstantialBonus()
        {
            feats.Add(new Feat());
            feats.Add(new Feat());
            feats[0].Name = "feat 1";
            feats[0].Foci = new[] { "focus" };
            feats[0].Power = 1;
            feats[1].Name = "feat 2";
            feats[1].Power = 1;

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.ArmorClassModifiers, GroupConstants.NaturalArmor))
                .Returns(new[] { "feat 1", "feat 2", "other feat" });

            var armorClass = GenerateAndAssertArmorClass(11, 11, circumstantial: true);
            Assert.That(armorClass.NaturalArmorBonus, Is.EqualTo(1));
        }

        [Test]
        public void TotalDodgeBonusFromFeatApplied()
        {
            feats.Add(new Feat());
            feats.Add(new Feat());
            feats[0].Name = "feat 1";
            feats[0].Power = 1;
            feats[1].Name = "feat 2";
            feats[1].Power = 1;

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.ArmorClassModifiers, GroupConstants.DodgeBonus))
                .Returns(new[] { "feat 1", "feat 2", "other feat" });

            var armorClass = GenerateAndAssertArmorClass(12, touch: 12);
            Assert.That(armorClass.DodgeBonus, Is.EqualTo(2));
        }

        [Test]
        public void IfDodgeBonusHasCircumstantialArmorBonus_MarkCircumstantialBonus()
        {
            feats.Add(new Feat());
            feats.Add(new Feat());
            feats[0].Name = "feat 1";
            feats[0].Power = 1;
            feats[1].Name = "feat 2";
            feats[1].Foci = new[] { "focus" };
            feats[1].Power = 1;

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.ArmorClassModifiers, GroupConstants.DodgeBonus))
                .Returns(new[] { "feat 1", "feat 2", "other feat" });

            var armorClass = GenerateAndAssertArmorClass(11, touch: 11, circumstantial: true);
            Assert.That(armorClass.DodgeBonus, Is.EqualTo(1));
        }

        [Test]
        public void DoNotOverwriteCircumstantialBonusWithDodgeBonus()
        {
            feats.Add(new Feat());
            feats.Add(new Feat());
            feats[0].Name = "feat 1";
            feats[0].Foci = new[] { "focus" };
            feats[0].Power = 1;
            feats[1].Name = "feat 2";
            feats[1].Power = 1;

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.ArmorClassModifiers, GroupConstants.NaturalArmor))
                .Returns(new[] { "feat 1", "other feat" });

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.ArmorClassModifiers, GroupConstants.DodgeBonus))
                .Returns(new[] { "feat 2", "other feat" });

            var armorClass = GenerateAndAssertArmorClass(11, touch: 11, circumstantial: true);
            Assert.That(armorClass.DodgeBonus, Is.EqualTo(1));
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

            var armorClass = GenerateAndAssertArmorClass(11, 11);
            Assert.That(armorClass.NaturalArmorBonus, Is.EqualTo(1));
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

            var armorClass = GenerateAndAssertArmorClass(12, 12, 10);
            Assert.That(armorClass.NaturalArmorBonus, Is.EqualTo(2));
        }

        [Test]
        public void TotalNaturalArmorBonusFromFeatAndItemsApplied()
        {
            feats.Add(new Feat());
            feats.Add(new Feat());
            feats[0].Name = "feat 1";
            feats[0].Power = 1;
            feats[1].Name = "feat 2";
            feats[1].Power = 1;

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.ArmorClassModifiers, GroupConstants.NaturalArmor))
                .Returns(new[] { "feat 1", "feat 2", "ring", "other ring" });

            var ring = new Item();
            ring.Name = "ring";
            ring.Magic.Bonus = 2;

            var otherRing = new Item();
            otherRing.Name = "other ring";
            otherRing.Magic.Bonus = 1;

            equipment.Treasure.Items = new[] { ring, otherRing };

            var armorClass = GenerateAndAssertArmorClass(14, 14, 10);
            Assert.That(armorClass.NaturalArmorBonus, Is.EqualTo(4));
        }

        [Test]
        public void SizeModifiesArmorClass()
        {
            sizeModifiers["size"] = 9266;
            sizeModifiers["other size"] = 90210;

            var armorClass = GenerateAndAssertArmorClass(9276, 9276, 9276);
            Assert.That(armorClass.SizeModifier, Is.EqualTo(9266));
        }

        [Test]
        public void SizeModifiesArmorClassNegatively()
        {
            sizeModifiers["size"] = -4;
            sizeModifiers["other size"] = -2;

            var armorClass = GenerateAndAssertArmorClass(6, 6, 6);
            Assert.That(armorClass.SizeModifier, Is.EqualTo(-4));
        }

        [Test]
        public void ArmorClassesAreSummed()
        {
            armorBonuses["shield"] = 1;
            armorBonuses["armor"] = 1;
            adjustedDexterityBonus = 1;

            var feat = new Feat();
            feat.Name = "feat 1";
            feat.Power = 1;
            feats.Add(feat);

            var otherFeat = new Feat();
            otherFeat.Name = "feat 2";
            otherFeat.Power = 1;
            feats.Add(otherFeat);

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.ArmorClassModifiers, GroupConstants.Deflection))
                .Returns(new[] { "ring" });
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.ArmorClassModifiers, GroupConstants.NaturalArmor))
                .Returns(new[] { "feat 1" });
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.ArmorClassModifiers, GroupConstants.DodgeBonus))
                .Returns(new[] { "feat 2" });

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

            sizeModifiers[race.Size] = 1;

            var armorClass = GenerateAndAssertArmorClass(19, 17, 14);
            Assert.That(armorClass.ArmorBonus, Is.EqualTo(2));
            Assert.That(armorClass.DeflectionBonus, Is.EqualTo(1));
            Assert.That(armorClass.DodgeBonus, Is.EqualTo(1));
            Assert.That(armorClass.NaturalArmorBonus, Is.EqualTo(1));
            Assert.That(armorClass.ShieldBonus, Is.EqualTo(2));
            Assert.That(armorClass.SizeModifier, Is.EqualTo(1));
        }

        [Test]
        public void CursedArmorWithOppositeEffectHasNegativeBonus()
        {
            armorBonuses["armor"] = 3;
            equipment.Armor = new Item { Name = "armor" };
            equipment.Armor.Magic.Bonus = 1;
            equipment.Armor.Magic.Curse = CurseConstants.OppositeEffect;

            var armorClass = GenerateAndAssertArmorClass(12, 12);
            Assert.That(armorClass.ArmorBonus, Is.EqualTo(2));
        }

        [Test]
        public void CursedArmorWithOppositeEffectHasNegativeEffect()
        {
            armorBonuses["armor"] = 1;
            equipment.Armor = new Item { Name = "armor" };
            equipment.Armor.Magic.Bonus = 2;
            equipment.Armor.Magic.Curse = CurseConstants.OppositeEffect;

            var armorClass = GenerateAndAssertArmorClass(9, 9);
            Assert.That(armorClass.ArmorBonus, Is.EqualTo(-1));
        }

        [Test]
        public void CursedArmorWithOppositeEffectLosesToItem()
        {
            armorBonuses["armor"] = 1;
            equipment.Armor = new Item { Name = "armor" };
            equipment.Armor.Magic.Bonus = 2;
            equipment.Armor.Magic.Curse = CurseConstants.OppositeEffect;

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.ArmorClassModifiers, GroupConstants.ArmorBonus))
                .Returns(new[] { "bracers", "other item" });

            var bracers = new Item();
            bracers.Name = "bracers";
            bracers.Magic.Bonus = 1;

            equipment.Treasure.Items = new[] { bracers };

            var armorClass = GenerateAndAssertArmorClass(11, 11);
            Assert.That(armorClass.ArmorBonus, Is.EqualTo(1));
        }

        [Test]
        public void CursedArmorWithOppositeEffectWinsAgainstItem()
        {
            armorBonuses["armor"] = 3;
            equipment.Armor = new Item { Name = "armor" };
            equipment.Armor.Magic.Bonus = 1;
            equipment.Armor.Magic.Curse = CurseConstants.OppositeEffect;

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.ArmorClassModifiers, GroupConstants.ArmorBonus))
                .Returns(new[] { "bracers", "other item" });

            var bracers = new Item();
            bracers.Name = "bracers";
            bracers.Magic.Bonus = 1;

            equipment.Treasure.Items = new[] { bracers };

            var armorClass = GenerateAndAssertArmorClass(12, 12);
            Assert.That(armorClass.ArmorBonus, Is.EqualTo(2));
        }

        [Test]
        public void CursedShieldWithOppositeEffectHasNegativeBonus()
        {
            armorBonuses["shield"] = 3;
            equipment.OffHand = new Item { Name = "shield" };
            equipment.OffHand.ItemType = ItemTypeConstants.Armor;
            equipment.OffHand.Attributes = new[] { AttributeConstants.Shield };
            equipment.OffHand.Magic.Bonus = 1;
            equipment.OffHand.Magic.Curse = CurseConstants.OppositeEffect;

            var armorClass = GenerateAndAssertArmorClass(12, 12);
            Assert.That(armorClass.ShieldBonus, Is.EqualTo(2));
        }

        [Test]
        public void CursedShieldWithOppositeEffectHasNegativeEffect()
        {
            armorBonuses["shield"] = 1;
            equipment.OffHand = new Item { Name = "shield" };
            equipment.OffHand.ItemType = ItemTypeConstants.Armor;
            equipment.OffHand.Attributes = new[] { AttributeConstants.Shield };
            equipment.OffHand.Magic.Bonus = 2;
            equipment.OffHand.Magic.Curse = CurseConstants.OppositeEffect;

            var armorClass = GenerateAndAssertArmorClass(9, 9);
            Assert.That(armorClass.ShieldBonus, Is.EqualTo(-1));
        }

        [Test]
        public void CursedArmorWithDelusionHasNoBonus()
        {
            armorBonuses["armor"] = 3;
            equipment.Armor = new Item { Name = "armor" };
            equipment.Armor.Magic.Bonus = 1;
            equipment.Armor.Magic.Curse = CurseConstants.Delusion;

            var armorClass = GenerateAndAssertArmorClass(13, 13);
            Assert.That(armorClass.ArmorBonus, Is.EqualTo(3));
        }

        [Test]
        public void CursedArmorWithDelusionLosesToItem()
        {
            armorBonuses["armor"] = 1;
            equipment.Armor = new Item { Name = "armor" };
            equipment.Armor.Magic.Bonus = 2;
            equipment.Armor.Magic.Curse = CurseConstants.Delusion;

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.ArmorClassModifiers, GroupConstants.ArmorBonus))
                .Returns(new[] { "bracers", "other item" });

            var bracers = new Item();
            bracers.Name = "bracers";
            bracers.Magic.Bonus = 2;

            equipment.Treasure.Items = new[] { bracers };

            var armorClass = GenerateAndAssertArmorClass(12, 12);
            Assert.That(armorClass.ArmorBonus, Is.EqualTo(2));
        }

        [Test]
        public void CursedArmorWithDelusionWinsAgainstItem()
        {
            armorBonuses["armor"] = 3;
            equipment.Armor = new Item { Name = "armor" };
            equipment.Armor.Magic.Bonus = 1;
            equipment.Armor.Magic.Curse = CurseConstants.Delusion;

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.ArmorClassModifiers, GroupConstants.ArmorBonus))
                .Returns(new[] { "bracers", "other item" });

            var bracers = new Item();
            bracers.Name = "bracers";
            bracers.Magic.Bonus = 2;

            equipment.Treasure.Items = new[] { bracers };

            var armorClass = GenerateAndAssertArmorClass(13, 13);
            Assert.That(armorClass.ArmorBonus, Is.EqualTo(3));
        }

        [Test]
        public void CursedShieldWithDelusionHasNoBonus()
        {
            armorBonuses["shield"] = 3;
            equipment.OffHand = new Item { Name = "shield" };
            equipment.OffHand.ItemType = ItemTypeConstants.Armor;
            equipment.OffHand.Attributes = new[] { AttributeConstants.Shield };
            equipment.OffHand.Magic.Bonus = 1;
            equipment.OffHand.Magic.Curse = CurseConstants.Delusion;

            var armorClass = GenerateAndAssertArmorClass(13, 13);
            Assert.That(armorClass.ShieldBonus, Is.EqualTo(3));
        }
    }
}