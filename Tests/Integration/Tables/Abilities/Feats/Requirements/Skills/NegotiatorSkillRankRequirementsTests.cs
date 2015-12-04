using CharacterGen.Common.Abilities.Feats;
using CharacterGen.Common.Abilities.Skills;
using CharacterGen.Tables;
using NUnit.Framework;
using System;

namespace CharacterGen.Tests.Integration.Tables.Abilities.Feats.Requirements.Skills
{
    [TestFixture]
    public class NegotiatorSkillRankRequirementsTests : AdjustmentsTests
    {
        protected override String tableName
        {
            get { return String.Format(TableNameConstants.Formattable.Adjustments.FEATSkillRankRequirements, FeatConstants.Negotiator); }
        }

        [Test]
        public override void CollectionNames()
        {
            var skills = new[] { SkillConstants.Diplomacy, SkillConstants.SenseMotive };
            AssertCollectionNames(skills);
        }

        [TestCase(SkillConstants.Diplomacy, 0)]
        [TestCase(SkillConstants.SenseMotive, 0)]
        public override void Adjustment(String name, Int32 adjustment)
        {
            base.Adjustment(name, adjustment);
        }
    }
}
