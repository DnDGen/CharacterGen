using System;
using CharacterGen.Abilities.Feats;
using CharacterGen.Abilities.Stats;
using CharacterGen.Domain.Tables;
using NUnit.Framework;

namespace CharacterGen.Tests.Integration.Tables.Abilities.Feats.Requirements.Stats
{
    [TestFixture]
    public class DodgeStatRequirementsTests : AdjustmentsTests
    {
        protected override string tableName
        {
            get { return string.Format(TableNameConstants.Formattable.Adjustments.FEATStatRequirements, FeatConstants.Dodge); }
        }

        [Test]
        public override void CollectionNames()
        {
            var stats = new[] { StatConstants.Dexterity };
            AssertCollectionNames(stats);
        }

        [TestCase(StatConstants.Dexterity, 13)]
        public override void Adjustment(string name, int adjustment)
        {
            base.Adjustment(name, adjustment);
        }
    }
}
