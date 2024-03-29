﻿using DnDGen.CharacterGen.Tables;
using DnDGen.CharacterGen.Feats;
using DnDGen.CharacterGen.Skills;
using NUnit.Framework;

namespace DnDGen.CharacterGen.Tests.Integration.Tables.Feats.Requirements.Skills
{
    [TestFixture]
    public class AthleticSkillRankRequirementsTests : AdjustmentsTests
    {
        protected override string tableName
        {
            get { return string.Format(TableNameConstants.Formattable.Adjustments.FEATSkillRankRequirements, FeatConstants.Athletic); }
        }

        [Test]
        public override void CollectionNames()
        {
            var skills = new[] { SkillConstants.Climb, SkillConstants.Swim };
            AssertCollectionNames(skills);
        }

        [TestCase(SkillConstants.Climb, 0)]
        [TestCase(SkillConstants.Swim, 0)]
        public override void Adjustment(string name, int adjustment)
        {
            base.Adjustment(name, adjustment);
        }
    }
}
