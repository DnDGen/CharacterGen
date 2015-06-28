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

        [TestCase(FeatConstants.ScribeScrollId,
            FeatConstants.ScribeScrollId,
            "",
            0,
            "",
            "",
            0,
            0,
            0)]
        [TestCase(FeatConstants.SimpleWeaponProficiencyId + WeaponConstants.Club,
            FeatConstants.SimpleWeaponProficiencyId,
            WeaponConstants.Club,
            0,
            "",
            "",
            0,
            0,
            0)]
        [TestCase(FeatConstants.SimpleWeaponProficiencyId + WeaponConstants.Dagger,
            FeatConstants.SimpleWeaponProficiencyId,
            WeaponConstants.Dagger,
            0,
            "",
            "",
            0,
            0,
            0)]
        [TestCase(FeatConstants.SimpleWeaponProficiencyId + WeaponConstants.HeavyCrossbow,
            FeatConstants.SimpleWeaponProficiencyId,
            WeaponConstants.HeavyCrossbow,
            0,
            "",
            "",
            0,
            0,
            0)]
        [TestCase(FeatConstants.SimpleWeaponProficiencyId + WeaponConstants.LightCrossbow,
            FeatConstants.SimpleWeaponProficiencyId,
            WeaponConstants.LightCrossbow,
            0,
            "",
            "",
            0,
            0,
            0)]
        [TestCase(FeatConstants.SimpleWeaponProficiencyId + WeaponConstants.Quarterstaff,
            FeatConstants.SimpleWeaponProficiencyId,
            WeaponConstants.Quarterstaff,
            0,
            "",
            "",
            0,
            0,
            0)]
        public override void Data(String name, String featId, String focusType, Int32 frequencyQuantity, String frequencyQuantityStat, String frequencyTimePeriod, Int32 minimumLevel, Int32 maximumLevel, Int32 strength)
        {
            base.Data(name, featId, focusType, frequencyQuantity, frequencyQuantityStat, frequencyTimePeriod, minimumLevel, maximumLevel, strength);
        }
    }
}
