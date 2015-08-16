using CharacterGen.Common.Abilities.Feats;
using CharacterGen.Common.CharacterClasses;
using CharacterGen.Generators.Domain.Items;
using CharacterGen.Generators.Items;
using CharacterGen.Selectors;
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
        private Item magicalWeapon;
        private List<Feat> feats;
        private CharacterClass characterClass;

        [SetUp]
        public void Setup()
        {
            mockPercentileSelector = new Mock<IPercentileSelector>();
            mockMundaneWeaponGenerator = new Mock<IMundaneItemGenerator>();
            mockMagicalWeaponGenerator = new Mock<IMagicalItemGenerator>();
            weaponGenerator = new WeaponGenerator();
            magicalWeapon = new Item();
            feats = new List<Feat>();
            characterClass = new CharacterClass();

            characterClass.Level = 9266;
            mockPercentileSelector.Setup(s => s.SelectFrom("Level9266Power")).Returns("power");
            mockMagicalWeaponGenerator.Setup(g => g.GenerateAtPower("power")).Returns(magicalWeapon);
        }

        [Test]
        public void GenerateMundaneWeapon()
        {
            var mundaneWeapon = new Item();
            mockPercentileSelector.Setup(s => s.SelectFrom("Level9266Power")).Returns(PowerConstants.Mundane);
            mockMundaneWeaponGenerator.Setup(g => g.Generate()).Returns(mundaneWeapon);

            var weapon = weaponGenerator.GenerateFrom(feats, characterClass);
            Assert.That(weapon, Is.EqualTo(mundaneWeapon));
        }

        [Test]
        public void IfCannotWieldMundaneWeapon_Regenerate()
        {
            throw new NotImplementedException();
        }

        [Test]
        public void PreferMundaneWeaponsPickedAsFocusForNonProficiencyFeats()
        {
            throw new NotImplementedException();
        }

        [Test]
        public void PreferMundaneWeaponsPickedAsFocusForProficiencyFeats()
        {
            throw new NotImplementedException();
        }

        [Test]
        public void NoPreferenceForMundaneWeapons()
        {
            throw new NotImplementedException();
        }

        [Test]
        public void GenerateMagicalWeapon()
        {
            var weapon = weaponGenerator.GenerateFrom(feats, characterClass);
            Assert.That(weapon, Is.EqualTo(magicalWeapon));
        }

        [Test]
        public void IfCannotWieldMagicalWeapon_Regenerate()
        {
            throw new NotImplementedException();
        }

        [Test]
        public void IfMagicalWeaponIsNotWeapon_Regenerate()
        {
            throw new NotImplementedException();
        }

        [Test]
        public void PreferMagicalWeaponsPickedAsFocusForNonProficiencyFeats()
        {
            throw new NotImplementedException();
        }

        [Test]
        public void PreferMagicalWeaponsPickedAsFocusForProficiencyFeats()
        {
            throw new NotImplementedException();
        }

        [Test]
        public void NoPreferenceForMagicalWeapons()
        {
            throw new NotImplementedException();
        }
    }
}
