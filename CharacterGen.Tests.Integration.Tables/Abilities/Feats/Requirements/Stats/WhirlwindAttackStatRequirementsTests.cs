using CharacterGen.Abilities.Feats;
using CharacterGen.Abilities.Stats;
using CharacterGen.Domain.Tables;
using NUnit.Framework;
using System;

namespace CharacterGen.Tests.Integration.Tables.Abilities.Feats.Requirements.Stats
{
    [TestFixture]
    public class WhirlwindAttackStatRequirementsTests : AdjustmentsTests
    {
        protected override string tableName
        {
            get { return string.Format(TableNameConstants.Formattable.Adjustments.FEATStatRequirements, FeatConstants.WhirlwindAttack); }
        }

        [Test]
        public override void CollectionNames()
        {
            var stats = new[] { StatConstants.Dexterity, StatConstants.Intelligence };
            AssertCollectionNames(stats);
        }

        [TestCase(StatConstants.Dexterity, 13)]
        [TestCase(StatConstants.Intelligence, 13)]
        public override void Adjustment(string name, int adjustment)
        {
            base.Adjustment(name, adjustment);
        }
    }
}
