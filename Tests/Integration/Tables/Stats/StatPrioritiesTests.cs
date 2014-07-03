﻿using System;
using System.Collections.Generic;
using NPCGen.Common.Abilities.Stats;
using NPCGen.Common.CharacterClasses;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Tables.Stats
{
    [TestFixture]
    public class StatPrioritiesTests : CollectionTests
    {
        protected override String tableName
        {
            get { return "StatPriorities"; }
        }

        protected override IEnumerable<String> nameCollection
        {
            get { return CharacterClassConstants.GetClassNames(); }
        }

        [TestCase(CharacterClassConstants.Barbarian, StatConstants.Strength, StatConstants.Dexterity)]
        [TestCase(CharacterClassConstants.Bard, StatConstants.Charisma, StatConstants.Intelligence)]
        [TestCase(CharacterClassConstants.Cleric, StatConstants.Wisdom, StatConstants.Constitution)]
        [TestCase(CharacterClassConstants.Druid, StatConstants.Wisdom, StatConstants.Dexterity)]
        [TestCase(CharacterClassConstants.Fighter, StatConstants.Strength, StatConstants.Constitution)]
        [TestCase(CharacterClassConstants.Monk, StatConstants.Wisdom, StatConstants.Strength)]
        [TestCase(CharacterClassConstants.Paladin, StatConstants.Charisma, StatConstants.Strength)]
        [TestCase(CharacterClassConstants.Ranger, StatConstants.Dexterity, StatConstants.Strength)]
        [TestCase(CharacterClassConstants.Rogue, StatConstants.Dexterity, StatConstants.Intelligence)]
        [TestCase(CharacterClassConstants.Sorcerer, StatConstants.Charisma, StatConstants.Dexterity)]
        [TestCase(CharacterClassConstants.Wizard, StatConstants.Intelligence, StatConstants.Dexterity)]
        public void Collection(String name, params String[] items)
        {
            AssertCollectionAndOrder(name, items);
        }
    }
}