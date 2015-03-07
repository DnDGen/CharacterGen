using System;
using NPCGen.Common.Alignments;
using NPCGen.Common.CharacterClasses;
using NPCGen.Common.Races;
using NPCGen.Tables.Interfaces;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Tables.Races.BaseRaces.Good
{
    [TestFixture]
    public class GoodWizardBaseRacesTests : PercentileTests
    {
        protected override String tableName
        {
            get { return String.Format(TableNameConstants.Formattable.Percentile.GOODNESSCLASSBaseRaces, AlignmentConstants.Good, CharacterClassConstants.Wizard); }
        }

        [Test]
        public override void TableIsComplete()
        {
            AssertTableIsComplete();
        }

        [TestCase(RaceConstants.BaseRaces.GrayElfId, 3, 7)]
        [TestCase(RaceConstants.BaseRaces.HighElfId, 8, 41)]
        [TestCase(RaceConstants.BaseRaces.RockGnomeId, 44, 48)]
        [TestCase(RaceConstants.BaseRaces.HalfElfId, 49, 58)]
        [TestCase(RaceConstants.BaseRaces.LightfootHalflingId, 59, 63)]
        [TestCase(RaceConstants.BaseRaces.TallfellowHalflingId, 65, 67)]
        [TestCase(RaceConstants.BaseRaces.HumanId, 69, 96)]
        [TestCase(EmptyContent, 98, 100)]
        public override void Percentile(String content, Int32 lower, Int32 upper)
        {
            base.Percentile(content, lower, upper);
        }

        [TestCase(RaceConstants.BaseRaces.AasimarId, 1)]
        [TestCase(RaceConstants.BaseRaces.HillDwarfId, 2)]
        [TestCase(RaceConstants.BaseRaces.WoodElfId, 42)]
        [TestCase(RaceConstants.BaseRaces.ForestGnomeId, 43)]
        [TestCase(RaceConstants.BaseRaces.DeepHalflingId, 64)]
        [TestCase(RaceConstants.BaseRaces.HalfOrcId, 68)]
        [TestCase(RaceConstants.BaseRaces.SvirfneblinId, 97)]
        public override void Percentile(String content, Int32 roll)
        {
            base.Percentile(content, roll);
        }
    }
}