using CharacterGen.Abilities.Feats;
using CharacterGen.Abilities.Skills;
using CharacterGen.Domain.Tables;
using NUnit.Framework;
using System;

namespace CharacterGen.Tests.Integration.Tables.Abilities.Feats.Requirements.Skills
{
    [TestFixture]
    public class MountedCombatSkillRankRequirementsTests : AdjustmentsTests
    {
        protected override string tableName
        {
            get { return string.Format(TableNameConstants.Formattable.Adjustments.FEATSkillRankRequirements, FeatConstants.MountedCombat); }
        }

        [Test]
        public override void CollectionNames()
        {
            var skills = new[] { SkillConstants.Ride };
            AssertCollectionNames(skills);
        }

        [TestCase(SkillConstants.Ride, 1)]
        public override void Adjustment(string name, int adjustment)
        {
            base.Adjustment(name, adjustment);
        }
    }
}
