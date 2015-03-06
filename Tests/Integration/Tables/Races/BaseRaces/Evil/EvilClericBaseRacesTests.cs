using System;
using NPCGen.Common.Alignments;
using NPCGen.Common.CharacterClasses;
using NPCGen.Common.Races;
using NPCGen.Tables.Interfaces;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Tables.Races.BaseRaces.Evil
{
    [TestFixture]
    public class EvilClericBaseRacesTests : PercentileTests
    {
        protected override String tableName
        {
            get { return String.Format(TableNameConstants.Formattable.Percentile.GOODNESSCLASSBaseRaces, AlignmentConstants.Evil, CharacterClassConstants.Cleric); }
        }

        [Test]
        public override void TableIsComplete()
        {
            AssertTableIsComplete();
        }

        [TestCase(RaceConstants.BaseRaces.DeepDwarfId, 1, 2)]
        [TestCase(RaceConstants.BaseRaces.WoodElfId, 6, 8)]
        [TestCase(RaceConstants.BaseRaces.HalfElfId, 9, 18)]
        [TestCase(RaceConstants.BaseRaces.LightfootHalflingId, 19, 20)]
        [TestCase(RaceConstants.BaseRaces.HalfOrcId, 23, 25)]
        [TestCase(RaceConstants.BaseRaces.HumanId, 26, 56)]
        [TestCase(RaceConstants.BaseRaces.LizardfolkId, 57, 63)]
        [TestCase(RaceConstants.BaseRaces.DrowId, 69, 71)]
        [TestCase(RaceConstants.BaseRaces.GnollId, 73, 74)]
        [TestCase(RaceConstants.BaseRaces.TroglodyteId, 75, 89)]
        [TestCase(RaceConstants.BaseRaces.BugbearId, 90, 91)]
        [TestCase(EmptyContent, 96, 100)]
        public override void Percentile(String content, Int32 lower, Int32 upper)
        {
            base.Percentile(content, lower, upper);
        }

        [TestCase(RaceConstants.BaseRaces.HillDwarfId, 3)]
        [TestCase(RaceConstants.BaseRaces.HighElfId, 4)]
        [TestCase(RaceConstants.BaseRaces.WildElfId, 5)]
        [TestCase(RaceConstants.BaseRaces.DeepHalflingId, 21)]
        [TestCase(RaceConstants.BaseRaces.TallfellowHalflingId, 22)]
        [TestCase(RaceConstants.BaseRaces.GoblinId, 64)]
        [TestCase(RaceConstants.BaseRaces.HobgoblinId, 65)]
        [TestCase(RaceConstants.BaseRaces.KoboldId, 66)]
        [TestCase(RaceConstants.BaseRaces.OrcId, 67)]
        [TestCase(RaceConstants.BaseRaces.TieflingId, 68)]
        [TestCase(RaceConstants.BaseRaces.DuergarDwarfId, 72)]
        [TestCase(RaceConstants.BaseRaces.OgreId, 92)]
        [TestCase(RaceConstants.BaseRaces.MinotaurId, 93)]
        [TestCase(RaceConstants.BaseRaces.MindFlayerId, 94)]
        [TestCase(RaceConstants.BaseRaces.OgreMageId, 95)]
        public override void Percentile(String content, Int32 roll)
        {
            base.Percentile(content, roll);
        }
    }
}