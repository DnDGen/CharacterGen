using System;
using NPCGen.Common.CharacterClasses;
using NPCGen.Tables.Interfaces;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Tables.CharacterClasses
{
    [TestFixture]
    public class ProhibitedFieldsTests : CollectionTests
    {
        protected override String tableName
        {
            get { return TableNameConstants.Set.Collection.ProhibitedFields; }
        }

        [TestCase(CharacterClassConstants.Barbarian)]
        [TestCase(CharacterClassConstants.Bard)]
        [TestCase(CharacterClassConstants.Cleric)]
        [TestCase(CharacterClassConstants.Druid)]
        [TestCase(CharacterClassConstants.Fighter)]
        [TestCase(CharacterClassConstants.Monk)]
        [TestCase(CharacterClassConstants.Paladin)]
        [TestCase(CharacterClassConstants.Ranger)]
        [TestCase(CharacterClassConstants.Rogue)]
        [TestCase(CharacterClassConstants.Sorcerer)]
        [TestCase(CharacterClassConstants.Wizard,
            CharacterClassConstants.Schools.Abjuration,
            CharacterClassConstants.Schools.Conjuration,
            CharacterClassConstants.Schools.Enchantment,
            CharacterClassConstants.Schools.Evocation,
            CharacterClassConstants.Schools.Illusion,
            CharacterClassConstants.Schools.Necromancy,
            CharacterClassConstants.Schools.Transmutation)]
        [TestCase("Lawful Good",
            CharacterClassConstants.Domains.Chaos,
            CharacterClassConstants.Domains.Evil)]
        [TestCase("Neutral Good",
            CharacterClassConstants.Domains.Chaos,
            CharacterClassConstants.Domains.Law,
            CharacterClassConstants.Domains.Evil)]
        [TestCase("Chaotic Good",
            CharacterClassConstants.Domains.Law,
            CharacterClassConstants.Domains.Evil)]
        [TestCase("Lawful Neutral",
            CharacterClassConstants.Domains.Chaos,
            CharacterClassConstants.Domains.Good,
            CharacterClassConstants.Domains.Evil)]
        [TestCase("True Neutral",
            CharacterClassConstants.Domains.Chaos,
            CharacterClassConstants.Domains.Law,
            CharacterClassConstants.Domains.Good,
            CharacterClassConstants.Domains.Evil)]
        [TestCase("Chaotic Neutral",
            CharacterClassConstants.Domains.Law,
            CharacterClassConstants.Domains.Good,
            CharacterClassConstants.Domains.Evil)]
        [TestCase("Lawful Evil",
            CharacterClassConstants.Domains.Chaos,
            CharacterClassConstants.Domains.Good)]
        [TestCase("Neutral Evil",
            CharacterClassConstants.Domains.Chaos,
            CharacterClassConstants.Domains.Law,
            CharacterClassConstants.Domains.Good)]
        [TestCase("Chaotic Evil",
            CharacterClassConstants.Domains.Law,
            CharacterClassConstants.Domains.Good)]
        public override void DistinctCollection(String name, params String[] collection)
        {
            base.DistinctCollection(name, collection);
        }
    }
}