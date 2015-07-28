using CharacterGen.Common.Abilities.Feats;
using CharacterGen.Common.CharacterClasses;
using CharacterGen.Tables;
using NUnit.Framework;
using System;

namespace CharacterGen.Tests.Integration.Tables.Abilities.Feats.Requirements.Classes
{
    [TestFixture]
    public class LeadershipClassRequirementsTests : AdjustmentsTests
    {
        protected override String tableName
        {
            get
            {
                return String.Format(TableNameConstants.Formattable.Adjustments.FEATClassRequirements, FeatConstants.Leadership);
            }
        }

        [Test]
        public override void CollectionNames()
        {
            var classes = new[]
            {
                CharacterClassConstants.Barbarian,
                CharacterClassConstants.Bard,
                CharacterClassConstants.Cleric,
                CharacterClassConstants.Druid,
                CharacterClassConstants.Fighter,
                CharacterClassConstants.Monk,
                CharacterClassConstants.Paladin,
                CharacterClassConstants.Ranger,
                CharacterClassConstants.Rogue,
                CharacterClassConstants.Sorcerer,
                CharacterClassConstants.Wizard
            };

            AssertCollectionNames(classes);
        }

        [TestCase(CharacterClassConstants.Barbarian, 6)]
        [TestCase(CharacterClassConstants.Bard, 6)]
        [TestCase(CharacterClassConstants.Cleric, 6)]
        [TestCase(CharacterClassConstants.Druid, 6)]
        [TestCase(CharacterClassConstants.Fighter, 6)]
        [TestCase(CharacterClassConstants.Monk, 6)]
        [TestCase(CharacterClassConstants.Paladin, 6)]
        [TestCase(CharacterClassConstants.Ranger, 6)]
        [TestCase(CharacterClassConstants.Rogue, 6)]
        [TestCase(CharacterClassConstants.Sorcerer, 6)]
        [TestCase(CharacterClassConstants.Wizard, 6)]
        public override void Adjustment(String name, Int32 adjustment)
        {
            base.Adjustment(name, adjustment);
        }
    }
}
