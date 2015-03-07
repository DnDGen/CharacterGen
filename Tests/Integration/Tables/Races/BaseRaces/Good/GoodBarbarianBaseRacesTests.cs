using System;
using NPCGen.Common.Alignments;
using NPCGen.Common.CharacterClasses;
using NPCGen.Common.Races;
using NPCGen.Tables.Interfaces;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Tables.Races.BaseRaces.Good
{
    [TestFixture]
    public class GoodBarbarianBaseRacesTests : PercentileTests
    {
        protected override String tableName
        {
            get { return String.Format(TableNameConstants.Formattable.Percentile.GOODNESSCLASSBaseRaces, AlignmentConstants.Good, CharacterClassConstants.Barbarian); }
        }

        [Test]
        public override void TableIsComplete()
        {
            AssertTableIsComplete();
        }

        [TestCase(RaceConstants.BaseRaces.HillDwarfId, 1, 2)]
        [TestCase(RaceConstants.BaseRaces.WildElfId, 3, 32)]
        [TestCase(RaceConstants.BaseRaces.WoodElfId, 33, 34)]
        [TestCase(RaceConstants.BaseRaces.HalfOrcId, 37, 61)]
        [TestCase(RaceConstants.BaseRaces.HumanId, 62, 98)]
        [TestCase(EmptyContent, 99, 100)]
        public override void Percentile(String content, Int32 lower, Int32 upper)
        {
            base.Percentile(content, lower, upper);
        }

        [TestCase(RaceConstants.BaseRaces.HalfElfId, 35)]
        [TestCase(RaceConstants.BaseRaces.LightfootHalflingId, 36)]
        public override void Percentile(String content, Int32 roll)
        {
            base.Percentile(content, roll);
        }
    }
}