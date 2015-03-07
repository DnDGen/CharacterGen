using System;
using NPCGen.Common.Alignments;
using NPCGen.Common.CharacterClasses;
using NPCGen.Common.Races;
using NPCGen.Tables.Interfaces;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Tables.Races.BaseRaces.Good
{
    [TestFixture]
    public class GoodRogueBaseRacesTests : PercentileTests
    {
        protected override String tableName
        {
            get { return String.Format(TableNameConstants.Formattable.Percentile.GOODNESSCLASSBaseRaces, AlignmentConstants.Good, CharacterClassConstants.Rogue); }
        }

        [Test]
        public override void TableIsComplete()
        {
            AssertTableIsComplete();
        }

        [TestCase(RaceConstants.BaseRaces.HillDwarfId, 1, 5)]
        [TestCase(RaceConstants.BaseRaces.HighElfId, 7, 19)]
        [TestCase(RaceConstants.BaseRaces.RockGnomeId, 21, 25)]
        [TestCase(RaceConstants.BaseRaces.HalfElfId, 26, 35)]
        [TestCase(RaceConstants.BaseRaces.LightfootHalflingId, 36, 60)]
        [TestCase(RaceConstants.BaseRaces.DeepHalflingId, 61, 66)]
        [TestCase(RaceConstants.BaseRaces.TallfellowHalflingId, 67, 72)]
        [TestCase(RaceConstants.BaseRaces.HalfOrcId, 73, 77)]
        [TestCase(RaceConstants.BaseRaces.HumanId, 78, 96)]
        [TestCase(EmptyContent, 98, 100)]
        public override void Percentile(String content, Int32 lower, Int32 upper)
        {
            base.Percentile(content, lower, upper);
        }

        [TestCase(RaceConstants.BaseRaces.MountainDwarfId, 6)]
        [TestCase(RaceConstants.BaseRaces.ForestGnomeId, 20)]
        [TestCase(RaceConstants.BaseRaces.SvirfneblinId, 97)]
        public override void Percentile(String content, Int32 roll)
        {
            base.Percentile(content, roll);
        }
    }
}