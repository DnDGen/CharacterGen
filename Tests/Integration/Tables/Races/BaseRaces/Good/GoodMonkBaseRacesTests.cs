using System;
using NPCGen.Common.Alignments;
using NPCGen.Common.CharacterClasses;
using NPCGen.Common.Races;
using NPCGen.Tables.Interfaces;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Tables.Races.BaseRaces.Good
{
    [TestFixture]
    public class GoodMonkBaseRacesTests : PercentileTests
    {
        protected override String tableName
        {
            get { return String.Format(TableNameConstants.Formattable.Percentile.GOODNESSCLASSBaseRaces, AlignmentConstants.Good, CharacterClassConstants.Monk); }
        }

        [Test]
        public override void TableIsComplete()
        {
            AssertTableIsComplete();
        }

        [TestCase(RaceConstants.BaseRaces.AasimarId, 1, 2)]
        [TestCase(RaceConstants.BaseRaces.HighElfId, 4, 13)]
        [TestCase(RaceConstants.BaseRaces.HalfElfId, 14, 18)]
        [TestCase(RaceConstants.BaseRaces.HalfOrcId, 21, 25)]
        [TestCase(RaceConstants.BaseRaces.HumanId, 26, 97)]
        [TestCase(EmptyContent, 98, 100)]
        public override void Percentile(String content, Int32 lower, Int32 upper)
        {
            base.Percentile(content, lower, upper);
        }

        [TestCase(RaceConstants.BaseRaces.HillDwarfId, 3)]
        [TestCase(RaceConstants.BaseRaces.LightfootHalflingId, 19)]
        [TestCase(RaceConstants.BaseRaces.DeepHalflingId, 20)]
        public override void Percentile(String content, Int32 roll)
        {
            base.Percentile(content, roll);
        }
    }
}