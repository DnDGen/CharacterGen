using DnDGen.CharacterGen.Abilities;
using DnDGen.CharacterGen.Tables;
using DnDGen.CharacterGen.Races;
using NUnit.Framework;

namespace DnDGen.CharacterGen.Tests.Integration.Tables.Abilities.Races
{
    [TestFixture]
    public class WereratAbilityAdjustmentsTests : AdjustmentsTests
    {
        protected override string tableName
        {
            get { return string.Format(TableNameConstants.Formattable.Adjustments.RACEAbilityAdjustments, RaceConstants.Metaraces.Wererat); }
        }

        [Test]
        public override void CollectionNames()
        {
            var abilityGroups = collectionsMapper.Map(TableNameConstants.Set.Collection.AbilityGroups);
            AssertCollectionNames(abilityGroups[GroupConstants.All]);
        }

        [TestCase(AbilityConstants.Charisma, -2)]
        [TestCase(AbilityConstants.Constitution, 2)]
        [TestCase(AbilityConstants.Dexterity, 2)]
        [TestCase(AbilityConstants.Intelligence, 2)]
        [TestCase(AbilityConstants.Strength, 0)]
        [TestCase(AbilityConstants.Wisdom, 0)]
        public void RacialAbilityAdjustment(string name, int adjustment)
        {
            base.Adjustment(name, adjustment);
        }
    }
}
