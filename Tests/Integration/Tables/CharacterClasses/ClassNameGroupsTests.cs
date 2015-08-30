using CharacterGen.Common.CharacterClasses;
using CharacterGen.Common.Combats;
using CharacterGen.Tables;
using NUnit.Framework;
using System;

namespace CharacterGen.Tests.Integration.Tables.CharacterClasses
{
    [TestFixture]
    public class ClassNameGroupsTests : CollectionTests
    {
        protected override String tableName
        {
            get { return TableNameConstants.Set.Collection.ClassNameGroups; }
        }

        [Test]
        public override void CollectionNames()
        {
            var names = new[]
            {
                GroupConstants.Healers,
                GroupConstants.Mages,
                GroupConstants.Spellcasters,
                GroupConstants.Stealth,
                GroupConstants.Warriors,
                GroupConstants.GoodBaseAttack,
                GroupConstants.AverageBaseAttack,
                "Lawful Good",
                "Neutral Good",
                "Chaotic Good",
                "Lawful Neutral",
                "True Neutral",
                "Chaotic Neutral",
                "Lawful Evil",
                "Neutral Evil",
                "Chaotic Evil",
                SavingThrowConstants.Fortitude,
                SavingThrowConstants.Reflex,
                SavingThrowConstants.Will
            };

            AssertCollectionNames(names);
        }

        [TestCase(GroupConstants.Healers,
            CharacterClassConstants.Bard,
            CharacterClassConstants.Cleric,
            CharacterClassConstants.Druid,
            CharacterClassConstants.Paladin,
            CharacterClassConstants.Ranger)]
        [TestCase(GroupConstants.Mages,
            CharacterClassConstants.Bard,
            CharacterClassConstants.Ranger,
            CharacterClassConstants.Sorcerer,
            CharacterClassConstants.Wizard)]
        [TestCase(GroupConstants.Spellcasters,
            CharacterClassConstants.Bard,
            CharacterClassConstants.Cleric,
            CharacterClassConstants.Druid,
            CharacterClassConstants.Paladin,
            CharacterClassConstants.Ranger,
            CharacterClassConstants.Wizard,
            CharacterClassConstants.Sorcerer)]
        [TestCase(GroupConstants.Stealth,
            CharacterClassConstants.Bard,
            CharacterClassConstants.Rogue,
            CharacterClassConstants.Ranger)]
        [TestCase(GroupConstants.Warriors,
            CharacterClassConstants.Barbarian,
            CharacterClassConstants.Fighter,
            CharacterClassConstants.Monk,
            CharacterClassConstants.Paladin,
            CharacterClassConstants.Ranger)]
        [TestCase(GroupConstants.GoodBaseAttack,
            CharacterClassConstants.Barbarian,
            CharacterClassConstants.Fighter,
            CharacterClassConstants.Paladin,
            CharacterClassConstants.Ranger)]
        [TestCase(GroupConstants.AverageBaseAttack,
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
        [TestCase(SavingThrowConstants.Fortitude,
            CharacterClassConstants.Barbarian,
            CharacterClassConstants.Cleric,
            CharacterClassConstants.Druid,
            CharacterClassConstants.Fighter,
            CharacterClassConstants.Monk,
            CharacterClassConstants.Paladin,
            CharacterClassConstants.Ranger)]
        [TestCase(SavingThrowConstants.Reflex,
            CharacterClassConstants.Bard,
            CharacterClassConstants.Monk,
            CharacterClassConstants.Ranger,
            CharacterClassConstants.Rogue)]
        [TestCase(SavingThrowConstants.Will,
            CharacterClassConstants.Bard,
            CharacterClassConstants.Cleric,
            CharacterClassConstants.Druid,
            CharacterClassConstants.Monk,
            CharacterClassConstants.Sorcerer,
            CharacterClassConstants.Wizard)]
        public override void DistinctCollection(String name, params String[] collection)
        {
            base.DistinctCollection(name, collection);
        }
    }
}