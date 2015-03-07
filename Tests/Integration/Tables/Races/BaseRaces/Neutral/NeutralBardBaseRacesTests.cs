using System;
using NPCGen.Common.Alignments;
using NPCGen.Common.CharacterClasses;
using NPCGen.Common.Races;
using NPCGen.Tables.Interfaces;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Tables.Races.BaseRaces.Neutral
{
    [TestFixture]
    public class NeutralBardBaseRacesTests : PercentileTests
    {
        protected override String tableName
        {
            get { return String.Format(TableNameConstants.Formattable.Percentile.GOODNESSCLASSBaseRaces, AlignmentConstants.Neutral, CharacterClassConstants.Bard); }
        }

        [Test]
        public override void TableIsComplete()
        {
            AssertTableIsComplete();
        }

        [TestCase(RaceConstants.BaseRaces.HillDwarfId, 2, 3)]
        [TestCase(RaceConstants.BaseRaces.GrayElfId, 4, 5)]
        [TestCase(RaceConstants.BaseRaces.HighElfId, 6, 15)]
        [TestCase(RaceConstants.BaseRaces.WoodElfId, 17, 21)]
        [TestCase(RaceConstants.BaseRaces.RockGnomeId, 22, 23)]
        [TestCase(RaceConstants.BaseRaces.HalfElfId, 24, 33)]
        [TestCase(RaceConstants.BaseRaces.LightfootHalflingId, 34, 36)]
        [TestCase(RaceConstants.BaseRaces.HalfOrcId, 39, 40)]
        [TestCase(RaceConstants.BaseRaces.HumanId, 41, 98)]
        [TestCase(EmptyContent, 99, 100)]
        public override void Percentile(String content, Int32 lower, Int32 upper)
        {
            base.Percentile(content, lower, upper);
        }

        [TestCase(RaceConstants.BaseRaces.DeepDwarfId, 1)]
        [TestCase(RaceConstants.BaseRaces.WildElfId, 16)]
        [TestCase(RaceConstants.BaseRaces.DeepHalflingId, 37)]
        [TestCase(RaceConstants.BaseRaces.TallfellowHalflingId, 38)]
        public override void Percentile(String content, Int32 roll)
        {
            base.Percentile(content, roll);
        }
    }
}