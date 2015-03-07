using System;
using NPCGen.Common.Alignments;
using NPCGen.Common.CharacterClasses;
using NPCGen.Common.Races;
using NPCGen.Tables.Interfaces;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Tables.Races.BaseRaces.Good
{
    [TestFixture]
    public class GoodRangerBaseRacesTests : PercentileTests
    {
        protected override String tableName
        {
            get { return String.Format(TableNameConstants.Formattable.Percentile.GOODNESSCLASSBaseRaces, AlignmentConstants.Good, CharacterClassConstants.Ranger); }
        }

        [Test]
        public override void TableIsComplete()
        {
            AssertTableIsComplete();
        }

        [TestCase(RaceConstants.BaseRaces.HillDwarfId, 1, 5)]
        [TestCase(RaceConstants.BaseRaces.HighElfId, 6, 20)]
        [TestCase(RaceConstants.BaseRaces.WoodElfId, 22, 36)]
        [TestCase(RaceConstants.BaseRaces.ForestGnomeId, 37, 41)]
        [TestCase(RaceConstants.BaseRaces.HalfElfId, 43, 57)]
        [TestCase(RaceConstants.BaseRaces.HalfOrcId, 60, 64)]
        [TestCase(RaceConstants.BaseRaces.HumanId, 65, 97)]
        [TestCase(EmptyContent, 98, 100)]
        public override void Percentile(String content, Int32 lower, Int32 upper)
        {
            base.Percentile(content, lower, upper);
        }

        [TestCase(RaceConstants.BaseRaces.WildElfId, 21)]
        [TestCase(RaceConstants.BaseRaces.RockGnomeId, 42)]
        [TestCase(RaceConstants.BaseRaces.LightfootHalflingId, 58)]
        [TestCase(RaceConstants.BaseRaces.TallfellowHalflingId, 59)]
        public override void Percentile(String content, Int32 roll)
        {
            base.Percentile(content, roll);
        }
    }
}