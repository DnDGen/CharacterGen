using CharacterGen.Abilities.Feats;
using CharacterGen.Abilities.Skills;
using CharacterGen.Domain.Tables;
using NUnit.Framework;
using System;

namespace CharacterGen.Tests.Integration.Tables.Abilities.Feats.Requirements.Skills
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
