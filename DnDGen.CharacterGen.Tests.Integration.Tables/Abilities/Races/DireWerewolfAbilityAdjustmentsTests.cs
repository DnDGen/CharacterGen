using DnDGen.CharacterGen.Abilities;
using DnDGen.CharacterGen.Races;
using DnDGen.CharacterGen.Tables;
using NUnit.Framework;

namespace DnDGen.CharacterGen.Tests.Integration.Tables.Abilities.Races
{
    [TestFixture]
    public class DireWerewolfAbilityAdjustmentsTests : AdjustmentsTests
    {
        protected override string tableName
        {
            get { return string.Format(TableNameConstants.Formattable.Adjustments.RACEAbilityAdjustments, RaceConstants.Metaraces.Werewolf_Dire); }
        }

        [Test]
        public override void CollectionNames()
        {
            var abilityGroups = GetTable(TableNameConstants.Set.Collection.AbilityGroups);
            AssertCollectionNames(abilityGroups[GroupConstants.All]);
        }

        [TestCase(AbilityConstants.Charisma, 2)]
        [TestCase(AbilityConstants.Constitution, 4)]
        [TestCase(AbilityConstants.Dexterity, 4)]
        [TestCase(AbilityConstants.Intelligence, 0)]
        [TestCase(AbilityConstants.Strength, 6)]
        [TestCase(AbilityConstants.Wisdom, 0)]
        public void RacialAbilityAdjustment(string name, int adjustment)
        {
            base.Adjustment(name, adjustment);
        }
    }
}
