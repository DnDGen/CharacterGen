﻿using DnDGen.CharacterGen.Alignments;
using DnDGen.CharacterGen.CharacterClasses;
using DnDGen.CharacterGen.Combats;
using DnDGen.CharacterGen.Tables;
using DnDGen.CharacterGen.Magics;
using NUnit.Framework;

namespace DnDGen.CharacterGen.Tests.Integration.Tables.CharacterClasses
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
                GroupConstants.All,
                GroupConstants.AverageBaseAttack,
                GroupConstants.GoodBaseAttack,
                GroupConstants.NPCs,
                GroupConstants.PhysicalCombat,
                GroupConstants.Players,
                GroupConstants.PoorBaseAttack,
                GroupConstants.PreparesSpells,
                GroupConstants.Spellcasters,
                GroupConstants.Stealth,
                AlignmentConstants.LawfulGood,
                AlignmentConstants.NeutralGood,
                AlignmentConstants.ChaoticGood,
                AlignmentConstants.LawfulNeutral,
                AlignmentConstants.TrueNeutral,
                AlignmentConstants.ChaoticNeutral,
                AlignmentConstants.LawfulEvil,
                AlignmentConstants.NeutralEvil,
                AlignmentConstants.ChaoticEvil,
                SavingThrowConstants.Fortitude,
                SavingThrowConstants.Reflex,
                SavingThrowConstants.Will,
                CharacterClassConstants.TrainingTypes.Intuitive,
                CharacterClassConstants.TrainingTypes.SelfTaught,
                CharacterClassConstants.TrainingTypes.Trained,
                SpellConstants.Sources.Arcane,
                SpellConstants.Sources.Divine,
            };

            AssertCollectionNames(names);
        }

        [TestCase(GroupConstants.Spellcasters,
            CharacterClassConstants.Adept,
            CharacterClassConstants.Bard,
            CharacterClassConstants.Cleric,
            CharacterClassConstants.Druid,
            CharacterClassConstants.Paladin,
            CharacterClassConstants.Ranger,
            CharacterClassConstants.Sorcerer,
            CharacterClassConstants.Wizard)]
        [TestCase(GroupConstants.Stealth,
            CharacterClassConstants.Bard,
            CharacterClassConstants.Rogue,
            CharacterClassConstants.Ranger)]
        [TestCase(GroupConstants.PhysicalCombat,
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
            CharacterClassConstants.Adept,
            CharacterClassConstants.Commoner,
            CharacterClassConstants.Sorcerer,
            CharacterClassConstants.Wizard)]
        [TestCase(AlignmentConstants.LawfulGood,
            CharacterClassConstants.Adept,
            CharacterClassConstants.Aristocrat,
            CharacterClassConstants.Cleric,
            CharacterClassConstants.Commoner,
            CharacterClassConstants.Expert,
            CharacterClassConstants.Fighter,
            CharacterClassConstants.Monk,
            CharacterClassConstants.Paladin,
            CharacterClassConstants.Ranger,
            CharacterClassConstants.Rogue,
            CharacterClassConstants.Sorcerer,
            CharacterClassConstants.Warrior,
            CharacterClassConstants.Wizard)]
        [TestCase(AlignmentConstants.NeutralGood,
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
        [TestCase(AlignmentConstants.ChaoticGood,
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
        [TestCase(AlignmentConstants.LawfulNeutral,
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
        [TestCase(AlignmentConstants.TrueNeutral,
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
        [TestCase(AlignmentConstants.ChaoticNeutral,
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
        [TestCase(AlignmentConstants.LawfulEvil,
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
        [TestCase(AlignmentConstants.NeutralEvil,
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
        [TestCase(AlignmentConstants.ChaoticEvil,
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
        public void ClassNameGroup(string name, params string[] collection)
        {
            base.DistinctCollection(name, collection);
        }

        [Test]
        public void AllClasses()
        {
            var classes = new[]
            {
                CharacterClassConstants.Adept,
                CharacterClassConstants.Aristocrat,
                CharacterClassConstants.Barbarian,
                CharacterClassConstants.Bard,
                CharacterClassConstants.Cleric,
                CharacterClassConstants.Commoner,
                CharacterClassConstants.Druid,
                CharacterClassConstants.Expert,
                CharacterClassConstants.Fighter,
                CharacterClassConstants.Monk,
                CharacterClassConstants.Paladin,
                CharacterClassConstants.Ranger,
                CharacterClassConstants.Rogue,
                CharacterClassConstants.Sorcerer,
                CharacterClassConstants.Warrior,
                CharacterClassConstants.Wizard,
            };

            base.DistinctCollection(GroupConstants.All, classes);
        }
    }
}