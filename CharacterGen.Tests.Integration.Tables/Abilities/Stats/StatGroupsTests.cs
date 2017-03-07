using CharacterGen.Abilities.Stats;
using CharacterGen.CharacterClasses;
using CharacterGen.Domain.Tables;
using NUnit.Framework;

namespace CharacterGen.Tests.Integration.Tables.Abilities.Stats
{
    [TestFixture]
    public class StatGroupsTests : CollectionTests
    {
        protected override string tableName
        {
            get
            {
                return TableNameConstants.Set.Collection.StatGroups;
            }
        }

        public override void CollectionNames()
        {
            var names = new[]
            {
                CharacterClassConstants.Bard + GroupConstants.Spellcasters,
                CharacterClassConstants.Cleric + GroupConstants.Spellcasters,
                CharacterClassConstants.Druid + GroupConstants.Spellcasters,
                CharacterClassConstants.Paladin + GroupConstants.Spellcasters,
                CharacterClassConstants.Ranger + GroupConstants.Spellcasters,
                CharacterClassConstants.Sorcerer + GroupConstants.Spellcasters,
                CharacterClassConstants.Wizard + GroupConstants.Spellcasters,
                CharacterClassConstants.Adept + GroupConstants.Spellcasters,
                GroupConstants.All
            };

            AssertCollectionNames(names);
        }

        [TestCase(CharacterClassConstants.Bard + GroupConstants.Spellcasters, StatConstants.Charisma)]
        [TestCase(CharacterClassConstants.Cleric + GroupConstants.Spellcasters, StatConstants.Wisdom)]
        [TestCase(CharacterClassConstants.Druid + GroupConstants.Spellcasters, StatConstants.Wisdom)]
        [TestCase(CharacterClassConstants.Paladin + GroupConstants.Spellcasters, StatConstants.Wisdom)]
        [TestCase(CharacterClassConstants.Ranger + GroupConstants.Spellcasters, StatConstants.Wisdom)]
        [TestCase(CharacterClassConstants.Sorcerer + GroupConstants.Spellcasters, StatConstants.Charisma)]
        [TestCase(CharacterClassConstants.Wizard + GroupConstants.Spellcasters, StatConstants.Intelligence)]
        [TestCase(CharacterClassConstants.Adept + GroupConstants.Spellcasters, StatConstants.Wisdom)]
        [TestCase(GroupConstants.All,
            StatConstants.Charisma,
            StatConstants.Constitution,
            StatConstants.Dexterity,
            StatConstants.Intelligence,
            StatConstants.Strength,
            StatConstants.Wisdom)]
        public void StatGroup(string name, params string[] stats)
        {
            base.Collection(name, stats);
        }
    }
}
