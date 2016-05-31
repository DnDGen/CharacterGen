using CharacterGen.Abilities.Feats;
using CharacterGen.Abilities.Stats;
using CharacterGen.Domain.Tables;
using NUnit.Framework;
using System;

namespace CharacterGen.Tests.Integration.Tables.Abilities.Feats.Requirements.Stats
{
    [TestFixture]
    public class NaturalSpellStatRequirementsTests : AdjustmentsTests
    {
        protected override string tableName
        {
            get { return string.Format(TableNameConstants.Formattable.Adjustments.FEATStatRequirements, FeatConstants.NaturalSpell); }
        }

        [Test]
        public override void CollectionNames()
        {
            var stats = new[] { StatConstants.Wisdom };
            AssertCollectionNames(stats);
        }

        [TestCase(StatConstants.Wisdom, 13)]
        public override void Adjustment(string name, int adjustment)
        {
            base.Adjustment(name, adjustment);
        }
    }
}
