using CharacterGen.Common.Abilities.Feats;
using CharacterGen.Common.CharacterClasses;
using CharacterGen.Common.Races;
using CharacterGen.Generators.Domain.Items;
using CharacterGen.Generators.Items;
using CharacterGen.Selectors;
using CharacterGen.Tables;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using TreasureGen.Common.Items;
using TreasureGen.Generators.Items.Magical;
using TreasureGen.Generators.Items.Mundane;

namespace CharacterGen.Tests.Unit.Generators.Items
{
    [TestFixture]
    public class ArmorGeneratorTests
    {
        private Mock<ICollectionsSelector> mockCollectionsSelector;
        private Mock<IPercentileSelector> mockPercentileSelector;
        private Mock<IMundaneItemGenerator> mockMundaneArmorGenerator;
        private Mock<IMagicalItemGenerator> mockMagicalArmorGenerator;
        private GearGenerator armorGenerator;
        private List<Feat> feats;
        private CharacterClass characterClass;
        private List<String> proficiencyFeats;
        private List<String> proficentArmors;
        private List<String> baseArmorTypes;
        private Item magicalArmor;
        private Race race;

        [SetUp]
        public void Setup()
        {
            mockCollectionsSelector = new Mock<ICollectionsSelector>();
            mockPercentileSelector = new Mock<IPercentileSelector>();
            mockMundaneArmorGenerator = new Mock<IMundaneItemGenerator>();
            mockMagicalArmorGenerator = new Mock<IMagicalItemGenerator>();
            armorGenerator = new ArmorGenerator(mockCollectionsSelector.Object, mockPercentileSelector.Object,
                mockMundaneArmorGenerator.Object, mockMagicalArmorGenerator.Object);
            feats = new List<Feat>();
            characterClass = new CharacterClass();
            proficiencyFeats = new List<String>();
            proficentArmors = new List<String>();
            baseArmorTypes = new List<String>();
            magicalArmor = new Item();
            race = new Race();

            race.Size = "size";
            magicalArmor = CreateArmor("magical armor");
            magicalArmor.IsMagical = true;
            baseArmorTypes.Add("base armor");
            proficentArmors.Remove(magicalArmor.Name);
            proficentArmors.Add(baseArmorTypes[0]);
            proficentArmors.Add("other armor");
            characterClass.Level = 9266;
            feats.Add(new Feat { Name = "light proficiency" });
            feats.Add(new Feat { Name = "other feat" });
            proficiencyFeats.Add(feats[0].Name);

            mockPercentileSelector.Setup(s => s.SelectFrom("Level9266Power")).Returns("power");
            mockMagicalArmorGenerator.Setup(g => g.GenerateAtPower("power")).Returns(magicalArmor);
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.ItemGroups, It.IsAny<String>()))
                .Returns((String table, String name) => new[] { name });
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.FeatGroups, ItemTypeConstants.Armor + GroupConstants.Proficiency))
                .Returns(proficiencyFeats);
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.ItemGroups, magicalArmor.Name))
                .Returns(baseArmorTypes);
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.ItemGroups, feats[0].Name))
                .Returns(proficentArmors);
        }

        [Test]
        public void GenerateNoArmor()
        {
            feats.Remove(feats[0]);
            var armor = armorGenerator.GenerateFrom(feats, characterClass, race);
            Assert.That(armor, Is.Null);
        }

        [Test]
        public void GenerateMundaneArmor()
        {
            var mundaneArmor = CreateArmor("mundane armor");
            mockPercentileSelector.Setup(s => s.SelectFrom("Level9266Power")).Returns(PowerConstants.Mundane);
            mockMundaneArmorGenerator.Setup(g => g.Generate()).Returns(mundaneArmor);

            var armor = armorGenerator.GenerateFrom(feats, characterClass, race);
            Assert.That(armor, Is.EqualTo(mundaneArmor));
        }

        private Item CreateArmor(String name)
        {
            var armor = new Item();
            armor.Name = name;
            armor.ItemType = ItemTypeConstants.Armor;
            armor.Traits.Add(race.Size);

            proficentArmors.Add(name);

            return armor;
        }

        [Test]
        public void IfCannotWearMundaneArmor_Regenerate()
        {
            var mundaneArmor = CreateArmor("mundane armor");
            var wrongMundaneArmor = CreateArmor("wrong mundane armor");
            mockPercentileSelector.Setup(s => s.SelectFrom("Level9266Power")).Returns(PowerConstants.Mundane);
            mockMundaneArmorGenerator.SetupSequence(g => g.Generate()).Returns(wrongMundaneArmor).Returns(mundaneArmor);

            proficentArmors.Remove(wrongMundaneArmor.Name);

            var armor = armorGenerator.GenerateFrom(feats, characterClass, race);
            Assert.That(armor, Is.EqualTo(mundaneArmor));
        }

        [Test]
        public void IfNotMundaneArmor_Regenerate()
        {
            var mundaneArmor = CreateArmor("mundane armor");
            var wrongMundaneArmor = CreateArmor("wrong mundane armor");
            mockPercentileSelector.Setup(s => s.SelectFrom("Level9266Power")).Returns(PowerConstants.Mundane);
            mockMundaneArmorGenerator.SetupSequence(g => g.Generate()).Returns(wrongMundaneArmor).Returns(mundaneArmor);

            wrongMundaneArmor.ItemType = "not armor";

            var armor = armorGenerator.GenerateFrom(feats, characterClass, race);
            Assert.That(armor, Is.EqualTo(mundaneArmor));
        }

        [Test]
        public void UseCumulativeArmorProficiencies()
        {
            feats.Add(new Feat { Name = "heavy proficiency" });

            var otherArmor = CreateArmor("other armor");
            proficiencyFeats.Add("heavy proficiency");

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.ItemGroups, feats[2].Name))
                .Returns(new[] { otherArmor.Name });

            mockMagicalArmorGenerator.SetupSequence(g => g.GenerateAtPower("power"))
                .Returns(otherArmor).Returns(magicalArmor);

            var armor = armorGenerator.GenerateFrom(feats, characterClass, race);
            Assert.That(armor, Is.EqualTo(otherArmor));
        }

        [Test]
        public void GenerateMagicalArmor()
        {
            var armor = armorGenerator.GenerateFrom(feats, characterClass, race);
            Assert.That(armor, Is.EqualTo(magicalArmor));
        }

        [Test]
        public void IfCannotWearMagicalArmor_Regenerate()
        {
            var wrongMagicalArmor = CreateArmor("wrong magical armor");
            mockMagicalArmorGenerator.SetupSequence(g => g.GenerateAtPower("power"))
                .Returns(wrongMagicalArmor).Returns(magicalArmor);

            proficentArmors.Remove(wrongMagicalArmor.Name);

            var armor = armorGenerator.GenerateFrom(feats, characterClass, race);
            Assert.That(armor, Is.EqualTo(magicalArmor));
        }

        [Test]
        public void IfMagicalArmorIsNotArmor_Regenerate()
        {
            var wrongMagicalArmor = CreateArmor("wrong magical armor");
            mockMagicalArmorGenerator.SetupSequence(g => g.GenerateAtPower("power"))
                .Returns(wrongMagicalArmor).Returns(magicalArmor);

            wrongMagicalArmor.ItemType = "not armor";

            var armor = armorGenerator.GenerateFrom(feats, characterClass, race);
            Assert.That(armor, Is.EqualTo(magicalArmor));
        }

        [Test]
        public void IfMagicalArmorContainsMetalAndClassIsDruid_Regenerate()
        {
            characterClass.ClassName = CharacterClassConstants.Druid;
            var wrongMagicalArmor = CreateArmor("wrong magical armor");
            mockMagicalArmorGenerator.SetupSequence(g => g.GenerateAtPower("power"))
                .Returns(wrongMagicalArmor).Returns(magicalArmor);

            wrongMagicalArmor.Attributes = wrongMagicalArmor.Attributes.Union(new[] { AttributeConstants.Metal });

            var armor = armorGenerator.GenerateFrom(feats, characterClass, race);
            Assert.That(armor, Is.EqualTo(magicalArmor));
        }

        [Test]
        public void IfMagicalArmorContainsMetalAndClassIsNotDruid_Keep()
        {
            var wrongMagicalArmor = CreateArmor("wrong magical armor");
            mockMagicalArmorGenerator.SetupSequence(g => g.GenerateAtPower("power"))
                .Returns(magicalArmor).Returns(wrongMagicalArmor);

            magicalArmor.Attributes = wrongMagicalArmor.Attributes.Union(new[] { AttributeConstants.Metal });

            var armor = armorGenerator.GenerateFrom(feats, characterClass, race);
            Assert.That(armor, Is.EqualTo(magicalArmor));
        }

        [Test]
        public void IfMagicalArmorDoesNotContainMetalAndClassIsDruid_Keep()
        {
            characterClass.ClassName = CharacterClassConstants.Druid;
            var wrongMagicalArmor = CreateArmor("wrong magical armor");
            mockMagicalArmorGenerator.SetupSequence(g => g.GenerateAtPower("power"))
                .Returns(magicalArmor).Returns(wrongMagicalArmor);

            magicalArmor.Attributes = wrongMagicalArmor.Attributes.Union(new[] { "other attribute" });

            var armor = armorGenerator.GenerateFrom(feats, characterClass, race);
            Assert.That(armor, Is.EqualTo(magicalArmor));
        }

        [Test]
        public void IfMagicalArmorDoesNotContainMetalAndClassIsNotDruid_Keep()
        {
            var wrongMagicalArmor = CreateArmor("wrong magical armor");
            mockMagicalArmorGenerator.SetupSequence(g => g.GenerateAtPower("power"))
                .Returns(magicalArmor).Returns(wrongMagicalArmor);

            magicalArmor.Attributes = wrongMagicalArmor.Attributes.Union(new[] { "other attribute" });

            var armor = armorGenerator.GenerateFrom(feats, characterClass, race);
            Assert.That(armor, Is.EqualTo(magicalArmor));
        }

        [Test]
        public void MundaneArmorMustFitCharacter()
        {
            var mundaneArmor = CreateArmor("mundane armor");
            var wrongMundaneArmor = CreateArmor("wrong mundane armor");
            var otherWrongMundaneArmor = CreateArmor("other wrong mundane armor");
            mockPercentileSelector.Setup(s => s.SelectFrom("Level9266Power")).Returns(PowerConstants.Mundane);
            mockMundaneArmorGenerator.SetupSequence(g => g.Generate())
                .Returns(wrongMundaneArmor).Returns(otherWrongMundaneArmor).Returns(mundaneArmor);

            wrongMundaneArmor.Traits.Clear();
            wrongMundaneArmor.Traits.Add("bigger size");
            otherWrongMundaneArmor.Traits.Clear();
            otherWrongMundaneArmor.Traits.Add("smaller size");

            var armor = armorGenerator.GenerateFrom(feats, characterClass, race);
            Assert.That(armor, Is.EqualTo(mundaneArmor));
        }

        [Test]
        public void MagicalArmorDoesNotHaveToFitCharacter()
        {
            var wrongMagicalArmor = CreateArmor("wrong magical armor");
            mockMagicalArmorGenerator.SetupSequence(g => g.GenerateAtPower("power"))
                .Returns(magicalArmor).Returns(wrongMagicalArmor);

            race.Size = "other size";

            var armor = armorGenerator.GenerateFrom(feats, characterClass, race);
            Assert.That(armor, Is.EqualTo(magicalArmor));
        }
    }
}
