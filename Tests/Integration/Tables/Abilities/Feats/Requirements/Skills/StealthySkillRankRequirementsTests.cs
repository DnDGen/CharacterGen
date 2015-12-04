using CharacterGen.Common.Abilities.Feats;
using CharacterGen.Common.Abilities.Skills;
using CharacterGen.Tables;
using NUnit.Framework;
using System;

namespace CharacterGen.Tests.Integration.Tables.Abilities.Feats.Requirements.Skills
{
    [TestFixture]
    public class StealthySkillRankRequirementsTests : AdjustmentsTests
    {
        protected override String tableName
        {
            get { return String.Format(TableNameConstants.Formattable.Adjustments.FEATSkillRankRequirements, FeatConstants.Stealthy); }
        }

        [Test]
        public override void CollectionNames()
        {
            var skills = new[] { SkillConstants.Hide, SkillConstants.MoveSilently };
            AssertCollectionNames(skills);
        }

        [TestCase(SkillConstants.Hide, 0)]
        [TestCase(SkillConstants.MoveSilently, 0)]
        public override void Adjustment(String name, Int32 adjustment)
        {
            base.Adjustment(name, adjustment);
        }
    }
}
