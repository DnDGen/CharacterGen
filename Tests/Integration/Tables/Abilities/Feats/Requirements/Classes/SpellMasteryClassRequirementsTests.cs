using CharacterGen.Common.Abilities.Feats;
using CharacterGen.Common.CharacterClasses;
using CharacterGen.Tables;
using NUnit.Framework;
using System;

namespace CharacterGen.Tests.Integration.Tables.Abilities.Feats.Requirements.Classes
{
    [TestFixture]
    public class SpellMasteryClassRequirementsTests : AdjustmentsTests
    {
        protected override String tableName
        {
            get
            {
                return String.Format(TableNameConstants.Formattable.Adjustments.FEATClassRequirements, FeatConstants.SpellMastery);
            }
        }

        [Test]
        public override void CollectionNames()
        {
            var classes = new[] { CharacterClassConstants.Wizard };
            AssertCollectionNames(classes);
        }

        [TestCase(CharacterClassConstants.Wizard, 1)]
        public override void Adjustment(String name, Int32 adjustment)
        {
            base.Adjustment(name, adjustment);
        }
    }
}
