using System;
using EquipmentGen.Common.Items;
using NPCGen.Common.Abilities.Feats;
using NPCGen.Common.Abilities.Skills;
using NPCGen.Common.Races;
using NPCGen.Tables.Interfaces;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Tables.Abilities.Feats.Data.Racial
{
    [TestFixture]
    public class HighElfFeatDataTests : CollectionTests
    {
        protected override String tableName
        {
            get { return String.Format(TableNameConstants.Formattable.Collection.RACEFeatData, RaceConstants.BaseRaces.HighElfId); }
        }

        [TestCase(FeatConstants.MartialWeaponProficiencyId + WeaponConstants.Longsword,
            FeatConstants.MartialWeaponProficiencyId,
            "",
            "0",
            "0",
            WeaponConstants.Longsword,
            "0",
            "")]
        [TestCase(FeatConstants.MartialWeaponProficiencyId + WeaponConstants.Rapier,
            FeatConstants.MartialWeaponProficiencyId,
            "",
            "0",
            "0",
            WeaponConstants.Rapier,
            "0",
            "")]
        [TestCase(FeatConstants.MartialWeaponProficiencyId + WeaponConstants.Longbow,
            FeatConstants.MartialWeaponProficiencyId,
            "",
            "0",
            "0",
            WeaponConstants.Longbow,
            "0",
            "")]
        [TestCase(FeatConstants.MartialWeaponProficiencyId + WeaponConstants.Shortbow,
            FeatConstants.MartialWeaponProficiencyId,
            "",
            "0",
            "0",
            WeaponConstants.Shortbow,
            "0",
            "")]
        [TestCase(FeatConstants.SaveBonusId,
            FeatConstants.SaveBonusId,
            "",
            "0",
            "2",
            "Enchantment spells or effects",
            "0",
            "")]
        [TestCase(FeatConstants.ImmuneToEffectId,
            FeatConstants.ImmuneToEffectId,
            "",
            "0",
            "0",
            "Sleep",
            "0",
            "")]
        [TestCase(FeatConstants.LowLightVisionId,
            FeatConstants.LowLightVisionId,
            "",
            "0",
            "0",
            "",
            "0",
            "")]
        [TestCase(FeatConstants.SkillBonusId + SkillConstants.Listen,
            FeatConstants.SkillBonusId,
            "",
            "0",
            "2",
            SkillConstants.Listen,
            "0",
            "")]
        [TestCase(FeatConstants.SkillBonusId + SkillConstants.Search,
            FeatConstants.SkillBonusId,
            "",
            "0",
            "2",
            SkillConstants.Search,
            "0",
            "")]
        [TestCase(FeatConstants.SkillBonusId + SkillConstants.Spot,
            FeatConstants.SkillBonusId,
            "",
            "0",
            "2",
            SkillConstants.Spot,
            "0",
            "")]
        public override void OrderedCollection(String name, params String[] collection)
        {
            base.OrderedCollection(name, collection);
        }
    }
}