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
            weaponGenerator = new WeaponGenerator(mockCollectionsSelector.Object, mockPercentileSelector.Object, mockMundaneWeaponGenerator.Object, mockMagicalWeaponGenerator.Object);
            magicalWeapon = new Item();
            feats = new List<Feat>();
            characterClass = new CharacterClass();
            proficiencyFeats = new List<String>();
            baseWeaponTypes = new List<String>();
            weapons = new List<String>();
            allProficientWeapons = new List<String>();
            race = new Race();

            race.Size = "size";
            race.BaseRace = "base race";
            magicalWeapon = CreateWeapon("magical weapon");
            magicalWeapon.IsMagical = true;
            characterClass.ClassName = "class name";
            characterClass.Level = 9266;
            baseWeaponTypes.Add("base weapon");
            weapons.Remove(magicalWeapon.Name);
            weapons.Add(baseWeaponTypes[0]);
            weapons.Add("different weapon");
            feats.Add(new Feat { Name = "all proficiency", Focus = FeatConstants.Foci.All });
            proficiencyFeats.Add(feats[0].Name);
            allProficientWeapons.Remove(magicalWeapon.Name);
            allProficientWeapons.Add(baseWeaponTypes[0]);

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
            feats.Add(new Feat { Name = "feat 1" });
            feats.Add(new Feat { Name = "feat 2", Focus = "focus" });

            Assert.That(() => weaponGenerator.GenerateFrom(feats, characterClass, race), Throws.ArgumentException.With.Message.ContainsSubstring("No weapons are allowed, which should never happen"));
            Assert.That(() => weaponGenerator.GenerateFrom(feats, characterClass, race), Throws.ArgumentException.With.Message.ContainsSubstring("Class: class name"));
            Assert.That(() => weaponGenerator.GenerateFrom(feats, characterClass, race), Throws.ArgumentException.With.Message.ContainsSubstring("Race: base race"));
            Assert.That(() => weaponGenerator.GenerateFrom(feats, characterClass, race), Throws.ArgumentException.With.Message.ContainsSubstring("Feats: all proficiency (All), feat 1 (), feat 2 (focus)"));
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
        public void PreferAnyMundaneAmmunitionsForMundaneWeaponsPickedAsFocusForNonProficiencyFeats()
        {
            var mundaneWeapon = CreateWeapon("mundane weapon");
            var otherMundaneWeapon = CreateWeapon("other mundane weapon");
            var otherAmmunition = CreateWeapon("other ammunition");
            var wrongMundaneWeapon = CreateWeapon("wrong mundane weapon");
            mockPercentileSelector.Setup(s => s.SelectFrom("Level9266Power")).Returns(PowerConstants.Mundane);
            mockMundaneWeaponGenerator.SetupSequence(g => g.Generate())
                .Returns(wrongMundaneWeapon).Returns(otherAmmunition).Returns(otherMundaneWeapon).Returns(mundaneWeapon);

            feats.Add(new Feat { Name = "feat2", Focus = wrongMundaneWeapon.Name });
            feats.Add(new Feat { Name = "feat3", Focus = mundaneWeapon.Name });
            feats.Add(new Feat { Name = "feat3", Focus = otherMundaneWeapon.Name });
            feats.Add(new Feat { Name = "feat4", Focus = mundaneWeapon.Name });
            proficiencyFeats.Add(feats[1].Name);

            allProficientWeapons.Remove(otherAmmunition.Name);

            var baseWeaponTypes = new[] { otherMundaneWeapon.Name, otherAmmunition.Name };
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.ItemGroups, otherMundaneWeapon.Name)).Returns(baseWeaponTypes);

            var weapon = weaponGenerator.GenerateFrom(feats, characterClass, race);
            Assert.That(weapon, Is.EqualTo(otherAmmunition));
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
        public void PreferAnyMundaneAmmunitionForMundaneWeaponsPickedAsFocusForProficiencyFeats()
        {
            var mundaneWeapon = CreateWeapon("mundane weapon");
            var otherMundaneWeapon = CreateWeapon("other mundane weapon");
            var otherAmmunition = CreateWeapon("other ammunition");
            var wrongMundaneWeapon = CreateWeapon("wrong mundane weapon");
            mockPercentileSelector.Setup(s => s.SelectFrom("Level9266Power")).Returns(PowerConstants.Mundane);
            mockMundaneWeaponGenerator.SetupSequence(g => g.Generate())
                .Returns(wrongMundaneWeapon).Returns(otherAmmunition).Returns(otherMundaneWeapon).Returns(mundaneWeapon);

            feats.Add(new Feat { Name = "feat2", Focus = mundaneWeapon.Name });
            feats.Add(new Feat { Name = "feat2", Focus = otherMundaneWeapon.Name });
            feats.Add(new Feat { Name = "feat3", Focus = mundaneWeapon.Name });
            proficiencyFeats.Add(feats[1].Name);
            proficiencyFeats.Add(feats[2].Name);
            proficiencyFeats.Add(feats[3].Name);

            allProficientWeapons.Remove(otherAmmunition.Name);

            var baseWeaponTypes = new[] { otherMundaneWeapon.Name, otherAmmunition.Name };
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.ItemGroups, otherMundaneWeapon.Name)).Returns(baseWeaponTypes);

            var weapon = weaponGenerator.GenerateFrom(feats, characterClass, race);
            Assert.That(weapon, Is.EqualTo(otherAmmunition));
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
        public void PreferAnyMagicalAmmunitionForMagicalWeaponsPickedAsFocusForNonProficiencyFeats()
        {
            var otherMagicalWeapon = CreateWeapon("other magical weapon");
            var otherAmmunition = CreateWeapon("other magical ammunition");
            var wrongMagicalWeapon = CreateWeapon("wrong magical weapon");
            mockMagicalWeaponGenerator.SetupSequence(g => g.GenerateAtPower("power"))
                .Returns(wrongMagicalWeapon).Returns(otherAmmunition).Returns(otherMagicalWeapon).Returns(magicalWeapon);

            feats.Add(new Feat { Name = "feat2", Focus = wrongMagicalWeapon.Name });
            feats.Add(new Feat { Name = "feat3", Focus = baseWeaponTypes[0] });
            feats.Add(new Feat { Name = "feat3", Focus = otherMagicalWeapon.Name });
            feats.Add(new Feat { Name = "feat4", Focus = baseWeaponTypes[0] });
            proficiencyFeats.Add(feats[1].Name);

            allProficientWeapons.Remove(otherAmmunition.Name);

            var otherBaseWeaponTypes = new[] { otherMagicalWeapon.Name, otherAmmunition.Name };
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.ItemGroups, otherMagicalWeapon.Name)).Returns(otherBaseWeaponTypes);

            var weapon = weaponGenerator.GenerateFrom(feats, characterClass, race);
            Assert.That(weapon, Is.EqualTo(otherAmmunition));
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
        public void PreferAnyMagicalAmmunitionForMagicalWeaponsPickedAsFocusForProficiencyFeats()
        {
            var otherMagicalWeapon = CreateWeapon("other magical weapon");
            var otherAmmunition = CreateWeapon("other magical ammunition");
            var wrongMagicalWeapon = CreateWeapon("wrong magical weapon");
            mockMagicalWeaponGenerator.SetupSequence(g => g.GenerateAtPower("power"))
                .Returns(wrongMagicalWeapon).Returns(otherAmmunition).Returns(otherMagicalWeapon).Returns(magicalWeapon);

            feats.Add(new Feat { Name = "feat2", Focus = baseWeaponTypes[0] });
            feats.Add(new Feat { Name = "feat2", Focus = otherMagicalWeapon.Name });
            feats.Add(new Feat { Name = "feat3", Focus = baseWeaponTypes[0] });
            proficiencyFeats.Add(feats[1].Name);
            proficiencyFeats.Add(feats[2].Name);
            proficiencyFeats.Add(feats[3].Name);

            allProficientWeapons.Remove(otherAmmunition.Name);

            var otherBaseWeaponTypes = new[] { otherMagicalWeapon.Name, otherAmmunition.Name };
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.ItemGroups, otherMagicalWeapon.Name)).Returns(otherBaseWeaponTypes);

            var weapon = weaponGenerator.GenerateFrom(feats, characterClass, race);
            Assert.That(weapon, Is.EqualTo(otherAmmunition));
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

        [Test]
        public void MagicalWeaponDoesNotHaveToFitCharacter()
        {
            var wrongMagicalWeapon = CreateWeapon("wrong magical weapon");
            mockMagicalWeaponGenerator.SetupSequence(g => g.GenerateAtPower("power"))
                .Returns(magicalWeapon).Returns(wrongMagicalWeapon);

            race.Size = "other size";

            var weapon = weaponGenerator.GenerateFrom(feats, characterClass, race);
            Assert.That(weapon, Is.EqualTo(magicalWeapon));
        }

        [Test]
        public void IfCannotMakeAppropriateWeaponWithinIterativeBuilder_MakeMundaneWeaponWithIterativeBuilder()
        {
            var wrongMagicalWeapon = CreateWeapon("wrong magical weapon");
            mockMagicalWeaponGenerator.Setup(g => g.GenerateAtPower("power")).Returns(wrongMagicalWeapon);
            allProficientWeapons.Remove(wrongMagicalWeapon.Name);

            var mundaneWeapon = CreateWeapon("mundane weapon");
            var wrongMundaneWeapon = CreateWeapon("wrong mundane weapon");
            var otherWrongMundaneWeapon = CreateWeapon("other wrong mundane weapon");
            mockMundaneWeaponGenerator.SetupSequence(g => g.Generate()).Returns(wrongMundaneWeapon).Returns(otherWrongMundaneWeapon).Returns(mundaneWeapon);

            allProficientWeapons.Remove(wrongMundaneWeapon.Name);
            otherWrongMundaneWeapon.Traits.Add("smaller size");
            otherWrongMundaneWeapon.Traits.Remove(race.Size);

            var weapon = weaponGenerator.GenerateFrom(feats, characterClass, race);
            Assert.That(weapon, Is.EqualTo(mundaneWeapon), weapon.Name);
        }

        [Test]
        public void IfCannotMakeAppropriateMundaneWeaponWithIterativeBuilder_GiveThemAClub()
        {
            var wrongMagicalWeapon = CreateWeapon("wrong magical weapon");
            mockMagicalWeaponGenerator.Setup(g => g.GenerateAtPower("power")).Returns(wrongMagicalWeapon);
            allProficientWeapons.Remove(wrongMagicalWeapon.Name);

            var wrongMundaneWeapon = CreateWeapon("wrong mundane weapon");
            mockMundaneWeaponGenerator.Setup(g => g.Generate()).Returns(wrongMundaneWeapon);
            allProficientWeapons.Remove(wrongMundaneWeapon.Name);

            var weapon = weaponGenerator.GenerateFrom(feats, characterClass, race);
            Assert.That(weapon.Name, Is.EqualTo(WeaponConstants.Club));
            Assert.That(weapon.Attributes, Contains.Item(AttributeConstants.Bludgeoning));
            Assert.That(weapon.Attributes, Contains.Item(AttributeConstants.Common));
            Assert.That(weapon.Attributes, Contains.Item(AttributeConstants.Melee));
            Assert.That(weapon.Attributes, Contains.Item(AttributeConstants.Wood));
            Assert.That(weapon.Contents, Is.Empty);
            Assert.That(weapon.IsMagical, Is.False);
            Assert.That(weapon.ItemType, Is.EqualTo(ItemTypeConstants.Weapon));
            Assert.That(weapon.Quantity, Is.EqualTo(1));
            Assert.That(weapon.Traits, Contains.Item(race.Size));
        }

        [Test]
        public void SaveBonusesOfAllDoNotCountAsProficiencyFeats()
        {
            feats.Add(new Feat { Name = FeatConstants.SaveBonus, Focus = FeatConstants.Foci.All });
            feats[0].Focus = baseWeaponTypes[0];

            var weapon = weaponGenerator.GenerateFrom(feats, characterClass, race);
            Assert.That(weapon, Is.EqualTo(magicalWeapon));
        }
    }
}
