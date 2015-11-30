using CharacterGen.Common.Abilities.Feats;
using CharacterGen.Common.CharacterClasses;
using CharacterGen.Common.Races;
using CharacterGen.Generators;
using CharacterGen.Generators.Domain.Items;
using CharacterGen.Generators.Items;
using CharacterGen.Generators.Verifiers.Exceptions;
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
        private IWeaponGenerator weaponGenerator;
        private Mock<IPercentileSelector> mockPercentileSelector;
        private Mock<IMundaneItemGenerator> mockMundaneWeaponGenerator;
        private Mock<IMagicalItemGenerator> mockMagicalWeaponGenerator;
        private Mock<ICollectionsSelector> mockCollectionsSelector;
        private Generator generator;
        private Item magicalWeapon;
        private List<Feat> feats;
        private List<String> proficiencyFeats;
        private CharacterClass characterClass;
        private List<String> baseWeaponTypes;
        private List<String> weapons;
        private List<String> allProficientWeapons;
        private Race race;
        private List<String> meleeWeapons;
        private List<String> twoHandedWeapons;
        private List<String> ammunitions;

        [SetUp]
        public void Setup()
        {
            mockPercentileSelector = new Mock<IPercentileSelector>();
            mockMundaneWeaponGenerator = new Mock<IMundaneItemGenerator>();
            mockMagicalWeaponGenerator = new Mock<IMagicalItemGenerator>();
            mockCollectionsSelector = new Mock<ICollectionsSelector>();
            generator = new ConfigurableIterationGenerator(3);
            weaponGenerator = new WeaponGenerator(mockCollectionsSelector.Object, mockPercentileSelector.Object, mockMundaneWeaponGenerator.Object, mockMagicalWeaponGenerator.Object, generator);
            magicalWeapon = new Item();
            feats = new List<Feat>();
            characterClass = new CharacterClass();
            proficiencyFeats = new List<String>();
            baseWeaponTypes = new List<String>();
            weapons = new List<String>();
            allProficientWeapons = new List<String>();
            race = new Race();
            meleeWeapons = new List<String>();
            twoHandedWeapons = new List<String>();
            ammunitions = new List<String>();

            race.Size = "size";
            race.BaseRace = "base race";
            magicalWeapon = CreateWeapon("magical weapon");
            magicalWeapon.IsMagical = true;
            magicalWeapon.Attributes = new[] { AttributeConstants.Melee };
            characterClass.ClassName = "class name";
            characterClass.Level = 9266;
            baseWeaponTypes.Add("base weapon");
            weapons.Remove(magicalWeapon.Name);
            weapons.Add(baseWeaponTypes[0]);
            weapons.Add("different weapon");
            meleeWeapons.Remove(magicalWeapon.Name);
            meleeWeapons.Add(baseWeaponTypes[0]);
            meleeWeapons.Add("different melee weapon");
            feats.Add(new Feat { Name = "all proficiency", Foci = new[] { FeatConstants.Foci.All } });
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
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.ItemGroups, baseWeaponTypes[0]))
                .Returns(baseWeaponTypes);
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.ItemGroups, FeatConstants.Foci.Weapons))
                .Returns(weapons);
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.ItemGroups, AttributeConstants.Melee))
                .Returns(meleeWeapons);
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.ItemGroups, AttributeConstants.TwoHanded))
                .Returns(twoHandedWeapons);
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.ItemGroups, AttributeConstants.Ammunition))
                .Returns(ammunitions);
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.FeatFoci, feats[0].Name))
                .Returns(allProficientWeapons);
        }

        [Test]
        public void ThrowExceptionIfNoWeaponsAreAllowed()
        {
            allProficientWeapons.Clear();
            feats.Add(new Feat { Name = "feat 1" });
            feats.Add(new Feat { Name = "feat 2", Foci = new[] { "focus" } });

            Assert.That(() => weaponGenerator.GenerateFrom(feats, characterClass, race), Throws.Exception.InstanceOf<NoWeaponProficienciesException>());
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
            meleeWeapons.Add(name);

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

            feats.Add(new Feat { Name = "feat2", Foci = new[] { mundaneWeapon.Name } });
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

            feats.Add(new Feat { Name = "feat2", Foci = new[] { wrongMundaneWeapon.Name } });
            feats.Add(new Feat { Name = "feat3", Foci = new[] { mundaneWeapon.Name } });
            proficiencyFeats.Add(feats[1].Name);

            var weapon = weaponGenerator.GenerateFrom(feats, characterClass, race);
            Assert.That(weapon, Is.EqualTo(mundaneWeapon));
        }

        [Test]
        public void DoNotPreferMundaneWeaponPickedAsFocusForWeaponFamiliarity()
        {
            var mundaneWeapon = CreateWeapon("mundane weapon");
            var wrongMundaneWeapon = CreateWeapon("wrong mundane weapon");
            mockPercentileSelector.Setup(s => s.SelectFrom("Level9266Power")).Returns(PowerConstants.Mundane);
            mockMundaneWeaponGenerator.SetupSequence(g => g.Generate()).Returns(wrongMundaneWeapon).Returns(mundaneWeapon);

            feats.Add(new Feat { Name = FeatConstants.WeaponFamiliarity, Foci = new[] { wrongMundaneWeapon.Name } });
            feats.Add(new Feat { Name = "feat3", Foci = new[] { mundaneWeapon.Name } });

            var weapon = weaponGenerator.GenerateFrom(feats, characterClass, race);
            Assert.That(weapon, Is.EqualTo(mundaneWeapon), weapon.Name);
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

            feats.Add(new Feat { Name = "feat2", Foci = new[] { wrongMundaneWeapon.Name } });
            feats.Add(new Feat { Name = "feat3", Foci = new[] { mundaneWeapon.Name, otherMundaneWeapon.Name } });
            feats.Add(new Feat { Name = "feat4", Foci = new[] { mundaneWeapon.Name } });
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

            feats.Add(new Feat { Name = "feat2", Foci = new[] { mundaneWeapon.Name } });
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

            feats.Add(new Feat { Name = "feat2", Foci = new[] { mundaneWeapon.Name, otherMundaneWeapon.Name } });
            feats.Add(new Feat { Name = "feat3", Foci = new[] { mundaneWeapon.Name } });
            proficiencyFeats.Add(feats[1].Name);
            proficiencyFeats.Add(feats[2].Name);

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

            feats.Add(new Feat { Name = "feat2", Foci = new[] { baseWeaponTypes[0] } });
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

            feats.Add(new Feat { Name = "feat2", Foci = new[] { wrongMagicalWeapon.Name } });
            feats.Add(new Feat { Name = "feat3", Foci = new[] { baseWeaponTypes[0] } });
            proficiencyFeats.Add(feats[1].Name);

            var weapon = weaponGenerator.GenerateFrom(feats, characterClass, race);
            Assert.That(weapon, Is.EqualTo(magicalWeapon));
        }

        [Test]
        public void DoNotPreferMagicalWeaponsPickedAsFocusForWeaponFamiliarity()
        {
            var wrongMagicalWeapon = CreateWeapon("wrong magical weapon");
            mockMagicalWeaponGenerator.SetupSequence(g => g.GenerateAtPower("power"))
                .Returns(wrongMagicalWeapon).Returns(magicalWeapon);

            feats.Add(new Feat { Name = FeatConstants.WeaponFamiliarity, Foci = new[] { wrongMagicalWeapon.Name } });
            feats.Add(new Feat { Name = "feat3", Foci = new[] { baseWeaponTypes[0] } });

            var weapon = weaponGenerator.GenerateFrom(feats, characterClass, race);
            Assert.That(weapon, Is.EqualTo(magicalWeapon), weapon.Name);
        }

        [Test]
        public void PreferAnyMagicalWeaponsPickedAsFocusForNonProficiencyFeats()
        {
            var otherMagicalWeapon = CreateWeapon("other magical weapon");
            var wrongMagicalWeapon = CreateWeapon("wrong magical weapon");
            mockMagicalWeaponGenerator.SetupSequence(g => g.GenerateAtPower("power"))
                .Returns(wrongMagicalWeapon).Returns(otherMagicalWeapon).Returns(magicalWeapon);

            feats.Add(new Feat { Name = "feat2", Foci = new[] { wrongMagicalWeapon.Name } });
            feats.Add(new Feat { Name = "feat3", Foci = new[] { baseWeaponTypes[0], otherMagicalWeapon.Name } });
            feats.Add(new Feat { Name = "feat4", Foci = new[] { baseWeaponTypes[0] } });
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

            feats.Add(new Feat { Name = "feat2", Foci = new[] { baseWeaponTypes[0] } });
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

            feats.Add(new Feat { Name = "feat2", Foci = new[] { baseWeaponTypes[0], otherMagicalWeapon.Name } });
            feats.Add(new Feat { Name = "feat3", Foci = new[] { baseWeaponTypes[0] } });
            proficiencyFeats.Add(feats[1].Name);
            proficiencyFeats.Add(feats[2].Name);

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
        public void SaveBonusesOfAllDoNotCountAsProficiencyFeats()
        {
            feats.Add(new Feat { Name = FeatConstants.SaveBonus, Foci = new[] { FeatConstants.Foci.All } });
            feats[0].Foci = new[] { baseWeaponTypes[0] };

            var weapon = weaponGenerator.GenerateFrom(feats, characterClass, race);
            Assert.That(weapon, Is.EqualTo(magicalWeapon), weapon.Name);
        }

        [Test]
        public void GeneralWeaponCannotBeAmmunition()
        {
            var ammunition = CreateWeapon("ammunition");
            meleeWeapons.Remove(ammunition.Name);
            ammunitions.Add(ammunition.Name);
            mockMagicalWeaponGenerator.SetupSequence(g => g.GenerateAtPower("power"))
                .Returns(ammunition).Returns(magicalWeapon);

            var weapon = weaponGenerator.GenerateFrom(feats, characterClass, race);
            Assert.That(weapon, Is.EqualTo(magicalWeapon), weapon.Name);
        }

        [Test]
        public void GenerateAmmunition()
        {
            var ammunition = CreateWeapon("ammunition");
            meleeWeapons.Remove(ammunition.Name);
            ammunitions.Add(ammunition.Name);
            mockMagicalWeaponGenerator.SetupSequence(g => g.GenerateAtPower("power"))
                .Returns(magicalWeapon).Returns(ammunition);

            allProficientWeapons.Remove("ammunition");
            allProficientWeapons.Add("ammo");

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.ItemGroups, ammunition.Name))
                .Returns(new[] { "ammo" });

            var generatedAmmunition = weaponGenerator.GenerateAmmunition(feats, characterClass, race, "ammo");
            Assert.That(generatedAmmunition, Is.EqualTo(ammunition), generatedAmmunition.Name);
        }

        [Test]
        public void GenerateAmmunitionForSpecificRangedWeaponFocus()
        {
            feats.Add(new Feat { Name = "feat2", Foci = new[] { baseWeaponTypes[0] } });

            var ammunition = CreateWeapon("ammunition");
            meleeWeapons.Remove(ammunition.Name);
            ammunitions.Add(ammunition.Name);
            mockMagicalWeaponGenerator.SetupSequence(g => g.GenerateAtPower("power"))
                .Returns(magicalWeapon).Returns(ammunition);

            baseWeaponTypes.Add("ammunition");

            var generatedAmmunition = weaponGenerator.GenerateAmmunition(feats, characterClass, race, "ammunition");
            Assert.That(generatedAmmunition, Is.Not.Null);
            Assert.That(generatedAmmunition, Is.EqualTo(ammunition), generatedAmmunition.Name);
        }

        [Test]
        public void GenerateNoAmmunition()
        {
            var ammunition = CreateWeapon("ammunition");
            meleeWeapons.Remove(ammunition.Name);
            ammunitions.Add(ammunition.Name);
            var mundaneWeapon = CreateWeapon("mundane weapon");
            mockMundaneWeaponGenerator.Setup(g => g.Generate()).Returns(mundaneWeapon);

            var generatedAmmunition = weaponGenerator.GenerateAmmunition(feats, characterClass, race, "ammunition");
            Assert.That(generatedAmmunition, Is.Null);
        }

        [Test]
        public void ReturnNullIfNotProficientInAmmunition()
        {
            var generatedAmmunition = weaponGenerator.GenerateAmmunition(feats, characterClass, race, "ammunition");
            Assert.That(generatedAmmunition, Is.Null);
        }

        [Test]
        public void MeleeWeaponMustBeMelee()
        {
            var rangedWeapon = CreateWeapon("ranged weapon");
            meleeWeapons.Remove(rangedWeapon.Name);
            mockMagicalWeaponGenerator.SetupSequence(g => g.GenerateAtPower("power"))
                .Returns(rangedWeapon).Returns(magicalWeapon);

            var weapon = weaponGenerator.GenerateMeleeFrom(feats, characterClass, race);
            Assert.That(weapon, Is.EqualTo(magicalWeapon), weapon.Name);
        }

        [Test]
        public void IfGenerationOfMeleeWeaponFails_TryWithoutNonProficiencyWeaponFoci()
        {
            var rangedWeapon = CreateWeapon("ranged weapon");
            meleeWeapons.Remove(rangedWeapon.Name);
            var otherMagicalWeapon = CreateWeapon("other magical weapon");

            mockMagicalWeaponGenerator.SetupSequence(g => g.GenerateAtPower("power"))
                .Returns(rangedWeapon).Returns(otherMagicalWeapon).Returns(magicalWeapon);

            feats.Add(new Feat { Name = "feat2", Foci = new[] { rangedWeapon.Name } });
            feats.Add(new Feat { Name = "feat3", Foci = new[] { otherMagicalWeapon.Name } });
            proficiencyFeats.Add("feat3");

            var weapon = weaponGenerator.GenerateMeleeFrom(feats, characterClass, race);
            Assert.That(weapon, Is.EqualTo(otherMagicalWeapon), weapon.Name);
        }

        [Test]
        public void IfGenerationOfMeleeWeaponFailsAgain_TryWithoutSpecificProficiencyWeaponFoci()
        {
            var rangedWeapon = CreateWeapon("ranged weapon");
            meleeWeapons.Remove(rangedWeapon.Name);
            var otherMagicalWeapon = CreateWeapon("other magical weapon");
            meleeWeapons.Remove(otherMagicalWeapon.Name);

            mockMagicalWeaponGenerator.SetupSequence(g => g.GenerateAtPower("power"))
                .Returns(rangedWeapon).Returns(otherMagicalWeapon).Returns(magicalWeapon);

            feats.Add(new Feat { Name = "feat2", Foci = new[] { rangedWeapon.Name } });
            feats.Add(new Feat { Name = "feat3", Foci = new[] { otherMagicalWeapon.Name } });
            proficiencyFeats.Add("feat3");

            var weapon = weaponGenerator.GenerateMeleeFrom(feats, characterClass, race);
            Assert.That(weapon, Is.EqualTo(magicalWeapon), weapon.Name);
        }

        [Test]
        public void OneHandedMeleeWeaponMustBeMelee()
        {
            var rangedWeapon = CreateWeapon("ranged weapon");
            meleeWeapons.Remove(rangedWeapon.Name);
            mockMagicalWeaponGenerator.SetupSequence(g => g.GenerateAtPower("power"))
                .Returns(rangedWeapon).Returns(magicalWeapon);

            var weapon = weaponGenerator.GenerateOneHandedMeleeFrom(feats, characterClass, race);
            Assert.That(weapon, Is.EqualTo(magicalWeapon), weapon.Name);
        }

        [Test]
        public void OneHandedMeleeWeaponMustBeOneHanded()
        {
            var twoHandedWeapon = CreateWeapon("two-handed weapon");
            twoHandedWeapons.Add(twoHandedWeapon.Name);

            mockMagicalWeaponGenerator.SetupSequence(g => g.GenerateAtPower("power"))
                .Returns(twoHandedWeapon).Returns(magicalWeapon);

            var weapon = weaponGenerator.GenerateOneHandedMeleeFrom(feats, characterClass, race);
            Assert.That(weapon, Is.EqualTo(magicalWeapon), weapon.Name);
        }

        [Test]
        public void IfGenerationOfOneHandedMeleeWeaponFails_TryWithoutNonProficiencyWeaponFoci()
        {
            var rangedWeapon = CreateWeapon("ranged weapon");
            meleeWeapons.Remove(rangedWeapon.Name);
            var otherMagicalWeapon = CreateWeapon("other magical weapon");

            mockMagicalWeaponGenerator.SetupSequence(g => g.GenerateAtPower("power"))
                .Returns(rangedWeapon).Returns(otherMagicalWeapon).Returns(magicalWeapon);

            feats.Add(new Feat { Name = "feat2", Foci = new[] { rangedWeapon.Name } });
            feats.Add(new Feat { Name = "feat3", Foci = new[] { otherMagicalWeapon.Name } });
            proficiencyFeats.Add("feat3");

            var weapon = weaponGenerator.GenerateOneHandedMeleeFrom(feats, characterClass, race);
            Assert.That(weapon, Is.EqualTo(otherMagicalWeapon), weapon.Name);
        }

        [Test]
        public void IfGenerationOfOneHandedMeleeWeaponFailsAgain_TryWithoutSpecificProficiencyWeaponFoci()
        {
            var rangedWeapon = CreateWeapon("ranged weapon");
            meleeWeapons.Remove(rangedWeapon.Name);
            var otherMagicalWeapon = CreateWeapon("other magical weapon");
            meleeWeapons.Remove(otherMagicalWeapon.Name);

            mockMagicalWeaponGenerator.SetupSequence(g => g.GenerateAtPower("power"))
                .Returns(rangedWeapon).Returns(otherMagicalWeapon).Returns(magicalWeapon);

            feats.Add(new Feat { Name = "feat2", Foci = new[] { rangedWeapon.Name } });
            feats.Add(new Feat { Name = "feat3", Foci = new[] { otherMagicalWeapon.Name } });
            proficiencyFeats.Add("feat3");

            var weapon = weaponGenerator.GenerateOneHandedMeleeFrom(feats, characterClass, race);
            Assert.That(weapon, Is.EqualTo(magicalWeapon), weapon.Name);
        }

        [Test]
        public void RangedWeaponMustNotBeMelee()
        {
            var rangedWeapon = CreateWeapon("ranged weapon");
            meleeWeapons.Remove(rangedWeapon.Name);
            mockMagicalWeaponGenerator.SetupSequence(g => g.GenerateAtPower("power"))
                .Returns(magicalWeapon).Returns(rangedWeapon);

            var weapon = weaponGenerator.GenerateRangedFrom(feats, characterClass, race);
            Assert.That(weapon, Is.EqualTo(rangedWeapon), weapon.Name);
        }

        [Test]
        public void IfGenerationOfRangedWeaponFails_TryWithoutNonProficiencyWeaponFoci()
        {
            var rangedWeapon = CreateWeapon("ranged weapon");
            meleeWeapons.Remove(rangedWeapon.Name);
            var otherMagicalWeapon = CreateWeapon("other magical weapon");
            meleeWeapons.Remove(otherMagicalWeapon.Name);

            mockMagicalWeaponGenerator.SetupSequence(g => g.GenerateAtPower("power"))
                .Returns(magicalWeapon).Returns(otherMagicalWeapon).Returns(rangedWeapon);

            feats.Add(new Feat { Name = "feat2", Foci = new[] { baseWeaponTypes[0] } });
            feats.Add(new Feat { Name = "feat3", Foci = new[] { otherMagicalWeapon.Name } });
            proficiencyFeats.Add("feat3");

            var weapon = weaponGenerator.GenerateRangedFrom(feats, characterClass, race);
            Assert.That(weapon, Is.EqualTo(otherMagicalWeapon), weapon.Name);
        }

        [Test]
        public void IfGenerationOfRangedWeaponFailsAgain_TryWithoutSpecificProficiencyWeaponFoci()
        {
            var rangedWeapon = CreateWeapon("ranged weapon");
            meleeWeapons.Remove(rangedWeapon.Name);
            var otherMagicalWeapon = CreateWeapon("other magical weapon");

            mockMagicalWeaponGenerator.SetupSequence(g => g.GenerateAtPower("power"))
                .Returns(magicalWeapon).Returns(otherMagicalWeapon).Returns(rangedWeapon);

            feats.Add(new Feat { Name = "feat2", Foci = new[] { baseWeaponTypes[0] } });
            feats.Add(new Feat { Name = "feat3", Foci = new[] { otherMagicalWeapon.Name } });
            proficiencyFeats.Add("feat3");

            var weapon = weaponGenerator.GenerateRangedFrom(feats, characterClass, race);
            Assert.That(weapon, Is.Not.Null);
            Assert.That(weapon, Is.EqualTo(rangedWeapon), weapon.Name);
        }

        [Test]
        public void RangedWeaponCannotBeAmmunition()
        {
            meleeWeapons.Remove(baseWeaponTypes[0]);
            var ammunition = CreateWeapon("ammunition");
            meleeWeapons.Remove(ammunition.Name);
            ammunitions.Add(ammunition.Name);
            mockMagicalWeaponGenerator.SetupSequence(g => g.GenerateAtPower("power"))
                .Returns(ammunition).Returns(magicalWeapon);

            var weapon = weaponGenerator.GenerateRangedFrom(feats, characterClass, race);
            Assert.That(weapon, Is.EqualTo(magicalWeapon), weapon.Name);
        }

        [Test]
        public void GetAmmunitionForRangedWeaponThatIsNotNonProficiencyFocus()
        {
            var wrongMagicalWeapon = CreateWeapon("wrong magical weapon");
            var ammunition = CreateWeapon("ammunition");
            meleeWeapons.Remove(ammunition.Name);
            ammunitions.Add(ammunition.Name);

            mockMagicalWeaponGenerator.SetupSequence(g => g.GenerateAtPower("power"))
                .Returns(wrongMagicalWeapon).Returns(magicalWeapon).Returns(ammunition);

            feats.Add(new Feat { Name = "feat2", Foci = new[] { wrongMagicalWeapon.Name } });
            feats.Add(new Feat { Name = "feat3", Foci = new[] { baseWeaponTypes[0] } });
            proficiencyFeats.Add(feats[2].Name);

            baseWeaponTypes.Add("ammunition");

            var ammo = weaponGenerator.GenerateAmmunition(feats, characterClass, race, "ammunition");
            Assert.That(ammo, Is.EqualTo(ammunition));
        }

        [Test]
        public void CanGetAmmunitionWhenNotExplicitlyProficientWithAmmunition()
        {
            var ammunition = CreateWeapon("ammunition");
            meleeWeapons.Remove(ammunition.Name);
            ammunitions.Add(ammunition.Name);
            allProficientWeapons.Remove(ammunition.Name);
            mockMagicalWeaponGenerator.SetupSequence(g => g.GenerateAtPower("power"))
                .Returns(magicalWeapon).Returns(ammunition);

            baseWeaponTypes.Add(ammunition.Name);

            var generatedAmmunition = weaponGenerator.GenerateAmmunition(feats, characterClass, race, ammunition.Name);
            Assert.That(generatedAmmunition, Is.EqualTo(ammunition), generatedAmmunition.Name);
        }
    }
}
