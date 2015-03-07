using System;
using NPCGen.Common.Alignments;
using NPCGen.Common.CharacterClasses;
using NPCGen.Common.Races;
using NPCGen.Tables.Interfaces;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Tables.Races.BaseRaces.Good
{
    [TestFixture]
    public class GoodPaladinBaseRacesTests : PercentileTests
    {
        protected override String tableName
        {
            get { return String.Format(TableNameConstants.Formattable.Percentile.GOODNESSCLASSBaseRaces, AlignmentConstants.Good, CharacterClassConstants.Paladin); }
        }

        [Test]
        public override void TableIsComplete()
        {
            AssertTableIsComplete();
        }

        [TestCase(RaceConstants.BaseRaces.AasimarId, 1, 10)]
        [TestCase(RaceConstants.BaseRaces.HillDwarfId, 11, 20)]
        [TestCase(RaceConstants.BaseRaces.HalfElfId, 23, 27)]
        [TestCase(RaceConstants.BaseRaces.HumanId, 31, 97)]
        [TestCase(EmptyContent, 98, 100)]
        public override void Percentile(String content, Int32 lower, Int32 upper)
        {
            base.Percentile(content, lower, upper);
        }

        [TestCase(RaceConstants.BaseRaces.MountainDwarfId, 21)]
        [TestCase(RaceConstants.BaseRaces.RockGnomeId, 22)]
        [TestCase(RaceConstants.BaseRaces.LightfootHalflingId, 28)]
        [TestCase(RaceConstants.BaseRaces.DeepHalflingId, 29)]
        [TestCase(RaceConstants.BaseRaces.HalfOrcId, 30)]
        public override void Percentile(String content, Int32 roll)
        {
            base.Percentile(content, roll);
        }
    }
}