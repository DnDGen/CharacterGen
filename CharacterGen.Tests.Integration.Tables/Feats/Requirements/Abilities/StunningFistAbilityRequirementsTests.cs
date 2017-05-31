using CharacterGen.Abilities;
using CharacterGen.Domain.Tables;
using CharacterGen.Feats;
using NUnit.Framework;

namespace CharacterGen.Tests.Integration.Tables.Feats.Requirements.Abilities
{
    [TestFixture]
    public class StunningFistAbilityRequirementsTests : AdjustmentsTests
    {
        protected override string tableName
        {
            get { return string.Format(TableNameConstants.Formattable.Adjustments.FEATAbilityRequirements, FeatConstants.StunningFist); }
        }

        [Test]
        public override void CollectionNames()
        {
            var stats = new[] { AbilityConstants.Dexterity, AbilityConstants.Wisdom };
            AssertCollectionNames(stats);
        }

        [TestCase(AbilityConstants.Dexterity, 13)]
        [TestCase(AbilityConstants.Wisdom, 13)]
        public void AbilityRequirementForFeat(string name, int adjustment)
        {
            base.Adjustment(name, adjustment);
        }
    }
}
