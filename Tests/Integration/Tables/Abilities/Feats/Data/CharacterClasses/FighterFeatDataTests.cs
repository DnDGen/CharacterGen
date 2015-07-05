﻿using System;
using NPCGen.Common.Abilities.Feats;
using NPCGen.Common.CharacterClasses;
using NPCGen.Tables.Interfaces;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Tables.Abilities.Feats.Data.CharacterClasses
{
    [TestFixture]
    public class FighterFeatDataTests : CharacterClassFeatDataTests
    {
        protected override String tableName
        {
            get { return String.Format(TableNameConstants.Formattable.Collection.CLASSFeatData, CharacterClassConstants.Fighter); }
        }

        [Test]
        public override void CollectionNames()
        {
            var names = new[] 
            {
                FeatConstants.SimpleWeaponProficiencyId,
                FeatConstants.MartialWeaponProficiencyId,
                FeatConstants.LightArmorProficiencyId,
                FeatConstants.MediumArmorProficiencyId,
                FeatConstants.HeavyArmorProficiencyId,
                FeatConstants.ShieldProficiencyId,
                FeatConstants.TowerShieldProficiencyId
            };

            AssertCollectionNames(names);
        }

        [TestCase(FeatConstants.SimpleWeaponProficiencyId,
            FeatConstants.SimpleWeaponProficiencyId,
            FeatConstants.SimpleWeaponProficiencyId,
            0,
            "",
            "",
            1,
            0,
            0)]
        [TestCase(FeatConstants.MartialWeaponProficiencyId,
            FeatConstants.MartialWeaponProficiencyId,
            FeatConstants.MartialWeaponProficiencyId,
            0,
            "",
            "",
            1,
            0,
            0)]
        [TestCase(FeatConstants.LightArmorProficiencyId,
            FeatConstants.LightArmorProficiencyId,
            "",
            0,
            "",
            "",
            1,
            0,
            0)]
        [TestCase(FeatConstants.MediumArmorProficiencyId,
            FeatConstants.MediumArmorProficiencyId,
            "",
            0,
            "",
            "",
            1,
            0,
            0)]
        [TestCase(FeatConstants.HeavyArmorProficiencyId,
            FeatConstants.HeavyArmorProficiencyId,
            "",
            0,
            "",
            "",
            1,
            0,
            0)]
        [TestCase(FeatConstants.ShieldProficiencyId,
            FeatConstants.ShieldProficiencyId,
            "",
            0,
            "",
            "",
            1,
            0,
            0)]
        [TestCase(FeatConstants.TowerShieldProficiencyId,
            FeatConstants.TowerShieldProficiencyId,
            "",
            0,
            "",
            "",
            1,
            0,
            0)]
        public override void Data(String name, String featId, String focusType, Int32 frequencyQuantity, String frequencyQuantityStat, String frequencyTimePeriod, Int32 minimumLevel, Int32 maximumLevel, Int32 strength)
        {
            base.Data(name, featId, focusType, frequencyQuantity, frequencyQuantityStat, frequencyTimePeriod, minimumLevel, maximumLevel, strength);
        }
    }
}