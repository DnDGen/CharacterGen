using CharacterGen.Common.Abilities.Feats;
using CharacterGen.Common.Abilities.Skills;
using CharacterGen.Tables;
using NUnit.Framework;
using System;

namespace CharacterGen.Tests.Integration.Tables.Abilities.Feats.Requirements.Skills
{
    [TestFixture]
    public class MagicalAptitudeSkillRankRequirementsTests : AdjustmentsTests
    {
        protected override String tableName
        {
            get { return String.Format(TableNameConstants.Formattable.Adjustments.FEATSkillRankRequirements, FeatConstants.MagicalAptitude); }
        }

        [Test]
        public override void CollectionNames()
        {
            var skills = new[] { SkillConstants.Spellcraft, SkillConstants.UseMagicDevice };
            AssertCollectionNames(skills);
        }

        [TestCase(SkillConstants.Spellcraft, 0)]
        [TestCase(SkillConstants.UseMagicDevice, 0)]
        public override void Adjustment(String name, Int32 adjustment)
        {
            base.Adjustment(name, adjustment);
        }
    }
}
