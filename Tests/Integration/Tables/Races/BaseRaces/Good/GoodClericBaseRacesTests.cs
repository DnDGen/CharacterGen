using System;
using NPCGen.Common.Alignments;
using NPCGen.Common.CharacterClasses;
using NPCGen.Common.Races;
using NPCGen.Tables.Interfaces;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Tables.Races.BaseRaces.Good
{
    [TestFixture]
    public class GoodClericBaseRacesTests : PercentileTests
    {
        protected override String tableName
        {
            get { return String.Format(TableNameConstants.Formattable.Percentile.GOODNESSCLASSBaseRaces, AlignmentConstants.Good, CharacterClassConstants.Cleric); }
        }

        [Test]
        public override void TableIsComplete()
        {
            AssertTableIsComplete();
        }

        [TestCase(RaceConstants.BaseRaces.AasimarId, 1)]
        [TestCase(RaceConstants.BaseRaces.DeepDwarfId, 2)]
        [TestCase(RaceConstants.BaseRaces.GrayElfId, 25)]
        [TestCase(RaceConstants.BaseRaces.WoodElfId, 41)]
        [TestCase(RaceConstants.BaseRaces.ForestGnomeId, 42)]
        [TestCase(RaceConstants.BaseRaces.DeepHalflingId, 67)]
        [TestCase(RaceConstants.BaseRaces.HalfOrcId, 70)]
        [TestCase(RaceConstants.BaseRaces.SvirfneblinId, 96)]
        public override void Percentile(String content, Int32 roll)
        {
            base.Percentile(content, roll);
        }

        [TestCase(RaceConstants.BaseRaces.HillDwarfId, 3, 22)]
        [TestCase(RaceConstants.BaseRaces.MountainDwarfId, 23, 24)]
        [TestCase(RaceConstants.BaseRaces.HighElfId, 26, 35)]
        [TestCase(RaceConstants.BaseRaces.WildElfId, 36, 40)]
        [TestCase(RaceConstants.BaseRaces.RockGnomeId, 43, 51)]
        [TestCase(RaceConstants.BaseRaces.HalfElfId, 52, 56)]
        [TestCase(RaceConstants.BaseRaces.LightfootHalflingId, 57, 66)]
        [TestCase(RaceConstants.BaseRaces.TallfellowHalflingId, 68, 69)]
        [TestCase(RaceConstants.BaseRaces.HumanId, 71, 95)]
        [TestCase(EmptyContent, 97, 100)]
        public override void Percentile(String content, Int32 lower, Int32 upper)
        {
            base.Percentile(content, lower, upper);
        }
    }
}