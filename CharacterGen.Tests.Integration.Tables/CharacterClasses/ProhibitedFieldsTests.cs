using System;
using CharacterGen.CharacterClasses;
using CharacterGen.Domain.Tables;
using NUnit.Framework;

namespace CharacterGen.Tests.Integration.Tables.CharacterClasses
{
    [TestFixture]
    public class ProhibitedFieldsTests : CollectionTests
    {
        protected override string tableName
        {
            get { return TableNameConstants.Set.Collection.ProhibitedFields; }
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
                "Lawful Good",
                "Neutral Good",
                "Chaotic Good",
                "Lawful Neutral",
                "True Neutral",
                "Chaotic Neutral",
                "Lawful Evil",
                "Neutral Evil",
                "Chaotic Evil"
            };

            AssertCollectionNames(names);
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