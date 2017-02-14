using CharacterGen.Abilities.Feats;
using CharacterGen.CharacterClasses;
using CharacterGen.Domain.Generators;
using CharacterGen.Domain.Generators.Items;
using CharacterGen.Domain.Selectors.Collections;
using CharacterGen.Domain.Selectors.Percentiles;
using CharacterGen.Domain.Tables;
using CharacterGen.Races;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using TreasureGen.Items;
using TreasureGen.Items.Magical;
using TreasureGen.Items.Mundane;

namespace CharacterGen.Tests.Unit.Generators.Items
{
    [TestFixture]
    public class ArmorGeneratorTests
    {
        private Mock<ICollectionsSelector> mockCollectionsSelector;
        private Mock<IPercentileSelector> mockPercentileSelector;
        private Mock<MundaneItemGenerator> mockMundaneArmorGenerator;
        private Mock<MagicalItemGenerator> mockMagicalArmorGenerator;
        private Generator generator;
        private IArmorGenerator armorGenerator;
        private List<Feat> feats;
        private CharacterClass characterClass;
        private List<string> armorProficiencyFeats;
        private List<string> shieldProficiencyFeats;
        private List<string> proficientArmors;
        private List<string> proficientShields;
        private List<string> baseArmorTypes;
        private List<string> baseShieldTypes;
        private Item magicalArmor;
        private Item magicalShield;
        private Race race;

        [SetUp]
        public void Setup()
        {
            mockCollectionsSelector = new Mock<ICollectionsSelector>();
            mockPercentileSelector = new Mock<IPercentileSelector>();
            mockMundaneArmorGenerator = new Mock<MundaneItemGenerator>();
            mockMagicalArmorGenerator = new Mock<MagicalItemGenerator>();
            generator = new ConfigurableIterationGenerator(3);
            armorGenerator = new ArmorGenerator(mockCollectionsSelector.Object, mockPercentileSelector.Object, mockMundaneArmorGenerator.Object, mockMagicalArmorGenerator.Object, generator);
            feats = new List<Feat>();
            characterClass = new CharacterClass();
            armorProficiencyFeats = new List<string>();
            shieldProficiencyFeats = new List<string>();
            proficientArmors = new List<string>();
            proficientShields = new List<string>();
            baseArmorTypes = new List<string>();
            baseShieldTypes = new List<string>();
            magicalArmor = new Item();
            race = new Race();

            race.Size = "size";
            magicalArmor = CreateArmor("magical armor");
            magicalArmor.IsMagical = true;
            magicalShield = CreateShield("magical shield");
            magicalShield.IsMagical = true;
            baseArmorTypes.Add("base armor");
            baseShieldTypes.Add("base shield");
            proficientArmors.Remove(magicalArmor.Name);
            proficientArmors.Add(baseArmorTypes[0]);
            proficientArmors.Add("other armor");
            proficientShields.Remove(magicalShield.Name);
            proficientShields.Add(baseShieldTypes[0]);
            proficientShields.Add("other shield");
            characterClass.Level = 9266;
            feats.Add(new Feat { Name = "light proficiency" });
            feats.Add(new Feat { Name = "shield proficiency" });
            feats.Add(new Feat { Name = "other feat" });
            armorProficiencyFeats.Add(feats[0].Name);
            shieldProficiencyFeats.Add(feats[1].Name);

            mockPercentileSelector.Setup(s => s.SelectFrom("Level9266Power")).Returns("power");
            mockMagicalArmorGenerator.Setup(g => g.GenerateAtPower("power")).Returns(magicalArmor);
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.ItemGroups, It.IsAny<string>()))
                .Returns((string table, string name) => new[] { name });
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.FeatGroups, ItemTypeConstants.Armor + GroupConstants.Proficiency))
                .Returns(armorProficiencyFeats);
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.FeatGroups, AttributeConstants.Shield + GroupConstants.Proficiency))
                .Returns(shieldProficiencyFeats);
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.ItemGroups, magicalArmor.Name))
                .Returns(baseArmorTypes);
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.ItemGroups, magicalShield.Name))
                .Returns(baseShieldTypes);
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.ItemGroups, feats[0].Name))
                .Returns(proficientArmors);
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.ItemGroups, feats[1].Name))
                .Returns(proficientShields);

            var index = 0;
            mockCollectionsSelector.Setup(s => s.SelectRandomFrom(It.IsAny<IEnumerable<string>>())).Returns((IEnumerable<string> ss) => ss.ElementAt(index++ % ss.Count()));
        }

        [Test]
        public void GenerateNoArmor()
        {
            feats.Remove(feats[0]);
            var armor = armorGenerator.GenerateArmorFrom(feats, characterClass, race);
            Assert.That(armor, Is.Null);
        }

        [Test]
        public void GenerateMundaneArmor()
        {
            var mundaneArmor = CreateArmor("mundane armor");
            mockPercentileSelector.Setup(s => s.SelectFrom("Level9266Power")).Returns(PowerConstants.Mundane);
            mockMundaneArmorGenerator.Setup(g => g.Generate()).Returns(mundaneArmor);

            var armor = armorGenerator.GenerateArmorFrom(feats, characterClass, race);
            Assert.That(armor, Is.EqualTo(mundaneArmor));
        }

        private Item CreateArmor(string name)
        {
            var armor = new Item();
            armor.Name = name;
            armor.ItemType = ItemTypeConstants.Armor;
            armor.Traits.Add(race.Size);

            proficientArmors.Add(name);

            return armor;
        }

        [Test]
        public void IfCannotWearMundaneArmor_Regenerate()
        {
            var mundaneArmor = CreateArmor("mundane armor");
            var wrongMundaneArmor = CreateArmor("wrong mundane armor");
            mockPercentileSelector.Setup(s => s.SelectFrom("Level9266Power")).Returns(PowerConstants.Mundane);
            mockMundaneArmorGenerator.SetupSequence(g => g.Generate()).Returns(wrongMundaneArmor).Returns(mundaneArmor);

            proficientArmors.Remove(wrongMundaneArmor.Name);

            var armor = armorGenerator.GenerateArmorFrom(feats, characterClass, race);
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

            var armor = armorGenerator.GenerateArmorFrom(feats, characterClass, race);
            Assert.That(armor, Is.EqualTo(mundaneArmor));
        }

        [Test]
        public void UseCumulativeArmorProficiencies()
        {
            feats.Add(new Feat { Name = "heavy proficiency" });

            var otherArmor = CreateArmor("other armor");
            armorProficiencyFeats.Add("heavy proficiency");

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.ItemGroups, feats[2].Name))
                .Returns(new[] { otherArmor.Name });

            mockMagicalArmorGenerator.SetupSequence(g => g.GenerateAtPower("power"))
                .Returns(otherArmor).Returns(magicalArmor);

            var armor = armorGenerator.GenerateArmorFrom(feats, characterClass, race);
            Assert.That(armor, Is.EqualTo(otherArmor));
        }

        [Test]
        public void IfMundaneArmorContainsMetalAndClassIsDruid_Regenerate()
        {
            characterClass.Name = CharacterClassConstants.Druid;
            var mundaneArmor = CreateArmor("mundane armor");
            var wrongMundaneArmor = CreateArmor("wrong mundane armor");
            mockPercentileSelector.Setup(s => s.SelectFrom("Level9266Power")).Returns(PowerConstants.Mundane);
            mockMundaneArmorGenerator.SetupSequence(g => g.Generate()).Returns(wrongMundaneArmor).Returns(mundaneArmor);

            wrongMundaneArmor.Attributes = wrongMundaneArmor.Attributes.Union(new[] { AttributeConstants.Metal });

            var armor = armorGenerator.GenerateArmorFrom(feats, characterClass, race);
            Assert.That(armor, Is.EqualTo(mundaneArmor));
        }

        [Test]
        public void IfMundaneArmorContainsMetalAndClassIsNotDruid_Keep()
        {
            var mundaneArmor = CreateArmor("mundane armor");
            var wrongMundaneArmor = CreateArmor("wrong mundane armor");
            mockPercentileSelector.Setup(s => s.SelectFrom("Level9266Power")).Returns(PowerConstants.Mundane);
            mockMundaneArmorGenerator.SetupSequence(g => g.Generate()).Returns(mundaneArmor).Returns(wrongMundaneArmor);

            mundaneArmor.Attributes = mundaneArmor.Attributes.Union(new[] { AttributeConstants.Metal });

            var armor = armorGenerator.GenerateArmorFrom(feats, characterClass, race);
            Assert.That(armor, Is.EqualTo(mundaneArmor));
        }

        [Test]
        public void IfMundaneArmorDoesNotContainMetalAndClassIsDruid_Keep()
        {
            characterClass.Name = CharacterClassConstants.Druid;
            var mundaneArmor = CreateArmor("mundane armor");
            var wrongMundaneArmor = CreateArmor("wrong mundane armor");
            mockPercentileSelector.Setup(s => s.SelectFrom("Level9266Power")).Returns(PowerConstants.Mundane);
            mockMundaneArmorGenerator.SetupSequence(g => g.Generate()).Returns(mundaneArmor).Returns(wrongMundaneArmor);

            mundaneArmor.Attributes = mundaneArmor.Attributes.Union(new[] { "other attribute" });

            var armor = armorGenerator.GenerateArmorFrom(feats, characterClass, race);
            Assert.That(armor, Is.EqualTo(mundaneArmor));
        }

        [Test]
        public void IfMundaneArmorDoesNotContainMetalAndClassIsNotDruid_Keep()
        {
            var mundaneArmor = CreateArmor("mundane armor");
            var wrongMundaneArmor = CreateArmor("wrong mundane armor");
            mockPercentileSelector.Setup(s => s.SelectFrom("Level9266Power")).Returns(PowerConstants.Mundane);
            mockMundaneArmorGenerator.SetupSequence(g => g.Generate()).Returns(mundaneArmor).Returns(wrongMundaneArmor);

            mundaneArmor.Attributes = mundaneArmor.Attributes.Union(new[] { "other attribute" });

            var armor = armorGenerator.GenerateArmorFrom(feats, characterClass, race);
            Assert.That(armor, Is.EqualTo(mundaneArmor));
        }

        [Test]
        public void GenerateMagicalArmor()
        {
            var armor = armorGenerator.GenerateArmorFrom(feats, characterClass, race);
            Assert.That(armor, Is.EqualTo(magicalArmor));
        }

        [Test]
        public void IfCannotWearMagicalArmor_Regenerate()
        {
            var wrongMagicalArmor = CreateArmor("wrong magical armor");
            mockMagicalArmorGenerator.SetupSequence(g => g.GenerateAtPower("power"))
                .Returns(wrongMagicalArmor).Returns(magicalArmor);

            proficientArmors.Remove(wrongMagicalArmor.Name);

            var armor = armorGenerator.GenerateArmorFrom(feats, characterClass, race);
            Assert.That(armor, Is.EqualTo(magicalArmor));
        }

        [Test]
        public void IfMagicalArmorIsNotArmor_Regenerate()
        {
            var wrongMagicalArmor = CreateArmor("wrong magical armor");
            mockMagicalArmorGenerator.SetupSequence(g => g.GenerateAtPower("power"))
                .Returns(wrongMagicalArmor).Returns(magicalArmor);

            wrongMagicalArmor.ItemType = "not armor";

            var armor = armorGenerator.GenerateArmorFrom(feats, characterClass, race);
            Assert.That(armor, Is.EqualTo(magicalArmor));
        }

        [Test]
        public void IfMagicalArmorContainsMetalAndClassIsDruid_Regenerate()
        {
            characterClass.Name = CharacterClassConstants.Druid;
            var wrongMagicalArmor = CreateArmor("wrong magical armor");
            mockMagicalArmorGenerator.SetupSequence(g => g.GenerateAtPower("power"))
                .Returns(wrongMagicalArmor).Returns(magicalArmor);

            wrongMagicalArmor.Attributes = wrongMagicalArmor.Attributes.Union(new[] { AttributeConstants.Metal });

            var armor = armorGenerator.GenerateArmorFrom(feats, characterClass, race);
            Assert.That(armor, Is.EqualTo(magicalArmor));
        }

        [Test]
        public void IfMagicalArmorContainsMetalAndClassIsNotDruid_Keep()
        {
            var wrongMagicalArmor = CreateArmor("wrong magical armor");
            mockMagicalArmorGenerator.SetupSequence(g => g.GenerateAtPower("power"))
                .Returns(magicalArmor).Returns(wrongMagicalArmor);

            magicalArmor.Attributes = wrongMagicalArmor.Attributes.Union(new[] { AttributeConstants.Metal });

            var armor = armorGenerator.GenerateArmorFrom(feats, characterClass, race);
            Assert.That(armor, Is.EqualTo(magicalArmor));
        }

        [Test]
        public void IfMagicalArmorDoesNotContainMetalAndClassIsDruid_Keep()
        {
            characterClass.Name = CharacterClassConstants.Druid;
            var wrongMagicalArmor = CreateArmor("wrong magical armor");
            mockMagicalArmorGenerator.SetupSequence(g => g.GenerateAtPower("power"))
                .Returns(magicalArmor).Returns(wrongMagicalArmor);

            magicalArmor.Attributes = wrongMagicalArmor.Attributes.Union(new[] { "other attribute" });

            var armor = armorGenerator.GenerateArmorFrom(feats, characterClass, race);
            Assert.That(armor, Is.EqualTo(magicalArmor));
        }

        [Test]
        public void IfMagicalArmorDoesNotContainMetalAndClassIsNotDruid_Keep()
        {
            var wrongMagicalArmor = CreateArmor("wrong magical armor");
            mockMagicalArmorGenerator.SetupSequence(g => g.GenerateAtPower("power"))
                .Returns(magicalArmor).Returns(wrongMagicalArmor);

            magicalArmor.Attributes = wrongMagicalArmor.Attributes.Union(new[] { "other attribute" });

            var armor = armorGenerator.GenerateArmorFrom(feats, characterClass, race);
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

            var armor = armorGenerator.GenerateArmorFrom(feats, characterClass, race);
            Assert.That(armor, Is.EqualTo(mundaneArmor));
        }

        [Test]
        public void MagicalArmorDoesNotHaveToFitCharacter()
        {
            var wrongMagicalArmor = CreateArmor("wrong magical armor");
            mockMagicalArmorGenerator.SetupSequence(g => g.GenerateAtPower("power"))
                .Returns(magicalArmor).Returns(wrongMagicalArmor);

            race.Size = "other size";

            var armor = armorGenerator.GenerateArmorFrom(feats, characterClass, race);
            Assert.That(armor, Is.EqualTo(magicalArmor));
        }

        [Test]
        public void ArmorCannotBeShield()
        {
            mockMagicalArmorGenerator.SetupSequence(g => g.GenerateAtPower("power"))
                .Returns(magicalShield).Returns(magicalArmor);

            var armor = armorGenerator.GenerateArmorFrom(feats, characterClass, race);
            Assert.That(armor, Is.EqualTo(magicalArmor));
        }

        [Test]
        public void GenerateMundaneDefaultArmor()
        {
            var wrongArmor = CreateArmor("wrong armor");

            proficientArmors.Remove("wrong armor");

            mockPercentileSelector.Setup(s => s.SelectFrom("Level9266Power")).Returns(PowerConstants.Mundane);
            mockMundaneArmorGenerator.Setup(g => g.Generate()).Returns(wrongArmor);

            var defaultArmor = new Item();
            mockMundaneArmorGenerator.Setup(g => g.Generate(It.Is<Item>(i => i.ItemType == ItemTypeConstants.Armor && i.Name == "base armor" && i.Traits.Single() == race.Size), true))
                .Returns(defaultArmor);

            var armor = armorGenerator.GenerateArmorFrom(feats, characterClass, race);
            Assert.That(armor, Is.EqualTo(defaultArmor));
        }

        [Test]
        public void GenerateMagicalDefaultArmor()
        {
            var wrongMagicalArmor = CreateArmor("wrong magical armor");
            mockMagicalArmorGenerator.Setup(g => g.GenerateAtPower("power")).Returns(wrongMagicalArmor);

            proficientArmors.Remove(wrongMagicalArmor.Name);

            var defaultArmor = new Item();
            mockMagicalArmorGenerator.Setup(g => g.Generate(It.Is<Item>(i => i.ItemType == ItemTypeConstants.Armor && i.Name == "base armor" && i.Magic.Bonus == 1), true))
                .Returns(defaultArmor);

            var armor = armorGenerator.GenerateArmorFrom(feats, characterClass, race);
            Assert.That(armor, Is.EqualTo(defaultArmor));
        }

        [Test]
        public void GenerateNoShield()
        {
            feats.Remove(feats[1]);
            var shield = armorGenerator.GenerateShieldFrom(feats, characterClass, race);
            Assert.That(shield, Is.Null);
        }

        [Test]
        public void GenerateMundaneShield()
        {
            var mundaneShield = CreateShield("mundane shield");
            mockPercentileSelector.Setup(s => s.SelectFrom("Level9266Power")).Returns(PowerConstants.Mundane);
            mockMundaneArmorGenerator.Setup(g => g.Generate()).Returns(mundaneShield);

            var shield = armorGenerator.GenerateShieldFrom(feats, characterClass, race);
            Assert.That(shield, Is.EqualTo(mundaneShield), shield.Name);
        }

        private Item CreateShield(string name)
        {
            var shield = new Item();
            shield.Name = name;
            shield.ItemType = ItemTypeConstants.Armor;
            shield.Attributes = new[] { AttributeConstants.Shield };
            shield.Traits.Add(race.Size);

            proficientShields.Add(name);

            return shield;
        }

        [Test]
        public void IfCannotUseMundaneShield_Regenerate()
        {
            var mundaneShield = CreateShield("mundane shield");
            var wrongMundaneShield = CreateShield("wrong mundane shield");
            mockPercentileSelector.Setup(s => s.SelectFrom("Level9266Power")).Returns(PowerConstants.Mundane);
            mockMundaneArmorGenerator.SetupSequence(g => g.Generate()).Returns(wrongMundaneShield).Returns(mundaneShield);

            proficientShields.Remove(wrongMundaneShield.Name);

            var shield = armorGenerator.GenerateShieldFrom(feats, characterClass, race);
            Assert.That(shield, Is.EqualTo(mundaneShield), shield.Name);
        }

        [Test]
        public void IfNotMundaneShield_Regenerate()
        {
            var mundaneShield = CreateShield("mundane shield");
            var wrongMundaneShield = CreateShield("wrong mundane shield");
            mockPercentileSelector.Setup(s => s.SelectFrom("Level9266Power")).Returns(PowerConstants.Mundane);
            mockMundaneArmorGenerator.SetupSequence(g => g.Generate()).Returns(wrongMundaneShield).Returns(mundaneShield);

            wrongMundaneShield.ItemType = "not armor";

            var shield = armorGenerator.GenerateShieldFrom(feats, characterClass, race);
            Assert.That(shield, Is.EqualTo(mundaneShield), shield.Name);
        }

        [Test]
        public void UseCumulativeShieldProficiencies()
        {
            feats.Add(new Feat { Name = "heavy proficiency" });

            var otherShield = CreateShield("other shield");
            shieldProficiencyFeats.Add("heavy proficiency");

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.ItemGroups, feats[2].Name))
                .Returns(new[] { otherShield.Name });

            mockMagicalArmorGenerator.SetupSequence(g => g.GenerateAtPower("power"))
                .Returns(otherShield).Returns(magicalShield);

            var shield = armorGenerator.GenerateShieldFrom(feats, characterClass, race);
            Assert.That(shield, Is.EqualTo(otherShield), shield.Name);
        }

        [Test]
        public void IfMundaneShieldContainsMetalAndClassIsDruid_Regenerate()
        {
            characterClass.Name = CharacterClassConstants.Druid;
            var mundaneShield = CreateShield("mundane shield");
            var wrongMundaneShield = CreateShield("wrong mundane shield");
            mockPercentileSelector.Setup(s => s.SelectFrom("Level9266Power")).Returns(PowerConstants.Mundane);
            mockMundaneArmorGenerator.SetupSequence(g => g.Generate()).Returns(wrongMundaneShield).Returns(mundaneShield);

            wrongMundaneShield.Attributes = wrongMundaneShield.Attributes.Union(new[] { AttributeConstants.Metal });

            var shield = armorGenerator.GenerateShieldFrom(feats, characterClass, race);
            Assert.That(shield, Is.EqualTo(mundaneShield), shield.Name);
        }

        [Test]
        public void IfMundaneShieldContainsMetalAndClassIsNotDruid_Keep()
        {
            var mundaneShield = CreateShield("mundane shield");
            var wrongMundaneShield = CreateShield("wrong mundane shield");
            mockPercentileSelector.Setup(s => s.SelectFrom("Level9266Power")).Returns(PowerConstants.Mundane);
            mockMundaneArmorGenerator.SetupSequence(g => g.Generate()).Returns(mundaneShield).Returns(wrongMundaneShield);

            mundaneShield.Attributes = mundaneShield.Attributes.Union(new[] { AttributeConstants.Metal });

            var shield = armorGenerator.GenerateShieldFrom(feats, characterClass, race);
            Assert.That(shield, Is.EqualTo(mundaneShield), shield.Name);
        }

        [Test]
        public void IfMundaneShieldDoesNotContainMetalAndClassIsDruid_Keep()
        {
            characterClass.Name = CharacterClassConstants.Druid;
            var mundaneShield = CreateShield("mundane shield");
            var wrongMundaneShield = CreateShield("wrong mundane shield");
            mockPercentileSelector.Setup(s => s.SelectFrom("Level9266Power")).Returns(PowerConstants.Mundane);
            mockMundaneArmorGenerator.SetupSequence(g => g.Generate()).Returns(mundaneShield).Returns(wrongMundaneShield);

            mundaneShield.Attributes = mundaneShield.Attributes.Union(new[] { "other attribute" });

            var shield = armorGenerator.GenerateShieldFrom(feats, characterClass, race);
            Assert.That(shield, Is.EqualTo(mundaneShield), shield.Name);
        }

        [Test]
        public void IfMundaneShieldDoesNotContainMetalAndClassIsNotDruid_Keep()
        {
            var mundaneShield = CreateShield("mundane shield");
            var wrongMundaneShield = CreateShield("wrong mundane shield");
            mockPercentileSelector.Setup(s => s.SelectFrom("Level9266Power")).Returns(PowerConstants.Mundane);
            mockMundaneArmorGenerator.SetupSequence(g => g.Generate()).Returns(mundaneShield).Returns(wrongMundaneShield);

            mundaneShield.Attributes = mundaneShield.Attributes.Union(new[] { "other attribute" });

            var shield = armorGenerator.GenerateShieldFrom(feats, characterClass, race);
            Assert.That(shield, Is.EqualTo(mundaneShield), shield.Name);
        }

        [Test]
        public void GenerateMagicalShield()
        {
            mockMagicalArmorGenerator.Setup(g => g.GenerateAtPower("power")).Returns(magicalShield);
            var shield = armorGenerator.GenerateShieldFrom(feats, characterClass, race);
            Assert.That(shield, Is.EqualTo(magicalShield), shield.Name);
        }

        [Test]
        public void IfCannotUseMagicalShield_Regenerate()
        {
            var wrongMagicalShield = CreateShield("wrong magical shield");
            mockMagicalArmorGenerator.SetupSequence(g => g.GenerateAtPower("power"))
                .Returns(wrongMagicalShield).Returns(magicalShield);

            proficientShields.Remove(wrongMagicalShield.Name);

            var shield = armorGenerator.GenerateShieldFrom(feats, characterClass, race);
            Assert.That(shield, Is.EqualTo(magicalShield), shield.Name);
        }

        [Test]
        public void IfMagicalShieldIsNotShield_Regenerate()
        {
            var wrongMagicalShield = CreateShield("wrong magical shield");
            mockMagicalArmorGenerator.SetupSequence(g => g.GenerateAtPower("power"))
                .Returns(wrongMagicalShield).Returns(magicalShield);

            wrongMagicalShield.ItemType = "not armor";

            var shield = armorGenerator.GenerateShieldFrom(feats, characterClass, race);
            Assert.That(shield, Is.EqualTo(magicalShield), shield.Name);
        }

        [Test]
        public void IfMagicalShieldContainsMetalAndClassIsDruid_Regenerate()
        {
            characterClass.Name = CharacterClassConstants.Druid;
            var wrongMagicalShield = CreateShield("wrong magical shield");
            mockMagicalArmorGenerator.SetupSequence(g => g.GenerateAtPower("power"))
                .Returns(wrongMagicalShield).Returns(magicalShield);

            wrongMagicalShield.Attributes = wrongMagicalShield.Attributes.Union(new[] { AttributeConstants.Metal });

            var shield = armorGenerator.GenerateShieldFrom(feats, characterClass, race);
            Assert.That(shield, Is.EqualTo(magicalShield), shield.Name);
        }

        [Test]
        public void IfMagicalShieldContainsMetalAndClassIsNotDruid_Keep()
        {
            var wrongMagicalShield = CreateShield("wrong magical shield");
            mockMagicalArmorGenerator.SetupSequence(g => g.GenerateAtPower("power"))
                .Returns(magicalShield).Returns(wrongMagicalShield);

            magicalShield.Attributes = wrongMagicalShield.Attributes.Union(new[] { AttributeConstants.Metal });

            var shield = armorGenerator.GenerateShieldFrom(feats, characterClass, race);
            Assert.That(shield, Is.EqualTo(magicalShield), shield.Name);
        }

        [Test]
        public void IfMagicalShieldDoesNotContainMetalAndClassIsDruid_Keep()
        {
            characterClass.Name = CharacterClassConstants.Druid;
            var wrongMagicalShield = CreateShield("wrong magical shield");
            mockMagicalArmorGenerator.SetupSequence(g => g.GenerateAtPower("power"))
                .Returns(magicalShield).Returns(wrongMagicalShield);

            magicalShield.Attributes = wrongMagicalShield.Attributes.Union(new[] { "other attribute" });

            var shield = armorGenerator.GenerateShieldFrom(feats, characterClass, race);
            Assert.That(shield, Is.EqualTo(magicalShield), shield.Name);
        }

        [Test]
        public void IfMagicalShieldDoesNotContainMetalAndClassIsNotDruid_Keep()
        {
            var wrongMagicalShield = CreateShield("wrong magical shield");
            mockMagicalArmorGenerator.SetupSequence(g => g.GenerateAtPower("power"))
                .Returns(magicalShield).Returns(wrongMagicalShield);

            magicalShield.Attributes = wrongMagicalShield.Attributes.Union(new[] { "other attribute" });

            var shield = armorGenerator.GenerateShieldFrom(feats, characterClass, race);
            Assert.That(shield, Is.EqualTo(magicalShield), shield.Name);
        }

        [Test]
        public void MundaneShieldMustFitCharacter()
        {
            var mundaneShield = CreateShield("mundane shield");
            var wrongMundaneShield = CreateShield("wrong mundane shield");
            var otherWrongMundaneShield = CreateShield("other wrong mundane shield");
            mockPercentileSelector.Setup(s => s.SelectFrom("Level9266Power")).Returns(PowerConstants.Mundane);
            mockMundaneArmorGenerator.SetupSequence(g => g.Generate())
                .Returns(wrongMundaneShield).Returns(otherWrongMundaneShield).Returns(mundaneShield);

            wrongMundaneShield.Traits.Clear();
            wrongMundaneShield.Traits.Add("bigger size");
            otherWrongMundaneShield.Traits.Clear();
            otherWrongMundaneShield.Traits.Add("smaller size");

            var shield = armorGenerator.GenerateShieldFrom(feats, characterClass, race);
            Assert.That(shield, Is.EqualTo(mundaneShield), shield.Name);
        }

        [Test]
        public void MagicalShieldDoesNotHaveToFitCharacter()
        {
            var wrongMagicalShield = CreateShield("wrong magical shield");
            mockMagicalArmorGenerator.SetupSequence(g => g.GenerateAtPower("power"))
                .Returns(magicalShield).Returns(wrongMagicalShield);

            race.Size = "other size";

            var shield = armorGenerator.GenerateShieldFrom(feats, characterClass, race);
            Assert.That(shield, Is.EqualTo(magicalShield), shield.Name);
        }

        [Test]
        public void ShieldCannotBeArmor()
        {
            mockMagicalArmorGenerator.SetupSequence(g => g.GenerateAtPower("power"))
                .Returns(magicalArmor).Returns(magicalShield);

            var shield = armorGenerator.GenerateShieldFrom(feats, characterClass, race);
            Assert.That(shield, Is.EqualTo(magicalShield), shield.Name);
        }

        [Test]
        public void GenerateMundaneDefaultShield()
        {
            var wrongShield = CreateShield("wrong shield");

            proficientShields.Remove("wrong shield");

            mockPercentileSelector.Setup(s => s.SelectFrom("Level9266Power")).Returns(PowerConstants.Mundane);
            mockMundaneArmorGenerator.Setup(g => g.Generate()).Returns(wrongShield);

            var defaultShield = new Item();
            mockMundaneArmorGenerator.Setup(g => g.Generate(It.Is<Item>(i => i.ItemType == ItemTypeConstants.Armor && i.Name == "base shield" && i.Traits.Single() == race.Size), true))
                .Returns(defaultShield);

            var shield = armorGenerator.GenerateShieldFrom(feats, characterClass, race);
            Assert.That(shield, Is.EqualTo(defaultShield));
        }

        [Test]
        public void GenerateMagicalDefaultShield()
        {
            var wrongMagicalShield = CreateShield("wrong magical shield");
            mockMagicalArmorGenerator.Setup(g => g.GenerateAtPower("power")).Returns(wrongMagicalShield);

            proficientShields.Remove(wrongMagicalShield.Name);

            var defaultShield = new Item();
            mockMagicalArmorGenerator.Setup(g => g.Generate(It.Is<Item>(i => i.ItemType == ItemTypeConstants.Armor && i.Name == "base shield" && i.Magic.Bonus == 1), true))
                .Returns(defaultShield);

            var shield = armorGenerator.GenerateShieldFrom(feats, characterClass, race);
            Assert.That(shield, Is.EqualTo(defaultShield));
        }

        [TestCase(1, 1)]
        [TestCase(2, 1)]
        [TestCase(3, 1)]
        [TestCase(4, 2)]
        [TestCase(5, 2)]
        [TestCase(6, 3)]
        [TestCase(7, 3)]
        [TestCase(8, 4)]
        [TestCase(9, 4)]
        [TestCase(10, 5)]
        [TestCase(11, 5)]
        [TestCase(12, 6)]
        [TestCase(13, 6)]
        [TestCase(14, 7)]
        [TestCase(15, 7)]
        [TestCase(16, 8)]
        [TestCase(17, 8)]
        [TestCase(18, 9)]
        [TestCase(19, 9)]
        [TestCase(20, 10)]
        public void NPCArmorIsHalfLevel(int npcLevel, int effectiveLevel)
        {
            characterClass.Level = npcLevel;
            characterClass.Name = "class name";
            characterClass.IsNPC = true;

            var npcArmor = CreateArmor("npc armor");

            var tableName = string.Format(TableNameConstants.Formattable.Percentile.LevelXPower, effectiveLevel);
            mockPercentileSelector.Setup(s => s.SelectFrom(tableName)).Returns("npc power");
            mockMagicalArmorGenerator.Setup(g => g.GenerateAtPower("npc power")).Returns(npcArmor);

            var weapon = armorGenerator.GenerateArmorFrom(feats, characterClass, race);
            Assert.That(weapon, Is.EqualTo(npcArmor));
        }

        [TestCase(1, 1)]
        [TestCase(2, 2)]
        [TestCase(3, 3)]
        [TestCase(4, 4)]
        [TestCase(5, 5)]
        [TestCase(6, 6)]
        [TestCase(7, 7)]
        [TestCase(8, 8)]
        [TestCase(9, 9)]
        [TestCase(10, 10)]
        [TestCase(11, 11)]
        [TestCase(12, 12)]
        [TestCase(13, 13)]
        [TestCase(14, 14)]
        [TestCase(15, 15)]
        [TestCase(16, 16)]
        [TestCase(17, 17)]
        [TestCase(18, 18)]
        [TestCase(19, 19)]
        [TestCase(20, 20)]
        public void PlayerCharacterArmorIsFullLevel(int level, int effectiveLevel)
        {
            characterClass.Level = level;
            characterClass.Name = "class name";
            characterClass.IsNPC = false;

            var playerArmor = CreateArmor("player weapon");

            var tableName = string.Format(TableNameConstants.Formattable.Percentile.LevelXPower, effectiveLevel);
            mockPercentileSelector.Setup(s => s.SelectFrom(tableName)).Returns("player power");
            mockMagicalArmorGenerator.Setup(g => g.GenerateAtPower("player power")).Returns(playerArmor);

            var weapon = armorGenerator.GenerateArmorFrom(feats, characterClass, race);
            Assert.That(weapon, Is.EqualTo(playerArmor));
        }

        [TestCase(1, 1)]
        [TestCase(2, 1)]
        [TestCase(3, 1)]
        [TestCase(4, 2)]
        [TestCase(5, 2)]
        [TestCase(6, 3)]
        [TestCase(7, 3)]
        [TestCase(8, 4)]
        [TestCase(9, 4)]
        [TestCase(10, 5)]
        [TestCase(11, 5)]
        [TestCase(12, 6)]
        [TestCase(13, 6)]
        [TestCase(14, 7)]
        [TestCase(15, 7)]
        [TestCase(16, 8)]
        [TestCase(17, 8)]
        [TestCase(18, 9)]
        [TestCase(19, 9)]
        [TestCase(20, 10)]
        public void NPCShieldIsHalfLevel(int npcLevel, int effectiveLevel)
        {
            characterClass.Level = npcLevel;
            characterClass.Name = "class name";
            characterClass.IsNPC = true;

            var npcShield = CreateShield("npc shield");

            var tableName = string.Format(TableNameConstants.Formattable.Percentile.LevelXPower, effectiveLevel);
            mockPercentileSelector.Setup(s => s.SelectFrom(tableName)).Returns("npc power");
            mockMagicalArmorGenerator.Setup(g => g.GenerateAtPower("npc power")).Returns(npcShield);

            var weapon = armorGenerator.GenerateShieldFrom(feats, characterClass, race);
            Assert.That(weapon, Is.EqualTo(npcShield));
        }

        [TestCase(1, 1)]
        [TestCase(2, 2)]
        [TestCase(3, 3)]
        [TestCase(4, 4)]
        [TestCase(5, 5)]
        [TestCase(6, 6)]
        [TestCase(7, 7)]
        [TestCase(8, 8)]
        [TestCase(9, 9)]
        [TestCase(10, 10)]
        [TestCase(11, 11)]
        [TestCase(12, 12)]
        [TestCase(13, 13)]
        [TestCase(14, 14)]
        [TestCase(15, 15)]
        [TestCase(16, 16)]
        [TestCase(17, 17)]
        [TestCase(18, 18)]
        [TestCase(19, 19)]
        [TestCase(20, 20)]
        public void PlayerCharacterShieldIsFullLevel(int level, int effectiveLevel)
        {
            characterClass.Level = level;
            characterClass.Name = "class name";
            characterClass.IsNPC = false;

            var playerShield = CreateShield("player shield");

            var tableName = string.Format(TableNameConstants.Formattable.Percentile.LevelXPower, effectiveLevel);
            mockPercentileSelector.Setup(s => s.SelectFrom(tableName)).Returns("player power");
            mockMagicalArmorGenerator.Setup(g => g.GenerateAtPower("player power")).Returns(playerShield);

            var weapon = armorGenerator.GenerateShieldFrom(feats, characterClass, race);
            Assert.That(weapon, Is.EqualTo(playerShield));
        }
    }
}
