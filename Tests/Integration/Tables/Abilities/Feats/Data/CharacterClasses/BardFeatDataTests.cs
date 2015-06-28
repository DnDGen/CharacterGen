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
    public class BardFeatDataTests : CollectionTests
    {
        protected override String tableName
        {
            get { return String.Format(TableNameConstants.Formattable.Collection.CLASSFeatData, CharacterClassConstants.Bard); }
        }

        [TestCase(FeatConstants.SimpleWeaponProficiencyId,
            FeatConstants.SimpleWeaponProficiencyId,
            "0",
            WeaponProficiencyConstants.All,
            "0",
            "0",
            "",
            "0")]
        [TestCase(FeatConstants.MartialWeaponProficiencyId + WeaponConstants.Longsword,
            FeatConstants.MartialWeaponProficiencyId,
            "0",
            WeaponConstants.Longsword,
            "0",
            "0",
            "",
            "0")]
        [TestCase(FeatConstants.MartialWeaponProficiencyId + WeaponConstants.Rapier,
            FeatConstants.MartialWeaponProficiencyId,
            "0",
            WeaponConstants.Rapier,
            "0",
            "0",
            "",
            "0")]
        [TestCase(FeatConstants.MartialWeaponProficiencyId + WeaponConstants.Sap,
            FeatConstants.MartialWeaponProficiencyId,
            "0",
            WeaponConstants.Sap,
            "0",
            "0",
            "",
            "0")]
        [TestCase(FeatConstants.MartialWeaponProficiencyId + WeaponConstants.ShortSword,
            FeatConstants.MartialWeaponProficiencyId,
            "0",
            WeaponConstants.ShortSword,
            "0",
            "0",
            "",
            "0")]
        [TestCase(FeatConstants.MartialWeaponProficiencyId + WeaponConstants.Shortbow,
            FeatConstants.MartialWeaponProficiencyId,
            "0",
            WeaponConstants.Shortbow,
            "0",
            "0",
            "",
            "0")]
        [TestCase(FeatConstants.MartialWeaponProficiencyId + WeaponConstants.Whip,
            FeatConstants.MartialWeaponProficiencyId,
            "0",
            WeaponConstants.Whip,
            "0",
            "0",
            "",
            "0")]
        [TestCase(FeatConstants.LightArmorProficiencyId,
            FeatConstants.LightArmorProficiencyId,
            "0",
            "",
            "0",
            "0",
            "",
            "0")]
        [TestCase(FeatConstants.ShieldProficiencyId,
            FeatConstants.ShieldProficiencyId,
            "0",
            "",
            "0",
            "0",
            "",
            "0")]
        [TestCase(FeatConstants.BardicMusicId,
            FeatConstants.BardicMusicId,
            "1",
            "",
            "0",
            "0",
            "",
            "0")]
        [TestCase(FeatConstants.BardicKnowledgeId,
            FeatConstants.BardicKnowledgeId,
            "1",
            "",
            "0",
            "0",
            "",
            "0")]
        [TestCase(FeatConstants.CountersongId,
            FeatConstants.CountersongId,
            "1",
            "",
            "0",
            "0",
            "",
            "0")]
        [TestCase(FeatConstants.FascinateId,
            FeatConstants.FascinateId,
            "1",
            "",
            "0",
            "0",
            "",
            "0")]
        [TestCase(FeatConstants.InspireCourageId + "1",
            FeatConstants.InspireCourageId,
            "1",
            "",
            "1",
            "0",
            "",
            "7")]
        [TestCase(FeatConstants.InspireCourageId + "2",
            FeatConstants.InspireCourageId,
            "8",
            "",
            "2",
            "0",
            "",
            "13")]
        [TestCase(FeatConstants.InspireCourageId + "3",
            FeatConstants.InspireCourageId,
            "14",
            "",
            "3",
            "0",
            "",
            "19")]
        [TestCase(FeatConstants.InspireCourageId + "4",
            FeatConstants.InspireCourageId,
            "20",
            "",
            "4",
            "0",
            "",
            "0")]
        [TestCase(FeatConstants.InspireCompetenceId,
            FeatConstants.InspireCompetenceId,
            "3",
            "",
            "0",
            "0",
            "",
            "0")]
        [TestCase(FeatConstants.SuggestionId,
            FeatConstants.SuggestionId,
            "6",
            "",
            "0",
            "0",
            "",
            "0")]
        [TestCase(FeatConstants.InspireGreatnessId,
            FeatConstants.InspireGreatnessId,
            "9",
            "",
            "0",
            "0",
            "",
            "0")]
        [TestCase(FeatConstants.SongOfFreedomId,
            FeatConstants.SongOfFreedomId,
            "12",
            "",
            "0",
            "0",
            "",
            "0")]
        [TestCase(FeatConstants.InspireHeroicsId,
            FeatConstants.InspireHeroicsId,
            "15",
            "",
            "0",
            "0",
            "",
            "0")]
        [TestCase(FeatConstants.MassSuggestionId,
            FeatConstants.MassSuggestionId,
            "18",
            "",
            "0",
            "0",
            "",
            "0")]
        public override void OrderedCollection(String name, params String[] collection)
        {
            base.OrderedCollection(name, collection);
        }
    }
}
