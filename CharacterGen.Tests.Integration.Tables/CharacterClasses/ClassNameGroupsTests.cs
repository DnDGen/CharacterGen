﻿using CharacterGen.CharacterClasses;
using CharacterGen.Combats;
using CharacterGen.Domain.Tables;
using CharacterGen.Magics;
using NUnit.Framework;

namespace CharacterGen.Tests.Integration.Tables.CharacterClasses
{
    [TestFixture]
    public class ClassNameGroupsTests : CollectionTests
    {
        protected override string tableName
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
                GroupConstants.PoorBaseAttack,
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
                SavingThrowConstants.Will,
                CharacterClassConstants.TrainingTypes.Intuitive,
                CharacterClassConstants.TrainingTypes.SelfTaught,
                CharacterClassConstants.TrainingTypes.Trained,
                SpellConstants.Sources.Arcane,
                GroupConstants.Players,
                GroupConstants.NPCs,
                GroupConstants.PreparesSpells,
                SpellConstants.Sources.Divine
            };

            AssertCollectionNames(names);
        }

        [TestCase(GroupConstants.Healers,
            CharacterClassConstants.Bard,
            CharacterClassConstants.Cleric,
            CharacterClassConstants.Druid,
            CharacterClassConstants.Paladin,
            CharacterClassConstants.Ranger,
            CharacterClassConstants.Adept)]
        [TestCase(GroupConstants.Mages,
            CharacterClassConstants.Sorcerer,
            CharacterClassConstants.Wizard)]
        [TestCase(GroupConstants.Spellcasters,
            CharacterClassConstants.Bard,
            CharacterClassConstants.Cleric,
            CharacterClassConstants.Druid,
            CharacterClassConstants.Paladin,
            CharacterClassConstants.Ranger,
            CharacterClassConstants.Wizard,
            CharacterClassConstants.Sorcerer,
            CharacterClassConstants.Adept)]
        [TestCase(GroupConstants.Stealth,
            CharacterClassConstants.Bard,
            CharacterClassConstants.Rogue,
            CharacterClassConstants.Ranger)]
        [TestCase(GroupConstants.Warriors,
            CharacterClassConstants.Barbarian,
            CharacterClassConstants.Fighter,
            CharacterClassConstants.Monk,
            CharacterClassConstants.Paladin,
            CharacterClassConstants.Ranger,
            CharacterClassConstants.Warrior)]
        [TestCase(GroupConstants.GoodBaseAttack,
            CharacterClassConstants.Barbarian,
            CharacterClassConstants.Fighter,
            CharacterClassConstants.Paladin,
            CharacterClassConstants.Ranger,
            CharacterClassConstants.Warrior)]
        [TestCase(GroupConstants.AverageBaseAttack,
            CharacterClassConstants.Bard,
            CharacterClassConstants.Cleric,
            CharacterClassConstants.Druid,
            CharacterClassConstants.Monk,
            CharacterClassConstants.Rogue,
            CharacterClassConstants.Aristocrat,
            CharacterClassConstants.Expert)]
        [TestCase(GroupConstants.PoorBaseAttack,
            CharacterClassConstants.Sorcerer,
            CharacterClassConstants.Wizard,
            CharacterClassConstants.Adept,
            CharacterClassConstants.Commoner)]
        [TestCase("Lawful Good",
            CharacterClassConstants.Cleric,
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
            CharacterClassConstants.Warrior)]
        [TestCase("Neutral Good",
            CharacterClassConstants.Cleric,
            CharacterClassConstants.Fighter,
            CharacterClassConstants.Barbarian,
            CharacterClassConstants.Bard,
            CharacterClassConstants.Ranger,
            CharacterClassConstants.Rogue,
            CharacterClassConstants.Sorcerer,
            CharacterClassConstants.Wizard,
            CharacterClassConstants.Druid,
            CharacterClassConstants.Adept,
            CharacterClassConstants.Aristocrat,
            CharacterClassConstants.Commoner,
            CharacterClassConstants.Expert,
            CharacterClassConstants.Warrior)]
        [TestCase("Chaotic Good",
            CharacterClassConstants.Cleric,
            CharacterClassConstants.Fighter,
            CharacterClassConstants.Barbarian,
            CharacterClassConstants.Bard,
            CharacterClassConstants.Ranger,
            CharacterClassConstants.Rogue,
            CharacterClassConstants.Sorcerer,
            CharacterClassConstants.Wizard,
            CharacterClassConstants.Adept,
            CharacterClassConstants.Aristocrat,
            CharacterClassConstants.Commoner,
            CharacterClassConstants.Expert,
            CharacterClassConstants.Warrior)]
        [TestCase("Lawful Neutral",
            CharacterClassConstants.Cleric,
            CharacterClassConstants.Fighter,
            CharacterClassConstants.Monk,
            CharacterClassConstants.Ranger,
            CharacterClassConstants.Rogue,
            CharacterClassConstants.Sorcerer,
            CharacterClassConstants.Wizard,
            CharacterClassConstants.Druid,
            CharacterClassConstants.Adept,
            CharacterClassConstants.Aristocrat,
            CharacterClassConstants.Commoner,
            CharacterClassConstants.Expert,
            CharacterClassConstants.Warrior)]
        [TestCase("True Neutral",
            CharacterClassConstants.Cleric,
            CharacterClassConstants.Fighter,
            CharacterClassConstants.Barbarian,
            CharacterClassConstants.Bard,
            CharacterClassConstants.Ranger,
            CharacterClassConstants.Rogue,
            CharacterClassConstants.Sorcerer,
            CharacterClassConstants.Wizard,
            CharacterClassConstants.Druid,
            CharacterClassConstants.Adept,
            CharacterClassConstants.Aristocrat,
            CharacterClassConstants.Commoner,
            CharacterClassConstants.Expert,
            CharacterClassConstants.Warrior)]
        [TestCase("Chaotic Neutral",
            CharacterClassConstants.Cleric,
            CharacterClassConstants.Fighter,
            CharacterClassConstants.Barbarian,
            CharacterClassConstants.Bard,
            CharacterClassConstants.Ranger,
            CharacterClassConstants.Rogue,
            CharacterClassConstants.Sorcerer,
            CharacterClassConstants.Wizard,
            CharacterClassConstants.Druid,
            CharacterClassConstants.Adept,
            CharacterClassConstants.Aristocrat,
            CharacterClassConstants.Commoner,
            CharacterClassConstants.Expert,
            CharacterClassConstants.Warrior)]
        [TestCase("Lawful Evil",
            CharacterClassConstants.Cleric,
            CharacterClassConstants.Fighter,
            CharacterClassConstants.Monk,
            CharacterClassConstants.Ranger,
            CharacterClassConstants.Rogue,
            CharacterClassConstants.Sorcerer,
            CharacterClassConstants.Wizard,
            CharacterClassConstants.Adept,
            CharacterClassConstants.Aristocrat,
            CharacterClassConstants.Commoner,
            CharacterClassConstants.Expert,
            CharacterClassConstants.Warrior)]
        [TestCase("Neutral Evil",
            CharacterClassConstants.Cleric,
            CharacterClassConstants.Fighter,
            CharacterClassConstants.Barbarian,
            CharacterClassConstants.Bard,
            CharacterClassConstants.Ranger,
            CharacterClassConstants.Rogue,
            CharacterClassConstants.Sorcerer,
            CharacterClassConstants.Wizard,
            CharacterClassConstants.Druid,
            CharacterClassConstants.Adept,
            CharacterClassConstants.Aristocrat,
            CharacterClassConstants.Commoner,
            CharacterClassConstants.Expert,
            CharacterClassConstants.Warrior)]
        [TestCase("Chaotic Evil",
            CharacterClassConstants.Cleric,
            CharacterClassConstants.Fighter,
            CharacterClassConstants.Barbarian,
            CharacterClassConstants.Bard,
            CharacterClassConstants.Ranger,
            CharacterClassConstants.Rogue,
            CharacterClassConstants.Sorcerer,
            CharacterClassConstants.Wizard,
            CharacterClassConstants.Adept,
            CharacterClassConstants.Aristocrat,
            CharacterClassConstants.Commoner,
            CharacterClassConstants.Expert,
            CharacterClassConstants.Warrior)]
        [TestCase(SavingThrowConstants.Fortitude,
            CharacterClassConstants.Barbarian,
            CharacterClassConstants.Cleric,
            CharacterClassConstants.Druid,
            CharacterClassConstants.Fighter,
            CharacterClassConstants.Monk,
            CharacterClassConstants.Paladin,
            CharacterClassConstants.Ranger,
            CharacterClassConstants.Warrior)]
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
            CharacterClassConstants.Wizard,
            CharacterClassConstants.Adept,
            CharacterClassConstants.Aristocrat,
            CharacterClassConstants.Expert)]
        [TestCase(CharacterClassConstants.TrainingTypes.Intuitive,
            CharacterClassConstants.Barbarian,
            CharacterClassConstants.Rogue,
            CharacterClassConstants.Sorcerer,
            CharacterClassConstants.Commoner)]
        [TestCase(CharacterClassConstants.TrainingTypes.SelfTaught,
            CharacterClassConstants.Bard,
            CharacterClassConstants.Fighter,
            CharacterClassConstants.Paladin,
            CharacterClassConstants.Ranger,
            CharacterClassConstants.Warrior)]
        [TestCase(CharacterClassConstants.TrainingTypes.Trained,
            CharacterClassConstants.Cleric,
            CharacterClassConstants.Druid,
            CharacterClassConstants.Monk,
            CharacterClassConstants.Wizard,
            CharacterClassConstants.Expert,
            CharacterClassConstants.Aristocrat,
            CharacterClassConstants.Adept)]
        [TestCase(SpellConstants.Sources.Arcane,
            CharacterClassConstants.Bard,
            CharacterClassConstants.Sorcerer,
            CharacterClassConstants.Wizard)]
        [TestCase(GroupConstants.NPCs,
            CharacterClassConstants.Adept,
            CharacterClassConstants.Aristocrat,
            CharacterClassConstants.Commoner,
            CharacterClassConstants.Expert,
            CharacterClassConstants.Warrior)]
        [TestCase(GroupConstants.Players,
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
            CharacterClassConstants.Wizard)]
        [TestCase(GroupConstants.PreparesSpells,
            CharacterClassConstants.Cleric,
            CharacterClassConstants.Druid,
            CharacterClassConstants.Paladin,
            CharacterClassConstants.Ranger,
            CharacterClassConstants.Wizard,
            CharacterClassConstants.Adept)]
        [TestCase(SpellConstants.Sources.Divine,
            CharacterClassConstants.Cleric,
            CharacterClassConstants.Druid,
            CharacterClassConstants.Paladin,
            CharacterClassConstants.Ranger,
            CharacterClassConstants.Adept)]
        public override void DistinctCollection(string name, params string[] collection)
        {
            base.DistinctCollection(name, collection);
        }
    }
}