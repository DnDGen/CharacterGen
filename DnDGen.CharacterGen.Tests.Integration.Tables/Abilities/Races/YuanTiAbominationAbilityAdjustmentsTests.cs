using DnDGen.CharacterGen.Abilities;
using DnDGen.CharacterGen.Tables;
using DnDGen.CharacterGen.Races;
using NUnit.Framework;

namespace DnDGen.CharacterGen.Tests.Integration.Tables.Abilities.Races
{
    [TestFixture]
    public class YuanTiAbominationAbilityAdjustmentsTests : AdjustmentsTests
    {
        protected override string tableName
        {
            get { return string.Format(TableNameConstants.Formattable.Adjustments.RACEAbilityAdjustments, RaceConstants.BaseRaces.YuanTiAbomination); }
        }

        [Test]
        public override void CollectionNames()
        {
            var abilityGroups = GetTable(TableNameConstants.Set.Collection.AbilityGroups);
            AssertCollectionNames(abilityGroups[GroupConstants.All]);
        }

        [TestCase(AbilityConstants.Charisma, 8)]
        [TestCase(AbilityConstants.Constitution, 6)]
        [TestCase(AbilityConstants.Dexterity, 2)]
        [TestCase(AbilityConstants.Intelligence, 10)]
        [TestCase(AbilityConstants.Strength, 8)]
        [TestCase(AbilityConstants.Wisdom, 10)]
        public void RacialAbilityAdjustment(string name, int adjustment)
        {
            base.Adjustment(name, adjustment);
        }
    }
}
