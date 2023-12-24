using DnDGen.CharacterGen.Alignments;
using DnDGen.CharacterGen.CharacterClasses;
using DnDGen.CharacterGen.Tables;
using DnDGen.CharacterGen.Races;
using NUnit.Framework;
using System.Linq;

namespace DnDGen.CharacterGen.Tests.Integration.Tables.Races.Metaraces
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
            var alignmentGroups = CollectionsMapper.Map(TableNameConstants.Set.Collection.AlignmentGroups);
            var classGroups = CollectionsMapper.Map(TableNameConstants.Set.Collection.ClassNameGroups);

            var names = new[]
            {
                GroupConstants.Genetic,
                GroupConstants.Lycanthrope,
                GroupConstants.Undead,
                GroupConstants.All,
                GroupConstants.HasWings,
            };

            names = names.Union(alignmentGroups[GroupConstants.All]).Union(classGroups[GroupConstants.All]).ToArray();

            AssertCollectionNames(names);
        }

        [TestCase(AlignmentConstants.ChaoticEvil,
            RaceConstants.Metaraces.Ghost,
            RaceConstants.Metaraces.HalfDragon,
            RaceConstants.Metaraces.HalfFiend,
            RaceConstants.Metaraces.Lich,
            RaceConstants.Metaraces.None,
            RaceConstants.Metaraces.Vampire,
            RaceConstants.Metaraces.Werewolf)]
        [TestCase(AlignmentConstants.ChaoticGood,
            RaceConstants.Metaraces.Ghost,
            RaceConstants.Metaraces.HalfDragon,
            RaceConstants.Metaraces.HalfCelestial,
            RaceConstants.Metaraces.None)]
        [TestCase(AlignmentConstants.ChaoticNeutral,
            RaceConstants.Metaraces.Ghost,
            RaceConstants.Metaraces.None)]
        [TestCase(AlignmentConstants.LawfulGood,
            RaceConstants.Metaraces.Ghost,
            RaceConstants.Metaraces.HalfDragon,
            RaceConstants.Metaraces.HalfCelestial,
            RaceConstants.Metaraces.None,
            RaceConstants.Metaraces.Werebear)]
        [TestCase(AlignmentConstants.LawfulEvil,
            RaceConstants.Metaraces.Ghost,
            RaceConstants.Metaraces.HalfDragon,
            RaceConstants.Metaraces.HalfFiend,
            RaceConstants.Metaraces.Lich,
            RaceConstants.Metaraces.Mummy,
            RaceConstants.Metaraces.None,
            RaceConstants.Metaraces.Vampire,
            RaceConstants.Metaraces.Wererat)]
        [TestCase(AlignmentConstants.LawfulNeutral,
            RaceConstants.Metaraces.Ghost,
            RaceConstants.Metaraces.Mummy,
            RaceConstants.Metaraces.None)]
        [TestCase(AlignmentConstants.NeutralEvil,
            RaceConstants.Metaraces.Ghost,
            RaceConstants.Metaraces.HalfDragon,
            RaceConstants.Metaraces.HalfFiend,
            RaceConstants.Metaraces.Lich,
            RaceConstants.Metaraces.Mummy,
            RaceConstants.Metaraces.None,
            RaceConstants.Metaraces.Vampire)]
        [TestCase(AlignmentConstants.NeutralGood,
            RaceConstants.Metaraces.Ghost,
            RaceConstants.Metaraces.HalfDragon,
            RaceConstants.Metaraces.HalfCelestial,
            RaceConstants.Metaraces.None)]
        [TestCase(AlignmentConstants.TrueNeutral,
            RaceConstants.Metaraces.Ghost,
            RaceConstants.Metaraces.None,
            RaceConstants.Metaraces.Wereboar,
            RaceConstants.Metaraces.Weretiger)]
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

        [Test]
        public void AllMetaracesHaveFullAlignmentGroup()
        {
            var alignmentGroups = CollectionsMapper.Map(TableNameConstants.Set.Collection.AlignmentGroups);
            var metaraceGroups = CollectionsMapper.Map(TableNameConstants.Set.Collection.MetaraceGroups);
            var alignmentMetaraces = metaraceGroups
                .Where(kvp => alignmentGroups[GroupConstants.All].Contains(kvp.Key)) //Get alignment-key metarace groups
                .SelectMany(kvp => kvp.Value) //get metaraces in those groups
                .Distinct();

            AssertCollection(alignmentMetaraces, metaraceGroups[GroupConstants.All]);
        }

        [Test]
        public void AllMetaracesHaveClassNameGroup()
        {
            var classGroups = CollectionsMapper.Map(TableNameConstants.Set.Collection.ClassNameGroups);
            var metaraceGroups = CollectionsMapper.Map(TableNameConstants.Set.Collection.MetaraceGroups);
            var classMetaraces = metaraceGroups
                .Where(kvp => classGroups[GroupConstants.All].Contains(kvp.Key)) //Get class-key metarace groups
                .SelectMany(kvp => kvp.Value) //get metaraces in those groups
                .Distinct();

            AssertCollection(classMetaraces, metaraceGroups[GroupConstants.All]);
        }
    }
}