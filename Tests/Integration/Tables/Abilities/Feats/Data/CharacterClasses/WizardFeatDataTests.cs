using System;
using EquipmentGen.Common.Items;
using NPCGen.Common.Abilities.Feats;
using NPCGen.Common.CharacterClasses;
using NPCGen.Tables.Interfaces;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Tables.Abilities.Feats.Data.CharacterClasses
{
    [TestFixture]
    public class WizardFeatDataTests : CharacterClassFeatDataTests
    {
        protected override String tableName
        {
            get { return String.Format(TableNameConstants.Formattable.Collection.CLASSFeatData, CharacterClassConstants.Wizard); }
        }

        [Test]
        public override void CollectionNames()
        {
            var names = new[] 
            {
                FeatConstants.ScribeScroll,
                FeatConstants.SimpleWeaponProficiency + WeaponConstants.Club,
                FeatConstants.SimpleWeaponProficiency + WeaponConstants.Dagger,
                FeatConstants.SimpleWeaponProficiency + WeaponConstants.HeavyCrossbow,
                FeatConstants.SimpleWeaponProficiency + WeaponConstants.LightCrossbow,
                FeatConstants.SimpleWeaponProficiency + WeaponConstants.Quarterstaff
            };

            AssertCollectionNames(names);
        }

        [TestCase(FeatConstants.ScribeScroll,
            FeatConstants.ScribeScroll,
            "",
            0,
            "",
            "",
            1,
            0,
            0)]
        [TestCase(FeatConstants.SimpleWeaponProficiency + WeaponConstants.Club,
            FeatConstants.SimpleWeaponProficiency,
            WeaponConstants.Club,
            0,
            "",
            "",
            1,
            0,
            0)]
        [TestCase(FeatConstants.SimpleWeaponProficiency + WeaponConstants.Dagger,
            FeatConstants.SimpleWeaponProficiency,
            WeaponConstants.Dagger,
            0,
            "",
            "",
            1,
            0,
            0)]
        [TestCase(FeatConstants.SimpleWeaponProficiency + WeaponConstants.HeavyCrossbow,
            FeatConstants.SimpleWeaponProficiency,
            WeaponConstants.HeavyCrossbow,
            0,
            "",
            "",
            1,
            0,
            0)]
        [TestCase(FeatConstants.SimpleWeaponProficiency + WeaponConstants.LightCrossbow,
            FeatConstants.SimpleWeaponProficiency,
            WeaponConstants.LightCrossbow,
            0,
            "",
            "",
            1,
            0,
            0)]
        [TestCase(FeatConstants.SimpleWeaponProficiency + WeaponConstants.Quarterstaff,
            FeatConstants.SimpleWeaponProficiency,
            WeaponConstants.Quarterstaff,
            0,
            "",
            "",
            1,
            0,
            0)]
        public override void Data(String name, String feat, String focusType, Int32 frequencyQuantity, String frequencyQuantityStat, String frequencyTimePeriod, Int32 minimumLevel, Int32 maximumLevel, Int32 strength)
        {
            base.Data(name, feat, focusType, frequencyQuantity, frequencyQuantityStat, frequencyTimePeriod, minimumLevel, maximumLevel, strength);
        }
    }
}
