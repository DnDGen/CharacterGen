using DnDGen.CharacterGen.Tables;
using DnDGen.CharacterGen.Feats;
using DnDGen.CharacterGen.Skills;
using NUnit.Framework;

namespace DnDGen.CharacterGen.Tests.Integration.Tables.Feats.Requirements.Skills
{
    [TestFixture]
    public class NimbleFingerSkillRankRequirementsTests : AdjustmentsTests
    {
        protected override string tableName
        {
            get { return string.Format(TableNameConstants.Formattable.Adjustments.FEATSkillRankRequirements, FeatConstants.NimbleFingers); }
        }

        [Test]
        public override void CollectionNames()
        {
            var skills = new[] { SkillConstants.DisableDevice, SkillConstants.OpenLock };
            AssertCollectionNames(skills);
        }

        [TestCase(SkillConstants.DisableDevice, 0)]
        [TestCase(SkillConstants.OpenLock, 0)]
        public override void Adjustment(string name, int adjustment)
        {
            base.Adjustment(name, adjustment);
        }
    }
}
