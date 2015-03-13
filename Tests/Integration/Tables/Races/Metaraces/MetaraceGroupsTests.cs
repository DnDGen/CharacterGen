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
            RaceConstants.Metaraces.HalfDragonId,
            RaceConstants.Metaraces.HalfFiendId,
            RaceConstants.Metaraces.WereratId,
            RaceConstants.Metaraces.WerewolfId)]
        [TestCase(AlignmentConstants.Good,
            RaceConstants.Metaraces.HalfDragonId,
            RaceConstants.Metaraces.HalfCelestialId,
            RaceConstants.Metaraces.WerebearId)]
        [TestCase(AlignmentConstants.Neutral,
            RaceConstants.Metaraces.WereboarId,
            RaceConstants.Metaraces.WeretigerId)]
        [TestCase(TableNameConstants.Set.Collection.Groups.Genetic,
            RaceConstants.Metaraces.HalfDragonId,
            RaceConstants.Metaraces.HalfFiendId,
            RaceConstants.Metaraces.HalfCelestialId)]
        [TestCase(TableNameConstants.Set.Collection.Groups.Lycanthrope,
            RaceConstants.Metaraces.WerebearId,
            RaceConstants.Metaraces.WereboarId,
            RaceConstants.Metaraces.WeretigerId,
            RaceConstants.Metaraces.WereratId,
            RaceConstants.Metaraces.WerewolfId)]
        public override void DistinctCollection(String name, params String[] collection)
        {
            base.DistinctCollection(name, collection);
        }
    }
}