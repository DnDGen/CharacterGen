using System;
using NPCGen.Common.Alignments;
using NPCGen.Common.CharacterClasses;
using NPCGen.Common.Races;
using NPCGen.Tables.Interfaces;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Tables.Races.BaseRaces.Evil
{
    [TestFixture]
    public class EvilBardBaseRacesTests : PercentileTests
    {
        protected override String tableName
        {
            get { return String.Format(TableNameConstants.Formattable.Percentile.GOODNESSCLASSBaseRaces, AlignmentConstants.Evil, CharacterClassConstants.Bard); }
        }

        [Test]
        public override void TableIsComplete()
        {
            AssertTableIsComplete();
        }

        [TestCase(RaceConstants.BaseRaces.HighElfId, 1)]
        [TestCase(RaceConstants.BaseRaces.WoodElfId, 2)]
        [TestCase(RaceConstants.BaseRaces.LightfootHalflingId, 18)]
        [TestCase(RaceConstants.BaseRaces.DeepHalflingId, 19)]
        [TestCase(RaceConstants.BaseRaces.TallfellowHalflingId, 20)]
        [TestCase(RaceConstants.BaseRaces.GoblinId, 98)]
        [TestCase(RaceConstants.BaseRaces.TieflingId, 99)]
        [TestCase(EmptyContent, 100)]
        public override void Percentile(String content, Int32 roll)
        {
            base.Percentile(content, roll);
        }

        [TestCase(RaceConstants.BaseRaces.HalfElfId, 3, 17)]
        [TestCase(RaceConstants.BaseRaces.HalfOrcId, 21, 22)]
        [TestCase(RaceConstants.BaseRaces.HumanId, 23, 97)]
        public override void Percentile(String content, Int32 lower, Int32 upper)
        {
            base.Percentile(content, lower, upper);
        }
    }
}