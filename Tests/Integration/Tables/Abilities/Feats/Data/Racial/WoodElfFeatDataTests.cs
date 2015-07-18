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
            get { return String.Format(TableNameConstants.Formattable.Collection.RACEFeatData, RaceConstants.BaseRaces.WoodElf); }
        }

        [Test]
        public override void CollectionNames()
        {
            var names = new[] 
            {
                FeatConstants.ImmuneToEffect,
                FeatConstants.SaveBonus,
                FeatConstants.LowLightVision,
                FeatConstants.MartialWeaponProficiency + WeaponConstants.Longsword,
                FeatConstants.MartialWeaponProficiency + WeaponConstants.Rapier,
                FeatConstants.MartialWeaponProficiency + WeaponConstants.Longbow,
                FeatConstants.MartialWeaponProficiency + WeaponConstants.Shortbow,
                FeatConstants.PassiveSecretDoorSearch,
                FeatConstants.SkillBonus + SkillConstants.Search,
                FeatConstants.SkillBonus + SkillConstants.Spot,
                FeatConstants.SkillBonus + SkillConstants.Listen
            };

            AssertCollectionNames(names);
        }

        [TestCase(FeatConstants.MartialWeaponProficiency + WeaponConstants.Longsword,
            FeatConstants.MartialWeaponProficiency,
            WeaponConstants.Longsword,
            0,
            "",
            0,
            "",
            0)]
        [TestCase(FeatConstants.MartialWeaponProficiency + WeaponConstants.Rapier,
            FeatConstants.MartialWeaponProficiency,
            WeaponConstants.Rapier,
            0,
            "",
            0,
            "",
            0)]
        [TestCase(FeatConstants.MartialWeaponProficiency + WeaponConstants.Longbow,
            FeatConstants.MartialWeaponProficiency,
            WeaponConstants.Longbow,
            0,
            "",
            0,
            "",
            0)]
        [TestCase(FeatConstants.MartialWeaponProficiency + WeaponConstants.Shortbow,
            FeatConstants.MartialWeaponProficiency,
            WeaponConstants.Shortbow,
            0,
            "",
            0,
            "",
            0)]
        [TestCase(FeatConstants.SaveBonus,
            FeatConstants.SaveBonus,
            "Enchantment spells or effects",
            0,
            "",
            0,
            "",
            2)]
        [TestCase(FeatConstants.ImmuneToEffect,
            FeatConstants.ImmuneToEffect,
            "Sleep",
            0,
            "",
            0,
            "",
            0)]
        [TestCase(FeatConstants.LowLightVision,
            FeatConstants.LowLightVision,
            "",
            0,
            "",
            0,
            "",
            0)]
        [TestCase(FeatConstants.SkillBonus + SkillConstants.Listen,
            FeatConstants.SkillBonus,
            SkillConstants.Listen,
            0,
            "",
            0,
            "",
            2)]
        [TestCase(FeatConstants.SkillBonus + SkillConstants.Search,
            FeatConstants.SkillBonus,
            SkillConstants.Search,
            0,
            "",
            0,
            "",
            2)]
        [TestCase(FeatConstants.SkillBonus + SkillConstants.Spot,
            FeatConstants.SkillBonus,
            SkillConstants.Spot,
            0,
            "",
            0,
            "",
            2)]
        [TestCase(FeatConstants.PassiveSecretDoorSearch,
            FeatConstants.PassiveSecretDoorSearch,
            "",
            0,
            "",
            0,
            "",
            0)]
        public override void Data(String name, String feat, String focus, Int32 frequencyQuantity, String frequencyTimePeriod, Int32 minimumHitDiceRequirement, String sizeRequirement, Int32 strength)
        {
            base.Data(name, feat, focus, frequencyQuantity, frequencyTimePeriod, minimumHitDiceRequirement, sizeRequirement, strength);
        }
    }
}
