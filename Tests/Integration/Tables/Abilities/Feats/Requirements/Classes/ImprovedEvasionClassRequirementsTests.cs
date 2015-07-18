using System;
using NPCGen.Common.Abilities.Feats;
using NPCGen.Common.CharacterClasses;
using NPCGen.Tables.Interfaces;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Tables.Abilities.Feats.Requirements.Classes
{
    [TestFixture]
    public class ImprovedEvasionClassRequirementsTests : AdjustmentsTests
    {
        protected override String tableName
        {
            get { return String.Format(TableNameConstants.Formattable.Adjustments.FEATClassRequirements, FeatConstants.ImprovedEvasion); }
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
        public override void Adjustment(String name, Int32 adjustment)
        {
            base.Adjustment(name, adjustment);
        }
    }
}
