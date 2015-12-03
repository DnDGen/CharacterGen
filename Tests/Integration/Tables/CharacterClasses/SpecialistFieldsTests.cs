using CharacterGen.Common.CharacterClasses;
using CharacterGen.Common.Races;
using CharacterGen.Tables;
using NUnit.Framework;
using System;

namespace CharacterGen.Tests.Integration.Tables.CharacterClasses
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
                CharacterClassConstants.Wizard,
                RaceConstants.Metaraces.Ghost,
                RaceConstants.Metaraces.HalfCelestial,
                RaceConstants.Metaraces.HalfDragon,
                RaceConstants.Metaraces.HalfFiend,
                RaceConstants.Metaraces.Lich,
                RaceConstants.Metaraces.Vampire,
                RaceConstants.Metaraces.Werebear,
                RaceConstants.Metaraces.Wereboar,
                RaceConstants.Metaraces.Wererat,
                RaceConstants.Metaraces.Weretiger,
                RaceConstants.Metaraces.Werewolf,
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
        [TestCase(RaceConstants.Metaraces.Ghost)]
        [TestCase(RaceConstants.Metaraces.HalfCelestial)]
        [TestCase(RaceConstants.Metaraces.HalfDragon)]
        [TestCase(RaceConstants.Metaraces.HalfFiend)]
        [TestCase(RaceConstants.Metaraces.Lich)]
        [TestCase(RaceConstants.Metaraces.Vampire,
            CharacterClassConstants.Domains.Chaos,
            CharacterClassConstants.Domains.Destruction,
            CharacterClassConstants.Domains.Evil,
            CharacterClassConstants.Domains.Trickery)]
        [TestCase(RaceConstants.Metaraces.Werebear)]
        [TestCase(RaceConstants.Metaraces.Wereboar)]
        [TestCase(RaceConstants.Metaraces.Wererat)]
        [TestCase(RaceConstants.Metaraces.Weretiger)]
        [TestCase(RaceConstants.Metaraces.Werewolf)]
        public override void DistinctCollection(String name, params String[] collection)
        {
            base.DistinctCollection(name, collection);
        }
    }
}