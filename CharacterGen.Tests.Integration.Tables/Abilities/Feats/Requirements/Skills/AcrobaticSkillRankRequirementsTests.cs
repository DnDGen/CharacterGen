using CharacterGen.Abilities.Feats;
using CharacterGen.Abilities.Skills;
using CharacterGen.Domain.Tables;
using NUnit.Framework;
using System;

namespace CharacterGen.Tests.Integration.Tables.Abilities.Feats.Requirements.Skills
{
    [TestFixture]
    public class AcrobaticSkillRankRequirementsTests : AdjustmentsTests
    {
        protected override string tableName
        {
            get { return string.Format(TableNameConstants.Formattable.Adjustments.FEATSkillRankRequirements, FeatConstants.Acrobatic); }
        }

        [Test]
        public override void CollectionNames()
        {
            var skills = new[] { SkillConstants.Jump, SkillConstants.Tumble };
            AssertCollectionNames(skills);
        }

        [TestCase(SkillConstants.Jump, 0)]
        [TestCase(SkillConstants.Tumble, 0)]
        public override void Adjustment(string name, int adjustment)
        {
            base.Adjustment(name, adjustment);
        }
    }
}
