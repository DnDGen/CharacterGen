using System;
using EquipmentGen.Common.Items;
using NPCGen.Common.Abilities.Feats;
using NPCGen.Common.CharacterClasses;
using NPCGen.Common.Items;
using NPCGen.Tables.Interfaces;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Tables.Abilities.Feats.Data.CharacterClasses
{
    [TestFixture]
    public class BardFeatDataTests : CharacterClassFeatDataTests
    {
        protected override String tableName
        {
            get { return String.Format(TableNameConstants.Formattable.Collection.CLASSFeatData, CharacterClassConstants.Bard); }
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
        [TestCase(FeatConstants.MartialWeaponProficiencyId + WeaponConstants.Longsword,
            FeatConstants.MartialWeaponProficiencyId,
            WeaponConstants.Longsword,
            0,
            "",
            "",
            0,
            0,
            0)]
        [TestCase(FeatConstants.MartialWeaponProficiencyId + WeaponConstants.Rapier,
            FeatConstants.MartialWeaponProficiencyId,
            WeaponConstants.Rapier,
            0,
            "",
            "",
            0,
            0,
            0)]
        [TestCase(FeatConstants.MartialWeaponProficiencyId + WeaponConstants.Sap,
            FeatConstants.MartialWeaponProficiencyId,
            WeaponConstants.Sap,
            0,
            "",
            "",
            0,
            0,
            0)]
        [TestCase(FeatConstants.MartialWeaponProficiencyId + WeaponConstants.ShortSword,
            FeatConstants.MartialWeaponProficiencyId,
            WeaponConstants.ShortSword,
            0,
            "",
            "",
            0,
            0,
            0)]
        [TestCase(FeatConstants.MartialWeaponProficiencyId + WeaponConstants.Shortbow,
            FeatConstants.MartialWeaponProficiencyId,
            WeaponConstants.Shortbow,
            0,
            "",
            "",
            0,
            0,
            0)]
        [TestCase(FeatConstants.MartialWeaponProficiencyId + WeaponConstants.Whip,
            FeatConstants.MartialWeaponProficiencyId,
            WeaponConstants.Whip,
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
        [TestCase(FeatConstants.ShieldProficiencyId,
            FeatConstants.ShieldProficiencyId,
            "",
            0,
            "",
            "",
            0,
            0,
            0)]
        [TestCase(FeatConstants.BardicMusicId,
            FeatConstants.BardicMusicId,
            "",
            0,
            "",
            "",
            1,
            0,
            0)]
        [TestCase(FeatConstants.BardicKnowledgeId,
            FeatConstants.BardicKnowledgeId,
            "",
            0,
            "",
            "",
            1,
            0,
            0)]
        [TestCase(FeatConstants.CountersongId,
            FeatConstants.CountersongId,
            "",
            0,
            "",
            "",
            1,
            0,
            0)]
        [TestCase(FeatConstants.FascinateId,
            FeatConstants.FascinateId,
            "",
            0,
            "",
            "",
            1,
            0,
            0)]
        [TestCase(FeatConstants.InspireCourageId + "1",
            FeatConstants.InspireCourageId,
            "",
            0,
            "",
            "",
            1,
            7,
            1)]
        [TestCase(FeatConstants.InspireCourageId + "2",
            FeatConstants.InspireCourageId,
            "",
            0,
            "",
            "",
            8,
            13,
            2)]
        [TestCase(FeatConstants.InspireCourageId + "3",
            FeatConstants.InspireCourageId,
            "",
            0,
            "",
            "",
            14,
            19,
            3)]
        [TestCase(FeatConstants.InspireCourageId + "4",
            FeatConstants.InspireCourageId,
            "",
            0,
            "",
            "",
            20,
            0,
            4)]
        [TestCase(FeatConstants.InspireCompetenceId,
            FeatConstants.InspireCompetenceId,
            "",
            0,
            "",
            "",
            3,
            0,
            0)]
        [TestCase(FeatConstants.SuggestionId,
            FeatConstants.SuggestionId,
            "",
            0,
            "",
            "",
            6,
            0,
            0)]
        [TestCase(FeatConstants.InspireGreatnessId,
            FeatConstants.InspireGreatnessId,
            "",
            0,
            "",
            "",
            9,
            0,
            0)]
        [TestCase(FeatConstants.SongOfFreedomId,
            FeatConstants.SongOfFreedomId,
            "",
            0,
            "",
            "",
            12,
            0,
            0)]
        [TestCase(FeatConstants.InspireHeroicsId,
            FeatConstants.InspireHeroicsId,
            "",
            0,
            "",
            "",
            15,
            0,
            0)]
        [TestCase(FeatConstants.MassSuggestionId,
            FeatConstants.MassSuggestionId,
            "",
            0,
            "",
            "",
            18,
            0,
            0)]
        public override void Data(String name, String featId, String focusType, Int32 frequencyQuantity, String frequencyQuantityStat, String frequencyTimePeriod, Int32 minimumLevel, Int32 maximumLevel, Int32 strength)
        {
            base.Data(name, featId, focusType, frequencyQuantity, frequencyQuantityStat, frequencyTimePeriod, minimumLevel, maximumLevel, strength);
        }
    }
}
