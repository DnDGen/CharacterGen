using CharacterGen.Common.Abilities.Stats;
using CharacterGen.Common.CharacterClasses;
using CharacterGen.Tables;
using NUnit.Framework;
using System;

namespace CharacterGen.Tests.Integration.Tables.Abilities.Stats
{
    [TestFixture]
    public class StatPrioritiesTests : CollectionTests
    {
        protected override String tableName
        {
            get { return TableNameConstants.Set.Collection.StatPriorities; }
        }

        [Test]
        public override void CollectionNames()
        {
            var names = new[]
            {
                CharacterClassConstants.Barbarian,
                CharacterClassConstants.Bard,
                CharacterClassConstants.Cleric,
                CharacterClassConstants.Druid,
                CharacterClassConstants.Fighter,
                CharacterClassConstants.Monk,
                CharacterClassConstants.Paladin,
                CharacterClassConstants.Ranger,
                CharacterClassConstants.Rogue,
                CharacterClassConstants.Sorcerer,
                CharacterClassConstants.Wizard
            };

            AssertCollectionNames(names);
        }

        [TestCase(CharacterClassConstants.Barbarian, StatConstants.Strength, StatConstants.Dexterity)]
        [TestCase(CharacterClassConstants.Bard, StatConstants.Charisma, StatConstants.Intelligence)]
        [TestCase(CharacterClassConstants.Cleric, StatConstants.Wisdom, StatConstants.Charisma)]
        [TestCase(CharacterClassConstants.Druid, StatConstants.Wisdom, StatConstants.Charisma)]
        [TestCase(CharacterClassConstants.Fighter, StatConstants.Strength, StatConstants.Constitution)]
        [TestCase(CharacterClassConstants.Monk, StatConstants.Wisdom, StatConstants.Strength)]
        [TestCase(CharacterClassConstants.Paladin, StatConstants.Charisma, StatConstants.Wisdom)]
        [TestCase(CharacterClassConstants.Ranger, StatConstants.Strength, StatConstants.Wisdom)]
        [TestCase(CharacterClassConstants.Rogue, StatConstants.Dexterity, StatConstants.Intelligence)]
        [TestCase(CharacterClassConstants.Sorcerer, StatConstants.Charisma, StatConstants.Dexterity)]
        [TestCase(CharacterClassConstants.Wizard, StatConstants.Intelligence, StatConstants.Dexterity)]
        public override void OrderedCollection(String name, params String[] items)
        {
            base.OrderedCollection(name, items);
        }
    }
}