using CharacterGen.Common.Abilities.Stats;
using CharacterGen.Common.Races;
using CharacterGen.Tables;
using NUnit.Framework;
using System;

namespace CharacterGen.Tests.Integration.Tables.Abilities.Stats
{
    [TestFixture]
    public class OldStatAdjustmentsTests : AdjustmentsTests
    {
        protected override string tableName
        {
            get
            {
                return String.Format(TableNameConstants.Formattable.Adjustments.AGEStatAdjustments, RaceConstants.Ages.Old);
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

        [TestCase(StatConstants.Charisma, 2)]
        [TestCase(StatConstants.Constitution, -3)]
        [TestCase(StatConstants.Dexterity, -3)]
        [TestCase(StatConstants.Intelligence, 2)]
        [TestCase(StatConstants.Strength, -3)]
        [TestCase(StatConstants.Wisdom, 2)]
        public override void Adjustment(string name, int adjustment)
        {
            base.Adjustment(name, adjustment);
        }
    }
}
