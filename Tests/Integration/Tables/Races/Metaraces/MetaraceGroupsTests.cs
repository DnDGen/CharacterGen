using CharacterGen.Common.Alignments;
using CharacterGen.Common.CharacterClasses;
using CharacterGen.Common.Races;
using CharacterGen.Tables;
using NUnit.Framework;

namespace CharacterGen.Tests.Integration.Tables.Races.Metaraces
{
    [TestFixture]
    public class MetaraceGroupsTests : CollectionTests
    {
        protected override string tableName
        {
            get { return TableNameConstants.Set.Collection.MetaraceGroups; }
        }

        [Test]
        public override void CollectionNames()
        {
            var names = new[]
            {
                AlignmentConstants.Evil,
                AlignmentConstants.Good,
                AlignmentConstants.Neutral,
                GroupConstants.Genetic,
                GroupConstants.Lycanthrope,
                GroupConstants.Undead,
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
                CharacterClassConstants.Wizard
            };

            AssertCollectionNames(names);
        }

        [TestCase(AlignmentConstants.Evil,
            RaceConstants.Metaraces.HalfDragon,
            RaceConstants.Metaraces.HalfFiend,
            RaceConstants.Metaraces.Wererat,
            RaceConstants.Metaraces.Werewolf,
            RaceConstants.Metaraces.Vampire,
            RaceConstants.Metaraces.Lich,
            RaceConstants.Metaraces.Ghost,
            RaceConstants.Metaraces.None)]
        [TestCase(AlignmentConstants.Neutral,
            RaceConstants.Metaraces.Wereboar,
            RaceConstants.Metaraces.Weretiger,
            RaceConstants.Metaraces.Ghost,
            RaceConstants.Metaraces.None)]
        [TestCase(GroupConstants.Genetic,
            RaceConstants.Metaraces.HalfDragon,
            RaceConstants.Metaraces.HalfFiend,
            RaceConstants.Metaraces.HalfCelestial,
            RaceConstants.Metaraces.None)]
        [TestCase(GroupConstants.Lycanthrope,
            RaceConstants.Metaraces.Werebear,
            RaceConstants.Metaraces.Wereboar,
            RaceConstants.Metaraces.Weretiger,
            RaceConstants.Metaraces.Wererat,
            RaceConstants.Metaraces.Werewolf,
            RaceConstants.Metaraces.None)]
        [TestCase(GroupConstants.Undead,
            RaceConstants.Metaraces.Vampire,
            RaceConstants.Metaraces.Lich,
            RaceConstants.Metaraces.Ghost,
            RaceConstants.Metaraces.None)]
        public override void DistinctCollection(string name, params string[] collection)
        {
            base.DistinctCollection(name, collection);
        }

        [TestCase(AlignmentConstants.Good)]
        [TestCase(CharacterClassConstants.Paladin)]
        public void GoodMetaraces(string name)
        {
            var metaraces = new[]
            {
                RaceConstants.Metaraces.HalfDragon,
                RaceConstants.Metaraces.HalfCelestial,
                RaceConstants.Metaraces.Werebear,
                RaceConstants.Metaraces.Ghost,
                RaceConstants.Metaraces.None
            };

            base.DistinctCollection(name, metaraces);
        }

        [TestCase(CharacterClassConstants.Adept)]
        [TestCase(CharacterClassConstants.Bard)]
        [TestCase(CharacterClassConstants.Cleric)]
        [TestCase(CharacterClassConstants.Druid)]
        [TestCase(CharacterClassConstants.Ranger)]
        [TestCase(CharacterClassConstants.Sorcerer)]
        [TestCase(CharacterClassConstants.Wizard)]
        public void SpellcasterMetarace(string className)
        {
            var metaraces = new[]
            {
                RaceConstants.Metaraces.Werebear,
                RaceConstants.Metaraces.Wereboar,
                RaceConstants.Metaraces.Weretiger,
                RaceConstants.Metaraces.Wererat,
                RaceConstants.Metaraces.Werewolf,
                RaceConstants.Metaraces.Vampire,
                RaceConstants.Metaraces.Lich,
                RaceConstants.Metaraces.Ghost,
                RaceConstants.Metaraces.HalfDragon,
                RaceConstants.Metaraces.HalfFiend,
                RaceConstants.Metaraces.HalfCelestial,
                RaceConstants.Metaraces.None
            };

            base.DistinctCollection(className, metaraces);
        }

        [TestCase(CharacterClassConstants.Aristocrat)]
        [TestCase(CharacterClassConstants.Barbarian)]
        [TestCase(CharacterClassConstants.Commoner)]
        [TestCase(CharacterClassConstants.Expert)]
        [TestCase(CharacterClassConstants.Fighter)]
        [TestCase(CharacterClassConstants.Monk)]
        [TestCase(CharacterClassConstants.Rogue)]
        [TestCase(CharacterClassConstants.Warrior)]
        public void NonSpellcasterMetarace(string className)
        {
            var metaraces = new[]
            {
                RaceConstants.Metaraces.Werebear,
                RaceConstants.Metaraces.Wereboar,
                RaceConstants.Metaraces.Weretiger,
                RaceConstants.Metaraces.Wererat,
                RaceConstants.Metaraces.Werewolf,
                RaceConstants.Metaraces.Vampire,
                RaceConstants.Metaraces.Ghost,
                RaceConstants.Metaraces.HalfDragon,
                RaceConstants.Metaraces.HalfFiend,
                RaceConstants.Metaraces.HalfCelestial,
                RaceConstants.Metaraces.None
            };

            base.DistinctCollection(className, metaraces);
        }
    }
}