using CharacterGen.Common.Abilities.Feats;
using CharacterGen.Common.CharacterClasses;
using CharacterGen.Tables;
using NUnit.Framework;
using System;

namespace CharacterGen.Tests.Integration.Tables.Abilities.Feats.Requirements.Classes
{
    [TestFixture]
    public class ScribeScrollClassRequirementsTests : AdjustmentsTests
    {
        protected override String tableName
        {
            get
            {
                return String.Format(TableNameConstants.Formattable.Adjustments.FEATClassRequirements, FeatConstants.ScribeScroll);
            }
        }

        [Test]
        public override void CollectionNames()
        {
            var classes = new[]
            {
                CharacterClassConstants.Bard,
                CharacterClassConstants.Cleric,
                CharacterClassConstants.Druid,
                CharacterClassConstants.Paladin,
                CharacterClassConstants.Ranger,
                CharacterClassConstants.Sorcerer,
                CharacterClassConstants.Wizard
            };

            AssertCollectionNames(classes);
        }

        [TestCase(CharacterClassConstants.Bard, 1)]
        [TestCase(CharacterClassConstants.Cleric, 1)]
        [TestCase(CharacterClassConstants.Druid, 1)]
        [TestCase(CharacterClassConstants.Paladin, 4)]
        [TestCase(CharacterClassConstants.Ranger, 4)]
        [TestCase(CharacterClassConstants.Sorcerer, 1)]
        [TestCase(CharacterClassConstants.Wizard, 1)]
        public override void Adjustment(String name, Int32 adjustment)
        {
            base.Adjustment(name, adjustment);
        }
    }
}
