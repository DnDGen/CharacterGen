using DnDGen.CharacterGen.Abilities;
using DnDGen.CharacterGen.Tables;
using DnDGen.CharacterGen.Races;
using NUnit.Framework;

namespace DnDGen.CharacterGen.Tests.Integration.Tables.Abilities.Races
{
    [TestFixture]
    public class ScragAbilityAdjustmentsTests : AdjustmentsTests
    {
        protected override string tableName
        {
            get { return string.Format(TableNameConstants.Formattable.Adjustments.RACEAbilityAdjustments, RaceConstants.BaseRaces.Scrag); }
        }

        [Test]
        public override void CollectionNames()
        {
            var abilityGroups = collectionsMapper.Map(TableNameConstants.Set.Collection.AbilityGroups);
            AssertCollectionNames(abilityGroups[GroupConstants.All]);
        }

        [TestCase(AbilityConstants.Charisma, -4)]
        [TestCase(AbilityConstants.Constitution, 12)]
        [TestCase(AbilityConstants.Dexterity, 4)]
        [TestCase(AbilityConstants.Intelligence, -4)]
        [TestCase(AbilityConstants.Strength, 12)]
        [TestCase(AbilityConstants.Wisdom, -2)]
        public void RacialAbilityAdjustment(string name, int adjustment)
        {
            base.Adjustment(name, adjustment);
        }
    }
}
