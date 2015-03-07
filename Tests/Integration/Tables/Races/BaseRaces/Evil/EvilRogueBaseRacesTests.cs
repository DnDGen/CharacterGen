using System;
using NPCGen.Common.Alignments;
using NPCGen.Common.CharacterClasses;
using NPCGen.Common.Races;
using NPCGen.Tables.Interfaces;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Tables.Races.BaseRaces.Evil
{
    [TestFixture]
    public class EvilRogueBaseRacesTests : PercentileTests
    {
        protected override String tableName
        {
            get { return String.Format(TableNameConstants.Formattable.Percentile.GOODNESSCLASSBaseRaces, AlignmentConstants.Evil, CharacterClassConstants.Rogue); }
        }

        [Test]
        public override void TableIsComplete()
        {
            AssertTableIsComplete();
        }

        [TestCase(RaceConstants.BaseRaces.DeepDwarfId, 1)]
        [TestCase(RaceConstants.BaseRaces.HighElfId, 2)]
        [TestCase(RaceConstants.BaseRaces.WoodElfId, 3)]
        [TestCase(RaceConstants.BaseRaces.DeepHalflingId, 39)]
        [TestCase(RaceConstants.BaseRaces.TallfellowHalflingId, 40)]
        [TestCase(RaceConstants.BaseRaces.HobgoblinId, 86)]
        [TestCase(RaceConstants.BaseRaces.KoboldId, 87)]
        [TestCase(RaceConstants.BaseRaces.MindFlayerId, 94)]
        public override void Percentile(String content, Int32 roll)
        {
            base.Percentile(content, roll);
        }

        [TestCase(RaceConstants.BaseRaces.HalfElfId, 4, 18)]
        [TestCase(RaceConstants.BaseRaces.LightfootHalflingId, 19, 38)]
        [TestCase(RaceConstants.BaseRaces.HalfOrcId, 41, 50)]
        [TestCase(RaceConstants.BaseRaces.HumanId, 51, 70)]
        [TestCase(RaceConstants.BaseRaces.GoblinId, 71, 85)]
        [TestCase(RaceConstants.BaseRaces.TieflingId, 88, 89)]
        [TestCase(RaceConstants.BaseRaces.BugbearId, 90, 93)]
        [TestCase(EmptyContent, 95, 100)]
        public override void Percentile(String content, Int32 lower, Int32 upper)
        {
            base.Percentile(content, lower, upper);
        }
    }
}