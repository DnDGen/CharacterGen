using System;
using NPCGen.Common.Abilities.Feats;
using NPCGen.Common.Abilities.Skills;
using NPCGen.Common.Races;
using NPCGen.Tables.Interfaces;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Tables.Abilities.Feats.Data.Racial
{
    [TestFixture]
    public class DeepHalflingFeatDataTests : RacialFeatDataTests
    {
        protected override String tableName
        {
            get { return String.Format(TableNameConstants.Formattable.Collection.RACEFeatData, RaceConstants.BaseRaces.DeepHalflingId); }
        }

        [TestCase(FeatConstants.SaveBonusId + "Fear",
            FeatConstants.SaveBonusId,
            "Fear",
            0,
            "",
            0,
            "",
            2)]
        [TestCase(FeatConstants.SaveBonusId + "All",
            FeatConstants.SaveBonusId,
            "",
            0,
            "",
            0,
            "",
            1)]
        [TestCase(FeatConstants.AttackBonusId + "ThrowOrSling",
            FeatConstants.AttackBonusId,
            "Thrown weapons and slings",
            0,
            "",
            0,
            "",
            1)]
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
        [TestCase(FeatConstants.SkillBonusId + SkillConstants.Listen,
            FeatConstants.SkillBonusId,
            SkillConstants.Listen,
            0,
            "",
            0,
            "",
            2)]
        [TestCase(FeatConstants.SkillBonusId + SkillConstants.Appraise,
            FeatConstants.SkillBonusId,
            SkillConstants.Appraise + " (Stone or metal items)",
            0,
            "",
            0,
            "",
            2)]
        public override void Data(String name, String featId, String focus, Int32 frequencyQuantity, String frequencyTimePeriod, Int32 minimumHitDiceRequirement, String sizeRequirement, Int32 strength)
        {
            base.Data(name, featId, focus, frequencyQuantity, frequencyTimePeriod, minimumHitDiceRequirement, sizeRequirement, strength);
        }
    }
}
