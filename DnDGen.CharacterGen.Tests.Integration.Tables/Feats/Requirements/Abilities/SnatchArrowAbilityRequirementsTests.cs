﻿using DnDGen.CharacterGen.Abilities;
using DnDGen.CharacterGen.Tables;
using DnDGen.CharacterGen.Feats;
using NUnit.Framework;

namespace DnDGen.CharacterGen.Tests.Integration.Tables.Feats.Requirements.Abilities
{
    [TestFixture]
    public class SnatchArrowAbilityRequirementsTests : AdjustmentsTests
    {
        protected override string tableName
        {
            get { return string.Format(TableNameConstants.Formattable.Adjustments.FEATAbilityRequirements, FeatConstants.SnatchArrows); }
        }

        [Test]
        public override void CollectionNames()
        {
            var stats = new[] { AbilityConstants.Dexterity };
            AssertCollectionNames(stats);
        }

        [TestCase(AbilityConstants.Dexterity, 15)]
        public void AbilityRequirementForFeat(string name, int adjustment)
        {
            base.Adjustment(name, adjustment);
        }
    }
}
