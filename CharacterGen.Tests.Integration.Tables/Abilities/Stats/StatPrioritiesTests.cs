using CharacterGen.Abilities.Stats;
using CharacterGen.CharacterClasses;
using CharacterGen.Domain.Tables;
using NUnit.Framework;
using System;

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
                CharacterClassConstants.Wizard,
                CharacterClassConstants.Adept,
                CharacterClassConstants.Aristocrat,
                CharacterClassConstants.Commoner,
                CharacterClassConstants.Expert,
                CharacterClassConstants.Warrior
            };

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