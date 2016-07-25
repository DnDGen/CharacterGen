﻿using System;
using CharacterGen.Abilities.Feats;
using CharacterGen.Abilities.Stats;
using CharacterGen.Domain.Tables;
using NUnit.Framework;

namespace CharacterGen.Tests.Integration.Tables.Abilities.Feats.Requirements.Stats
{
    [TestFixture]
    public class CombatExpertiseStatRequirementsTests : AdjustmentsTests
    {
        protected override string tableName
        {
            get { return string.Format(TableNameConstants.Formattable.Adjustments.FEATStatRequirements, FeatConstants.CombatExpertise); }
        }

        [Test]
        public override void CollectionNames()
        {
            var stats = new[] { StatConstants.Intelligence };
            AssertCollectionNames(stats);
        }

        [TestCase(StatConstants.Intelligence, 13)]
        public override void Adjustment(string name, int adjustment)
        {
            base.Adjustment(name, adjustment);
        }
    }
}