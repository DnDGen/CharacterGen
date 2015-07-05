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
    public class HillDwarfFeatDataTests : RacialFeatDataTests
    {
        protected override String tableName
        {
            get { return String.Format(TableNameConstants.Formattable.Collection.RACEFeatData, RaceConstants.BaseRaces.HillDwarfId); }
        }

        [Test]
        public override void CollectionNames()
        {
            var names = new[] 
            {
                FeatConstants.DarkvisionId,
                FeatConstants.StonecunningId,
                FeatConstants.WeaponFamiliarityId + WeaponConstants.DwarvenUrgrosh,
                FeatConstants.WeaponFamiliarityId + WeaponConstants.DwarvenWaraxe,
                FeatConstants.StabilityId,
                FeatConstants.SaveBonusId + "Poison",
                FeatConstants.SaveBonusId + "Spell",
                FeatConstants.AttackBonusId + RaceConstants.BaseRaces.Orc,
                FeatConstants.AttackBonusId + RaceConstants.BaseRaces.Goblin,
                FeatConstants.DodgeBonusId,
                FeatConstants.SkillBonusId + SkillConstants.Appraise
            };

            AssertCollectionNames(names);
        }

        [TestCase(FeatConstants.SkillBonusId + SkillConstants.Appraise,
            FeatConstants.SkillBonusId,
            SkillConstants.Appraise + " (Stone or metal items)",
            0,
            "",
            0,
            "",
            2)]
        [TestCase(FeatConstants.AttackBonusId + RaceConstants.BaseRaces.Goblin,
            FeatConstants.AttackBonusId,
            "Goblinoids",
            0,
            "",
            0,
            "",
            1)]
        [TestCase(FeatConstants.AttackBonusId + RaceConstants.BaseRaces.Orc,
            FeatConstants.AttackBonusId,
            "Orcs",
            0,
            "",
            0,
            "",
            1)]
        [TestCase(FeatConstants.SaveBonusId + "Spell",
            FeatConstants.SaveBonusId,
            "Spells and spell-like effects",
            0,
            "",
            0,
            "",
            2)]
        [TestCase(FeatConstants.SaveBonusId + "Poison",
            FeatConstants.SaveBonusId,
            "Poison",
            0,
            "",
            0,
            "",
            2)]
        [TestCase(FeatConstants.DodgeBonusId,
            FeatConstants.DodgeBonusId,
            "Giants",
            0,
            "",
            0,
            "",
            4)]
        [TestCase(FeatConstants.StabilityId,
            FeatConstants.StabilityId,
            "",
            0,
            "",
            0,
            "",
            0)]
        [TestCase(FeatConstants.WeaponFamiliarityId + WeaponConstants.DwarvenWaraxe,
            FeatConstants.WeaponFamiliarityId,
            WeaponConstants.DwarvenWaraxe,
            0,
            "",
            0,
            "",
            0)]
        [TestCase(FeatConstants.WeaponFamiliarityId + WeaponConstants.DwarvenUrgrosh,
            FeatConstants.WeaponFamiliarityId,
            WeaponConstants.DwarvenUrgrosh,
            0,
            "",
            0,
            "",
            0)]
        [TestCase(FeatConstants.StonecunningId,
            FeatConstants.StonecunningId,
            "",
            0,
            "",
            0,
            "",
            0)]
        [TestCase(FeatConstants.DarkvisionId,
            FeatConstants.DarkvisionId,
            "",
            0,
            "",
            0,
            "",
            60)]
        public override void Data(String name, String featId, String focus, Int32 frequencyQuantity, String frequencyTimePeriod, Int32 minimumHitDiceRequirement, String sizeRequirement, Int32 strength)
        {
            base.Data(name, featId, focus, frequencyQuantity, frequencyTimePeriod, minimumHitDiceRequirement, sizeRequirement, strength);
        }
    }
}