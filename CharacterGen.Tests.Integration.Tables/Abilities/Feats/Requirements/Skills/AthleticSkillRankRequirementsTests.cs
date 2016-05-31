using CharacterGen.Abilities.Feats;
using CharacterGen.Abilities.Skills;
using CharacterGen.Domain.Tables;
using NUnit.Framework;
using System;

namespace CharacterGen.Tests.Integration.Tables.Abilities.Feats.Requirements.Skills
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
