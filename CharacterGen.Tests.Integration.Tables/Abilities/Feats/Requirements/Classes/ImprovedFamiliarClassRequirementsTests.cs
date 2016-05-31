using CharacterGen.Abilities.Feats;
using CharacterGen.CharacterClasses;
using CharacterGen.Domain.Tables;
using NUnit.Framework;
using System;

namespace CharacterGen.Tests.Integration.Tables.Abilities.Feats.Requirements.Classes
{
    [TestFixture]
    public class ImprovedFamiliarClassRequirementsTests : AdjustmentsTests
    {
        protected override string tableName
        {
            get
            {
                return string.Format(TableNameConstants.Formattable.Adjustments.FEATClassRequirements, FeatConstants.ImprovedFamiliar);
            }
        }

        [Test]
        public override void CollectionNames()
        {
            var classes = new[]
            {
                CharacterClassConstants.Sorcerer,
                CharacterClassConstants.Wizard
            };

            AssertCollectionNames(classes);
        }

        [TestCase(CharacterClassConstants.Sorcerer, 1)]
        [TestCase(CharacterClassConstants.Wizard, 1)]
        public override void Adjustment(string name, int adjustment)
        {
            base.Adjustment(name, adjustment);
        }
    }
}
