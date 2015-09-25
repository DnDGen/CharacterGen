using CharacterGen.Common.Abilities.Stats;
using CharacterGen.Common.Races;
using CharacterGen.Tables;
using NUnit.Framework;
using System;

namespace CharacterGen.Tests.Integration.Tables.Abilities.Stats
{
    [TestFixture]
    public class VenerableStatAdjustmentsTests : AdjustmentsTests
    {
        protected override string tableName
        {
            get
            {
                return String.Format(TableNameConstants.Formattable.Adjustments.AGEStatAdjustments, RaceConstants.Ages.Venerable);
            }
        }

        public override void CollectionNames()
        {
            var names = new[]
            {
                StatConstants.Charisma,
                StatConstants.Constitution,
                StatConstants.Dexterity,
                StatConstants.Intelligence,
                StatConstants.Strength,
                StatConstants.Wisdom
            };

            AssertCollectionNames(names);
        }

        [TestCase(StatConstants.Charisma, 3)]
        [TestCase(StatConstants.Constitution, -6)]
        [TestCase(StatConstants.Dexterity, -6)]
        [TestCase(StatConstants.Intelligence, 3)]
        [TestCase(StatConstants.Strength, -6)]
        [TestCase(StatConstants.Wisdom, 3)]
        public override void Adjustment(string name, int adjustment)
        {
            base.Adjustment(name, adjustment);
        }
    }
}
