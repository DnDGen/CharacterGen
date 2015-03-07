using System;
using NPCGen.Common.Alignments;
using NPCGen.Common.CharacterClasses;
using NPCGen.Common.Races;
using NPCGen.Tables.Interfaces;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Tables.Races.BaseRaces.Neutral
{
    [TestFixture]
    public class NeutralDruidBaseRacesTests : PercentileTests
    {
        protected override String tableName
        {
            get { return String.Format(TableNameConstants.Formattable.Percentile.GOODNESSCLASSBaseRaces, AlignmentConstants.Neutral, CharacterClassConstants.Druid); }
        }

        [Test]
        public override void TableIsComplete()
        {
            AssertTableIsComplete();
        }

        [TestCase(RaceConstants.BaseRaces.HighElfId, 2, 6)]
        [TestCase(RaceConstants.BaseRaces.WildElfId, 7, 11)]
        [TestCase(RaceConstants.BaseRaces.WoodElfId, 12, 31)]
        [TestCase(RaceConstants.BaseRaces.HalfElfId, 33, 37)]
        [TestCase(RaceConstants.BaseRaces.HumanId, 41, 88)]
        [TestCase(RaceConstants.BaseRaces.LizardfolkId, 89, 98)]
        [TestCase(EmptyContent, 99, 100)]
        public override void Percentile(String content, Int32 lower, Int32 upper)
        {
            base.Percentile(content, lower, upper);
        }

        [TestCase(RaceConstants.BaseRaces.GrayElfId, 1)]
        [TestCase(RaceConstants.BaseRaces.ForestGnomeId, 32)]
        [TestCase(RaceConstants.BaseRaces.LightfootHalflingId, 38)]
        [TestCase(RaceConstants.BaseRaces.TallfellowHalflingId, 39)]
        [TestCase(RaceConstants.BaseRaces.HalfOrcId, 40)]
        public override void Percentile(String content, Int32 roll)
        {
            base.Percentile(content, roll);
        }
    }
}