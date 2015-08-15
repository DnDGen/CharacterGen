using CharacterGen.Common.Abilities.Feats;
using CharacterGen.Common.Abilities.Stats;
using CharacterGen.Tables;
using NUnit.Framework;
using System;

namespace CharacterGen.Tests.Integration.Tables.Abilities.Feats.Requirements.Stats
{
    [TestFixture]
    public class NaturalSpellStatRequirementsTests : AdjustmentsTests
    {
        protected override String tableName
        {
            get { return String.Format(TableNameConstants.Formattable.Adjustments.FEATStatRequirements, FeatConstants.NaturalSpell); }
        }

        [Test]
        public override void CollectionNames()
        {
            var stats = new[] { StatConstants.Wisdom };
            AssertCollectionNames(stats);
        }

        [TestCase(StatConstants.Wisdom, 13)]
        public override void Adjustment(String name, Int32 adjustment)
        {
            base.Adjustment(name, adjustment);
        }
    }
}
