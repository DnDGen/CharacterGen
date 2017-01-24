using CharacterGen.Abilities.Stats;
using CharacterGen.CharacterClasses;
using CharacterGen.Domain.Tables;
using NUnit.Framework;

namespace CharacterGen.Tests.Integration.Tables.Abilities.Stats
{
    [TestFixture]
    public class StatPrioritiesTests : CollectionTests
    {
        protected override string tableName
        {
            get { return TableNameConstants.Set.Collection.StatPriorities; }
        }

        [Test]
        public override void CollectionNames()
        {
            var classGroups = CollectionsMapper.Map(TableNameConstants.Set.Collection.ClassNameGroups);
            var names = classGroups[GroupConstants.All];

            AssertCollectionNames(names);
        }

        [TestCase(CharacterClassConstants.Adept,
            StatConstants.Wisdom)]
        [TestCase(CharacterClassConstants.Aristocrat)]
        [TestCase(CharacterClassConstants.Barbarian,
            StatConstants.Strength,
            StatConstants.Dexterity,
            StatConstants.Constitution)]
        [TestCase(CharacterClassConstants.Bard,
            StatConstants.Charisma,
            StatConstants.Intelligence,
            StatConstants.Constitution)]
        [TestCase(CharacterClassConstants.Cleric,
            StatConstants.Wisdom,
            StatConstants.Charisma,
            StatConstants.Constitution)]
        [TestCase(CharacterClassConstants.Commoner)]
        [TestCase(CharacterClassConstants.Druid,
            StatConstants.Wisdom,
            StatConstants.Constitution)]
        [TestCase(CharacterClassConstants.Expert,
            StatConstants.Intelligence)]
        [TestCase(CharacterClassConstants.Fighter,
            StatConstants.Strength,
            StatConstants.Constitution)]
        [TestCase(CharacterClassConstants.Monk,
            StatConstants.Wisdom,
            StatConstants.Strength,
            StatConstants.Dexterity,
            StatConstants.Constitution)]
        [TestCase(CharacterClassConstants.Paladin,
            StatConstants.Charisma,
            StatConstants.Wisdom,
            StatConstants.Strength,
            StatConstants.Constitution)]
        [TestCase(CharacterClassConstants.Ranger,
            StatConstants.Strength,
            StatConstants.Wisdom,
            StatConstants.Dexterity,
            StatConstants.Constitution)]
        [TestCase(CharacterClassConstants.Rogue,
            StatConstants.Dexterity,
            StatConstants.Intelligence,
            StatConstants.Constitution)]
        [TestCase(CharacterClassConstants.Sorcerer,
            StatConstants.Charisma,
            StatConstants.Dexterity,
            StatConstants.Constitution)]
        [TestCase(CharacterClassConstants.Warrior,
            StatConstants.Strength,
            StatConstants.Constitution)]
        [TestCase(CharacterClassConstants.Wizard,
            StatConstants.Intelligence,
            StatConstants.Dexterity,
            StatConstants.Constitution)]
        public override void OrderedCollection(string name, params string[] items)
        {
            base.OrderedCollection(name, items);
        }
    }
}