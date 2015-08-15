using CharacterGen.Common.Abilities.Feats;
using CharacterGen.Common.Abilities.Skills;
using CharacterGen.Tables;
using NUnit.Framework;
using System;

namespace CharacterGen.Tests.Integration.Tables.Abilities.Feats.Requirements.Skills
{
    [TestFixture]
    public class MountedArcherySkillRankRequirementsTests : AdjustmentsTests
    {
        protected override String tableName
        {
            get { return String.Format(TableNameConstants.Formattable.Adjustments.FEATSkillRankRequirements, FeatConstants.MountedArchery); }
        }

        [Test]
        public override void CollectionNames()
        {
            var skills = new[] { SkillConstants.Ride };
            AssertCollectionNames(skills);
        }

        [TestCase(SkillConstants.Ride, 1)]
        public override void Adjustment(String name, Int32 adjustment)
        {
            base.Adjustment(name, adjustment);
        }
    }
}
