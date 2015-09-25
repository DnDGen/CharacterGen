using CharacterGen.Common.Abilities.Stats;
using CharacterGen.Common.Races;
using CharacterGen.Tables;
using NUnit.Framework;
using System;

namespace CharacterGen.Tests.Integration.Tables.Abilities.Stats
{
    [TestFixture]
    public class AdulthoodStatAdjustmentsTests : AdjustmentsTests
    {
        protected override string tableName
        {
            get
            {
                return String.Format(TableNameConstants.Formattable.Adjustments.AGEStatAdjustments, RaceConstants.Ages.Adulthood);
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

        [TestCase(StatConstants.Charisma, 0)]
        [TestCase(StatConstants.Constitution, 0)]
        [TestCase(StatConstants.Dexterity, 0)]
        [TestCase(StatConstants.Intelligence, 0)]
        [TestCase(StatConstants.Strength, 0)]
        [TestCase(StatConstants.Wisdom, 0)]
        public override void Adjustment(string name, int adjustment)
        {
            base.Adjustment(name, adjustment);
        }
    }
}
