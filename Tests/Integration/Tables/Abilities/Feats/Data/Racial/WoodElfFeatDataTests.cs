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
    public class WoodElfFeatDataTests : RacialFeatDataTests
    {
        protected override String tableName
        {
            get { return String.Format(TableNameConstants.Formattable.Collection.RACEFeatData, RaceConstants.BaseRaces.WoodElfId); }
        }

        [Test]
        public override void CollectionNames()
        {
            var names = new[] 
            {
                FeatConstants.ImmuneToEffectId,
                FeatConstants.SaveBonusId,
                FeatConstants.LowLightVisionId,
                FeatConstants.MartialWeaponProficiencyId + WeaponConstants.Longsword,
                FeatConstants.MartialWeaponProficiencyId + WeaponConstants.Rapier,
                FeatConstants.MartialWeaponProficiencyId + WeaponConstants.Longbow,
                FeatConstants.MartialWeaponProficiencyId + WeaponConstants.Shortbow,
                FeatConstants.PassiveSecretDoorSearchId,
                FeatConstants.SkillBonusId + SkillConstants.Search,
                FeatConstants.SkillBonusId + SkillConstants.Spot,
                FeatConstants.SkillBonusId + SkillConstants.Listen
            };

            AssertCollectionNames(names);
        }

        [TestCase(FeatConstants.MartialWeaponProficiencyId + WeaponConstants.Longsword,
            FeatConstants.MartialWeaponProficiencyId,
            WeaponConstants.Longsword,
            0,
            "",
            0,
            "",
            0)]
        [TestCase(FeatConstants.MartialWeaponProficiencyId + WeaponConstants.Rapier,
            FeatConstants.MartialWeaponProficiencyId,
            WeaponConstants.Rapier,
            0,
            "",
            0,
            "",
            0)]
        [TestCase(FeatConstants.MartialWeaponProficiencyId + WeaponConstants.Longbow,
            FeatConstants.MartialWeaponProficiencyId,
            WeaponConstants.Longbow,
            0,
            "",
            0,
            "",
            0)]
        [TestCase(FeatConstants.MartialWeaponProficiencyId + WeaponConstants.Shortbow,
            FeatConstants.MartialWeaponProficiencyId,
            WeaponConstants.Shortbow,
            0,
            "",
            0,
            "",
            0)]
        [TestCase(FeatConstants.SaveBonusId,
            FeatConstants.SaveBonusId,
            "Enchantment spells or effects",
            0,
            "",
            0,
            "",
            2)]
        [TestCase(FeatConstants.ImmuneToEffectId,
            FeatConstants.ImmuneToEffectId,
            "Sleep",
            0,
            "",
            0,
            "",
            0)]
        [TestCase(FeatConstants.LowLightVisionId,
            FeatConstants.LowLightVisionId,
            "",
            0,
            "",
            0,
            "",
            0)]
        [TestCase(FeatConstants.SkillBonusId + SkillConstants.Listen,
            FeatConstants.SkillBonusId,
            SkillConstants.Listen,
            0,
            "",
            0,
            "",
            2)]
        [TestCase(FeatConstants.SkillBonusId + SkillConstants.Search,
            FeatConstants.SkillBonusId,
            SkillConstants.Search,
            0,
            "",
            0,
            "",
            2)]
        [TestCase(FeatConstants.SkillBonusId + SkillConstants.Spot,
            FeatConstants.SkillBonusId,
            SkillConstants.Spot,
            0,
            "",
            0,
            "",
            2)]
        [TestCase(FeatConstants.PassiveSecretDoorSearchId,
            FeatConstants.PassiveSecretDoorSearchId,
            "",
            0,
            "",
            0,
            "",
            0)]
        public override void Data(String name, String featId, String focus, Int32 frequencyQuantity, String frequencyTimePeriod, Int32 minimumHitDiceRequirement, String sizeRequirement, Int32 strength)
        {
            base.Data(name, featId, focus, frequencyQuantity, frequencyTimePeriod, minimumHitDiceRequirement, sizeRequirement, strength);
        }
    }
}
