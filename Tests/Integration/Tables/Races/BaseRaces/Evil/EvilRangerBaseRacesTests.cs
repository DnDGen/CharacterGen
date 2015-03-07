using System;
using NPCGen.Common.Alignments;
using NPCGen.Common.CharacterClasses;
using NPCGen.Common.Races;
using NPCGen.Tables.Interfaces;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Tables.Races.BaseRaces.Evil
{
    [TestFixture]
    public class EvilRangerBaseRacesTests : PercentileTests
    {
        protected override String tableName
        {
            get { return String.Format(TableNameConstants.Formattable.Percentile.GOODNESSCLASSBaseRaces, AlignmentConstants.Evil, CharacterClassConstants.Ranger); }
        }

        [Test]
        public override void TableIsComplete()
        {
            AssertTableIsComplete();
        }

        [TestCase(RaceConstants.BaseRaces.HighElfId, 1)]
        [TestCase(RaceConstants.BaseRaces.LightfootHalflingId, 29)]
        [TestCase(RaceConstants.BaseRaces.TallfellowHalflingId, 30)]
        [TestCase(RaceConstants.BaseRaces.HobgoblinId, 72)]
        [TestCase(RaceConstants.BaseRaces.TroglodyteId, 93)]
        [TestCase(RaceConstants.BaseRaces.BugbearId, 94)]
        [TestCase(RaceConstants.BaseRaces.OgreId, 95)]
        public override void Percentile(String content, Int32 roll)
        {
            base.Percentile(content, roll);
        }

        [TestCase(RaceConstants.BaseRaces.WoodElfId, 2, 11)]
        [TestCase(RaceConstants.BaseRaces.HalfElfId, 12, 28)]
        [TestCase(RaceConstants.BaseRaces.HalfOrcId, 31, 39)]
        [TestCase(RaceConstants.BaseRaces.HumanId, 40, 69)]
        [TestCase(RaceConstants.BaseRaces.LizardfolkId, 70, 71)]
        [TestCase(RaceConstants.BaseRaces.GnollId, 73, 92)]
        [TestCase(EmptyContent, 96, 100)]
        public override void Percentile(String content, Int32 lower, Int32 upper)
        {
            base.Percentile(content, lower, upper);
        }
    }
}