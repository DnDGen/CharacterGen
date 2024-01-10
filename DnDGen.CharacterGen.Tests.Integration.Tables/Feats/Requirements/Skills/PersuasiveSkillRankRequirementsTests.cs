using DnDGen.CharacterGen.Tables;
using DnDGen.CharacterGen.Feats;
using DnDGen.CharacterGen.Skills;
using NUnit.Framework;

namespace DnDGen.CharacterGen.Tests.Integration.Tables.Feats.Requirements.Skills
{
    [TestFixture]
    public class PersuasiveSkillRankRequirementsTests : AdjustmentsTests
    {
        protected override string tableName
        {
            get { return string.Format(TableNameConstants.Formattable.Adjustments.FEATSkillRankRequirements, FeatConstants.Persuasive); }
        }

        [Test]
        public override void CollectionNames()
        {
            var skills = new[] { SkillConstants.Bluff, SkillConstants.Intimidate };
            AssertCollectionNames(skills);
        }

        [TestCase(SkillConstants.Bluff, 0)]
        [TestCase(SkillConstants.Intimidate, 0)]
        public override void Adjustment(string name, int adjustment)
        {
            base.Adjustment(name, adjustment);
        }
    }
}
