using System;
using NPCGen.Common.Alignments;
using NPCGen.Common.CharacterClasses;
using NPCGen.Common.Races;
using NPCGen.Tables.Interfaces;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Tables.Races.BaseRaces.Neutral
{
    [TestFixture]
    public class NeutralMonkBaseRacesTests : PercentileTests
    {
        protected override String tableName
        {
            get { return String.Format(TableNameConstants.Formattable.Percentile.GOODNESSCLASSBaseRaces, AlignmentConstants.Neutral, CharacterClassConstants.Monk); }
        }

        [Test]
        public override void TableIsComplete()
        {
            AssertTableIsComplete();
        }

        [TestCase(RaceConstants.BaseRaces.HighElfId, 1, 2)]
        [TestCase(RaceConstants.BaseRaces.HalfElfId, 4, 13)]
        [TestCase(RaceConstants.BaseRaces.HalfOrcId, 16, 25)]
        [TestCase(RaceConstants.BaseRaces.HumanId, 26, 100)]
        public override void Percentile(String content, Int32 lower, Int32 upper)
        {
            base.Percentile(content, lower, upper);
        }

        [TestCase(RaceConstants.BaseRaces.WoodElfId, 3)]
        [TestCase(RaceConstants.BaseRaces.LightfootHalflingId, 14)]
        [TestCase(RaceConstants.BaseRaces.DeepHalflingId, 15)]
        public override void Percentile(String content, Int32 roll)
        {
            base.Percentile(content, roll);
        }
    }
}