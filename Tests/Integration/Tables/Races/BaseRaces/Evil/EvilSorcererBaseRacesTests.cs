using System;
using NPCGen.Common.Alignments;
using NPCGen.Common.CharacterClasses;
using NPCGen.Common.Races;
using NPCGen.Tables.Interfaces;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Tables.Races.BaseRaces.Evil
{
    [TestFixture]
    public class EvilSorcererBaseRacesTests : PercentileTests
    {
        protected override String tableName
        {
            get { return String.Format(TableNameConstants.Formattable.Percentile.GOODNESSCLASSBaseRaces, AlignmentConstants.Evil, CharacterClassConstants.Sorcerer); }
        }

        [Test]
        public override void TableIsComplete()
        {
            AssertTableIsComplete();
        }

        [TestCase(RaceConstants.BaseRaces.WildElfId, 1)]
        [TestCase(RaceConstants.BaseRaces.DeepHalflingId, 22)]
        [TestCase(RaceConstants.BaseRaces.TallfellowHalflingId, 23)]
        [TestCase(RaceConstants.BaseRaces.LizardfolkId, 69)]
        [TestCase(RaceConstants.BaseRaces.GoblinId, 70)]
        [TestCase(RaceConstants.BaseRaces.HobgoblinId, 71)]
        [TestCase(RaceConstants.BaseRaces.GnollId, 87)]
        [TestCase(RaceConstants.BaseRaces.BugbearId, 91)]
        [TestCase(RaceConstants.BaseRaces.OgreId, 92)]
        [TestCase(RaceConstants.BaseRaces.MinotaurId, 93)]
        [TestCase(RaceConstants.BaseRaces.MindFlayerId, 94)]
        [TestCase(RaceConstants.BaseRaces.OgreMageId, 95)]
        public override void Percentile(String content, Int32 roll)
        {
            base.Percentile(content, roll);
        }

        [TestCase(RaceConstants.BaseRaces.HalfElfId, 2, 16)]
        [TestCase(RaceConstants.BaseRaces.LightfootHalflingId, 17, 21)]
        [TestCase(RaceConstants.BaseRaces.HalfOrcId, 24, 28)]
        [TestCase(RaceConstants.BaseRaces.HumanId, 29, 68)]
        [TestCase(RaceConstants.BaseRaces.KoboldId, 72, 86)]
        [TestCase(RaceConstants.BaseRaces.TroglodyteId, 88, 90)]
        [TestCase(EmptyContent, 96, 100)]
        public override void Percentile(String content, Int32 lower, Int32 upper)
        {
            base.Percentile(content, lower, upper);
        }
    }
}