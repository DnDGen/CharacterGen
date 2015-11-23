using CharacterGen.Common.Alignments;
using CharacterGen.Common.Races;
using CharacterGen.Tables;
using NUnit.Framework;
using System;

namespace CharacterGen.Tests.Integration.Tables.Races.Metaraces
{
    [TestFixture]
    public class MetaraceGroupsTests : CollectionTests
    {
        protected override String tableName
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
                GroupConstants.Undead
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
            RaceConstants.Metaraces.Ghost)]
        [TestCase(AlignmentConstants.Good,
            RaceConstants.Metaraces.HalfDragon,
            RaceConstants.Metaraces.HalfCelestial,
            RaceConstants.Metaraces.Werebear,
            RaceConstants.Metaraces.Ghost)]
        [TestCase(AlignmentConstants.Neutral,
            RaceConstants.Metaraces.Wereboar,
            RaceConstants.Metaraces.Weretiger,
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
            RaceConstants.Metaraces.Vampire,
            RaceConstants.Metaraces.Lich,
            RaceConstants.Metaraces.Ghost)]
        public override void DistinctCollection(String name, params String[] collection)
        {
            base.DistinctCollection(name, collection);
        }
    }
}