using System;
using NPCGen.Common.Alignments;
using NPCGen.Common.Races;
using NPCGen.Tables.Interfaces;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Tables.Races.Metaraces
{
    [TestFixture]
    public class MetaraceGroupsTests : CollectionTests
    {
        protected override String tableName
        {
            get { return TableNameConstants.Set.Collection.MetaraceGroups; }
        }

        [TestCase(AlignmentConstants.Evil,
            RaceConstants.Metaraces.HalfDragon,
            RaceConstants.Metaraces.HalfFiend,
            RaceConstants.Metaraces.Wererat,
            RaceConstants.Metaraces.Werewolf)]
        [TestCase(AlignmentConstants.Good,
            RaceConstants.Metaraces.HalfDragon,
            RaceConstants.Metaraces.HalfCelestial,
            RaceConstants.Metaraces.Werebear)]
        [TestCase(AlignmentConstants.Neutral,
            RaceConstants.Metaraces.Wereboar,
            RaceConstants.Metaraces.Weretiger)]
        [TestCase(TableNameConstants.Set.Collection.Groups.Genetic,
            RaceConstants.Metaraces.HalfDragon,
            RaceConstants.Metaraces.HalfFiend,
            RaceConstants.Metaraces.HalfCelestial)]
        [TestCase(TableNameConstants.Set.Collection.Groups.Lycanthrope,
            RaceConstants.Metaraces.Werebear,
            RaceConstants.Metaraces.Wereboar,
            RaceConstants.Metaraces.Weretiger,
            RaceConstants.Metaraces.Wererat,
            RaceConstants.Metaraces.Werewolf)]
        public override void DistinctCollection(String name, params String[] collection)
        {
            base.DistinctCollection(name, collection);
        }
    }
}