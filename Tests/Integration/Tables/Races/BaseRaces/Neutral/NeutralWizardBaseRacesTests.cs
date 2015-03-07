using System;
using NPCGen.Common.Alignments;
using NPCGen.Common.CharacterClasses;
using NPCGen.Common.Races;
using NPCGen.Tables.Interfaces;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Tables.Races.BaseRaces.Neutral
{
    [TestFixture]
    public class NeutralWizardBaseRacesTests : PercentileTests
    {
        protected override String tableName
        {
            get { return String.Format(TableNameConstants.Formattable.Percentile.GOODNESSCLASSBaseRaces, AlignmentConstants.Neutral, CharacterClassConstants.Wizard); }
        }

        [Test]
        public override void TableIsComplete()
        {
            AssertTableIsComplete();
        }

        [TestCase(RaceConstants.BaseRaces.HighElfId, 2, 26)]
        [TestCase(RaceConstants.BaseRaces.WoodElfId, 27, 28)]
        [TestCase(RaceConstants.BaseRaces.HalfElfId, 30, 44)]
        [TestCase(RaceConstants.BaseRaces.LightfootHalflingId, 45, 47)]
        [TestCase(RaceConstants.BaseRaces.TallfellowHalflingId, 48, 49)]
        [TestCase(RaceConstants.BaseRaces.HumanId, 51, 97)]
        [TestCase(EmptyContent, 99, 100)]
        public override void Percentile(String content, Int32 lower, Int32 upper)
        {
            base.Percentile(content, lower, upper);
        }

        [TestCase(RaceConstants.BaseRaces.GrayElfId, 1)]
        [TestCase(RaceConstants.BaseRaces.RockGnomeId, 29)]
        [TestCase(RaceConstants.BaseRaces.HalfOrcId, 50)]
        [TestCase(RaceConstants.BaseRaces.DoppelgangerId, 98)]
        public override void Percentile(String content, Int32 roll)
        {
            base.Percentile(content, roll);
        }
    }
}