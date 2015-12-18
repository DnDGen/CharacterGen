using CharacterGen.Common.Abilities.Feats;
using CharacterGen.Common.Abilities.Stats;
using CharacterGen.Common.Alignments;
using CharacterGen.Common.CharacterClasses;
using CharacterGen.Common.Items;
using CharacterGen.Common.Magics;
using CharacterGen.Common.Races;
using CharacterGen.Generators.Domain.Magics;
using CharacterGen.Generators.Magics;
using CharacterGen.Selectors;
using CharacterGen.Tables;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using TreasureGen.Common.Items;

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

            characterClass.ClassName = "class name";

            mockAdjustmentsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Adjustments.ArcaneSpellFailures)).Returns(arcaneSpellFailures);
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
            var spells = new List<Spells>();
            mockSpellsGenerator.Setup(g => g.GenerateFrom(characterClass, stats)).Returns(spells);

            var magic = magicGenerator.GenerateWith(alignment, characterClass, race, stats, feats, equipment);
            Assert.That(magic.SpellsPerDay, Is.EqualTo(spells));
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

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.ClassNameGroups, SpellConstants.Arcane)).Returns(new[] { "other class", CharacterClassConstants.Bard });

            var magic = magicGenerator.GenerateWith(alignment, characterClass, race, stats, feats, equipment);
            Assert.That(magic.ArcaneSpellFailure, Is.EqualTo(0));
        }

        [Test]
        public void GenerateNoArcaneSpellFailureIfBardWithLightArmor()
        {
            characterClass.ClassName = CharacterClassConstants.Bard;
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

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.ClassNameGroups, SpellConstants.Arcane)).Returns(new[] { characterClass.ClassName, CharacterClassConstants.Bard });
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

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.ClassNameGroups, SpellConstants.Arcane)).Returns(new[] { characterClass.ClassName });

            var magic = magicGenerator.GenerateWith(alignment, characterClass, race, stats, feats, equipment);
            Assert.That(magic.ArcaneSpellFailure, Is.EqualTo(0));
        }

        [Test]
        public void GenerateArcaneSpellFailureIfArcaneSpellcasterAndHasArmor()
        {
            equipment.Armor = new Item();
            equipment.Armor.Name = "armor";
            arcaneSpellFailures[equipment.Armor.Name] = 9266;

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.ClassNameGroups, SpellConstants.Arcane)).Returns(new[] { characterClass.ClassName });

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

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.ClassNameGroups, SpellConstants.Arcane)).Returns(new[] { characterClass.ClassName });

            var magic = magicGenerator.GenerateWith(alignment, characterClass, race, stats, feats, equipment);
            Assert.That(magic.ArcaneSpellFailure, Is.EqualTo(9266));
        }

        [Test]
        public void GenerateNoArcaneSpellFailureIfArcaneSpellcasterAndNotShieldInOffHand()
        {
            equipment.OffHand = new Item();
            equipment.OffHand.Name = "weapon";
            arcaneSpellFailures[equipment.OffHand.Name] = 9266;

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.ClassNameGroups, SpellConstants.Arcane)).Returns(new[] { characterClass.ClassName });

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

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.ClassNameGroups, SpellConstants.Arcane)).Returns(new[] { characterClass.ClassName });

            var magic = magicGenerator.GenerateWith(alignment, characterClass, race, stats, feats, equipment);
            Assert.That(magic.ArcaneSpellFailure, Is.EqualTo(9266 + 90210));
        }

        [Test]
        public void GenerateArcaneSpellFailureIfBardWithNonLightArmor()
        {
            characterClass.ClassName = CharacterClassConstants.Bard;
            equipment.Armor = new Item();
            equipment.Armor.Name = "armor";
            arcaneSpellFailures[equipment.Armor.Name] = 9266;

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.ClassNameGroups, SpellConstants.Arcane)).Returns(new[] { characterClass.ClassName });
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.ItemGroups, FeatConstants.LightArmorProficiency)).Returns(new[] { "other armor" });

            var magic = magicGenerator.GenerateWith(alignment, characterClass, race, stats, feats, equipment);
            Assert.That(magic.ArcaneSpellFailure, Is.EqualTo(9266));
        }

        [Test]
        public void GenerateArcaneSpellFailureIfBardWithShield()
        {
            characterClass.ClassName = CharacterClassConstants.Bard;
            equipment.OffHand = new Item();
            equipment.OffHand.Name = "shield";
            equipment.OffHand.ItemType = ItemTypeConstants.Armor;
            equipment.OffHand.Attributes = new[] { AttributeConstants.Shield };

            arcaneSpellFailures[equipment.OffHand.Name] = 9266;

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.ClassNameGroups, SpellConstants.Arcane)).Returns(new[] { characterClass.ClassName });
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.ItemGroups, FeatConstants.LightArmorProficiency)).Returns(new[] { "other armor" });

            var magic = magicGenerator.GenerateWith(alignment, characterClass, race, stats, feats, equipment);
            Assert.That(magic.ArcaneSpellFailure, Is.EqualTo(9266));
        }

        [Test]
        public void GenerateArcaneSpellFailureIfBardWithShieldAndLightArmor()
        {
            characterClass.ClassName = CharacterClassConstants.Bard;
            equipment.OffHand = new Item();
            equipment.OffHand.Name = "shield";
            equipment.OffHand.ItemType = ItemTypeConstants.Armor;
            equipment.OffHand.Attributes = new[] { AttributeConstants.Shield };
            equipment.Armor = new Item();
            equipment.Armor.Name = "armor";

            arcaneSpellFailures[equipment.Armor.Name] = 9266;
            arcaneSpellFailures[equipment.OffHand.Name] = 90210;

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.ClassNameGroups, SpellConstants.Arcane)).Returns(new[] { characterClass.ClassName });
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.ItemGroups, FeatConstants.LightArmorProficiency)).Returns(new[] { equipment.Armor.Name });

            var magic = magicGenerator.GenerateWith(alignment, characterClass, race, stats, feats, equipment);
            Assert.That(magic.ArcaneSpellFailure, Is.EqualTo(90210));
        }

        [Test]
        public void GenerateArcaneSpellFailureIfBardWithNonLightArmorAndShield()
        {
            characterClass.ClassName = CharacterClassConstants.Bard;
            equipment.Armor = new Item();
            equipment.Armor.Name = "armor";

            equipment.OffHand = new Item();
            equipment.OffHand.Name = "shield";
            equipment.OffHand.ItemType = ItemTypeConstants.Armor;
            equipment.OffHand.Attributes = new[] { AttributeConstants.Shield };

            arcaneSpellFailures[equipment.Armor.Name] = 9266;
            arcaneSpellFailures[equipment.OffHand.Name] = 90210;

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.ClassNameGroups, SpellConstants.Arcane)).Returns(new[] { characterClass.ClassName });
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.ItemGroups, FeatConstants.LightArmorProficiency)).Returns(new[] { "other armor" });

            var magic = magicGenerator.GenerateWith(alignment, characterClass, race, stats, feats, equipment);
            Assert.That(magic.ArcaneSpellFailure, Is.EqualTo(9266 + 90210));
        }

        [Test]
        public void MithralArmorDecreasesArcaneSpellFailureBy10()
        {
            equipment.Armor = new Item();
            equipment.Armor.Name = "armor";
            equipment.Armor.Traits.Add(TraitConstants.Mithral);
            arcaneSpellFailures[equipment.Armor.Name] = 9266;

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.ClassNameGroups, SpellConstants.Arcane)).Returns(new[] { characterClass.ClassName });

            var magic = magicGenerator.GenerateWith(alignment, characterClass, race, stats, feats, equipment);
            Assert.That(magic.ArcaneSpellFailure, Is.EqualTo(9256));
        }

        [Test]
        public void MithralArmorCannotDecreasesArcaneSpellFailureBelow0()
        {
            equipment.Armor = new Item();
            equipment.Armor.Name = "armor";
            equipment.Armor.Traits.Add(TraitConstants.Mithral);
            arcaneSpellFailures[equipment.Armor.Name] = 5;

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.ClassNameGroups, SpellConstants.Arcane)).Returns(new[] { characterClass.ClassName });

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
            equipment.OffHand.Traits.Add(TraitConstants.Mithral);
            arcaneSpellFailures[equipment.OffHand.Name] = 9266;

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.ClassNameGroups, SpellConstants.Arcane)).Returns(new[] { characterClass.ClassName });

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
            equipment.OffHand.Traits.Add(TraitConstants.Mithral);
            arcaneSpellFailures[equipment.OffHand.Name] = 5;

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.ClassNameGroups, SpellConstants.Arcane)).Returns(new[] { characterClass.ClassName });

            var magic = magicGenerator.GenerateWith(alignment, characterClass, race, stats, feats, equipment);
            Assert.That(magic.ArcaneSpellFailure, Is.EqualTo(0));
        }
    }
}
