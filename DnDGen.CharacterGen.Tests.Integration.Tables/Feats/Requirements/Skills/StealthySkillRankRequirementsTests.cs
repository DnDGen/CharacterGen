﻿using DnDGen.CharacterGen.Tables;
using DnDGen.CharacterGen.Feats;
using DnDGen.CharacterGen.Skills;
using NUnit.Framework;

namespace DnDGen.CharacterGen.Tests.Integration.Tables.Feats.Requirements.Skills
{
    [TestFixture]
    public class StealthySkillRankRequirementsTests : AdjustmentsTests
    {
        protected override string tableName
        {
            get { return string.Format(TableNameConstants.Formattable.Adjustments.FEATSkillRankRequirements, FeatConstants.Stealthy); }
        }

        [Test]
        public override void CollectionNames()
        {
            var skills = new[] { SkillConstants.Hide, SkillConstants.MoveSilently };
            AssertCollectionNames(skills);
        }

        [TestCase(SkillConstants.Hide, 0)]
        [TestCase(SkillConstants.MoveSilently, 0)]
        public override void Adjustment(string name, int adjustment)
        {
            base.Adjustment(name, adjustment);
        }
    }
}
