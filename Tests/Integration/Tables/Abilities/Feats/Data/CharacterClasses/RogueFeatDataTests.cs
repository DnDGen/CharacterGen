﻿using System;
using TreasureGen.Common.Items;
using CharacterGen.Common.Abilities.Feats;
using CharacterGen.Common.CharacterClasses;
using CharacterGen.Common.Items;
using CharacterGen.Tables;
using NUnit.Framework;

namespace CharacterGen.Tests.Integration.Tables.Abilities.Feats.Data.CharacterClasses
{
    [TestFixture]
    public class RogueFeatDataTests : CharacterClassFeatDataTests
    {
        protected override String tableName
        {
            get { return String.Format(TableNameConstants.Formattable.Collection.CLASSFeatData, CharacterClassConstants.Rogue); }
        }

        [Test]
        public override void CollectionNames()
        {
            var names = new[] 
            {
                FeatConstants.SneakAttack + "1",
                FeatConstants.SneakAttack + "2",
                FeatConstants.SneakAttack + "3",
                FeatConstants.SneakAttack + "4",
                FeatConstants.SneakAttack + "5",
                FeatConstants.SneakAttack + "6",
                FeatConstants.SneakAttack + "7",
                FeatConstants.SneakAttack + "8",
                FeatConstants.SneakAttack + "9",
                FeatConstants.SneakAttack + "10",
                FeatConstants.Trapfinding,
                FeatConstants.Evasion,
                FeatConstants.UncannyDodge,
                FeatConstants.TrapSense + "1",
                FeatConstants.TrapSense + "2",
                FeatConstants.TrapSense + "3",
                FeatConstants.TrapSense + "4",
                FeatConstants.TrapSense + "5",
                FeatConstants.TrapSense + "6",
                FeatConstants.ImprovedUncannyDodge,
                FeatConstants.SimpleWeaponProficiency,
                FeatConstants.ExoticWeaponProficiency + WeaponConstants.HandCrossbow,
                FeatConstants.MartialWeaponProficiency + WeaponConstants.Rapier,
                FeatConstants.MartialWeaponProficiency + WeaponConstants.Sap,
                FeatConstants.MartialWeaponProficiency + WeaponConstants.Shortbow,
                FeatConstants.MartialWeaponProficiency + WeaponConstants.ShortSword,
                FeatConstants.LightArmorProficiency
            };

            AssertCollectionNames(names);
        }

        [TestCase(FeatConstants.SneakAttack + "1",
            FeatConstants.SneakAttack,
            "",
            0,
            "",
            "",
            1,
            2,
            1)]
        [TestCase(FeatConstants.SneakAttack + "2",
            FeatConstants.SneakAttack,
            "",
            0,
            "",
            "",
            3,
            4,
            2)]
        [TestCase(FeatConstants.SneakAttack + "3",
            FeatConstants.SneakAttack,
            "",
            0,
            "",
            "",
            5,
            6,
            3)]
        [TestCase(FeatConstants.SneakAttack + "4",
            FeatConstants.SneakAttack,
            "",
            0,
            "",
            "",
            7,
            8,
            4)]
        [TestCase(FeatConstants.SneakAttack + "5",
            FeatConstants.SneakAttack,
            "",
            0,
            "",
            "",
            9,
            10,
            5)]
        [TestCase(FeatConstants.SneakAttack + "6",
            FeatConstants.SneakAttack,
            "",
            0,
            "",
            "",
            11,
            12,
            6)]
        [TestCase(FeatConstants.SneakAttack + "7",
            FeatConstants.SneakAttack,
            "",
            0,
            "",
            "",
            13,
            14,
            7)]
        [TestCase(FeatConstants.SneakAttack + "8",
            FeatConstants.SneakAttack,
            "",
            0,
            "",
            "",
            15,
            16,
            8)]
        [TestCase(FeatConstants.SneakAttack + "9",
            FeatConstants.SneakAttack,
            "",
            0,
            "",
            "",
            17,
            18,
            9)]
        [TestCase(FeatConstants.SneakAttack + "10",
            FeatConstants.SneakAttack,
            "",
            0,
            "",
            "",
            19,
            0,
            10)]
        [TestCase(FeatConstants.Trapfinding,
            FeatConstants.Trapfinding,
            "",
            0,
            "",
            "",
            1,
            0,
            0)]
        [TestCase(FeatConstants.Evasion,
            FeatConstants.Evasion,
            "",
            0,
            "",
            "",
            2,
            0,
            0)]
        [TestCase(FeatConstants.UncannyDodge,
            FeatConstants.UncannyDodge,
            "",
            0,
            "",
            "",
            4,
            0,
            0)]
        [TestCase(FeatConstants.TrapSense + "1",
            FeatConstants.TrapSense,
            "",
            0,
            "",
            "",
            3,
            5,
            1)]
        [TestCase(FeatConstants.TrapSense + "2",
            FeatConstants.TrapSense,
            "",
            0,
            "",
            "",
            6,
            8,
            2)]
        [TestCase(FeatConstants.TrapSense + "3",
            FeatConstants.TrapSense,
            "",
            0,
            "",
            "",
            9,
            11,
            3)]
        [TestCase(FeatConstants.TrapSense + "4",
            FeatConstants.TrapSense,
            "",
            0,
            "",
            "",
            12,
            14,
            4)]
        [TestCase(FeatConstants.TrapSense + "5",
            FeatConstants.TrapSense,
            "",
            0,
            "",
            "",
            15,
            17,
            5)]
        [TestCase(FeatConstants.TrapSense + "6",
            FeatConstants.TrapSense,
            "",
            0,
            "",
            "",
            18,
            0,
            6)]
        [TestCase(FeatConstants.ImprovedUncannyDodge,
            FeatConstants.ImprovedUncannyDodge,
            "",
            0,
            "",
            "",
            8,
            0,
            0)]
        [TestCase(FeatConstants.SimpleWeaponProficiency,
            FeatConstants.SimpleWeaponProficiency,
            ProficiencyConstants.All,
            0,
            "",
            "",
            1,
            0,
            0)]
        [TestCase(FeatConstants.ExoticWeaponProficiency + WeaponConstants.HandCrossbow,
            FeatConstants.ExoticWeaponProficiency,
            WeaponConstants.HandCrossbow,
            0,
            "",
            "",
            1,
            0,
            0)]
        [TestCase(FeatConstants.MartialWeaponProficiency + WeaponConstants.Rapier,
            FeatConstants.MartialWeaponProficiency,
            WeaponConstants.Rapier,
            0,
            "",
            "",
            1,
            0,
            0)]
        [TestCase(FeatConstants.MartialWeaponProficiency + WeaponConstants.Sap,
            FeatConstants.MartialWeaponProficiency,
            WeaponConstants.Sap,
            0,
            "",
            "",
            1,
            0,
            0)]
        [TestCase(FeatConstants.MartialWeaponProficiency + WeaponConstants.Shortbow,
            FeatConstants.MartialWeaponProficiency,
            WeaponConstants.Shortbow,
            0,
            "",
            "",
            1,
            0,
            0)]
        [TestCase(FeatConstants.MartialWeaponProficiency + WeaponConstants.ShortSword,
            FeatConstants.MartialWeaponProficiency,
            WeaponConstants.ShortSword,
            0,
            "",
            "",
            1,
            0,
            0)]
        [TestCase(FeatConstants.LightArmorProficiency,
            FeatConstants.LightArmorProficiency,
            "",
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
