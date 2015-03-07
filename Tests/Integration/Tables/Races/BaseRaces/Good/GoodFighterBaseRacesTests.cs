using System;
using NPCGen.Common.Alignments;
using NPCGen.Common.CharacterClasses;
using NPCGen.Common.Races;
using NPCGen.Tables.Interfaces;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Tables.Races.BaseRaces.Good
{
    [TestFixture]
    public class GoodFighterBaseRacesTests : PercentileTests
    {
        protected override String tableName
        {
            get { return String.Format(TableNameConstants.Formattable.Percentile.GOODNESSCLASSBaseRaces, AlignmentConstants.Good, CharacterClassConstants.Fighter); }
        }

        [Test]
        public override void TableIsComplete()
        {
            AssertTableIsComplete();
        }

        [TestCase(RaceConstants.BaseRaces.DeepDwarfId, 1, 3)]
        [TestCase(RaceConstants.BaseRaces.HillDwarfId, 4, 33)]
        [TestCase(RaceConstants.BaseRaces.MountainDwarfId, 34, 41)]
        [TestCase(RaceConstants.BaseRaces.HighElfId, 43, 47)]
        [TestCase(RaceConstants.BaseRaces.HalfElfId, 49, 50)]
        [TestCase(RaceConstants.BaseRaces.HalfOrcId, 53, 57)]
        [TestCase(RaceConstants.BaseRaces.HumanId, 58, 97)]
        [TestCase(EmptyContent, 98, 100)]
        public override void Percentile(String content, Int32 lower, Int32 upper)
        {
            base.Percentile(content, lower, upper);
        }

        [TestCase(RaceConstants.BaseRaces.GrayElfId, 42)]
        [TestCase(RaceConstants.BaseRaces.RockGnomeId, 48)]
        [TestCase(RaceConstants.BaseRaces.LightfootHalflingId, 51)]
        [TestCase(RaceConstants.BaseRaces.DeepHalflingId, 52)]
        public override void Percentile(String content, Int32 roll)
        {
            base.Percentile(content, roll);
        }
    }
}