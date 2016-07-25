﻿using CharacterGen.Abilities.Feats;
using CharacterGen.Abilities.Skills;
using CharacterGen.Domain.Tables;
using NUnit.Framework;
using System;

namespace CharacterGen.Tests.Integration.Tables.Abilities.Feats.Requirements.Skills
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