using System;
using NPCGen.Common.Alignments;
using NPCGen.Common.CharacterClasses;
using NPCGen.Common.Races;
using NPCGen.Tables.Interfaces;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Tables.Races.BaseRaces.Evil
{
    [TestFixture]
    public class EvilFighterBaseRacesTests : PercentileTests
    {
        protected override String tableName
        {
            get { return String.Format(TableNameConstants.Formattable.Percentile.GOODNESSCLASSBaseRaces, AlignmentConstants.Evil, CharacterClassConstants.Fighter); }
        }

        [Test]
        public override void TableIsComplete()
        {
            AssertTableIsComplete();
        }

        [TestCase(RaceConstants.BaseRaces.DeepDwarfId, 1, 2)]
        [TestCase(RaceConstants.BaseRaces.HillDwarfId, 3, 4)]
        [TestCase(RaceConstants.BaseRaces.WoodElfId, 6, 7)]
        [TestCase(RaceConstants.BaseRaces.HalfElfId, 8, 12)]
        [TestCase(RaceConstants.BaseRaces.HalfOrcId, 15, 23)]
        [TestCase(RaceConstants.BaseRaces.HumanId, 24, 53)]
        [TestCase(RaceConstants.BaseRaces.HobgoblinId, 56, 80)]
        [TestCase(RaceConstants.BaseRaces.OrcId, 82, 86)]
        [TestCase(RaceConstants.BaseRaces.DrowId, 87, 88)]
        [TestCase(EmptyContent, 97, 100)]
        public override void Percentile(String content, Int32 lower, Int32 upper)
        {
            base.Percentile(content, lower, upper);
        }

        [TestCase(RaceConstants.BaseRaces.HighElfId, 5)]
        [TestCase(RaceConstants.BaseRaces.LightfootHalflingId, 13)]
        [TestCase(RaceConstants.BaseRaces.DeepHalflingId, 14)]
        [TestCase(RaceConstants.BaseRaces.LizardfolkId, 54)]
        [TestCase(RaceConstants.BaseRaces.GoblinId, 55)]
        [TestCase(RaceConstants.BaseRaces.KoboldId, 81)]
        [TestCase(RaceConstants.BaseRaces.DuergarDwarfId, 89)]
        [TestCase(RaceConstants.BaseRaces.DerroId, 90)]
        [TestCase(RaceConstants.BaseRaces.GnollId, 91)]
        [TestCase(RaceConstants.BaseRaces.TroglodyteId, 92)]
        [TestCase(RaceConstants.BaseRaces.BugbearId, 93)]
        [TestCase(RaceConstants.BaseRaces.OgreId, 94)]
        [TestCase(RaceConstants.BaseRaces.MindFlayerId, 95)]
        [TestCase(RaceConstants.BaseRaces.OgreMageId, 96)]
        public override void Percentile(String content, Int32 roll)
        {
            base.Percentile(content, roll);
        }
    }
}