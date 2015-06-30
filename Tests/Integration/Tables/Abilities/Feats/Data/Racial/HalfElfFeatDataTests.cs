using System;
using NPCGen.Common.Abilities.Feats;
using NPCGen.Common.Abilities.Skills;
using NPCGen.Common.Races;
using NPCGen.Tables.Interfaces;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Tables.Abilities.Feats.Data.Racial
{
    [TestFixture]
    public class HalfElfFeatDataTests : RacialFeatDataTests
    {
        protected override String tableName
        {
            get { return String.Format(TableNameConstants.Formattable.Collection.RACEFeatData, RaceConstants.BaseRaces.HalfElfId); }
        }

        [TestCase(FeatConstants.SkillBonusId + SkillConstants.Listen,
            FeatConstants.SkillBonusId,
            "",
            "0",
            "1",
            SkillConstants.Listen,
            "0",
            "")]
        [TestCase(FeatConstants.SkillBonusId + SkillConstants.Search,
            FeatConstants.SkillBonusId,
            "",
            "0",
            "1",
            SkillConstants.Search,
            "0",
            "")]
        [TestCase(FeatConstants.SkillBonusId + SkillConstants.Spot,
            FeatConstants.SkillBonusId,
            "",
            "0",
            "1",
            SkillConstants.Spot,
            "0",
            "")]
        [TestCase(FeatConstants.SkillBonusId + SkillConstants.Diplomacy,
            FeatConstants.SkillBonusId,
            "",
            "0",
            "2",
            SkillConstants.Diplomacy,
            "0",
            "")]
        [TestCase(FeatConstants.SkillBonusId + SkillConstants.GatherInformation,
            FeatConstants.SkillBonusId,
            "",
            "0",
            "2",
            SkillConstants.GatherInformation,
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
        [TestCase(FeatConstants.ElvenBloodId,
            FeatConstants.ElvenBloodId,
            "",
            "0",
            "0",
            "",
            "0",
            "")]
        public override void OrderedCollection(String name, params String[] collection)
        {
            base.OrderedCollection(name, collection);
        }
    }
}
