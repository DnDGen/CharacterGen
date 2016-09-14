using CharacterGen.Abilities.Feats;
using CharacterGen.Abilities.Stats;
using CharacterGen.Alignments;
using CharacterGen.CharacterClasses;
using CharacterGen.Domain.Generators.Magics;
using CharacterGen.Domain.Selectors.Collections;
using CharacterGen.Domain.Tables;
using CharacterGen.Items;
using CharacterGen.Magics;
using CharacterGen.Races;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using TreasureGen.Items;

namespace CharacterGen.Tests.Unit.Generators.Magics
{
    [TestFixture]
    public class MagicGeneratorTests
    {
        private Mock<ISpellsGenerator> mockSpellsGenerator;
        private Mock<IAnimalGenerator> mockAnimalGenerator;
        private Mock<ICollectionsSelector> mockCollectionsSelector;
        private Mock<IAdjustmentsSelector> mockAdjustmentsSelector;
        private IMagicGenerator magicGenerator;
        private CharacterClass characterClass;
        private List<Feat> feats;
        private Alignment alignment;
        private Race race;
        private Dictionary<string, Stat> stats;
        private Equipment equipment;
        private Dictionary<string, int> arcaneSpellFailures;
        private List<string> classesThatPrepareSpells;

        [SetUp]
        public void Setup()
        {
            mockSpellsGenerator = new Mock<ISpellsGenerator>();
            mockAnimalGenerator = new Mock<IAnimalGenerator>();
            mockCollectionsSelector = new Mock<ICollectionsSelector>();
            mockAdjustmentsSelector = new Mock<IAdjustmentsSelector>();
            magicGenerator = new MagicGenerator(mockSpellsGenerator.Object, mockAnimalGenerator.Object, mockCollectionsSelector.Object, mockAdjustmentsSelector.Object);
            characterClass = new CharacterClass();
            feats = new List<Feat>();
            alignment = new Alignment();
            race = new Race();
            stats = new Dictionary<string, Stat>();
            equipment = new Equipment();
            arcaneSpellFailures = new Dictionary<string, int>();
            classesThatPrepareSpells = new List<string>();

            characterClass.Name = "class name";
            classesThatPrepareSpells.Add(characterClass.Name);
            classesThatPrepareSpells.Add("other class");

            mockAdjustmentsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Adjustments.ArcaneSpellFailures)).Returns(arcaneSpellFailures);
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.ClassNameGroups, GroupConstants.PreparesSpells)).Returns(classesThatPrepareSpells);
        }

        [Test]
        public void GenerateMagic()
        {
            var magic = magicGenerator.GenerateWith(alignment, characterClass, race, stats, feats, equipment);
            Assert.That(magic, Is.Not.Null);
        }

        [Test]
        public void GenerateSpells()
        {
            var spellsPerDay = new List<SpellQuantity>();
            var knownSpells = new List<Spell>();
            var preparedSpells = new List<Spell>();

            mockSpellsGenerator.Setup(g => g.GeneratePerDay(characterClass, stats)).Returns(spellsPerDay);
            mockSpellsGenerator.Setup(g => g.GenerateKnown(characterClass, stats)).Returns(knownSpells);
            mockSpellsGenerator.Setup(g => g.GeneratePrepared(characterClass, knownSpells, spellsPerDay)).Returns(preparedSpells);

            var magic = magicGenerator.GenerateWith(alignment, characterClass, race, stats, feats, equipment);
            Assert.That(magic.SpellsPerDay, Is.EqualTo(spellsPerDay));
            Assert.That(magic.KnownSpells, Is.EqualTo(knownSpells));
            Assert.That(magic.PreparedSpells, Is.EqualTo(preparedSpells));
        }

        [Test]
        public void DoNotGeneratePreparedSpellsIfClassDoesNotPrepareSpells()
        {
            classesThatPrepareSpells.Remove(characterClass.Name);

            var spellsPerDay = new List<SpellQuantity> { new SpellQuantity() };
            var knownSpells = new List<Spell> { new Spell() };
            var preparedSpells = new List<Spell> { new Spell() };

            mockSpellsGenerator.Setup(g => g.GeneratePerDay(characterClass, stats)).Returns(spellsPerDay);
            mockSpellsGenerator.Setup(g => g.GenerateKnown(characterClass, stats)).Returns(knownSpells);
            mockSpellsGenerator.Setup(g => g.GeneratePrepared(characterClass, knownSpells, spellsPerDay)).Returns(preparedSpells);

            var magic = magicGenerator.GenerateWith(alignment, characterClass, race, stats, feats, equipment);
            Assert.That(magic.SpellsPerDay, Is.EqualTo(spellsPerDay));
            Assert.That(magic.KnownSpells, Is.EqualTo(knownSpells));
            Assert.That(magic.PreparedSpells, Is.Not.EqualTo(preparedSpells));
            Assert.That(magic.PreparedSpells, Is.Empty);
        }

        [Test]
        public void DoNotGenerateAnimal()
        {
            mockAnimalGenerator.Setup(g => g.GenerateFrom(alignment, characterClass, race, feats)).Returns(string.Empty);

            var magic = magicGenerator.GenerateWith(alignment, characterClass, race, stats, feats, equipment);
            Assert.That(magic.Animal, Is.Empty);
        }

        [Test]
        public void GenerateAnimal()
        {
            mockAnimalGenerator.Setup(g => g.GenerateFrom(alignment, characterClass, race, feats)).Returns("animal");

            var magic = magicGenerator.GenerateWith(alignment, characterClass, race, stats, feats, equipment);
            Assert.That(magic.Animal, Is.EqualTo("animal"));
        }

        [Test]
        public void GenerateNoArcaneSpellFailureIfNotArcaneSpellcaster()
        {
            equipment.Armor = new Item();
            equipment.Armor.Name = "armor";
            arcaneSpellFailures[equipment.Armor.Name] = 9266;

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.ClassNameGroups, SpellConstants.Sources.Arcane)).Returns(new[] { "other class", CharacterClassConstants.Bard });

            var magic = magicGenerator.GenerateWith(alignment, characterClass, race, stats, feats, equipment);
            Assert.That(magic.ArcaneSpellFailure, Is.EqualTo(0));
        }

        [Test]
        public void GenerateNoArcaneSpellFailureIfBardWithLightArmor()
        {
            characterClass.Name = CharacterClassConstants.Bard;
            equipment.Armor = new Item();
            equipment.Armor.Name = "armor";
            arcaneSpellFailures[equipment.Armor.Name] = 9266;

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.ItemGroups, FeatConstants.LightArmorProficiency)).Returns(new[] { equipment.Armor.Name });

            var magic = magicGenerator.GenerateWith(alignment, characterClass, race, stats, feats, equipment);
            Assert.That(magic.ArcaneSpellFailure, Is.EqualTo(0));
        }

        [Test]
        public void GenerateArcaneSpellFailureIfArcaneSpellcasterWithLightArmor()
        {
            equipment.Armor = new Item();
            equipment.Armor.Name = "armor";
            arcaneSpellFailures[equipment.Armor.Name] = 9266;

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.ClassNameGroups, SpellConstants.Sources.Arcane)).Returns(new[] { characterClass.Name, CharacterClassConstants.Bard });
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.ItemGroups, FeatConstants.LightArmorProficiency)).Returns(new[] { equipment.Armor.Name });

            var magic = magicGenerator.GenerateWith(alignment, characterClass, race, stats, feats, equipment);
            Assert.That(magic.ArcaneSpellFailure, Is.EqualTo(9266));
        }

        [Test]
        public void GenerateNoArcaneSpellFailureIfNotBardOrArcaneSpellcasterWithLightArmor()
        {
            equipment.Armor = new Item();
            equipment.Armor.Name = "armor";
            arcaneSpellFailures[equipment.Armor.Name] = 9266;

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.ItemGroups, FeatConstants.LightArmorProficiency)).Returns(new[] { equipment.Armor.Name });

            var magic = magicGenerator.GenerateWith(alignment, characterClass, race, stats, feats, equipment);
            Assert.That(magic.ArcaneSpellFailure, Is.EqualTo(0));
        }

        [Test]
        public void GenerateNoArcaneSpellFailureIfNoArmorOrOffHand()
        {
            arcaneSpellFailures[string.Empty] = 9266;

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.ClassNameGroups, SpellConstants.Sources.Arcane)).Returns(new[] { characterClass.Name });

            var magic = magicGenerator.GenerateWith(alignment, characterClass, race, stats, feats, equipment);
            Assert.That(magic.ArcaneSpellFailure, Is.EqualTo(0));
        }

        [Test]
        public void GenerateArcaneSpellFailureIfArcaneSpellcasterAndHasArmor()
        {
            equipment.Armor = new Item();
            equipment.Armor.Name = "armor";
            arcaneSpellFailures[equipment.Armor.Name] = 9266;

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.ClassNameGroups, SpellConstants.Sources.Arcane)).Returns(new[] { characterClass.Name });

            var magic = magicGenerator.GenerateWith(alignment, characterClass, race, stats, feats, equipment);
            Assert.That(magic.ArcaneSpellFailure, Is.EqualTo(9266));
        }

        [Test]
        public void GenerateArcaneSpellFailureIfArcaneSpellcasterAndHasShield()
        {
            equipment.OffHand = new Item();
            equipment.OffHand.Name = "shield";
            equipment.OffHand.ItemType = ItemTypeConstants.Armor;
            equipment.OffHand.Attributes = new[] { AttributeConstants.Shield };
            arcaneSpellFailures[equipment.OffHand.Name] = 9266;

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.ClassNameGroups, SpellConstants.Sources.Arcane)).Returns(new[] { characterClass.Name });

            var magic = magicGenerator.GenerateWith(alignment, characterClass, race, stats, feats, equipment);
            Assert.That(magic.ArcaneSpellFailure, Is.EqualTo(9266));
        }

        [Test]
        public void GenerateNoArcaneSpellFailureIfArcaneSpellcasterAndNotShieldInOffHand()
        {
            equipment.OffHand = new Item();
            equipment.OffHand.Name = "weapon";
            arcaneSpellFailures[equipment.OffHand.Name] = 9266;

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.ClassNameGroups, SpellConstants.Sources.Arcane)).Returns(new[] { characterClass.Name });

            var magic = magicGenerator.GenerateWith(alignment, characterClass, race, stats, feats, equipment);
            Assert.That(magic.ArcaneSpellFailure, Is.EqualTo(0));
        }

        [Test]
        public void GenerateArcaneSpellFailureIfArcaneSpellcasterAndHasArmorAndShield()
        {
            equipment.Armor = new Item();
            equipment.Armor.Name = "armor";

            equipment.OffHand = new Item();
            equipment.OffHand.Name = "shield";
            equipment.OffHand.ItemType = ItemTypeConstants.Armor;
            equipment.OffHand.Attributes = new[] { AttributeConstants.Shield };

            arcaneSpellFailures[equipment.Armor.Name] = 9266;
            arcaneSpellFailures[equipment.OffHand.Name] = 90210;

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.ClassNameGroups, SpellConstants.Sources.Arcane)).Returns(new[] { characterClass.Name });

            var magic = magicGenerator.GenerateWith(alignment, characterClass, race, stats, feats, equipment);
            Assert.That(magic.ArcaneSpellFailure, Is.EqualTo(9266 + 90210));
        }

        [Test]
        public void GenerateArcaneSpellFailureIfBardWithNonLightArmor()
        {
            characterClass.Name = CharacterClassConstants.Bard;
            equipment.Armor = new Item();
            equipment.Armor.Name = "armor";
            arcaneSpellFailures[equipment.Armor.Name] = 9266;

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.ClassNameGroups, SpellConstants.Sources.Arcane)).Returns(new[] { characterClass.Name });
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.ItemGroups, FeatConstants.LightArmorProficiency)).Returns(new[] { "other armor" });

            var magic = magicGenerator.GenerateWith(alignment, characterClass, race, stats, feats, equipment);
            Assert.That(magic.ArcaneSpellFailure, Is.EqualTo(9266));
        }

        [Test]
        public void GenerateArcaneSpellFailureIfBardWithShield()
        {
            characterClass.Name = CharacterClassConstants.Bard;
            equipment.OffHand = new Item();
            equipment.OffHand.Name = "shield";
            equipment.OffHand.ItemType = ItemTypeConstants.Armor;
            equipment.OffHand.Attributes = new[] { AttributeConstants.Shield };

            arcaneSpellFailures[equipment.OffHand.Name] = 9266;

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.ClassNameGroups, SpellConstants.Sources.Arcane)).Returns(new[] { characterClass.Name });
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.ItemGroups, FeatConstants.LightArmorProficiency)).Returns(new[] { "other armor" });

            var magic = magicGenerator.GenerateWith(alignment, characterClass, race, stats, feats, equipment);
            Assert.That(magic.ArcaneSpellFailure, Is.EqualTo(9266));
        }

        [Test]
        public void GenerateArcaneSpellFailureIfBardWithShieldAndLightArmor()
        {
            characterClass.Name = CharacterClassConstants.Bard;
            equipment.OffHand = new Item();
            equipment.OffHand.Name = "shield";
            equipment.OffHand.ItemType = ItemTypeConstants.Armor;
            equipment.OffHand.Attributes = new[] { AttributeConstants.Shield };
            equipment.Armor = new Item();
            equipment.Armor.Name = "armor";

            arcaneSpellFailures[equipment.Armor.Name] = 9266;
            arcaneSpellFailures[equipment.OffHand.Name] = 90210;

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.ClassNameGroups, SpellConstants.Sources.Arcane)).Returns(new[] { characterClass.Name });
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.ItemGroups, FeatConstants.LightArmorProficiency)).Returns(new[] { equipment.Armor.Name });

            var magic = magicGenerator.GenerateWith(alignment, characterClass, race, stats, feats, equipment);
            Assert.That(magic.ArcaneSpellFailure, Is.EqualTo(90210));
        }

        [Test]
        public void GenerateArcaneSpellFailureIfBardWithNonLightArmorAndShield()
        {
            characterClass.Name = CharacterClassConstants.Bard;
            equipment.Armor = new Item();
            equipment.Armor.Name = "armor";

            equipment.OffHand = new Item();
            equipment.OffHand.Name = "shield";
            equipment.OffHand.ItemType = ItemTypeConstants.Armor;
            equipment.OffHand.Attributes = new[] { AttributeConstants.Shield };

            arcaneSpellFailures[equipment.Armor.Name] = 9266;
            arcaneSpellFailures[equipment.OffHand.Name] = 90210;

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.ClassNameGroups, SpellConstants.Sources.Arcane)).Returns(new[] { characterClass.Name });
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.ItemGroups, FeatConstants.LightArmorProficiency)).Returns(new[] { "other armor" });

            var magic = magicGenerator.GenerateWith(alignment, characterClass, race, stats, feats, equipment);
            Assert.That(magic.ArcaneSpellFailure, Is.EqualTo(9266 + 90210));
        }

        [Test]
        public void MithralArmorDecreasesArcaneSpellFailureBy10()
        {
            equipment.Armor = new Item();
            equipment.Armor.Name = "armor";
            equipment.Armor.Traits.Add(TraitConstants.SpecialMaterials.Mithral);
            arcaneSpellFailures[equipment.Armor.Name] = 9266;

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.ClassNameGroups, SpellConstants.Sources.Arcane)).Returns(new[] { characterClass.Name });

            var magic = magicGenerator.GenerateWith(alignment, characterClass, race, stats, feats, equipment);
            Assert.That(magic.ArcaneSpellFailure, Is.EqualTo(9256));
        }

        [Test]
        public void MithralArmorCannotDecreasesArcaneSpellFailureBelow0()
        {
            equipment.Armor = new Item();
            equipment.Armor.Name = "armor";
            equipment.Armor.Traits.Add(TraitConstants.SpecialMaterials.Mithral);
            arcaneSpellFailures[equipment.Armor.Name] = 5;

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.ClassNameGroups, SpellConstants.Sources.Arcane)).Returns(new[] { characterClass.Name });

            var magic = magicGenerator.GenerateWith(alignment, characterClass, race, stats, feats, equipment);
            Assert.That(magic.ArcaneSpellFailure, Is.EqualTo(0));
        }

        [Test]
        public void MithralShieldDecreasesArcaneSpellFailureBy10()
        {
            equipment.OffHand = new Item();
            equipment.OffHand.Name = "shield";
            equipment.OffHand.ItemType = ItemTypeConstants.Armor;
            equipment.OffHand.Attributes = new[] { AttributeConstants.Shield };
            equipment.OffHand.Traits.Add(TraitConstants.SpecialMaterials.Mithral);
            arcaneSpellFailures[equipment.OffHand.Name] = 9266;

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.ClassNameGroups, SpellConstants.Sources.Arcane)).Returns(new[] { characterClass.Name });

            var magic = magicGenerator.GenerateWith(alignment, characterClass, race, stats, feats, equipment);
            Assert.That(magic.ArcaneSpellFailure, Is.EqualTo(9256));
        }

        [Test]
        public void MithralShieldCannotDecreasesArcaneSpellFailureBelow0()
        {
            equipment.OffHand = new Item();
            equipment.OffHand.Name = "shield";
            equipment.OffHand.ItemType = ItemTypeConstants.Armor;
            equipment.OffHand.Attributes = new[] { AttributeConstants.Shield };
            equipment.OffHand.Traits.Add(TraitConstants.SpecialMaterials.Mithral);
            arcaneSpellFailures[equipment.OffHand.Name] = 5;

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.ClassNameGroups, SpellConstants.Sources.Arcane)).Returns(new[] { characterClass.Name });

            var magic = magicGenerator.GenerateWith(alignment, characterClass, race, stats, feats, equipment);
            Assert.That(magic.ArcaneSpellFailure, Is.EqualTo(0));
        }
    }
}
