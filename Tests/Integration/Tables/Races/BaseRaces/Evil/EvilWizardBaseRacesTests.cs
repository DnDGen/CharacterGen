using System;
using NPCGen.Common.Alignments;
using NPCGen.Common.CharacterClasses;
using NPCGen.Common.Races;
using NPCGen.Tables.Interfaces;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Tables.Races.BaseRaces.Evil
{
    [TestFixture]
    public class EvilWizardBaseRacesTests : PercentileTests
    {
        protected override String tableName
        {
            get { return String.Format(TableNameConstants.Formattable.Percentile.GOODNESSCLASSBaseRaces, AlignmentConstants.Evil, CharacterClassConstants.Wizard); }
        }

        [Test]
        public override void TableIsComplete()
        {
            AssertTableIsComplete();
        }

        [TestCase(RaceConstants.BaseRaces.HighElfId, 1, 10)]
        [TestCase(RaceConstants.BaseRaces.HalfElfId, 12, 26)]
        [TestCase(RaceConstants.BaseRaces.HumanId, 29, 78)]
        [TestCase(RaceConstants.BaseRaces.HobgoblinId, 79, 80)]
        [TestCase(RaceConstants.BaseRaces.DrowId, 82, 91)]
        [TestCase(RaceConstants.BaseRaces.OgreMageId, 95, 96)]
        [TestCase(EmptyContent, 97, 100)]
        public override void Percentile(String content, Int32 lower, Int32 upper)
        {
            base.Percentile(content, lower, upper);
        }

        [TestCase(RaceConstants.BaseRaces.WoodElfId, 11)]
        [TestCase(RaceConstants.BaseRaces.LightfootHalflingId, 27)]
        [TestCase(RaceConstants.BaseRaces.TallfellowHalflingId, 28)]
        [TestCase(RaceConstants.BaseRaces.TieflingId, 81)]
        [TestCase(RaceConstants.BaseRaces.GnollId, 92)]
        [TestCase(RaceConstants.BaseRaces.BugbearId, 93)]
        [TestCase(RaceConstants.BaseRaces.MindFlayerId, 94)]
        public override void Percentile(String content, Int32 roll)
        {
            base.Percentile(content, roll);
        }
    }
}