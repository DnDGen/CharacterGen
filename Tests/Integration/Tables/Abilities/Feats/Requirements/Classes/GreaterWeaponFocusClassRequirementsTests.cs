using System;
using NPCGen.Common.Abilities.Feats;
using NPCGen.Common.CharacterClasses;
using NPCGen.Tables.Interfaces;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Tables.Abilities.Feats.Requirements.Classes
{
    [TestFixture]
    public class GreaterWeaponFocusClassRequirementsTests : AdjustmentsTests
    {
        protected override String tableName
        {
            get { return String.Format(TableNameConstants.Formattable.Adjustments.FEATClassRequirements, FeatConstants.GreaterWeaponFocus); }
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
        public override void Adjustment(String name, Int32 adjustment)
        {
            base.Adjustment(name, adjustment);
        }
    }
}
