using CharacterGen.Common.Abilities.Stats;
using CharacterGen.Common.Races;
using CharacterGen.Tables;
using NUnit.Framework;
using System;

namespace CharacterGen.Tests.Integration.Tables.Abilities.Stats
{
    [TestFixture]
    public class MiddleAgeStatAdjustmentsTests : AdjustmentsTests
    {
        protected override string tableName
        {
            get
            {
                return String.Format(TableNameConstants.Formattable.Adjustments.AGEStatAdjustments, RaceConstants.Ages.MiddleAge);
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

        [TestCase(StatConstants.Charisma, 1)]
        [TestCase(StatConstants.Constitution, -1)]
        [TestCase(StatConstants.Dexterity, -1)]
        [TestCase(StatConstants.Intelligence, 1)]
        [TestCase(StatConstants.Strength, -1)]
        [TestCase(StatConstants.Wisdom, 1)]
        public override void Adjustment(string name, int adjustment)
        {
            base.Adjustment(name, adjustment);
        }
    }
}
