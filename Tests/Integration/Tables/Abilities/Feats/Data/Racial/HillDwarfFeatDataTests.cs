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
    public class HillDwarfFeatDataTests : CollectionTests
    {
        protected override String tableName
        {
            get { return String.Format(TableNameConstants.Formattable.Collection.RACEFeatData, RaceConstants.BaseRaces.HillDwarfId); }
        }

        [TestCase(FeatConstants.SkillBonusId + SkillConstants.Appraise,
            FeatConstants.SkillBonusId,
            "",
            "0",
            "2",
            SkillConstants.Appraise + " (Stone or metal items)",
            "0",
            "")]
        [TestCase(FeatConstants.AttackBonusId + RaceConstants.BaseRaces.Goblin,
            FeatConstants.AttackBonusId,
            "",
            "0",
            "1",
            "Goblinoids",
            "0",
            "")]
        [TestCase(FeatConstants.AttackBonusId + RaceConstants.BaseRaces.Orc,
            FeatConstants.AttackBonusId,
            "",
            "0",
            "1",
            "Orcs",
            "0",
            "")]
        [TestCase(FeatConstants.SaveBonusId + "Spell",
            FeatConstants.SaveBonusId,
            "",
            "0",
            "2",
            "Spells and spell-like effects",
            "0",
            "")]
        [TestCase(FeatConstants.SaveBonusId + "Poison",
            FeatConstants.SaveBonusId,
            "",
            "0",
            "2",
            "Poison",
            "0",
            "")]
        [TestCase(FeatConstants.DodgeBonusId,
            FeatConstants.DodgeBonusId,
            "",
            "0",
            "4",
            "Giants",
            "0",
            "")]
        [TestCase(FeatConstants.StabilityId,
            FeatConstants.StabilityId,
            "",
            "0",
            "0",
            "",
            "0",
            "")]
        [TestCase(FeatConstants.WeaponFamiliarityId + WeaponConstants.DwarvenWaraxe,
            FeatConstants.WeaponFamiliarityId,
            "",
            "0",
            "0",
            WeaponConstants.DwarvenWaraxe,
            "0",
            "")]
        [TestCase(FeatConstants.WeaponFamiliarityId + WeaponConstants.DwarvenUrgrosh,
            FeatConstants.WeaponFamiliarityId,
            "",
            "0",
            "0",
            WeaponConstants.DwarvenUrgrosh,
            "0",
            "")]
        [TestCase(FeatConstants.StonecunningId,
            FeatConstants.StonecunningId,
            "",
            "0",
            "0",
            "",
            "0",
            "")]
        [TestCase(FeatConstants.DarkvisionId,
            FeatConstants.DarkvisionId,
            "",
            "0",
            "60",
            "",
            "0",
            "")]
        public override void OrderedCollection(String name, params String[] collection)
        {
            base.OrderedCollection(name, collection);
        }
    }
}