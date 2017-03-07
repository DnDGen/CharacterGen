using CharacterGen.Abilities.Feats;
using CharacterGen.Abilities.Skills;
using CharacterGen.Domain.Tables;
using NUnit.Framework;
using System.Linq;

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
            var skills = new[]
            {
                $"{SkillConstants.Knowledge}/{SkillConstants.Foci.Knowledge.Nature}",
                SkillConstants.Survival
            };
            AssertCollectionNames(skills);
        }

        [TestCase(SkillConstants.Knowledge, 0, SkillConstants.Foci.Knowledge.Nature)]
        [TestCase(SkillConstants.Survival, 0)]
        public void RequiredSkill(string skill, int ranks, string focus = "")
        {
            var name = skill;
            if (focus.Any())
                name = $"{skill}/{focus}";

            Adjustment(name, ranks);
        }
    }
}
