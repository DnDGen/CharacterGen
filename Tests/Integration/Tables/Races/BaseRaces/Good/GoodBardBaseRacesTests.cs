using System;
using NPCGen.Common.Alignments;
using NPCGen.Common.CharacterClasses;
using NPCGen.Common.Races;
using NPCGen.Tables.Interfaces;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Tables.Races.BaseRaces.Good
{
    [TestFixture]
    public class GoodBardBaseRacesTests : PercentileTests
    {
        protected override String tableName
        {
            get { return String.Format(TableNameConstants.Formattable.Percentile.GOODNESSCLASSBaseRaces, AlignmentConstants.Good, CharacterClassConstants.Bard); }
        }

        [Test]
        public override void TableIsComplete()
        {
            AssertTableIsComplete();
        }

        [TestCase(RaceConstants.BaseRaces.AasimarId, 1)]
        [TestCase(RaceConstants.BaseRaces.WildElfId, 37)]
        [TestCase(RaceConstants.BaseRaces.WoodElfId, 38)]
        [TestCase(RaceConstants.BaseRaces.ForestGnomeId, 39)]
        [TestCase(RaceConstants.BaseRaces.LightfootHalflingId, 54)]
        [TestCase(RaceConstants.BaseRaces.DeepHalflingId, 55)]
        [TestCase(RaceConstants.BaseRaces.SvirfneblinId, 98)]
        public override void Percentile(String content, Int32 roll)
        {
            base.Percentile(content, roll);
        }

        [TestCase(RaceConstants.BaseRaces.HillDwarfId, 2, 6)]
        [TestCase(RaceConstants.BaseRaces.GrayElfId, 7, 11)]
        [TestCase(RaceConstants.BaseRaces.HighElfId, 12, 36)]
        [TestCase(RaceConstants.BaseRaces.RockGnomeId, 40, 44)]
        [TestCase(RaceConstants.BaseRaces.HalfElfId, 45, 53)]
        [TestCase(RaceConstants.BaseRaces.TallfellowHalflingId, 56, 57)]
        [TestCase(RaceConstants.BaseRaces.HumanId, 58, 97)]
        [TestCase(EmptyContent, 99, 100)]
        public override void Percentile(String content, Int32 lower, Int32 upper)
        {
            base.Percentile(content, lower, upper);
        }
    }
}