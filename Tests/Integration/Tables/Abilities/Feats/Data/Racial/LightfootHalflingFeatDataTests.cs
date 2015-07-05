using System;
using NPCGen.Common.Abilities.Feats;
using NPCGen.Common.Abilities.Skills;
using NPCGen.Common.Races;
using NPCGen.Tables.Interfaces;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Tables.Abilities.Feats.Data.Racial
{
    [TestFixture]
    public class LightfootHalflingFeatDataTests : RacialFeatDataTests
    {
        protected override String tableName
        {
            get { return String.Format(TableNameConstants.Formattable.Collection.RACEFeatData, RaceConstants.BaseRaces.LightfootHalflingId); }
        }

        [Test]
        public override void CollectionNames()
        {
            var names = new[] 
            {
                FeatConstants.SaveBonusId + "All",
                FeatConstants.SaveBonusId + "Fear",
                FeatConstants.AttackBonusId + "ThrowOrSling",
                FeatConstants.SkillBonusId + SkillConstants.Listen,
                FeatConstants.SkillBonusId + SkillConstants.Climb,
                FeatConstants.SkillBonusId + SkillConstants.Jump,
                FeatConstants.SkillBonusId + SkillConstants.MoveSilently
            };

            AssertCollectionNames(names);
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
        [TestCase(FeatConstants.SkillBonusId + SkillConstants.Listen,
            FeatConstants.SkillBonusId,
            SkillConstants.Listen,
            0,
            "",
            0,
            "",
            2)]
        [TestCase(FeatConstants.SkillBonusId + SkillConstants.Climb,
            FeatConstants.SkillBonusId,
            SkillConstants.Climb,
            0,
            "",
            0,
            "",
            2)]
        [TestCase(FeatConstants.SkillBonusId + SkillConstants.Jump,
            FeatConstants.SkillBonusId,
            SkillConstants.Jump,
            0,
            "",
            0,
            "",
            2)]
        [TestCase(FeatConstants.SkillBonusId + SkillConstants.MoveSilently,
            FeatConstants.SkillBonusId,
            SkillConstants.MoveSilently,
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