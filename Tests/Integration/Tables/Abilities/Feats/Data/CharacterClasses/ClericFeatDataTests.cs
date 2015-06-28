using System;
using NPCGen.Common.Abilities.Feats;
using NPCGen.Common.Abilities.Stats;
using NPCGen.Common.CharacterClasses;
using NPCGen.Common.Items;
using NPCGen.Tables.Interfaces;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Tables.Abilities.Feats.Data.CharacterClasses
{
    [TestFixture]
    public class ClericFeatDataTests : CharacterClassFeatDataTests
    {
        protected override String tableName
        {
            get { return String.Format(TableNameConstants.Formattable.Collection.CLASSFeatData, CharacterClassConstants.Cleric); }
        }

        [TestCase(FeatConstants.SimpleWeaponProficiencyId,
            FeatConstants.SimpleWeaponProficiencyId,
            WeaponProficiencyConstants.All,
            0,
            "",
            "",
            0,
            0,
            0)]
        [TestCase(FeatConstants.LightArmorProficiencyId,
            FeatConstants.LightArmorProficiencyId,
            "",
            0,
            "",
            "",
            0,
            0,
            0)]
        [TestCase(FeatConstants.MediumArmorProficiencyId,
            FeatConstants.MediumArmorProficiencyId,
            "",
            0,
            "",
            "",
            0,
            0,
            0)]
        [TestCase(FeatConstants.HeavyArmorProficiencyId,
            FeatConstants.HeavyArmorProficiencyId,
            "",
            0,
            "",
            "",
            0,
            0,
            0)]
        [TestCase(FeatConstants.ShieldProficiencyId,
            FeatConstants.ShieldProficiencyId,
            "",
            0,
            "",
            "",
            0,
            0,
            0)]
        [TestCase(FeatConstants.TurnId,
            FeatConstants.TurnId,
            "Undead",
            3,
            StatConstants.Charisma,
            FeatConstants.Frequencies.Day,
            0,
            0,
            0)]
        public override void Data(String name, String featId, String focusType, Int32 frequencyQuantity, String frequencyQuantityStat, String frequencyTimePeriod, Int32 minimumLevel, Int32 maximumLevel, Int32 strength)
        {
            base.Data(name, featId, focusType, frequencyQuantity, frequencyQuantityStat, frequencyTimePeriod, minimumLevel, maximumLevel, strength);
        }
    }
}