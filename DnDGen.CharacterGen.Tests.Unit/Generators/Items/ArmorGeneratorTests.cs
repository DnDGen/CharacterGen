﻿using DnDGen.CharacterGen.CharacterClasses;
using DnDGen.CharacterGen.Feats;
using DnDGen.CharacterGen.Generators.Items;
using DnDGen.CharacterGen.Races;
using DnDGen.CharacterGen.Tables;
using DnDGen.Infrastructure.Generators;
using DnDGen.Infrastructure.Selectors.Collections;
using DnDGen.Infrastructure.Selectors.Percentiles;
using DnDGen.RollGen;
using DnDGen.TreasureGen.Items;
using DnDGen.TreasureGen.Items.Magical;
using DnDGen.TreasureGen.Items.Mundane;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace DnDGen.CharacterGen.Tests.Unit.Generators.Items
{
    [TestFixture]
    public class ArmorGeneratorTests
    {
        private Mock<ICollectionSelector> mockCollectionsSelector;
        private Mock<IPercentileSelector> mockPercentileSelector;
        private Mock<MundaneItemGenerator> mockMundaneArmorGenerator;
        private Mock<MagicalItemGenerator> mockMagicalArmorGenerator;
        private Mock<JustInTimeFactory> mockJustInTimeFactory;
        private Mock<Dice> mockDice;
        private IArmorGenerator armorGenerator;
        private List<Feat> feats;
        private CharacterClass characterClass;
        private List<string> armorProficiencyFeats;
        private List<string> shieldProficiencyFeats;
        private List<string> proficientArmors;
        private List<string> proficientShields;
        private Armor magicalArmor;
        private Armor magicalShield;
        private Race race;
        private string powerTableName;
        private string power;

        [SetUp]
        public void Setup()
        {
            mockCollectionsSelector = new Mock<ICollectionSelector>();
            mockPercentileSelector = new Mock<IPercentileSelector>();
            mockMundaneArmorGenerator = new Mock<MundaneItemGenerator>();
            mockMagicalArmorGenerator = new Mock<MagicalItemGenerator>();
            mockJustInTimeFactory = new Mock<JustInTimeFactory>();
            mockDice = new Mock<Dice>();
            armorGenerator = new ArmorGenerator(mockCollectionsSelector.Object, mockPercentileSelector.Object, mockJustInTimeFactory.Object, mockDice.Object);

            feats = new List<Feat>();
            characterClass = new CharacterClass();
            armorProficiencyFeats = new List<string>();
            shieldProficiencyFeats = new List<string>();
            proficientArmors = new List<string>();
            proficientShields = new List<string>();
            race = new Race();

            race.Size = "size";
            magicalArmor = CreateArmor("magical armor");
            magicalArmor.IsMagical = true;
            magicalShield = CreateShield("magical shield");
            magicalShield.IsMagical = true;

            proficientArmors.Remove(magicalArmor.Name);
            proficientArmors.Add("my armor");
            proficientArmors.Add("other armor");
            proficientArmors.Add("metal armor");
            proficientArmors.Add("specific armor");
            proficientArmors.Add("specific metal armor");
            proficientShields.Remove(magicalShield.Name);
            proficientShields.Add("my shield");
            proficientShields.Add("other shield");
            proficientShields.Add("metal shield");
            proficientShields.Add("specific shield");
            proficientShields.Add("specific metal shield");

            characterClass.Level = 9266;
            feats.Add(new Feat { Name = "light proficiency" });
            feats.Add(new Feat { Name = "shield proficiency" });
            feats.Add(new Feat { Name = "other feat" });
            armorProficiencyFeats.Add(feats[0].Name);
            shieldProficiencyFeats.Add(feats[1].Name);

            powerTableName = string.Format(TableNameConstants.Formattable.Percentile.LevelXPower, characterClass.Level);
            power = "my power";

            mockPercentileSelector.Setup(s => s.SelectFrom(powerTableName)).Returns(power);

            mockCollectionsSelector
                .Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.FeatGroups, ItemTypeConstants.Armor + GroupConstants.Proficiency))
                .Returns(armorProficiencyFeats);
            mockCollectionsSelector
                .Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.FeatGroups, AttributeConstants.Shield + GroupConstants.Proficiency))
                .Returns(shieldProficiencyFeats);
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.ItemGroups, feats[0].Name)).Returns(proficientArmors);
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.ItemGroups, feats[1].Name)).Returns(proficientShields);

            var index = 0;

            mockCollectionsSelector
                .Setup(s => s.SelectRandomFrom(It.IsAny<IEnumerable<string>>()))
                .Returns((IEnumerable<string> ss) => ss.ElementAt(index++ % ss.Count()));

            mockCollectionsSelector
                .Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.ItemGroups, AttributeConstants.Specific))
                .Returns(() => new[] { magicalArmor.Name, magicalShield.Name }
                    .Union(proficientArmors)
                    .Union(proficientShields)
                    .Where(a => a.Contains("specific")));
            mockCollectionsSelector
                .Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.ItemGroups, AttributeConstants.Metal))
                .Returns(() => new[] { magicalArmor.Name, magicalShield.Name }
                    .Union(proficientArmors)
                    .Union(proficientShields)
                    .Where(a => a.Contains("metal")));

            mockJustInTimeFactory.Setup(f => f.Build<MundaneItemGenerator>(ItemTypeConstants.Armor)).Returns(mockMundaneArmorGenerator.Object);
            mockJustInTimeFactory.Setup(f => f.Build<MagicalItemGenerator>(ItemTypeConstants.Armor)).Returns(mockMagicalArmorGenerator.Object);

            mockDice.Setup(d => d.Roll(1).d(100).AsTrueOrFalse(It.IsAny<int>())).Returns(false);
            mockDice.Setup(d => d.Roll(1).d(100).AsTrueOrFalse(It.IsAny<double>())).Returns(false);
            mockDice.Setup(d => d.Roll(1).d(3).AsTrueOrFalse(It.IsAny<int>())).Returns(false);
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

            mockPercentileSelector.Setup(s => s.SelectFrom(powerTableName)).Returns(PowerConstants.Mundane);
            mockCollectionsSelector.Setup(s => s.SelectRandomFrom(ProficientArmorSet())).Returns("my random armor");
            mockMundaneArmorGenerator.Setup(g => g.Generate("my random armor", race.Size)).Returns(mundaneArmor);

            var armor = armorGenerator.GenerateArmorFrom(feats, characterClass, race);
            Assert.That(armor, Is.EqualTo(mundaneArmor));
        }

        private IEnumerable<string> ProficientArmorSet()
        {
            return It.Is<IEnumerable<string>>(ss => ss.Intersect(proficientArmors).Count() == proficientArmors.Count);
        }

        private IEnumerable<string> ProficientSet(params string[] expected)
        {
            return It.Is<IEnumerable<string>>(ss => ss.Intersect(expected).Count() == expected.Length && ss.Count() == expected.Length);
        }

        private IEnumerable<string> ProficientShieldSet()
        {
            return It.Is<IEnumerable<string>>(ss => ss.Intersect(proficientShields).Count() == proficientShields.Count);
        }

        private Armor CreateArmor(string name)
        {
            var armor = new Armor();
            armor.Name = name;
            armor.ItemType = ItemTypeConstants.Armor;
            armor.Size = race.Size;

            proficientArmors.Add(name);

            return armor;
        }

        [Test]
        public void IfNotMundaneArmor_Regenerate()
        {
            var mundaneArmor = CreateArmor("mundane armor");
            var wrongMundaneArmor = new Item() { Name = "wrong armor", ItemType = ItemTypeConstants.Armor };

            mockPercentileSelector.Setup(s => s.SelectFrom(powerTableName)).Returns(PowerConstants.Mundane);
            mockCollectionsSelector.Setup(s => s.SelectRandomFrom(ProficientArmorSet())).Returns("my random armor");
            mockMundaneArmorGenerator.SetupSequence(g => g.Generate("my random armor", race.Size)).Returns(wrongMundaneArmor).Returns(mundaneArmor);

            var armor = armorGenerator.GenerateArmorFrom(feats, characterClass, race);
            Assert.That(armor, Is.EqualTo(mundaneArmor));
        }

        [Test]
        public void UseCumulativeArmorProficiencies()
        {
            feats.Add(new Feat { Name = "heavy proficiency" });

            var otherArmor = CreateArmor("other armor");
            armorProficiencyFeats.Add("heavy proficiency");
            proficientArmors.Remove("other armor");

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.ItemGroups, feats[2].Name))
                .Returns(new[] { otherArmor.Name });

            mockCollectionsSelector.Setup(s => s.SelectRandomFrom(ProficientArmorSet())).Returns("my random armor");
            mockMagicalArmorGenerator
                .Setup(g => g.Generate(power, "my random armor", race.Size))
                .Returns(magicalArmor);

            var armor = armorGenerator.GenerateArmorFrom(feats, characterClass, race);
            Assert.That(armor, Is.EqualTo(otherArmor));
        }

        [Test]
        public void IfMundaneArmorContainsMetalAndClassIsDruid_Regenerate()
        {
            characterClass.Name = CharacterClassConstants.Druid;
            var mundaneArmor = CreateArmor("mundane armor");
            var wrongMundaneArmor = CreateArmor("wrong mundane armor");
            mockPercentileSelector.Setup(s => s.SelectFrom(powerTableName)).Returns(PowerConstants.Mundane);
            mockCollectionsSelector.Setup(s => s.SelectRandomFrom(ProficientArmorSet())).Returns("my random armor");
            mockMundaneArmorGenerator
                .Setup(g => g.Generate("my random armor", race.Size))
                .Returns(mundaneArmor);

            wrongMundaneArmor.Attributes = wrongMundaneArmor.Attributes.Union(new[] { AttributeConstants.Metal });

            var armor = armorGenerator.GenerateArmorFrom(feats, characterClass, race);
            Assert.That(armor, Is.EqualTo(mundaneArmor));
        }

        [Test]
        public void IfMundaneArmorContainsMetalAndClassIsNotDruid_Keep()
        {
            var mundaneArmor = CreateArmor("mundane armor");
            var wrongMundaneArmor = CreateArmor("wrong mundane armor");
            mockPercentileSelector.Setup(s => s.SelectFrom(powerTableName)).Returns(PowerConstants.Mundane);
            mockCollectionsSelector.Setup(s => s.SelectRandomFrom(ProficientArmorSet())).Returns("my random armor");
            mockMundaneArmorGenerator.SetupSequence(g => g.Generate("my random armor", race.Size)).Returns(mundaneArmor).Returns(wrongMundaneArmor);

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
            mockPercentileSelector.Setup(s => s.SelectFrom(powerTableName)).Returns(PowerConstants.Mundane);
            mockCollectionsSelector.Setup(s => s.SelectRandomFrom(ProficientArmorSet())).Returns("my random armor");
            mockMundaneArmorGenerator.SetupSequence(g => g.Generate("my random armor", race.Size)).Returns(mundaneArmor).Returns(wrongMundaneArmor);

            mundaneArmor.Attributes = mundaneArmor.Attributes.Union(new[] { "other attribute" });

            var armor = armorGenerator.GenerateArmorFrom(feats, characterClass, race);
            Assert.That(armor, Is.EqualTo(mundaneArmor));
        }

        [Test]
        public void IfMundaneArmorDoesNotContainMetalAndClassIsNotDruid_Keep()
        {
            var mundaneArmor = CreateArmor("mundane armor");
            var wrongMundaneArmor = CreateArmor("wrong mundane armor");
            mockPercentileSelector.Setup(s => s.SelectFrom(powerTableName)).Returns(PowerConstants.Mundane);
            mockCollectionsSelector.Setup(s => s.SelectRandomFrom(ProficientArmorSet())).Returns("my random armor");
            mockMundaneArmorGenerator.SetupSequence(g => g.Generate("my random armor", race.Size)).Returns(mundaneArmor).Returns(wrongMundaneArmor);

            mundaneArmor.Attributes = mundaneArmor.Attributes.Union(new[] { "other attribute" });

            var armor = armorGenerator.GenerateArmorFrom(feats, characterClass, race);
            Assert.That(armor, Is.EqualTo(mundaneArmor));
        }

        [Test]
        public void GenerateArmorFrom_GenerateMagicalArmor()
        {
            mockCollectionsSelector.Setup(s => s.SelectRandomFrom(ProficientSet("my armor", "other armor", "metal armor"))).Returns("my random armor");
            mockMagicalArmorGenerator.Setup(g => g.Generate(power, "my random armor", race.Size)).Returns(magicalArmor);

            var armor = armorGenerator.GenerateArmorFrom(feats, characterClass, race);
            Assert.That(armor, Is.EqualTo(magicalArmor));
        }

        [TestCase(PowerConstants.Minor, 0.98)]
        [TestCase(PowerConstants.Medium, 0.97)]
        [TestCase(PowerConstants.Major, 0.97)]
        public void GenerateArmorFrom_GenerateMagicalArmor_Specific(string power, double specificThreshold)
        {
            mockPercentileSelector.Setup(s => s.SelectFrom(powerTableName)).Returns(power);

            mockDice.Setup(d => d.Roll(1).d(100).AsTrueOrFalse(specificThreshold)).Returns(true);

            mockCollectionsSelector.Setup(s => s.SelectRandomFrom(ProficientSet("specific armor", "specific metal armor"))).Returns("my random armor");
            mockMagicalArmorGenerator.Setup(g => g.Generate(power, "my random armor", race.Size)).Returns(magicalArmor);

            var armor = armorGenerator.GenerateArmorFrom(feats, characterClass, race);
            Assert.That(armor, Is.EqualTo(magicalArmor));
        }

        [TestCase(PowerConstants.Minor, 0.98, 92)]
        [TestCase(PowerConstants.Medium, 0.97, 64)]
        [TestCase(PowerConstants.Major, 0.97, 64)]
        public void GenerateArmorFrom_GenerateMagicalArmor_SpecificOnReroll(string power, double specificThreshold, int rollThreshold)
        {
            mockPercentileSelector.Setup(s => s.SelectFrom(powerTableName)).Returns(power);

            mockDice.Setup(d => d.Roll(1).d(100).AsTrueOrFalse(rollThreshold)).Returns(true);
            mockDice.SetupSequence(d => d.Roll(1).d(100).AsTrueOrFalse(specificThreshold)).Returns(false).Returns(true);

            mockCollectionsSelector.Setup(s => s.SelectRandomFrom(ProficientSet("specific armor", "specific metal armor"))).Returns("my random armor");
            mockMagicalArmorGenerator.Setup(g => g.Generate(power, "my random armor", race.Size)).Returns(magicalArmor);

            var armor = armorGenerator.GenerateArmorFrom(feats, characterClass, race);
            Assert.That(armor, Is.EqualTo(magicalArmor));
        }

        [TestCase(PowerConstants.Minor, 0.98)]
        [TestCase(PowerConstants.Medium, 0.97)]
        [TestCase(PowerConstants.Major, 0.97)]
        public void GenerateArmorFrom_GenerateMagicalArmor_NotSpecific_IfNoSpecificAvailable(string power, double specificThreshold)
        {
            mockPercentileSelector.Setup(s => s.SelectFrom(powerTableName)).Returns(power);

            proficientArmors.Remove("specific armor");
            proficientArmors.Remove("specific metal armor");

            mockDice.Setup(d => d.Roll(1).d(100).AsTrueOrFalse(specificThreshold)).Returns(true);

            mockCollectionsSelector.Setup(s => s.SelectRandomFrom(ProficientSet("my armor", "other armor", "metal armor"))).Returns("my random armor");
            mockMagicalArmorGenerator.Setup(g => g.Generate(power, "my random armor", race.Size)).Returns(magicalArmor);

            var armor = armorGenerator.GenerateArmorFrom(feats, characterClass, race);
            Assert.That(armor, Is.EqualTo(magicalArmor));
        }

        [TestCase(false, false)]
        [TestCase(false, true)]
        [TestCase(true, false)]
        public void GenerateArmorFrom_GenerateMagicalArmor_DruidAndNonmetal_ByRoll(bool specialMaterial, bool dragonhide)
        {
            characterClass.Name = CharacterClassConstants.Druid;

            mockDice.Setup(d => d.Roll(1).d(100).AsTrueOrFalse(.95)).Returns(specialMaterial);
            mockDice.Setup(d => d.Roll(1).d(3).AsTrueOrFalse(3)).Returns(dragonhide);

            mockCollectionsSelector.Setup(s => s.SelectRandomFrom(ProficientSet("my armor", "other armor"))).Returns("my random armor");
            mockMagicalArmorGenerator.Setup(g => g.Generate(power, "my random armor", race.Size)).Returns(magicalArmor);

            var armor = armorGenerator.GenerateArmorFrom(feats, characterClass, race);
            Assert.That(armor, Is.EqualTo(magicalArmor));
        }

        [TestCase(PowerConstants.Minor, 0.98, false, false)]
        [TestCase(PowerConstants.Minor, 0.98, false, true)]
        [TestCase(PowerConstants.Minor, 0.98, true, false)]
        [TestCase(PowerConstants.Medium, 0.97, false, false)]
        [TestCase(PowerConstants.Medium, 0.97, false, true)]
        [TestCase(PowerConstants.Medium, 0.97, true, false)]
        [TestCase(PowerConstants.Major, 0.97, false, false)]
        [TestCase(PowerConstants.Major, 0.97, false, true)]
        [TestCase(PowerConstants.Major, 0.97, true, false)]
        public void GenerateArmorFrom_GenerateMagicalArmor_DruidAndNonmetal_ByRoll_Specific(string power, double specificThreshold, bool specialMaterial, bool dragonhide)
        {
            mockPercentileSelector.Setup(s => s.SelectFrom(powerTableName)).Returns(power);

            mockDice.Setup(d => d.Roll(1).d(100).AsTrueOrFalse(specificThreshold)).Returns(true);

            characterClass.Name = CharacterClassConstants.Druid;

            mockDice.Setup(d => d.Roll(1).d(100).AsTrueOrFalse(.95)).Returns(specialMaterial);
            mockDice.Setup(d => d.Roll(1).d(3).AsTrueOrFalse(3)).Returns(dragonhide);

            mockCollectionsSelector.Setup(s => s.SelectRandomFrom(ProficientSet("specific armor"))).Returns("my random armor");
            mockMagicalArmorGenerator.Setup(g => g.Generate(power, "my random armor", race.Size)).Returns(magicalArmor);

            var armor = armorGenerator.GenerateArmorFrom(feats, characterClass, race);
            Assert.That(armor, Is.EqualTo(magicalArmor));
        }

        [TestCase(PowerConstants.Minor, 0.98, 92, false, false)]
        [TestCase(PowerConstants.Minor, 0.98, 92, false, true)]
        [TestCase(PowerConstants.Minor, 0.98, 92, true, false)]
        [TestCase(PowerConstants.Medium, 0.97, 64, false, false)]
        [TestCase(PowerConstants.Medium, 0.97, 64, false, true)]
        [TestCase(PowerConstants.Medium, 0.97, 64, true, false)]
        [TestCase(PowerConstants.Major, 0.97, 64, false, false)]
        [TestCase(PowerConstants.Major, 0.97, 64, false, true)]
        [TestCase(PowerConstants.Major, 0.97, 64, true, false)]
        public void GenerateArmorFrom_GenerateMagicalArmor_DruidAndNonmetal_ByRoll_SpecificOnReroll(
            string power,
            double specificThreshold,
            int rollThreshold,
            bool specialMaterial,
            bool dragonhide)
        {
            mockPercentileSelector.Setup(s => s.SelectFrom(powerTableName)).Returns(power);

            mockDice.Setup(d => d.Roll(1).d(100).AsTrueOrFalse(rollThreshold)).Returns(true);
            mockDice.SetupSequence(d => d.Roll(1).d(100).AsTrueOrFalse(specificThreshold)).Returns(false).Returns(true);

            characterClass.Name = CharacterClassConstants.Druid;

            mockDice.Setup(d => d.Roll(1).d(100).AsTrueOrFalse(.95)).Returns(specialMaterial);
            mockDice.Setup(d => d.Roll(1).d(3).AsTrueOrFalse(3)).Returns(dragonhide);

            mockCollectionsSelector.Setup(s => s.SelectRandomFrom(ProficientSet("specific armor"))).Returns("my random armor");
            mockMagicalArmorGenerator.Setup(g => g.Generate(power, "my random armor", race.Size)).Returns(magicalArmor);

            var armor = armorGenerator.GenerateArmorFrom(feats, characterClass, race);
            Assert.That(armor, Is.EqualTo(magicalArmor));
        }

        [TestCase(PowerConstants.Minor, 0.98, false, false)]
        [TestCase(PowerConstants.Minor, 0.98, false, true)]
        [TestCase(PowerConstants.Minor, 0.98, true, false)]
        [TestCase(PowerConstants.Medium, 0.97, false, false)]
        [TestCase(PowerConstants.Medium, 0.97, false, true)]
        [TestCase(PowerConstants.Medium, 0.97, true, false)]
        [TestCase(PowerConstants.Major, 0.97, false, false)]
        [TestCase(PowerConstants.Major, 0.97, false, true)]
        [TestCase(PowerConstants.Major, 0.97, true, false)]
        public void GenerateArmorFrom_GenerateMagicalArmor_DruidAndNonmetal_ByRoll_NotSpecific_WhenNoneAvailable(
            string power,
            double specificThreshold,
            bool specialMaterial,
            bool dragonhide)
        {
            proficientArmors.Remove("specific armor");
            //Not removing the metal specific armor, as it should be removed by not allowing metal

            mockDice.Setup(d => d.Roll(1).d(100).AsTrueOrFalse(specificThreshold)).Returns(true);

            characterClass.Name = CharacterClassConstants.Druid;

            mockDice.Setup(d => d.Roll(1).d(100).AsTrueOrFalse(.95)).Returns(specialMaterial);
            mockDice.Setup(d => d.Roll(1).d(3).AsTrueOrFalse(3)).Returns(dragonhide);

            mockCollectionsSelector.Setup(s => s.SelectRandomFrom(ProficientSet("my armor", "other armor"))).Returns("my random armor");
            mockMagicalArmorGenerator.Setup(g => g.Generate(power, "my random armor", race.Size)).Returns(magicalArmor);

            var armor = armorGenerator.GenerateArmorFrom(feats, characterClass, race);
            Assert.That(armor, Is.EqualTo(magicalArmor));
        }

        [Test]
        public void GenerateArmorFrom_GenerateMagicalArmor_DruidAndMetal()
        {
            characterClass.Name = CharacterClassConstants.Druid;

            mockDice.Setup(d => d.Roll(1).d(100).AsTrueOrFalse(.95)).Returns(true);
            mockDice.Setup(d => d.Roll(1).d(3).AsTrueOrFalse(3)).Returns(true);

            mockCollectionsSelector.Setup(s => s.SelectRandomFrom(ProficientSet("my armor", "other armor", "metal armor"))).Returns("metal armor");
            mockMagicalArmorGenerator.Setup(g => g.Generate(power, "metal armor", race.Size, TraitConstants.SpecialMaterials.Dragonhide)).Returns(magicalArmor);

            var armor = armorGenerator.GenerateArmorFrom(feats, characterClass, race);
            Assert.That(armor, Is.EqualTo(magicalArmor));
        }

        [TestCase(PowerConstants.Minor, 0.98)]
        [TestCase(PowerConstants.Medium, 0.97)]
        [TestCase(PowerConstants.Major, 0.97)]
        public void GenerateArmorFrom_GenerateMagicalArmor_DruidAndMetal_Specific(string power, double specificThreshold)
        {
            mockPercentileSelector.Setup(s => s.SelectFrom(powerTableName)).Returns(power);

            mockDice.Setup(d => d.Roll(1).d(100).AsTrueOrFalse(specificThreshold)).Returns(true);

            characterClass.Name = CharacterClassConstants.Druid;

            mockDice.Setup(d => d.Roll(1).d(100).AsTrueOrFalse(.95)).Returns(true);
            mockDice.Setup(d => d.Roll(1).d(3).AsTrueOrFalse(3)).Returns(true);

            mockCollectionsSelector.Setup(s => s.SelectRandomFrom(ProficientSet("specific armor", "specific metal armor"))).Returns("specific metal armor");
            //Can't apply Dragonhide to specific armor
            mockMagicalArmorGenerator.Setup(g => g.Generate(power, "specific metal armor", race.Size)).Returns(magicalArmor);

            var armor = armorGenerator.GenerateArmorFrom(feats, characterClass, race);
            Assert.That(armor, Is.EqualTo(magicalArmor));
        }

        [TestCase(PowerConstants.Minor, 0.98, 92)]
        [TestCase(PowerConstants.Medium, 0.97, 64)]
        [TestCase(PowerConstants.Major, 0.97, 64)]
        public void GenerateArmorFrom_GenerateMagicalArmor_DruidAndMetal_SpecificOnReroll(string power, double specificThreshold, int rollThreshold)
        {
            mockPercentileSelector.Setup(s => s.SelectFrom(powerTableName)).Returns(power);

            mockDice.Setup(d => d.Roll(1).d(100).AsTrueOrFalse(rollThreshold)).Returns(true);
            mockDice.SetupSequence(d => d.Roll(1).d(100).AsTrueOrFalse(specificThreshold)).Returns(false).Returns(true);

            characterClass.Name = CharacterClassConstants.Druid;

            mockDice.Setup(d => d.Roll(1).d(100).AsTrueOrFalse(.95)).Returns(true);
            mockDice.Setup(d => d.Roll(1).d(3).AsTrueOrFalse(3)).Returns(true);

            mockCollectionsSelector.Setup(s => s.SelectRandomFrom(ProficientSet("specific armor", "specific metal armor"))).Returns("specific metal armor");
            //Can't apply Dragonhide to specific armor
            mockMagicalArmorGenerator.Setup(g => g.Generate(power, "specific metal armor", race.Size)).Returns(magicalArmor);

            var armor = armorGenerator.GenerateArmorFrom(feats, characterClass, race);
            Assert.That(armor, Is.EqualTo(magicalArmor));
        }

        [TestCase(PowerConstants.Minor, 0.98)]
        [TestCase(PowerConstants.Medium, 0.97)]
        [TestCase(PowerConstants.Major, 0.97)]
        public void GenerateArmorFrom_GenerateMagicalArmor_DruidAndMetal_NotSpecific_WhenNoneAvailable(string power, double specificThreshold)
        {
            mockPercentileSelector.Setup(s => s.SelectFrom(powerTableName)).Returns(power);

            proficientArmors.Remove("specific armor");
            proficientArmors.Remove("specific metal armor");

            mockDice.Setup(d => d.Roll(1).d(100).AsTrueOrFalse(specificThreshold)).Returns(true);

            characterClass.Name = CharacterClassConstants.Druid;

            mockDice.Setup(d => d.Roll(1).d(100).AsTrueOrFalse(.95)).Returns(true);
            mockDice.Setup(d => d.Roll(1).d(3).AsTrueOrFalse(3)).Returns(true);

            mockCollectionsSelector.Setup(s => s.SelectRandomFrom(ProficientSet("my armor", "other armor", "metal armor"))).Returns("metal armor");
            mockMagicalArmorGenerator.Setup(g => g.Generate(power, "metal armor", race.Size, TraitConstants.SpecialMaterials.Dragonhide)).Returns(magicalArmor);

            var armor = armorGenerator.GenerateArmorFrom(feats, characterClass, race);
            Assert.That(armor, Is.EqualTo(magicalArmor));
        }

        [Test]
        public void GenerateArmorFrom_GenerateMagicalArmor_DruidAndNonmetal()
        {
            characterClass.Name = CharacterClassConstants.Druid;

            mockDice.Setup(d => d.Roll(1).d(100).AsTrueOrFalse(.95)).Returns(true);
            mockDice.Setup(d => d.Roll(1).d(3).AsTrueOrFalse(3)).Returns(true);

            mockCollectionsSelector.Setup(s => s.SelectRandomFrom(ProficientSet("my armor", "other armor", "metal armor"))).Returns("my random armor");
            mockMagicalArmorGenerator.Setup(g => g.Generate(power, "my random armor", race.Size)).Returns(magicalArmor);

            var armor = armorGenerator.GenerateArmorFrom(feats, characterClass, race);
            Assert.That(armor, Is.EqualTo(magicalArmor));
        }

        [Test]
        public void IfMagicalArmorDoesNotContainMetalAndClassIsDruid_Keep()
        {
            characterClass.Name = CharacterClassConstants.Druid;
            var wrongMagicalArmor = CreateArmor("wrong magical armor");
            mockCollectionsSelector.Setup(s => s.SelectRandomFrom(ProficientArmorSet())).Returns("my random armor");
            mockMagicalArmorGenerator.SetupSequence(g => g.Generate(power, "my random armor"))
                .Returns(magicalArmor).Returns(wrongMagicalArmor);

            magicalArmor.Attributes = wrongMagicalArmor.Attributes.Union(new[] { "other attribute" });

            var armor = armorGenerator.GenerateArmorFrom(feats, characterClass, race);
            Assert.That(armor, Is.EqualTo(magicalArmor));
        }

        [Test]
        public void IfMagicalArmorDoesNotContainMetalAndClassIsNotDruid_Keep()
        {
            var wrongMagicalArmor = CreateArmor("wrong magical armor");
            mockCollectionsSelector.Setup(s => s.SelectRandomFrom(ProficientArmorSet())).Returns("my random armor");
            mockMagicalArmorGenerator.SetupSequence(g => g.Generate(power, "my random armor"))
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

            mockPercentileSelector.Setup(s => s.SelectFrom(powerTableName)).Returns(PowerConstants.Mundane);
            mockCollectionsSelector.Setup(s => s.SelectRandomFrom(ProficientArmorSet())).Returns("my random armor");
            mockMundaneArmorGenerator.SetupSequence(g => g.Generate("my random armor", race.Size))
                .Returns(wrongMundaneArmor).Returns(otherWrongMundaneArmor).Returns(mundaneArmor);

            wrongMundaneArmor.Size = "bigger size";
            otherWrongMundaneArmor.Size = "smaller size";

            var armor = armorGenerator.GenerateArmorFrom(feats, characterClass, race);
            Assert.That(armor, Is.EqualTo(mundaneArmor));
        }

        [Test]
        public void MagicalArmorMustFitCharacter()
        {
            var magicalArmor = CreateArmor("mundane armor");
            var wrongMagicalArmor = CreateArmor("wrong mundane armor");
            var otherWrongMagicalArmor = CreateArmor("other wrong mundane armor");

            mockCollectionsSelector.Setup(s => s.SelectRandomFrom(ProficientArmorSet())).Returns("my random armor");
            mockMagicalArmorGenerator.SetupSequence(g => g.Generate(power, "my random armor", race.Size))
                .Returns(wrongMagicalArmor).Returns(otherWrongMagicalArmor).Returns(magicalArmor);

            wrongMagicalArmor.Size = "bigger size";
            otherWrongMagicalArmor.Size = "smaller size";

            var armor = armorGenerator.GenerateArmorFrom(feats, characterClass, race);
            Assert.That(armor, Is.EqualTo(magicalArmor));
        }

        [Test]
        public void GenerateMundaneDefaultArmor()
        {
            var wrongArmor = CreateArmor("wrong armor");
            wrongArmor.Size = "wrong size";
            proficientArmors.Add(ArmorConstants.HideArmor);

            mockPercentileSelector.Setup(s => s.SelectFrom(powerTableName)).Returns(PowerConstants.Mundane);
            mockCollectionsSelector.Setup(s => s.SelectRandomFrom(ProficientArmorSet())).Returns("my random armor");
            mockMundaneArmorGenerator.Setup(g => g.Generate("my random armor", race.Size)).Returns(wrongArmor);

            var defaultArmor = new Armor();
            defaultArmor.Name = ArmorConstants.HideArmor;
            mockMundaneArmorGenerator
                .Setup(g => g.Generate(It.Is<Armor>(i => i.ItemType == ItemTypeConstants.Armor && i.Name == ArmorConstants.HideArmor && i.Size == race.Size), true))
                .Returns(defaultArmor);

            var armor = armorGenerator.GenerateArmorFrom(feats, characterClass, race);
            Assert.That(armor, Is.EqualTo(defaultArmor), armor.Name);
        }

        [Test]
        public void GenerateMagicalDefaultArmor()
        {
            var wrongMagicalArmor = CreateArmor("wrong magical armor");
            wrongMagicalArmor.Size = "wrong size";
            proficientArmors.Add(ArmorConstants.HideArmor);

            mockCollectionsSelector.Setup(s => s.SelectRandomFrom(ProficientArmorSet())).Returns("my random armor");
            mockMagicalArmorGenerator.Setup(g => g.Generate(power, "my random armor", race.Size)).Returns(wrongMagicalArmor);

            var defaultArmor = new Armor();
            defaultArmor.Name = ArmorConstants.HideArmor;
            mockMagicalArmorGenerator.Setup(g => g.Generate(It.Is<Armor>(i => i.ItemType == ItemTypeConstants.Armor && i.Name == ArmorConstants.HideArmor && i.Magic.Bonus == 1), true))
                .Returns(defaultArmor);

            var armor = armorGenerator.GenerateArmorFrom(feats, characterClass, race);
            Assert.That(armor, Is.EqualTo(defaultArmor));
        }

        [Test]
        public void GetOnlyStandardMundaneDefaultArmor()
        {
            var wrongArmor = CreateArmor("wrong armor");
            wrongArmor.Size = "wrong size";

            proficientArmors.Add(ArmorConstants.HideArmor);
            proficientArmors.Add(ArmorConstants.DemonArmor);

            mockCollectionsSelector.Setup(s => s.SelectRandomFrom(ProficientArmorSet())).Returns("my random armor");
            mockMundaneArmorGenerator.Setup(g => g.Generate("my random armor", race.Size)).Returns(wrongArmor);
            mockPercentileSelector.Setup(s => s.SelectFrom(powerTableName)).Returns(PowerConstants.Mundane);

            var defaultArmor = new Armor();
            defaultArmor.Name = ArmorConstants.HideArmor;
            mockMundaneArmorGenerator.Setup(g => g.Generate(It.Is<Armor>(i => i.ItemType == ItemTypeConstants.Armor && i.Name == ArmorConstants.HideArmor && i.Size == race.Size), true))
                .Returns(defaultArmor);

            var armor = armorGenerator.GenerateArmorFrom(feats, characterClass, race);
            Assert.That(armor, Is.EqualTo(defaultArmor));
        }

        [Test]
        public void GetOnlyStandardMagicalDefaultArmor()
        {
            var wrongMagicalArmor = CreateArmor("wrong magical armor");
            wrongMagicalArmor.Size = "wrong size";

            proficientArmors.Add(ArmorConstants.HideArmor);

            mockCollectionsSelector.Setup(s => s.SelectRandomFrom(ProficientArmorSet())).Returns("my random armor");
            mockMagicalArmorGenerator.Setup(g => g.Generate(power, "my random armor", race.Size)).Returns(wrongMagicalArmor);

            var defaultArmor = new Armor();
            defaultArmor.Name = ArmorConstants.HideArmor;
            mockMagicalArmorGenerator.Setup(g => g.Generate(It.Is<Armor>(i => i.ItemType == ItemTypeConstants.Armor && i.Name == ArmorConstants.HideArmor && i.Magic.Bonus == 1), true))
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
            mockPercentileSelector.Setup(s => s.SelectFrom(powerTableName)).Returns(PowerConstants.Mundane);
            mockCollectionsSelector.Setup(s => s.SelectRandomFrom(ProficientShieldSet())).Returns("my random shield");
            mockMundaneArmorGenerator.Setup(g => g.Generate("my random shield", race.Size)).Returns(mundaneShield);

            var shield = armorGenerator.GenerateShieldFrom(feats, characterClass, race);
            Assert.That(shield, Is.EqualTo(mundaneShield), shield.Name);
        }

        private Armor CreateShield(string name)
        {
            var shield = new Armor();
            shield.Name = name;
            shield.ItemType = ItemTypeConstants.Armor;
            shield.Attributes = new[] { AttributeConstants.Shield };
            shield.Size = race.Size;

            proficientShields.Add(name);

            return shield;
        }

        [Test]
        public void IfNotMundaneShield_Regenerate()
        {
            var mundaneShield = CreateShield("mundane shield");
            var wrongMundaneShield = new Item { Name = "wrong mundane shield", ItemType = ItemTypeConstants.Armor };
            wrongMundaneShield.Attributes = mundaneShield.Attributes;

            mockPercentileSelector.Setup(s => s.SelectFrom(powerTableName)).Returns(PowerConstants.Mundane);
            mockCollectionsSelector.Setup(s => s.SelectRandomFrom(ProficientShieldSet())).Returns("my random shield");
            mockMundaneArmorGenerator.SetupSequence(g => g.Generate("my random shield", race.Size)).Returns(wrongMundaneShield).Returns(mundaneShield);

            var shield = armorGenerator.GenerateShieldFrom(feats, characterClass, race);
            Assert.That(shield, Is.EqualTo(mundaneShield), shield.Name);
        }

        [Test]
        public void UseCumulativeShieldProficiencies()
        {
            feats.Add(new Feat { Name = "heavy proficiency" });

            var otherShield = CreateShield("other shield");
            shieldProficiencyFeats.Add("heavy proficiency");
            proficientShields.Remove("other shield");

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.ItemGroups, feats[2].Name))
                .Returns(new[] { otherShield.Name });

            mockCollectionsSelector.Setup(s => s.SelectRandomFrom(ProficientShieldSet())).Returns("my random shield");
            mockMagicalArmorGenerator.SetupSequence(g => g.Generate(power, "my random shield", race.Size))
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
            mockPercentileSelector.Setup(s => s.SelectFrom(powerTableName)).Returns(PowerConstants.Mundane);
            mockCollectionsSelector.Setup(s => s.SelectRandomFrom(ProficientShieldSet())).Returns("my random shield");
            mockMundaneArmorGenerator.SetupSequence(g => g.Generate("my random shield", race.Size)).Returns(wrongMundaneShield).Returns(mundaneShield);

            wrongMundaneShield.Attributes = wrongMundaneShield.Attributes.Union(new[] { AttributeConstants.Metal });

            var shield = armorGenerator.GenerateShieldFrom(feats, characterClass, race);
            Assert.That(shield, Is.EqualTo(mundaneShield), shield.Name);
        }

        [Test]
        public void IfMundaneShieldContainsMetalAndClassIsNotDruid_Keep()
        {
            var mundaneShield = CreateShield("mundane shield");
            var wrongMundaneShield = CreateShield("wrong mundane shield");
            mockPercentileSelector.Setup(s => s.SelectFrom(powerTableName)).Returns(PowerConstants.Mundane);
            mockCollectionsSelector.Setup(s => s.SelectRandomFrom(ProficientShieldSet())).Returns("my random shield");
            mockMundaneArmorGenerator.SetupSequence(g => g.Generate("my random shield", race.Size)).Returns(mundaneShield).Returns(wrongMundaneShield);

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
            mockPercentileSelector.Setup(s => s.SelectFrom(powerTableName)).Returns(PowerConstants.Mundane);
            mockCollectionsSelector.Setup(s => s.SelectRandomFrom(ProficientShieldSet())).Returns("my random shield");
            mockMundaneArmorGenerator.SetupSequence(g => g.Generate("my random shield", race.Size)).Returns(mundaneShield).Returns(wrongMundaneShield);

            mundaneShield.Attributes = mundaneShield.Attributes.Union(new[] { "other attribute" });

            var shield = armorGenerator.GenerateShieldFrom(feats, characterClass, race);
            Assert.That(shield, Is.EqualTo(mundaneShield), shield.Name);
        }

        [Test]
        public void IfMundaneShieldDoesNotContainMetalAndClassIsNotDruid_Keep()
        {
            var mundaneShield = CreateShield("mundane shield");
            var wrongMundaneShield = CreateShield("wrong mundane shield");
            mockPercentileSelector.Setup(s => s.SelectFrom(powerTableName)).Returns(PowerConstants.Mundane);
            mockCollectionsSelector.Setup(s => s.SelectRandomFrom(ProficientShieldSet())).Returns("my random shield");
            mockMundaneArmorGenerator.SetupSequence(g => g.Generate("my random shield", race.Size)).Returns(mundaneShield).Returns(wrongMundaneShield);

            mundaneShield.Attributes = mundaneShield.Attributes.Union(new[] { "other attribute" });

            var shield = armorGenerator.GenerateShieldFrom(feats, characterClass, race);
            Assert.That(shield, Is.EqualTo(mundaneShield), shield.Name);
        }

        [Test]
        public void GenerateMagicalShield()
        {
            var shield = armorGenerator.GenerateShieldFrom(feats, characterClass, race);
            Assert.That(shield, Is.EqualTo(magicalShield), shield.Name);
        }

        [Test]
        public void IfMagicalShieldIsNotShield_Regenerate()
        {
            var wrongMagicalShield = new Item { Name = "wrong magical shield", ItemType = ItemTypeConstants.Armor };
            wrongMagicalShield.Attributes = new[] { AttributeConstants.Shield };

            mockCollectionsSelector.Setup(s => s.SelectRandomFrom(ProficientShieldSet())).Returns("my random shield");
            mockMagicalArmorGenerator.SetupSequence(g => g.Generate(power, "my random shield"))
                .Returns(wrongMagicalShield).Returns(magicalShield);

            var shield = armorGenerator.GenerateShieldFrom(feats, characterClass, race);
            Assert.That(shield, Is.EqualTo(magicalShield), shield.Name);
        }

        [Test]
        public void IfMagicalShieldContainsMetalAndClassIsDruid_Regenerate()
        {
            characterClass.Name = CharacterClassConstants.Druid;
            var wrongMagicalShield = CreateShield("wrong magical shield");
            wrongMagicalShield.Attributes = wrongMagicalShield.Attributes.Union(new[] { AttributeConstants.Metal });

            mockCollectionsSelector.Setup(s => s.SelectRandomFrom(ProficientShieldSet())).Returns("my random shield");
            mockMagicalArmorGenerator.SetupSequence(g => g.Generate(power, "my random shield"))
                .Returns(wrongMagicalShield).Returns(magicalShield);

            var shield = armorGenerator.GenerateShieldFrom(feats, characterClass, race);
            Assert.That(shield, Is.EqualTo(magicalShield), shield.Name);
        }

        [Test]
        public void IfMagicalShieldContainsMetalAndClassIsNotDruid_Keep()
        {
            var wrongMagicalShield = CreateShield("wrong magical shield");
            magicalShield.Attributes = wrongMagicalShield.Attributes.Union(new[] { AttributeConstants.Metal });

            mockCollectionsSelector.Setup(s => s.SelectRandomFrom(ProficientShieldSet())).Returns("my random shield");
            mockMagicalArmorGenerator.SetupSequence(g => g.Generate(power, "my random shield"))
                .Returns(magicalShield).Returns(wrongMagicalShield);

            var shield = armorGenerator.GenerateShieldFrom(feats, characterClass, race);
            Assert.That(shield, Is.EqualTo(magicalShield), shield.Name);
        }

        [Test]
        public void IfMagicalShieldDoesNotContainMetalAndClassIsDruid_Keep()
        {
            characterClass.Name = CharacterClassConstants.Druid;
            var wrongMagicalShield = CreateShield("wrong magical shield");
            mockCollectionsSelector.Setup(s => s.SelectRandomFrom(ProficientShieldSet())).Returns("my random shield");
            mockMagicalArmorGenerator.SetupSequence(g => g.Generate(power, "my random shield"))
                .Returns(magicalShield).Returns(wrongMagicalShield);

            magicalShield.Attributes = wrongMagicalShield.Attributes.Union(new[] { "other attribute" });

            var shield = armorGenerator.GenerateShieldFrom(feats, characterClass, race);
            Assert.That(shield, Is.EqualTo(magicalShield), shield.Name);
        }

        [Test]
        public void IfMagicalShieldDoesNotContainMetalAndClassIsNotDruid_Keep()
        {
            var wrongMagicalShield = CreateShield("wrong magical shield");
            mockCollectionsSelector.Setup(s => s.SelectRandomFrom(ProficientShieldSet())).Returns("my random shield");
            mockMagicalArmorGenerator.SetupSequence(g => g.Generate(power, "my random shield"))
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

            wrongMundaneShield.Size = "bigger size";
            otherWrongMundaneShield.Size = "smaller size";

            mockPercentileSelector.Setup(s => s.SelectFrom(powerTableName)).Returns(PowerConstants.Mundane);
            mockCollectionsSelector.Setup(s => s.SelectRandomFrom(ProficientShieldSet())).Returns("my random shield");
            mockMundaneArmorGenerator.SetupSequence(g => g.Generate("my random shield", race.Size))
                .Returns(wrongMundaneShield).Returns(otherWrongMundaneShield).Returns(mundaneShield);

            var shield = armorGenerator.GenerateShieldFrom(feats, characterClass, race);
            Assert.That(shield, Is.EqualTo(mundaneShield), shield.Name);
        }

        [Test]
        public void MagicalShieldMustFitCharacter()
        {
            var magicalShield = CreateShield("mundane shield");
            var wrongMagicalShield = CreateShield("wrong mundane shield");
            var otherWrongMagicalShield = CreateShield("other wrong mundane shield");

            wrongMagicalShield.Size = "bigger size";
            otherWrongMagicalShield.Size = "smaller size";

            mockCollectionsSelector.Setup(s => s.SelectRandomFrom(ProficientShieldSet())).Returns("my random shield");
            mockMagicalArmorGenerator.SetupSequence(g => g.Generate(power, "my random shield", race.Size))
                .Returns(wrongMagicalShield).Returns(otherWrongMagicalShield).Returns(magicalShield);

            var shield = armorGenerator.GenerateShieldFrom(feats, characterClass, race);
            Assert.That(shield, Is.EqualTo(magicalShield), shield.Name);
        }

        [Test]
        public void ShieldCannotBeArmor()
        {
            mockCollectionsSelector.Setup(s => s.SelectRandomFrom(ProficientShieldSet())).Returns("my random shield");
            mockMagicalArmorGenerator.SetupSequence(g => g.Generate(power, "my random shield"))
                .Returns(magicalArmor).Returns(magicalShield);

            var shield = armorGenerator.GenerateShieldFrom(feats, characterClass, race);
            Assert.That(shield, Is.EqualTo(magicalShield), shield.Name);
        }

        [Test]
        public void GenerateMundaneDefaultShield()
        {
            var wrongShield = CreateShield("wrong shield");
            wrongShield.Size = "wrong size";

            proficientShields.Add(ArmorConstants.HeavySteelShield);

            mockPercentileSelector.Setup(s => s.SelectFrom(powerTableName)).Returns(PowerConstants.Mundane);
            mockCollectionsSelector.Setup(s => s.SelectRandomFrom(ProficientShieldSet())).Returns("my random shield");
            mockMundaneArmorGenerator.Setup(g => g.Generate("my random shield", race.Size)).Returns(wrongShield);

            var defaultShield = new Armor();
            defaultShield.Name = ArmorConstants.HeavySteelShield;
            mockMundaneArmorGenerator.Setup(g => g.Generate(It.Is<Armor>(i => i.ItemType == ItemTypeConstants.Armor && i.Name == ArmorConstants.HeavySteelShield && i.Size == race.Size), true))
                .Returns(defaultShield);

            var shield = armorGenerator.GenerateShieldFrom(feats, characterClass, race);
            Assert.That(shield, Is.EqualTo(defaultShield));
        }

        [Test]
        public void GenerateMagicalDefaultShield()
        {
            var wrongMagicalShield = CreateShield("wrong magical shield");
            wrongMagicalShield.Size = "wrong size";

            proficientShields.Add(ArmorConstants.HeavySteelShield);

            mockCollectionsSelector.Setup(s => s.SelectRandomFrom(ProficientShieldSet())).Returns("my random shield");
            mockMagicalArmorGenerator.Setup(g => g.Generate(power, "my random shield", race.Size)).Returns(wrongMagicalShield);

            var defaultShield = new Armor();
            defaultShield.Name = ArmorConstants.HeavySteelShield;
            mockMagicalArmorGenerator.Setup(g => g.Generate(It.Is<Armor>(i => i.ItemType == ItemTypeConstants.Armor && i.Name == ArmorConstants.HeavySteelShield && i.Magic.Bonus == 1), true))
                .Returns(defaultShield);

            var shield = armorGenerator.GenerateShieldFrom(feats, characterClass, race);
            Assert.That(shield, Is.EqualTo(defaultShield), shield.Name);
        }

        [Test]
        public void GetOnlyStandardMundaneDefaultShield()
        {
            var wrongShield = CreateShield("wrong shield");
            wrongShield.Size = "wrong size";

            proficientShields.Add(ArmorConstants.HeavySteelShield);

            mockCollectionsSelector.Setup(s => s.SelectRandomFrom(ProficientShieldSet())).Returns("my random shield");
            mockMundaneArmorGenerator.Setup(g => g.Generate("my random shield", race.Size)).Returns(wrongShield);
            mockPercentileSelector.Setup(s => s.SelectFrom(powerTableName)).Returns(PowerConstants.Mundane);

            var defaultShield = new Armor();
            defaultShield.Name = ArmorConstants.HeavySteelShield;
            mockMundaneArmorGenerator.Setup(g => g.Generate(It.Is<Armor>(i => i.ItemType == ItemTypeConstants.Armor && i.Name == ArmorConstants.HeavySteelShield && i.Size == race.Size), true))
                .Returns(defaultShield);

            var shield = armorGenerator.GenerateShieldFrom(feats, characterClass, race);
            Assert.That(shield, Is.EqualTo(defaultShield));
        }

        [Test]
        public void GetOnlyStandardMagicalDefaultShield()
        {
            var wrongMagicalShield = CreateShield("wrong magical shield");
            wrongMagicalShield.Size = "wrong size";

            proficientShields.Add(ArmorConstants.HeavySteelShield);

            mockCollectionsSelector.Setup(s => s.SelectRandomFrom(ProficientShieldSet())).Returns("my random shield");
            mockMagicalArmorGenerator.Setup(g => g.Generate(power, "my random shield", race.Size)).Returns(wrongMagicalShield);

            var defaultShield = new Armor();
            defaultShield.Name = ArmorConstants.HeavySteelShield;
            mockMagicalArmorGenerator.Setup(g => g.Generate(It.Is<Armor>(i => i.ItemType == ItemTypeConstants.Armor && i.Name == ArmorConstants.HeavySteelShield && i.Magic.Bonus == 1), true))
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
            mockCollectionsSelector.Setup(s => s.SelectRandomFrom(ProficientShieldSet())).Returns("my random shield");
            mockMagicalArmorGenerator.Setup(g => g.Generate("npc power", "my random shield", race.Size)).Returns(npcArmor);

            var weapon = armorGenerator.GenerateArmorFrom(feats, characterClass, race);
            Assert.That(weapon, Is.EqualTo(npcArmor));
        }

        [Test]
        public void LevelAdjustmentAffectsNPCLevelForArmor()
        {
            characterClass.Level = 9266;
            characterClass.LevelAdjustment = 90210;
            characterClass.Name = "class name";
            characterClass.IsNPC = true;

            var npcArmor = CreateArmor("npc armor");

            var tableName = string.Format(TableNameConstants.Formattable.Percentile.LevelXPower, characterClass.EffectiveLevel);
            mockPercentileSelector.Setup(s => s.SelectFrom(tableName)).Returns("npc power");
            mockCollectionsSelector.Setup(s => s.SelectRandomFrom(ProficientShieldSet())).Returns("my random shield");
            mockMagicalArmorGenerator.Setup(g => g.Generate("npc power", "my random shield", race.Size)).Returns(npcArmor);

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

            var playerArmor = CreateArmor("player armor");

            var tableName = string.Format(TableNameConstants.Formattable.Percentile.LevelXPower, effectiveLevel);
            mockPercentileSelector.Setup(s => s.SelectFrom(tableName)).Returns("player power");
            mockCollectionsSelector.Setup(s => s.SelectRandomFrom(ProficientShieldSet())).Returns("my random shield");
            mockMagicalArmorGenerator.Setup(g => g.Generate("player power", "my random shield", race.Size)).Returns(playerArmor);

            var weapon = armorGenerator.GenerateArmorFrom(feats, characterClass, race);
            Assert.That(weapon, Is.EqualTo(playerArmor));
        }

        [Test]
        public void LevelAdjustmentAffectsPlayerLevelForArmor()
        {
            characterClass.Level = 9266;
            characterClass.LevelAdjustment = 90210;
            characterClass.Name = "class name";
            characterClass.IsNPC = false;

            var playerArmor = CreateArmor("player armor");

            var tableName = string.Format(TableNameConstants.Formattable.Percentile.LevelXPower, characterClass.EffectiveLevel);
            mockPercentileSelector.Setup(s => s.SelectFrom(tableName)).Returns("player power");
            mockCollectionsSelector.Setup(s => s.SelectRandomFrom(ProficientShieldSet())).Returns("my random shield");
            mockMagicalArmorGenerator.Setup(g => g.Generate("player power", "my random shield", race.Size)).Returns(playerArmor);

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
            mockCollectionsSelector.Setup(s => s.SelectRandomFrom(ProficientShieldSet())).Returns("my random shield");
            mockMagicalArmorGenerator.Setup(g => g.Generate("npc power", "my random shield", race.Size)).Returns(npcShield);

            var weapon = armorGenerator.GenerateShieldFrom(feats, characterClass, race);
            Assert.That(weapon, Is.EqualTo(npcShield));
        }

        [Test]
        public void LevelAdjustmentAffectsNPCLevelForShield()
        {
            characterClass.Level = 9266;
            characterClass.LevelAdjustment = 90210;
            characterClass.Name = "class name";
            characterClass.IsNPC = true;

            var npcShield = CreateShield("npc shield");

            var tableName = string.Format(TableNameConstants.Formattable.Percentile.LevelXPower, characterClass.EffectiveLevel);
            mockPercentileSelector.Setup(s => s.SelectFrom(tableName)).Returns("npc power");
            mockCollectionsSelector.Setup(s => s.SelectRandomFrom(ProficientShieldSet())).Returns("my random shield");
            mockMagicalArmorGenerator.Setup(g => g.Generate("npc power", "my random shield", race.Size)).Returns(npcShield);

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
            mockCollectionsSelector.Setup(s => s.SelectRandomFrom(ProficientShieldSet())).Returns("my random shield");
            mockMagicalArmorGenerator.Setup(g => g.Generate("player power", "my random shield", race.Size)).Returns(playerShield);

            var weapon = armorGenerator.GenerateShieldFrom(feats, characterClass, race);
            Assert.That(weapon, Is.EqualTo(playerShield));
        }

        [Test]
        public void LevelAdjustmentAffectsPlayerLevelForShield()
        {
            characterClass.Level = 9266;
            characterClass.LevelAdjustment = 90210;
            characterClass.Name = "class name";
            characterClass.IsNPC = false;

            var playerShield = CreateShield("player shield");

            var tableName = string.Format(TableNameConstants.Formattable.Percentile.LevelXPower, characterClass.EffectiveLevel);
            mockPercentileSelector.Setup(s => s.SelectFrom(tableName)).Returns("player power");
            mockCollectionsSelector.Setup(s => s.SelectRandomFrom(ProficientShieldSet())).Returns("my random shield");
            mockMagicalArmorGenerator.Setup(g => g.Generate("player power", "my random shield", race.Size)).Returns(playerShield);

            var weapon = armorGenerator.GenerateShieldFrom(feats, characterClass, race);
            Assert.That(weapon, Is.EqualTo(playerShield));
        }
    }
}
