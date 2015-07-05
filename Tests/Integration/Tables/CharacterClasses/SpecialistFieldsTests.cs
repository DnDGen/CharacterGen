using System;
using NPCGen.Common.CharacterClasses;
using NPCGen.Tables.Interfaces;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Tables.CharacterClasses
{
    [TestFixture]
    public class SpecialistFieldsTests : CollectionTests
    {
        protected override String tableName
        {
            get { return TableNameConstants.Set.Collection.SpecialistFields; }
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
                CharacterClassConstants.Wizard
            };

            AssertCollectionNames(names);
        }

        [TestCase(CharacterClassConstants.Barbarian)]
        [TestCase(CharacterClassConstants.Bard)]
        [TestCase(CharacterClassConstants.Cleric,
            CharacterClassConstants.Domains.Air,
            CharacterClassConstants.Domains.Animal,
            CharacterClassConstants.Domains.Chaos,
            CharacterClassConstants.Domains.Death,
            CharacterClassConstants.Domains.Destruction,
            CharacterClassConstants.Domains.Earth,
            CharacterClassConstants.Domains.Evil,
            CharacterClassConstants.Domains.Fire,
            CharacterClassConstants.Domains.Good,
            CharacterClassConstants.Domains.Healing,
            CharacterClassConstants.Domains.Knowledge,
            CharacterClassConstants.Domains.Law,
            CharacterClassConstants.Domains.Luck,
            CharacterClassConstants.Domains.Magic,
            CharacterClassConstants.Domains.Plant,
            CharacterClassConstants.Domains.Protection,
            CharacterClassConstants.Domains.Strength,
            CharacterClassConstants.Domains.Sun,
            CharacterClassConstants.Domains.Travel,
            CharacterClassConstants.Domains.Trickery,
            CharacterClassConstants.Domains.War,
            CharacterClassConstants.Domains.Water)]
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
            CharacterClassConstants.Schools.Divination,
            CharacterClassConstants.Schools.Enchantment,
            CharacterClassConstants.Schools.Evocation,
            CharacterClassConstants.Schools.Illusion,
            CharacterClassConstants.Schools.Necromancy,
            CharacterClassConstants.Schools.Transmutation)]
        public override void DistinctCollection(String name, params String[] collection)
        {
            base.DistinctCollection(name, collection);
        }
    }
}