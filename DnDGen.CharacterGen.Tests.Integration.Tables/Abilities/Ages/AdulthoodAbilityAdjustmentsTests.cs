using DnDGen.CharacterGen.Abilities;
using DnDGen.CharacterGen.Tables;
using DnDGen.CharacterGen.Races;
using NUnit.Framework;

namespace DnDGen.CharacterGen.Tests.Integration.Tables.Abilities.Ages
{
    [TestFixture]
    public class AdulthoodAbilityAdjustmentsTests : AdjustmentsTests
    {
        protected override string tableName
        {
            get
            {
                return string.Format(TableNameConstants.Formattable.Adjustments.AGEAbilityAdjustments, RaceConstants.Ages.Adulthood);
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

        [TestCase(AbilityConstants.Charisma, 0)]
        [TestCase(AbilityConstants.Constitution, 0)]
        [TestCase(AbilityConstants.Dexterity, 0)]
        [TestCase(AbilityConstants.Intelligence, 0)]
        [TestCase(AbilityConstants.Strength, 0)]
        [TestCase(AbilityConstants.Wisdom, 0)]
        public void AdulthoodAbilityAdjustment(string ability, int adjustment)
        {
            base.Adjustment(ability, adjustment);
        }
    }
}
