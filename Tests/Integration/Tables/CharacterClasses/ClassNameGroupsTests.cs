using System;
using NPCGen.Common.CharacterClasses;
using NPCGen.Tables.Interfaces;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Tables.CharacterClasses
{
    [TestFixture]
    public class ClassNameGroupsTests : CollectionTests
    {
        protected override String tableName
        {
            get { return TableNameConstants.Set.Collection.ClassNameGroups; }
        }

        [TestCase(TableNameConstants.Set.Collection.Groups.Healers,
            CharacterClassConstants.Bard,
            CharacterClassConstants.Cleric,
            CharacterClassConstants.Druid,
            CharacterClassConstants.Paladin,
            CharacterClassConstants.Ranger)]
        [TestCase(TableNameConstants.Set.Collection.Groups.Mages,
            CharacterClassConstants.Bard,
            CharacterClassConstants.Ranger,
            CharacterClassConstants.Sorcerer,
            CharacterClassConstants.Wizard)]
        [TestCase(TableNameConstants.Set.Collection.Groups.Spellcasters,
            CharacterClassConstants.Bard,
            CharacterClassConstants.Cleric,
            CharacterClassConstants.Druid,
            CharacterClassConstants.Paladin,
            CharacterClassConstants.Ranger,
            CharacterClassConstants.Wizard,
            CharacterClassConstants.Sorcerer)]
        [TestCase(TableNameConstants.Set.Collection.Groups.Stealth,
            CharacterClassConstants.Bard,
            CharacterClassConstants.Rogue,
            CharacterClassConstants.Ranger)]
        [TestCase(TableNameConstants.Set.Collection.Groups.Warriors,
            CharacterClassConstants.Barbarian,
            CharacterClassConstants.Fighter,
            CharacterClassConstants.Monk,
            CharacterClassConstants.Paladin,
            CharacterClassConstants.Ranger)]
        [TestCase(TableNameConstants.Set.Collection.Groups.GoodBaseAttack,
            CharacterClassConstants.Barbarian,
            CharacterClassConstants.Fighter,
            CharacterClassConstants.Paladin,
            CharacterClassConstants.Ranger)]
        [TestCase(TableNameConstants.Set.Collection.Groups.AverageBaseAttack,
            CharacterClassConstants.Bard,
            CharacterClassConstants.Cleric,
            CharacterClassConstants.Druid,
            CharacterClassConstants.Monk,
            CharacterClassConstants.Rogue)]
        [TestCase("Lawful Good",
            CharacterClassConstants.Cleric,
            CharacterClassConstants.Fighter,
            CharacterClassConstants.Monk,
            CharacterClassConstants.Paladin,
            CharacterClassConstants.Ranger,
            CharacterClassConstants.Rogue,
            CharacterClassConstants.Sorcerer,
            CharacterClassConstants.Wizard)]
        [TestCase("Neutral Good",
            CharacterClassConstants.Cleric,
            CharacterClassConstants.Fighter,
            CharacterClassConstants.Barbarian,
            CharacterClassConstants.Bard,
            CharacterClassConstants.Ranger,
            CharacterClassConstants.Rogue,
            CharacterClassConstants.Sorcerer,
            CharacterClassConstants.Wizard,
            CharacterClassConstants.Druid)]
        [TestCase("Chaotic Good",
            CharacterClassConstants.Cleric,
            CharacterClassConstants.Fighter,
            CharacterClassConstants.Barbarian,
            CharacterClassConstants.Bard,
            CharacterClassConstants.Ranger,
            CharacterClassConstants.Rogue,
            CharacterClassConstants.Sorcerer,
            CharacterClassConstants.Wizard)]
        [TestCase("Lawful Neutral",
            CharacterClassConstants.Cleric,
            CharacterClassConstants.Fighter,
            CharacterClassConstants.Monk,
            CharacterClassConstants.Ranger,
            CharacterClassConstants.Rogue,
            CharacterClassConstants.Sorcerer,
            CharacterClassConstants.Wizard,
            CharacterClassConstants.Druid)]
        [TestCase("True Neutral",
            CharacterClassConstants.Cleric,
            CharacterClassConstants.Fighter,
            CharacterClassConstants.Barbarian,
            CharacterClassConstants.Bard,
            CharacterClassConstants.Ranger,
            CharacterClassConstants.Rogue,
            CharacterClassConstants.Sorcerer,
            CharacterClassConstants.Wizard,
            CharacterClassConstants.Druid)]
        [TestCase("Chaotic Neutral",
            CharacterClassConstants.Cleric,
            CharacterClassConstants.Fighter,
            CharacterClassConstants.Barbarian,
            CharacterClassConstants.Bard,
            CharacterClassConstants.Ranger,
            CharacterClassConstants.Rogue,
            CharacterClassConstants.Sorcerer,
            CharacterClassConstants.Wizard,
            CharacterClassConstants.Druid)]
        [TestCase("Lawful Evil",
            CharacterClassConstants.Cleric,
            CharacterClassConstants.Fighter,
            CharacterClassConstants.Monk,
            CharacterClassConstants.Ranger,
            CharacterClassConstants.Rogue,
            CharacterClassConstants.Sorcerer,
            CharacterClassConstants.Wizard)]
        [TestCase("Neutral Evil",
            CharacterClassConstants.Cleric,
            CharacterClassConstants.Fighter,
            CharacterClassConstants.Barbarian,
            CharacterClassConstants.Bard,
            CharacterClassConstants.Ranger,
            CharacterClassConstants.Rogue,
            CharacterClassConstants.Sorcerer,
            CharacterClassConstants.Wizard,
            CharacterClassConstants.Druid)]
        [TestCase("Chaotic Evil",
            CharacterClassConstants.Cleric,
            CharacterClassConstants.Fighter,
            CharacterClassConstants.Barbarian,
            CharacterClassConstants.Bard,
            CharacterClassConstants.Ranger,
            CharacterClassConstants.Rogue,
            CharacterClassConstants.Sorcerer,
            CharacterClassConstants.Wizard)]
        public override void DistinctCollection(String name, params String[] collection)
        {
            base.DistinctCollection(name, collection);
        }
    }
}