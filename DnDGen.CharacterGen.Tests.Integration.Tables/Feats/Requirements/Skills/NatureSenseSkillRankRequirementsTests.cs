﻿using DnDGen.CharacterGen.Tables;
using DnDGen.CharacterGen.Feats;
using DnDGen.CharacterGen.Skills;
using NUnit.Framework;
using System.Linq;

namespace DnDGen.CharacterGen.Tests.Integration.Tables.Feats.Requirements.Skills
{
    [TestFixture]
    public class NatureSenseSkillRankRequirementsTests : AdjustmentsTests
    {
        protected override string tableName
        {
            get { return string.Format(TableNameConstants.Formattable.Adjustments.FEATSkillRankRequirements, FeatConstants.NatureSense); }
        }

        [Test]
        public override void CollectionNames()
        {
            var skills = new[]
            {
                $"{SkillConstants.Knowledge}/{SkillConstants.Foci.Knowledge.Nature}",
                SkillConstants.Survival
            };
            AssertCollectionNames(skills);
        }

        [TestCase(SkillConstants.Knowledge, 0, SkillConstants.Foci.Knowledge.Nature)]
        [TestCase(SkillConstants.Survival, 0)]
        public void RequiredSkill(string skill, int ranks, string focus = "")
        {
            var name = skill;
            if (focus.Any())
                name = $"{skill}/{focus}";

            Adjustment(name, ranks);
        }
    }
}
