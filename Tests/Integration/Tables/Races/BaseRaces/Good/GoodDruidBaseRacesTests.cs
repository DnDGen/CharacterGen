using System;
using NPCGen.Common.Alignments;
using NPCGen.Common.CharacterClasses;
using NPCGen.Common.Races;
using NPCGen.Tables.Interfaces;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Tables.Races.BaseRaces.Good
{
    [TestFixture]
    public class GoodDruidBaseRacesTests : PercentileTests
    {
        protected override String tableName
        {
            get { return String.Format(TableNameConstants.Formattable.Percentile.GOODNESSCLASSBaseRaces, AlignmentConstants.Good, CharacterClassConstants.Druid); }
        }

        [Test]
        public override void TableIsComplete()
        {
            AssertTableIsComplete();
        }

        [TestCase(RaceConstants.BaseRaces.GrayElfId, 1)]
        [TestCase(RaceConstants.BaseRaces.RockGnomeId, 37)]
        [TestCase(RaceConstants.BaseRaces.LightfootHalflingId, 47)]
        [TestCase(RaceConstants.BaseRaces.TallfellowHalflingId, 48)]
        [TestCase(RaceConstants.BaseRaces.HalfOrcId, 49)]
        [TestCase(EmptyContent, 100)]
        public override void Percentile(String content, Int32 roll)
        {
            base.Percentile(content, roll);
        }

        [TestCase(RaceConstants.BaseRaces.HighElfId, 2, 11)]
        [TestCase(RaceConstants.BaseRaces.WildElfId, 12, 21)]
        [TestCase(RaceConstants.BaseRaces.WoodElfId, 22, 31)]
        [TestCase(RaceConstants.BaseRaces.ForestGnomeId, 32, 36)]
        [TestCase(RaceConstants.BaseRaces.HalfElfId, 38, 46)]
        [TestCase(RaceConstants.BaseRaces.HumanId, 50, 99)]
        public override void Percentile(String content, Int32 lower, Int32 upper)
        {
            base.Percentile(content, lower, upper);
        }
    }
}