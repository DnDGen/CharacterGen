﻿using CharacterGen.Domain.Tables;
using CharacterGen.Feats;
using CharacterGen.Skills;
using NUnit.Framework;

namespace CharacterGen.Tests.Integration.Tables.Feats.Requirements.Skills
{
    [TestFixture]
    public class AlertnessSkillRankRequirementsTests : AdjustmentsTests
    {
        protected override string tableName
        {
            get { return string.Format(TableNameConstants.Formattable.Adjustments.FEATSkillRankRequirements, FeatConstants.Alertness); }
        }

        [Test]
        public override void CollectionNames()
        {
            var skills = new[] { SkillConstants.Listen, SkillConstants.Spot };
            AssertCollectionNames(skills);
        }

        [TestCase(SkillConstants.Listen, 0)]
        [TestCase(SkillConstants.Spot, 0)]
        public override void Adjustment(string name, int adjustment)
        {
            base.Adjustment(name, adjustment);
        }
    }
}
