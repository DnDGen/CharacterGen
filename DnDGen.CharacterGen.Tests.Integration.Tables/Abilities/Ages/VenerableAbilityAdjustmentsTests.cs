using DnDGen.CharacterGen.Abilities;
using DnDGen.CharacterGen.Tables;
using DnDGen.CharacterGen.Races;
using NUnit.Framework;

namespace DnDGen.CharacterGen.Tests.Integration.Tables.Abilities.Ages
{
    [TestFixture]
    public class VenerableAbilityAdjustmentsTests : AdjustmentsTests
    {
        protected override string tableName
        {
            get
            {
                return string.Format(TableNameConstants.Formattable.Adjustments.AGEAbilityAdjustments, RaceConstants.Ages.Venerable);
            }
        }

        public override void CollectionNames()
        {
            var names = new[]
            {
                AbilityConstants.Charisma,
                AbilityConstants.Constitution,
                AbilityConstants.Dexterity,
                AbilityConstants.Intelligence,
                AbilityConstants.Strength,
                AbilityConstants.Wisdom
            };

            AssertCollectionNames(names);
        }

        [TestCase(AbilityConstants.Charisma, 3)]
        [TestCase(AbilityConstants.Constitution, -6)]
        [TestCase(AbilityConstants.Dexterity, -6)]
        [TestCase(AbilityConstants.Intelligence, 3)]
        [TestCase(AbilityConstants.Strength, -6)]
        [TestCase(AbilityConstants.Wisdom, 3)]
        public override void Adjustment(string name, int adjustment)
        {
            base.Adjustment(name, adjustment);
        }
    }
}
