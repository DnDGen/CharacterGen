using CharacterGen.Abilities.Feats;
using CharacterGen.Abilities.Skills;
using CharacterGen.Domain.Tables;
using NUnit.Framework;
using System;

namespace CharacterGen.Tests.Integration.Tables.Abilities.Feats.Requirements.Skills
{
    [TestFixture]
    public class NatureSenseSkillRankRequirementsTests : AdjustmentsTests
    {
        protected override string tableName
        {
            get { return string.Format(TableNameConstants.Formattable.Adjustments.FEATSkillRankRequirements, FeatConstants.NatureSense); }
        }

        [Test]
        public override void CollectionNames()
        {
            var skills = new[] { SkillConstants.KnowledgeNature, SkillConstants.Survival };
            AssertCollectionNames(skills);
        }

        [TestCase(SkillConstants.KnowledgeNature, 0)]
        [TestCase(SkillConstants.Survival, 0)]
        public override void Adjustment(string name, int adjustment)
        {
            base.Adjustment(name, adjustment);
        }
    }
}
