using CharacterGen.Alignments;
using CharacterGen.CharacterClasses;
using CharacterGen.Domain.Tables;
using CharacterGen.Races;
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
                AlignmentConstants.LawfulEvil,
                AlignmentConstants.LawfulGood,
                AlignmentConstants.LawfulNeutral,
                AlignmentConstants.ChaoticEvil,
                AlignmentConstants.ChaoticGood,
                AlignmentConstants.ChaoticNeutral,
                AlignmentConstants.NeutralEvil,
                AlignmentConstants.NeutralGood,
                AlignmentConstants.TrueNeutral,
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
                CharacterClassConstants.Wizard,
                GroupConstants.All,
                GroupConstants.HasWings,
            };

            AssertCollectionNames(names);
        }

        [TestCase(AlignmentConstants.LawfulGood,
            RaceConstants.Metaraces.Ghost,
            RaceConstants.Metaraces.HalfDragon,
            RaceConstants.Metaraces.HalfCelestial,
            RaceConstants.Metaraces.Werebear)]
        [TestCase(AlignmentConstants.NeutralGood,
            RaceConstants.Metaraces.Ghost,
            RaceConstants.Metaraces.HalfDragon,
            RaceConstants.Metaraces.HalfCelestial)]
        [TestCase(AlignmentConstants.ChaoticGood,
            RaceConstants.Metaraces.Ghost,
            RaceConstants.Metaraces.HalfDragon,
            RaceConstants.Metaraces.HalfCelestial)]
        [TestCase(AlignmentConstants.LawfulEvil,
            RaceConstants.Metaraces.Ghost,
            RaceConstants.Metaraces.HalfDragon,
            RaceConstants.Metaraces.HalfFiend,
            RaceConstants.Metaraces.Lich,
            RaceConstants.Metaraces.Mummy,
            RaceConstants.Metaraces.Vampire,
            RaceConstants.Metaraces.Wererat)]
        [TestCase(AlignmentConstants.NeutralEvil,
            RaceConstants.Metaraces.Ghost,
            RaceConstants.Metaraces.HalfDragon,
            RaceConstants.Metaraces.HalfFiend,
            RaceConstants.Metaraces.Lich,
            RaceConstants.Metaraces.Mummy,
            RaceConstants.Metaraces.Vampire)]
        [TestCase(AlignmentConstants.ChaoticEvil,
            RaceConstants.Metaraces.Ghost,
            RaceConstants.Metaraces.HalfDragon,
            RaceConstants.Metaraces.HalfFiend,
            RaceConstants.Metaraces.Lich,
            RaceConstants.Metaraces.Vampire,
            RaceConstants.Metaraces.Werewolf)]
        [TestCase(AlignmentConstants.LawfulNeutral,
            RaceConstants.Metaraces.Ghost,
            RaceConstants.Metaraces.Mummy)]
        [TestCase(AlignmentConstants.TrueNeutral,
            RaceConstants.Metaraces.Ghost,
            RaceConstants.Metaraces.Wereboar,
            RaceConstants.Metaraces.Weretiger)]
        [TestCase(AlignmentConstants.ChaoticNeutral,
            RaceConstants.Metaraces.Ghost)]
        [TestCase(GroupConstants.Genetic,
            RaceConstants.Metaraces.HalfDragon,
            RaceConstants.Metaraces.HalfFiend,
            RaceConstants.Metaraces.HalfCelestial)]
        [TestCase(GroupConstants.Lycanthrope,
            RaceConstants.Metaraces.Werebear,
            RaceConstants.Metaraces.Wereboar,
            RaceConstants.Metaraces.Weretiger,
            RaceConstants.Metaraces.Wererat,
            RaceConstants.Metaraces.Werewolf)]
        [TestCase(GroupConstants.Undead,
            RaceConstants.Metaraces.Ghost,
            RaceConstants.Metaraces.Lich,
            RaceConstants.Metaraces.Mummy,
            RaceConstants.Metaraces.Vampire)]
        [TestCase(GroupConstants.HasWings,
            RaceConstants.Metaraces.HalfFiend,
            RaceConstants.Metaraces.HalfCelestial)]
        public void MetaraceGroup(string name, params string[] collection)
        {
            base.DistinctCollection(name, collection);
        }

        [Test]
        public void PaladinMetaraces()
        {
            var metaraces = new[]
            {
                RaceConstants.Metaraces.HalfCelestial,
                RaceConstants.Metaraces.HalfDragon,
                RaceConstants.Metaraces.Ghost,
                RaceConstants.Metaraces.None,
                RaceConstants.Metaraces.Werebear,
            };

            base.DistinctCollection(CharacterClassConstants.Paladin, metaraces);
        }

        [Test]
        public void AllMetaraces()
        {
            var metaraces = new[]
            {
                RaceConstants.Metaraces.Ghost,
                RaceConstants.Metaraces.HalfCelestial,
                RaceConstants.Metaraces.HalfDragon,
                RaceConstants.Metaraces.HalfFiend,
                RaceConstants.Metaraces.Lich,
                RaceConstants.Metaraces.Mummy,
                RaceConstants.Metaraces.None,
                RaceConstants.Metaraces.Vampire,
                RaceConstants.Metaraces.Werebear,
                RaceConstants.Metaraces.Wereboar,
                RaceConstants.Metaraces.Weretiger,
                RaceConstants.Metaraces.Wererat,
                RaceConstants.Metaraces.Werewolf,
            };

            base.DistinctCollection(GroupConstants.All, metaraces);
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
                RaceConstants.Metaraces.Ghost,
                RaceConstants.Metaraces.HalfDragon,
                RaceConstants.Metaraces.HalfFiend,
                RaceConstants.Metaraces.HalfCelestial,
                RaceConstants.Metaraces.Lich,
                RaceConstants.Metaraces.Mummy,
                RaceConstants.Metaraces.None,
                RaceConstants.Metaraces.Vampire,
                RaceConstants.Metaraces.Werebear,
                RaceConstants.Metaraces.Wereboar,
                RaceConstants.Metaraces.Weretiger,
                RaceConstants.Metaraces.Wererat,
                RaceConstants.Metaraces.Werewolf,
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
                RaceConstants.Metaraces.Ghost,
                RaceConstants.Metaraces.HalfDragon,
                RaceConstants.Metaraces.HalfFiend,
                RaceConstants.Metaraces.HalfCelestial,
                RaceConstants.Metaraces.Mummy,
                RaceConstants.Metaraces.None,
                RaceConstants.Metaraces.Vampire,
                RaceConstants.Metaraces.Werebear,
                RaceConstants.Metaraces.Wereboar,
                RaceConstants.Metaraces.Weretiger,
                RaceConstants.Metaraces.Wererat,
                RaceConstants.Metaraces.Werewolf,
            };

            base.DistinctCollection(className, metaraces);
        }
    }
}