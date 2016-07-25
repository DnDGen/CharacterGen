﻿using CharacterGen.Abilities.Feats;
using CharacterGen.Abilities.Skills;
using CharacterGen.Domain.Tables;
using NUnit.Framework;
using System;

namespace CharacterGen.Tests.Integration.Tables.Abilities.Feats.Requirements.Skills
{
    [TestFixture]
    public class DiligentSkillRankRequirementsTests : AdjustmentsTests
    {
        protected override string tableName
        {
            get { return string.Format(TableNameConstants.Formattable.Adjustments.FEATSkillRankRequirements, FeatConstants.Diligent); }
        }

        [Test]
        public override void CollectionNames()
        {
            var skills = new[] { SkillConstants.Appraise, SkillConstants.DecipherScript };
            AssertCollectionNames(skills);
        }

        [TestCase(SkillConstants.Appraise, 0)]
        [TestCase(SkillConstants.DecipherScript, 0)]
        public override void Adjustment(string name, int adjustment)
        {
            base.Adjustment(name, adjustment);
        }
    }
}