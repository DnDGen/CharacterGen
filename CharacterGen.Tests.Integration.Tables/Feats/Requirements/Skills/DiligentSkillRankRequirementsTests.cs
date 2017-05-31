using CharacterGen.Domain.Tables;
using CharacterGen.Feats;
using CharacterGen.Skills;
using NUnit.Framework;

namespace CharacterGen.Tests.Integration.Tables.Feats.Requirements.Skills
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
