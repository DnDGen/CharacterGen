using System;
using NPCGen.Common.CharacterClasses;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Tables.CharacterClasses
{
    [TestFixture]
    public class ClassNameGroupsTests : CollectionTests
    {
        protected override String tableName
        {
            get { return "ClassNameGroups"; }
        }

        [TestCase("Healers",
            CharacterClassConstants.Bard,
            CharacterClassConstants.Cleric,
            CharacterClassConstants.Druid,
            CharacterClassConstants.Paladin,
            CharacterClassConstants.Ranger)]
        [TestCase("Mages",
            CharacterClassConstants.Bard,
            CharacterClassConstants.Ranger,
            CharacterClassConstants.Sorcerer,
            CharacterClassConstants.Wizard)]
        [TestCase("Spellcasters",
            CharacterClassConstants.Bard,
            CharacterClassConstants.Cleric,
            CharacterClassConstants.Druid,
            CharacterClassConstants.Paladin,
            CharacterClassConstants.Ranger,
            CharacterClassConstants.Wizard,
            CharacterClassConstants.Sorcerer)]
        [TestCase("Stealth",
            CharacterClassConstants.Bard,
            CharacterClassConstants.Rogue,
            CharacterClassConstants.Ranger)]
        [TestCase("Warriors",
            CharacterClassConstants.Barbarian,
            CharacterClassConstants.Fighter,
            CharacterClassConstants.Monk,
            CharacterClassConstants.Paladin,
            CharacterClassConstants.Ranger)]
        [TestCase("Good Base Attack",
            CharacterClassConstants.Barbarian,
            CharacterClassConstants.Fighter,
            CharacterClassConstants.Paladin,
            CharacterClassConstants.Ranger)]
        [TestCase("Average Base Attack",
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