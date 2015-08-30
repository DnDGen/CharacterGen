using CharacterGen.Common.Abilities.Feats;
using CharacterGen.Common.CharacterClasses;
using CharacterGen.Common.Items;
using CharacterGen.Common.Races;
using CharacterGen.Generators.Domain.Items;
using CharacterGen.Generators.Items;
using CharacterGen.Selectors;
using CharacterGen.Tables;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using TreasureGen.Common.Items;
using TreasureGen.Generators.Items.Magical;
using TreasureGen.Generators.Items.Mundane;

namespace CharacterGen.Tests.Unit.Generators.Items
{
    [TestFixture]
    public class WeaponGeneratorTests
    {
        private GearGenerator weaponGenerator;
        private Mock<IPercentileSelector> mockPercentileSelector;
        private Mock<IMundaneItemGenerator> mockMundaneWeaponGenerator;
        private Mock<IMagicalItemGenerator> mockMagicalWeaponGenerator;
        private Mock<ICollectionsSelector> mockCollectionsSelector;
        private Item magicalWeapon;
        private List<Feat> feats;
        private List<String> proficiencyFeats;
        private CharacterClass characterClass;
        private List<String> baseWeaponTypes;
        private List<String> weapons;
        private List<String> allProficientWeapons;
        private Race race;

        [SetUp]
        public void Setup()
        {
            mockPercentileSelector = new Mock<IPercentileSelector>();
            mockMundaneWeaponGenerator = new Mock<IMundaneItemGenerator>();
            mockMagicalWeaponGenerator = new Mock<IMagicalItemGenerator>();
            mockCollectionsSelector = new Mock<ICollectionsSelector>();
            weaponGenerator = new WeaponGenerator(mockCollectionsSelector.Object, mockPercentileSelector.Object,
                mockMundaneWeaponGenerator.Object, mockMagicalWeaponGenerator.Object);
            magicalWeapon = new Item();
            feats = new List<Feat>();
            characterClass = new CharacterClass();
            proficiencyFeats = new List<String>();
            baseWeaponTypes = new List<String>();
            weapons = new List<String>();
            allProficientWeapons = new List<String>();
            race = new Race();

            magicalWeapon.Name = "magical weapon";
            magicalWeapon.ItemType = ItemTypeConstants.Weapon;
            characterClass.Level = 9266;
            baseWeaponTypes.Add("base weapon");
            weapons.Add(baseWeaponTypes[0]);
            weapons.Add("different weapon");
            feats.Add(new Feat { Name = "all proficiency", Focus = ProficiencyConstants.All });
            proficiencyFeats.Add(feats[0].Name);
            allProficientWeapons.Add(baseWeaponTypes[0]);
            race.Size = "size";

            mockPercentileSelector.Setup(s => s.SelectFrom("Level9266Power")).Returns("power");
            mockMagicalWeaponGenerator.Setup(g => g.GenerateAtPower("power")).Returns(magicalWeapon);
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.ItemGroups, It.IsAny<String>()))
                .Returns((String table, String name) => new[] { name });
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.FeatGroups, ItemTypeConstants.Weapon + GroupConstants.Proficiency))
                .Returns(proficiencyFeats);
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.ItemGroups, magicalWeapon.Name))
                .Returns(baseWeaponTypes);
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.ItemGroups, GroupConstants.Weapons))
                .Returns(weapons);
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.FeatFoci, feats[0].Name))
                .Returns(allProficientWeapons);
        }

        [Test]
        public void ThrowExceptionIfNoWeaponsAreAllowed()
        {
            allProficientWeapons.Clear();
            Assert.That(() => weaponGenerator.GenerateFrom(feats, characterClass, race), Throws.Exception);
        }

        [Test]
        public void GenerateMundaneWeapon()
        {
            var mundaneWeapon = CreateWeapon("mundane weapon");
            mockPercentileSelector.Setup(s => s.SelectFrom("Level9266Power")).Returns(PowerConstants.Mundane);
            mockMundaneWeaponGenerator.Setup(g => g.Generate()).Returns(mundaneWeapon);

            var weapon = weaponGenerator.GenerateFrom(feats, characterClass, race);
            Assert.That(weapon, Is.EqualTo(mundaneWeapon));
        }

        private Item CreateWeapon(String name)
        {
            var weapon = new Item();
            weapon.Name = name;
            weapon.ItemType = ItemTypeConstants.Weapon;
            weapon.Traits.Add(race.Size);

            weapons.Add(name);
            allProficientWeapons.Add(name);

            return weapon;
        }

        [Test]
        public void IfCannotWieldMundaneWeapon_Regenerate()
        {
            var mundaneWeapon = CreateWeapon("mundane weapon");
            var wrongMundaneWeapon = CreateWeapon("wrong mundane weapon");
            mockPercentileSelector.Setup(s => s.SelectFrom("Level9266Power")).Returns(PowerConstants.Mundane);
            mockMundaneWeaponGenerator.SetupSequence(g => g.Generate()).Returns(wrongMundaneWeapon).Returns(mundaneWeapon);

            allProficientWeapons.Remove(wrongMundaneWeapon.Name);

            var weapon = weaponGenerator.GenerateFrom(feats, characterClass, race);
            Assert.That(weapon, Is.EqualTo(mundaneWeapon));
        }

        [Test]
        public void IfNotAMundaneWeapon_Regenerate()
        {
            var mundaneWeapon = CreateWeapon("mundane weapon");
            var wrongMundaneWeapon = CreateWeapon("wrong mundane weapon");
            mockPercentileSelector.Setup(s => s.SelectFrom("Level9266Power")).Returns(PowerConstants.Mundane);
            mockMundaneWeaponGenerator.SetupSequence(g => g.Generate()).Returns(wrongMundaneWeapon).Returns(mundaneWeapon);

            wrongMundaneWeapon.ItemType = "not weapon";

            var weapon = weaponGenerator.GenerateFrom(feats, characterClass, race);
            Assert.That(weapon, Is.EqualTo(mundaneWeapon));
        }

        [Test]
        public void CanWieldSpecificMundaneWeaponProficiency()
        {
            var mundaneWeapon = CreateWeapon("mundane weapon");
            var wrongMundaneWeapon = CreateWeapon("wrong mundane weapon");
            mockPercentileSelector.Setup(s => s.SelectFrom("Level9266Power")).Returns(PowerConstants.Mundane);
            mockMundaneWeaponGenerator.SetupSequence(g => g.Generate()).Returns(mundaneWeapon).Returns(wrongMundaneWeapon);

            feats.Add(new Feat { Name = "feat2", Focus = mundaneWeapon.Name });
            proficiencyFeats.Add(feats[1].Name);

            var weapon = weaponGenerator.GenerateFrom(feats, characterClass, race);
            Assert.That(weapon, Is.EqualTo(mundaneWeapon));
        }

        [Test]
        public void PreferMundaneWeaponsPickedAsFocusForNonProficiencyFeats()
        {
            var mundaneWeapon = CreateWeapon("mundane weapon");
            var wrongMundaneWeapon = CreateWeapon("wrong mundane weapon");
            mockPercentileSelector.Setup(s => s.SelectFrom("Level9266Power")).Returns(PowerConstants.Mundane);
            mockMundaneWeaponGenerator.SetupSequence(g => g.Generate()).Returns(wrongMundaneWeapon).Returns(mundaneWeapon);

            feats.Add(new Feat { Name = "feat2", Focus = wrongMundaneWeapon.Name });
            feats.Add(new Feat { Name = "feat3", Focus = mundaneWeapon.Name });
            proficiencyFeats.Add(feats[1].Name);

            var weapon = weaponGenerator.GenerateFrom(feats, characterClass, race);
            Assert.That(weapon, Is.EqualTo(mundaneWeapon));
        }

        [Test]
        public void PreferAnyMundaneWeaponsPickedAsFocusForNonProficiencyFeats()
        {
            var mundaneWeapon = CreateWeapon("mundane weapon");
            var otherMundaneWeapon = CreateWeapon("other mundane weapon");
            var wrongMundaneWeapon = CreateWeapon("wrong mundane weapon");
            mockPercentileSelector.Setup(s => s.SelectFrom("Level9266Power")).Returns(PowerConstants.Mundane);
            mockMundaneWeaponGenerator.SetupSequence(g => g.Generate())
                .Returns(wrongMundaneWeapon).Returns(otherMundaneWeapon).Returns(mundaneWeapon);

            feats.Add(new Feat { Name = "feat2", Focus = wrongMundaneWeapon.Name });
            feats.Add(new Feat { Name = "feat3", Focus = mundaneWeapon.Name });
            feats.Add(new Feat { Name = "feat3", Focus = otherMundaneWeapon.Name });
            feats.Add(new Feat { Name = "feat4", Focus = mundaneWeapon.Name });
            proficiencyFeats.Add(feats[1].Name);

            var weapon = weaponGenerator.GenerateFrom(feats, characterClass, race);
            Assert.That(weapon, Is.EqualTo(otherMundaneWeapon));
        }

        [Test]
        public void PreferMundaneWeaponsPickedAsFocusForProficiencyFeats()
        {
            var mundaneWeapon = CreateWeapon("mundane weapon");
            var wrongMundaneWeapon = CreateWeapon("wrong mundane weapon");
            mockPercentileSelector.Setup(s => s.SelectFrom("Level9266Power")).Returns(PowerConstants.Mundane);
            mockMundaneWeaponGenerator.SetupSequence(g => g.Generate())
                .Returns(wrongMundaneWeapon).Returns(mundaneWeapon);

            feats.Add(new Feat { Name = "feat2", Focus = mundaneWeapon.Name });
            proficiencyFeats.Add(feats[1].Name);

            var weapon = weaponGenerator.GenerateFrom(feats, characterClass, race);
            Assert.That(weapon, Is.EqualTo(mundaneWeapon));
        }

        [Test]
        public void PreferAnyMundaneWeaponsPickedAsFocusForProficiencyFeats()
        {
            var mundaneWeapon = CreateWeapon("mundane weapon");
            var otherMundaneWeapon = CreateWeapon("other mundane weapon");
            var wrongMundaneWeapon = CreateWeapon("wrong mundane weapon");
            mockPercentileSelector.Setup(s => s.SelectFrom("Level9266Power")).Returns(PowerConstants.Mundane);
            mockMundaneWeaponGenerator.SetupSequence(g => g.Generate())
                .Returns(wrongMundaneWeapon).Returns(otherMundaneWeapon).Returns(mundaneWeapon);

            feats.Add(new Feat { Name = "feat2", Focus = mundaneWeapon.Name });
            feats.Add(new Feat { Name = "feat2", Focus = otherMundaneWeapon.Name });
            feats.Add(new Feat { Name = "feat3", Focus = mundaneWeapon.Name });
            proficiencyFeats.Add(feats[1].Name);
            proficiencyFeats.Add(feats[2].Name);
            proficiencyFeats.Add(feats[3].Name);

            var weapon = weaponGenerator.GenerateFrom(feats, characterClass, race);
            Assert.That(weapon, Is.EqualTo(otherMundaneWeapon));
        }

        [Test]
        public void NoPreferenceForMundaneWeapons()
        {
            var mundaneWeapon = CreateWeapon("mundane weapon");
            var wrongMundaneWeapon = CreateWeapon("wrong mundane weapon");
            mockPercentileSelector.Setup(s => s.SelectFrom("Level9266Power")).Returns(PowerConstants.Mundane);
            mockMundaneWeaponGenerator.SetupSequence(g => g.Generate())
                .Returns(mundaneWeapon).Returns(wrongMundaneWeapon);

            var weapon = weaponGenerator.GenerateFrom(feats, characterClass, race);
            Assert.That(weapon, Is.EqualTo(mundaneWeapon));
        }

        [Test]
        public void GenerateMagicalWeapon()
        {
            var weapon = weaponGenerator.GenerateFrom(feats, characterClass, race);
            Assert.That(weapon, Is.EqualTo(magicalWeapon));
        }

        [Test]
        public void IfCannotWieldMagicalWeapon_Regenerate()
        {
            var wrongMagicalWeapon = CreateWeapon("wrong magical weapon");
            mockMagicalWeaponGenerator.SetupSequence(g => g.GenerateAtPower("power"))
                .Returns(wrongMagicalWeapon).Returns(magicalWeapon);

            allProficientWeapons.Remove(wrongMagicalWeapon.Name);

            var weapon = weaponGenerator.GenerateFrom(feats, characterClass, race);
            Assert.That(weapon, Is.EqualTo(magicalWeapon));
        }

        [Test]
        public void IfMagicalWeaponIsNotWeapon_Regenerate()
        {
            var wrongMagicalWeapon = CreateWeapon("wrong magical weapon");
            mockMagicalWeaponGenerator.SetupSequence(g => g.GenerateAtPower("power"))
                .Returns(wrongMagicalWeapon).Returns(magicalWeapon);

            wrongMagicalWeapon.ItemType = "not weapon";

            var weapon = weaponGenerator.GenerateFrom(feats, characterClass, race);
            Assert.That(weapon, Is.EqualTo(magicalWeapon));
        }

        [Test]
        public void CanWieldSpecificMagicalWeaponProficiency()
        {
            var wrongMagicalWeapon = CreateWeapon("wrong magical weapon");
            mockMagicalWeaponGenerator.SetupSequence(g => g.GenerateAtPower("power"))
                .Returns(magicalWeapon).Returns(wrongMagicalWeapon);

            feats.Add(new Feat { Name = "feat2", Focus = baseWeaponTypes[0] });
            proficiencyFeats.Add(feats[1].Name);

            var weapon = weaponGenerator.GenerateFrom(feats, characterClass, race);
            Assert.That(weapon, Is.EqualTo(magicalWeapon));
        }

        [Test]
        public void PreferMagicalWeaponsPickedAsFocusForNonProficiencyFeats()
        {
            var wrongMagicalWeapon = CreateWeapon("wrong magical weapon");
            mockMagicalWeaponGenerator.SetupSequence(g => g.GenerateAtPower("power"))
                .Returns(wrongMagicalWeapon).Returns(magicalWeapon);

            feats.Add(new Feat { Name = "feat2", Focus = wrongMagicalWeapon.Name });
            feats.Add(new Feat { Name = "feat3", Focus = baseWeaponTypes[0] });
            proficiencyFeats.Add(feats[1].Name);

            var weapon = weaponGenerator.GenerateFrom(feats, characterClass, race);
            Assert.That(weapon, Is.EqualTo(magicalWeapon));
        }

        [Test]
        public void PreferAnyMagicalWeaponsPickedAsFocusForNonProficiencyFeats()
        {
            var otherMagicalWeapon = CreateWeapon("other magical weapon");
            var wrongMagicalWeapon = CreateWeapon("wrong magical weapon");
            mockMagicalWeaponGenerator.SetupSequence(g => g.GenerateAtPower("power"))
                .Returns(wrongMagicalWeapon).Returns(otherMagicalWeapon).Returns(magicalWeapon);

            feats.Add(new Feat { Name = "feat2", Focus = wrongMagicalWeapon.Name });
            feats.Add(new Feat { Name = "feat3", Focus = baseWeaponTypes[0] });
            feats.Add(new Feat { Name = "feat3", Focus = otherMagicalWeapon.Name });
            feats.Add(new Feat { Name = "feat4", Focus = baseWeaponTypes[0] });
            proficiencyFeats.Add(feats[1].Name);

            var weapon = weaponGenerator.GenerateFrom(feats, characterClass, race);
            Assert.That(weapon, Is.EqualTo(otherMagicalWeapon));
        }

        [Test]
        public void PreferMagicalWeaponsPickedAsFocusForProficiencyFeats()
        {
            var wrongMagicalWeapon = CreateWeapon("wrong magical weapon");
            mockMagicalWeaponGenerator.SetupSequence(g => g.GenerateAtPower("power"))
                .Returns(wrongMagicalWeapon).Returns(magicalWeapon);

            feats.Add(new Feat { Name = "feat2", Focus = baseWeaponTypes[0] });
            proficiencyFeats.Add(feats[1].Name);

            var weapon = weaponGenerator.GenerateFrom(feats, characterClass, race);
            Assert.That(weapon, Is.EqualTo(magicalWeapon));
        }

        [Test]
        public void PreferAnyMagicalWeaponsPickedAsFocusForProficiencyFeats()
        {
            var otherMagicalWeapon = CreateWeapon("other magical weapon");
            var wrongMagicalWeapon = CreateWeapon("wrong magical weapon");
            mockMagicalWeaponGenerator.SetupSequence(g => g.GenerateAtPower("power"))
                .Returns(wrongMagicalWeapon).Returns(otherMagicalWeapon).Returns(magicalWeapon);

            feats.Add(new Feat { Name = "feat2", Focus = baseWeaponTypes[0] });
            feats.Add(new Feat { Name = "feat2", Focus = otherMagicalWeapon.Name });
            feats.Add(new Feat { Name = "feat3", Focus = baseWeaponTypes[0] });
            proficiencyFeats.Add(feats[1].Name);
            proficiencyFeats.Add(feats[2].Name);
            proficiencyFeats.Add(feats[3].Name);

            var weapon = weaponGenerator.GenerateFrom(feats, characterClass, race);
            Assert.That(weapon, Is.EqualTo(otherMagicalWeapon));
        }

        [Test]
        public void NoPreferenceForMagicalWeapons()
        {
            var wrongMagicalWeapon = CreateWeapon("wrong magical weapon");
            mockMagicalWeaponGenerator.SetupSequence(g => g.GenerateAtPower("power"))
                .Returns(magicalWeapon).Returns(wrongMagicalWeapon);

            var weapon = weaponGenerator.GenerateFrom(feats, characterClass, race);
            Assert.That(weapon, Is.EqualTo(magicalWeapon));
        }

        [Test]
        public void MundaneWeaponMustFitCharacter()
        {
            var mundaneWeapon = CreateWeapon("mundane weapon");
            var wrongMundaneWeapon = CreateWeapon("wrong mundane weapon");
            var otherWrongMundaneWeapon = CreateWeapon("other wrong mundane weapon");
            mockPercentileSelector.Setup(s => s.SelectFrom("Level9266Power")).Returns(PowerConstants.Mundane);
            mockMundaneWeaponGenerator.SetupSequence(g => g.Generate())
                .Returns(wrongMundaneWeapon).Returns(otherWrongMundaneWeapon).Returns(mundaneWeapon);

            wrongMundaneWeapon.Traits.Clear();
            wrongMundaneWeapon.Traits.Add("bigger size");
            otherWrongMundaneWeapon.Traits.Clear();
            otherWrongMundaneWeapon.Traits.Add("smaller size");

            var weapon = weaponGenerator.GenerateFrom(feats, characterClass, race);
            Assert.That(weapon, Is.EqualTo(mundaneWeapon));
        }
    }
}
