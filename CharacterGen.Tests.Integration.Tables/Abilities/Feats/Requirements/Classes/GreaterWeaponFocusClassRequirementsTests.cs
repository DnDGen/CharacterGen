using System;
using CharacterGen.Abilities.Feats;
using CharacterGen.CharacterClasses;
using CharacterGen.Domain.Tables;
using NUnit.Framework;

namespace CharacterGen.Tests.Integration.Tables.Abilities.Feats.Requirements.Classes
{
    [TestFixture]
    public class GreaterWeaponFocusClassRequirementsTests : AdjustmentsTests
    {
        protected override string tableName
        {
            get { return string.Format(TableNameConstants.Formattable.Adjustments.FEATClassRequirements, FeatConstants.GreaterWeaponFocus); }
        }

        [Test]
        public override void CollectionNames()
        {
            var classes = new[] 
            {
                CharacterClassConstants.Fighter
            };

            AssertCollectionNames(classes);
        }

        [TestCase(CharacterClassConstants.Fighter, 8)]
        public override void Adjustment(string name, int adjustment)
        {
            base.Adjustment(name, adjustment);
        }
    }
}
