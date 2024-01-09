using DnDGen.CharacterGen.Tables;
using DnDGen.CharacterGen.Feats;
using DnDGen.CharacterGen.Skills;
using NUnit.Framework;

namespace DnDGen.CharacterGen.Tests.Integration.Tables.Feats.Requirements.Skills
{
    [TestFixture]
    public class SpiritedChargeSkillRankRequirementsTests : AdjustmentsTests
    {
        protected override string tableName
        {
            get { return string.Format(TableNameConstants.Formattable.Adjustments.FEATSkillRankRequirements, FeatConstants.SpiritedCharge); }
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
