using System;
using NPCGen.Common.Alignments;
using NPCGen.Common.CharacterClasses;
using NPCGen.Common.Races;
using NPCGen.Tables.Interfaces;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Tables.Races.BaseRaces.Neutral
{
    [TestFixture]
    public class NeutralClericBaseRacesTests : PercentileTests
    {
        protected override String tableName
        {
            get { return String.Format(TableNameConstants.Formattable.Percentile.GOODNESSCLASSBaseRaces, AlignmentConstants.Neutral, CharacterClassConstants.Cleric); }
        }

        [Test]
        public override void TableIsComplete()
        {
            AssertTableIsComplete();
        }

        [TestCase(RaceConstants.BaseRaces.DeepDwarfId, 1, 15)]
        [TestCase(RaceConstants.BaseRaces.HillDwarfId, 16, 25)]
        [TestCase(RaceConstants.BaseRaces.WoodElfId, 29, 38)]
        [TestCase(RaceConstants.BaseRaces.HalfElfId, 40, 48)]
        [TestCase(RaceConstants.BaseRaces.LightfootHalflingId, 49, 58)]
        [TestCase(RaceConstants.BaseRaces.HalfOrcId, 61, 62)]
        [TestCase(RaceConstants.BaseRaces.HumanId, 63, 90)]
        [TestCase(RaceConstants.BaseRaces.LizardfolkId, 91, 97)]
        [TestCase(EmptyContent, 99, 100)]
        public override void Percentile(String content, Int32 lower, Int32 upper)
        {
            base.Percentile(content, lower, upper);
        }

        [TestCase(RaceConstants.BaseRaces.MountainDwarfId, 26)]
        [TestCase(RaceConstants.BaseRaces.HighElfId, 27)]
        [TestCase(RaceConstants.BaseRaces.WildElfId, 28)]
        [TestCase(RaceConstants.BaseRaces.RockGnomeId, 39)]
        [TestCase(RaceConstants.BaseRaces.DeepHalflingId, 59)]
        [TestCase(RaceConstants.BaseRaces.TallfellowHalflingId, 60)]
        [TestCase(RaceConstants.BaseRaces.DoppelgangerId, 98)]
        public override void Percentile(String content, Int32 roll)
        {
            base.Percentile(content, roll);
        }
    }
}