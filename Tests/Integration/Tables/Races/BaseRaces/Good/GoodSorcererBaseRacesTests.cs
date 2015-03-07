using System;
using NPCGen.Common.Alignments;
using NPCGen.Common.CharacterClasses;
using NPCGen.Common.Races;
using NPCGen.Tables.Interfaces;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Tables.Races.BaseRaces.Good
{
    [TestFixture]
    public class GoodSorcererBaseRacesTests : PercentileTests
    {
        protected override String tableName
        {
            get { return String.Format(TableNameConstants.Formattable.Percentile.GOODNESSCLASSBaseRaces, AlignmentConstants.Good, CharacterClassConstants.Sorcerer); }
        }

        [Test]
        public override void TableIsComplete()
        {
            AssertTableIsComplete();
        }

        [TestCase(RaceConstants.BaseRaces.AasimarId, 1, 2)]
        [TestCase(RaceConstants.BaseRaces.HillDwarfId, 4, 5)]
        [TestCase(RaceConstants.BaseRaces.GrayElfId, 7, 8)]
        [TestCase(RaceConstants.BaseRaces.HighElfId, 9, 11)]
        [TestCase(RaceConstants.BaseRaces.WildElfId, 12, 36)]
        [TestCase(RaceConstants.BaseRaces.RockGnomeId, 39, 40)]
        [TestCase(RaceConstants.BaseRaces.HalfElfId, 41, 45)]
        [TestCase(RaceConstants.BaseRaces.LightfootHalflingId, 46, 54)]
        [TestCase(RaceConstants.BaseRaces.HalfOrcId, 57, 58)]
        [TestCase(RaceConstants.BaseRaces.HumanId, 59, 95)]
        [TestCase(EmptyContent, 97, 100)]
        public override void Percentile(String content, Int32 lower, Int32 upper)
        {
            base.Percentile(content, lower, upper);
        }

        [TestCase(RaceConstants.BaseRaces.DeepDwarfId, 3)]
        [TestCase(RaceConstants.BaseRaces.MountainDwarfId, 6)]
        [TestCase(RaceConstants.BaseRaces.WoodElfId, 37)]
        [TestCase(RaceConstants.BaseRaces.ForestGnomeId, 38)]
        [TestCase(RaceConstants.BaseRaces.DeepHalflingId, 55)]
        [TestCase(RaceConstants.BaseRaces.TallfellowHalflingId, 56)]
        [TestCase(RaceConstants.BaseRaces.SvirfneblinId, 96)]
        public override void Percentile(String content, Int32 roll)
        {
            base.Percentile(content, roll);
        }
    }
}