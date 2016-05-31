using System;
using CharacterGen.Abilities.Feats;
using CharacterGen.CharacterClasses;
using CharacterGen.Domain.Tables;
using NUnit.Framework;

namespace CharacterGen.Tests.Integration.Tables.Abilities.Feats.Requirements.Classes
{
    [TestFixture]
    public class ImprovedEvasionClassRequirementsTests : AdjustmentsTests
    {
        protected override string tableName
        {
            get { return string.Format(TableNameConstants.Formattable.Adjustments.FEATClassRequirements, FeatConstants.ImprovedEvasion); }
        }

        [Test]
        public override void CollectionNames()
        {
            var classes = new[] 
            {
                CharacterClassConstants.Rogue,
                CharacterClassConstants.Monk
            };

            AssertCollectionNames(classes);
        }

        [TestCase(CharacterClassConstants.Rogue, 10)]
        [TestCase(CharacterClassConstants.Monk, 9)]
        public override void Adjustment(string name, int adjustment)
        {
            base.Adjustment(name, adjustment);
        }
    }
}
