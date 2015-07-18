﻿using System;
using System.Linq;
using NPCGen.Common.Abilities;
using NPCGen.Common.CharacterClasses;
using NPCGen.Common.Races;
using NPCGen.Tables.Interfaces;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Tables.Abilities.Languages
{
    [TestFixture]
    public class BonusLanguagesTests : CollectionTests
    {
        protected override String tableName
        {
            get { return TableNameConstants.Set.Collection.BonusLanguages; }
        }

        [Test]
        public override void CollectionNames()
        {
            var names = new[] 
            {
                RaceConstants.BaseRaces.Aasimar,
                RaceConstants.BaseRaces.Bugbear,
                RaceConstants.BaseRaces.Derro,
                RaceConstants.BaseRaces.Doppelganger,
                RaceConstants.BaseRaces.Drow,
                RaceConstants.BaseRaces.DuergarDwarf,
                RaceConstants.BaseRaces.DeepDwarf,
                RaceConstants.BaseRaces.HillDwarf,
                RaceConstants.BaseRaces.MountainDwarf,
                RaceConstants.BaseRaces.GrayElf,
                RaceConstants.BaseRaces.HighElf,
                RaceConstants.BaseRaces.WildElf,
                RaceConstants.BaseRaces.WoodElf,
                RaceConstants.BaseRaces.Gnoll,
                RaceConstants.BaseRaces.ForestGnome,
                RaceConstants.BaseRaces.RockGnome,
                RaceConstants.BaseRaces.Svirfneblin,
                RaceConstants.BaseRaces.Goblin,
                RaceConstants.BaseRaces.HalfOrc,
                RaceConstants.BaseRaces.DeepHalfling,
                RaceConstants.BaseRaces.LightfootHalfling,
                RaceConstants.BaseRaces.TallfellowHalfling,
                RaceConstants.BaseRaces.Hobgoblin,
                RaceConstants.BaseRaces.Kobold,
                RaceConstants.BaseRaces.Lizardfolk,
                RaceConstants.BaseRaces.Minotaur,
                RaceConstants.BaseRaces.Ogre,
                RaceConstants.BaseRaces.OgreMage,
                RaceConstants.BaseRaces.Orc,
                RaceConstants.BaseRaces.Tiefling,
                RaceConstants.BaseRaces.Troglodyte,
                CharacterClassConstants.Cleric,
                CharacterClassConstants.Druid,
                CharacterClassConstants.Wizard,
                CharacterClassConstants.Barbarian,
                CharacterClassConstants.Bard,
                CharacterClassConstants.Fighter,
                CharacterClassConstants.Monk,
                CharacterClassConstants.Paladin,
                CharacterClassConstants.Ranger,
                CharacterClassConstants.Rogue,
                CharacterClassConstants.Sorcerer,
                RaceConstants.BaseRaces.HalfElf,
                RaceConstants.BaseRaces.Human,
                RaceConstants.BaseRaces.MindFlayer,
            };

            AssertCollectionNames(names);
        }

        [TestCase(RaceConstants.BaseRaces.Aasimar,
            LanguageConstants.Dwarven,
            LanguageConstants.Elven,
            LanguageConstants.Gnome,
            LanguageConstants.Draconic,
            LanguageConstants.Halfling,
            LanguageConstants.Sylvan)]
        [TestCase(RaceConstants.BaseRaces.Bugbear,
            LanguageConstants.Giant,
            LanguageConstants.Elven,
            LanguageConstants.Gnoll,
            LanguageConstants.Draconic,
            LanguageConstants.Orc)]
        [TestCase(RaceConstants.BaseRaces.Derro,
            LanguageConstants.Goblin,
            LanguageConstants.Gnome,
            LanguageConstants.Giant,
            LanguageConstants.Orc,
            LanguageConstants.Terran,
            LanguageConstants.Undercommon)]
        [TestCase(RaceConstants.BaseRaces.Doppelganger,
            LanguageConstants.Auran,
            LanguageConstants.Dwarven,
            LanguageConstants.Elven,
            LanguageConstants.Gnome,
            LanguageConstants.Giant,
            LanguageConstants.Halfling,
            LanguageConstants.Terran)]
        [TestCase(RaceConstants.BaseRaces.Drow,
            LanguageConstants.Abyssal,
            LanguageConstants.Aquan,
            LanguageConstants.Draconic,
            LanguageConstants.Gnome,
            LanguageConstants.Goblin)]
        [TestCase(RaceConstants.BaseRaces.DuergarDwarf,
            LanguageConstants.Giant,
            LanguageConstants.Goblin,
            LanguageConstants.Orc,
            LanguageConstants.Draconic,
            LanguageConstants.Terran)]
        [TestCase(RaceConstants.BaseRaces.DeepDwarf,
            LanguageConstants.Goblin,
            LanguageConstants.Gnome,
            LanguageConstants.Giant,
            LanguageConstants.Orc,
            LanguageConstants.Terran,
            LanguageConstants.Undercommon)]
        [TestCase(RaceConstants.BaseRaces.HillDwarf,
            LanguageConstants.Goblin,
            LanguageConstants.Gnome,
            LanguageConstants.Giant,
            LanguageConstants.Orc,
            LanguageConstants.Terran,
            LanguageConstants.Undercommon)]
        [TestCase(RaceConstants.BaseRaces.MountainDwarf,
            LanguageConstants.Goblin,
            LanguageConstants.Gnome,
            LanguageConstants.Giant,
            LanguageConstants.Orc,
            LanguageConstants.Terran,
            LanguageConstants.Undercommon)]
        [TestCase(RaceConstants.BaseRaces.GrayElf,
            LanguageConstants.Draconic,
            LanguageConstants.Gnoll,
            LanguageConstants.Goblin,
            LanguageConstants.Orc,
            LanguageConstants.Gnome,
            LanguageConstants.Sylvan)]
        [TestCase(RaceConstants.BaseRaces.HighElf,
            LanguageConstants.Draconic,
            LanguageConstants.Gnoll,
            LanguageConstants.Goblin,
            LanguageConstants.Orc,
            LanguageConstants.Gnome,
            LanguageConstants.Sylvan)]
        [TestCase(RaceConstants.BaseRaces.WildElf,
            LanguageConstants.Draconic,
            LanguageConstants.Gnoll,
            LanguageConstants.Goblin,
            LanguageConstants.Orc,
            LanguageConstants.Gnome,
            LanguageConstants.Sylvan)]
        [TestCase(RaceConstants.BaseRaces.WoodElf,
            LanguageConstants.Draconic,
            LanguageConstants.Gnoll,
            LanguageConstants.Goblin,
            LanguageConstants.Orc,
            LanguageConstants.Gnome,
            LanguageConstants.Sylvan)]
        [TestCase(RaceConstants.BaseRaces.Gnoll,
            LanguageConstants.Common,
            LanguageConstants.Goblin,
            LanguageConstants.Orc,
            LanguageConstants.Draconic,
            LanguageConstants.Elven)]
        [TestCase(RaceConstants.BaseRaces.ForestGnome,
            LanguageConstants.Dwarven,
            LanguageConstants.Elven,
            LanguageConstants.Draconic,
            LanguageConstants.Goblin,
            LanguageConstants.Giant)]
        [TestCase(RaceConstants.BaseRaces.RockGnome,
            LanguageConstants.Dwarven,
            LanguageConstants.Elven,
            LanguageConstants.Draconic,
            LanguageConstants.Goblin,
            LanguageConstants.Giant,
            LanguageConstants.Orc)]
        [TestCase(RaceConstants.BaseRaces.Svirfneblin,
            LanguageConstants.Dwarven,
            LanguageConstants.Elven,
            LanguageConstants.Terran,
            LanguageConstants.Goblin,
            LanguageConstants.Giant,
            LanguageConstants.Orc)]
        [TestCase(RaceConstants.BaseRaces.Goblin,
            LanguageConstants.Draconic,
            LanguageConstants.Elven,
            LanguageConstants.Giant,
            LanguageConstants.Gnoll,
            LanguageConstants.Orc)]
        [TestCase(RaceConstants.BaseRaces.HalfOrc,
            LanguageConstants.Abyssal,
            LanguageConstants.Goblin,
            LanguageConstants.Giant,
            LanguageConstants.Gnoll,
            LanguageConstants.Draconic)]
        [TestCase(RaceConstants.BaseRaces.DeepHalfling,
            LanguageConstants.Dwarven,
            LanguageConstants.Elven,
            LanguageConstants.Gnome,
            LanguageConstants.Goblin,
            LanguageConstants.Orc,
            LanguageConstants.Undercommon)]
        [TestCase(RaceConstants.BaseRaces.LightfootHalfling,
            LanguageConstants.Dwarven,
            LanguageConstants.Elven,
            LanguageConstants.Gnome,
            LanguageConstants.Goblin,
            LanguageConstants.Orc)]
        [TestCase(RaceConstants.BaseRaces.TallfellowHalfling,
            LanguageConstants.Dwarven,
            LanguageConstants.Elven,
            LanguageConstants.Gnome,
            LanguageConstants.Goblin,
            LanguageConstants.Orc)]
        [TestCase(RaceConstants.BaseRaces.Hobgoblin,
            LanguageConstants.Dwarven,
            LanguageConstants.Infernal,
            LanguageConstants.Orc,
            LanguageConstants.Draconic,
            LanguageConstants.Giant)]
        [TestCase(RaceConstants.BaseRaces.Kobold,
            LanguageConstants.Common,
            LanguageConstants.Undercommon)]
        [TestCase(RaceConstants.BaseRaces.Lizardfolk,
            LanguageConstants.Aquan,
            LanguageConstants.Goblin,
            LanguageConstants.Orc,
            LanguageConstants.Gnoll)]
        [TestCase(RaceConstants.BaseRaces.Minotaur,
            LanguageConstants.Terran,
            LanguageConstants.Goblin,
            LanguageConstants.Orc)]
        [TestCase(RaceConstants.BaseRaces.Ogre,
            LanguageConstants.Terran,
            LanguageConstants.Goblin,
            LanguageConstants.Orc,
            LanguageConstants.Dwarven)]
        [TestCase(RaceConstants.BaseRaces.OgreMage,
            LanguageConstants.Infernal,
            LanguageConstants.Goblin,
            LanguageConstants.Orc,
            LanguageConstants.Dwarven)]
        [TestCase(RaceConstants.BaseRaces.Orc,
            LanguageConstants.Giant,
            LanguageConstants.Goblin,
            LanguageConstants.Gnoll,
            LanguageConstants.Dwarven,
            LanguageConstants.Undercommon)]
        [TestCase(RaceConstants.BaseRaces.Tiefling,
            LanguageConstants.Draconic,
            LanguageConstants.Dwarven,
            LanguageConstants.Elven,
            LanguageConstants.Gnome,
            LanguageConstants.Goblin,
            LanguageConstants.Halfling,
            LanguageConstants.Orc)]
        [TestCase(RaceConstants.BaseRaces.Troglodyte,
            LanguageConstants.Common,
            LanguageConstants.Goblin,
            LanguageConstants.Giant,
            LanguageConstants.Orc)]
        [TestCase(CharacterClassConstants.Cleric,
            LanguageConstants.Abyssal,
            LanguageConstants.Celestial,
            LanguageConstants.Infernal)]
        [TestCase(CharacterClassConstants.Druid,
            LanguageConstants.Sylvan)]
        [TestCase(CharacterClassConstants.Wizard,
            LanguageConstants.Draconic)]
        [TestCase(CharacterClassConstants.Barbarian)]
        [TestCase(CharacterClassConstants.Bard)]
        [TestCase(CharacterClassConstants.Fighter)]
        [TestCase(CharacterClassConstants.Monk)]
        [TestCase(CharacterClassConstants.Paladin)]
        [TestCase(CharacterClassConstants.Ranger)]
        [TestCase(CharacterClassConstants.Rogue)]
        [TestCase(CharacterClassConstants.Sorcerer)]
        public override void DistinctCollection(String name, params String[] collection)
        {
            base.DistinctCollection(name, collection);
        }

        [TestCase(RaceConstants.BaseRaces.HalfElf)]
        [TestCase(RaceConstants.BaseRaces.Human)]
        [TestCase(RaceConstants.BaseRaces.MindFlayer)]
        public void AllLanguagesExceptDruidicAreBonusLanguages(String name)
        {
            var allLanguages = LanguageConstants.GetLanguages();
            var bonusLanguages = allLanguages.Except(new[] { LanguageConstants.Druidic }).ToArray();

            base.DistinctCollection(name, bonusLanguages);
        }
    }
}