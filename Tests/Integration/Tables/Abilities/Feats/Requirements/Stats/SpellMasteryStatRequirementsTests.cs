using CharacterGen.Common.Abilities.Feats;
using CharacterGen.Common.Abilities.Stats;
using CharacterGen.Tables;
using NUnit.Framework;
using System;

namespace CharacterGen.Tests.Integration.Tables.Abilities.Feats.Requirements.Stats
{
    [TestFixture]
    public class SpellMasteryStatRequirementsTests : AdjustmentsTests
    {
        protected override String tableName
        {
            get { return String.Format(TableNameConstants.Formattable.Adjustments.FEATStatRequirements, FeatConstants.SpellMastery); }
        }

        [Test]
        public override void CollectionNames()
        {
            var stats = new[] { StatConstants.Intelligence };
            AssertCollectionNames(stats);
        }

        [TestCase(StatConstants.Intelligence, 12)]
        public override void Adjustment(String name, Int32 adjustment)
        {
            base.Adjustment(name, adjustment);
        }
    }
}
