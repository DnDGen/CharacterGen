using CharacterGen.Domain.Tables;
using CharacterGen.Feats;
using CharacterGen.Skills;
using NUnit.Framework;

namespace CharacterGen.Tests.Integration.Tables.Feats.Requirements.Skills
{
    [TestFixture]
    public class MagicalAptitudeSkillRankRequirementsTests : AdjustmentsTests
    {
        protected override string tableName
        {
            get { return string.Format(TableNameConstants.Formattable.Adjustments.FEATSkillRankRequirements, FeatConstants.MagicalAptitude); }
        }

        [Test]
        public override void CollectionNames()
        {
            var skills = new[] { SkillConstants.Spellcraft, SkillConstants.UseMagicDevice };
            AssertCollectionNames(skills);
        }

        [TestCase(SkillConstants.Spellcraft, 0)]
        [TestCase(SkillConstants.UseMagicDevice, 0)]
        public override void Adjustment(string name, int adjustment)
        {
            base.Adjustment(name, adjustment);
        }
    }
}
