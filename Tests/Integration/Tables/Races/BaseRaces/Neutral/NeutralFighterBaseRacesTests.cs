using System;
using NPCGen.Common.Alignments;
using NPCGen.Common.CharacterClasses;
using NPCGen.Common.Races;
using NPCGen.Tables.Interfaces;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Tables.Races.BaseRaces.Neutral
{
    [TestFixture]
    public class NeutralFighterBaseRacesTests : PercentileTests
    {
        protected override String tableName
        {
            get { return String.Format(TableNameConstants.Formattable.Percentile.GOODNESSCLASSBaseRaces, AlignmentConstants.Neutral, CharacterClassConstants.Fighter); }
        }

        [Test]
        public override void TableIsComplete()
        {
            AssertTableIsComplete();
        }

        [TestCase(RaceConstants.BaseRaces.DeepDwarfId, 1, 10)]
        [TestCase(RaceConstants.BaseRaces.HillDwarfId, 11, 29)]
        [TestCase(RaceConstants.BaseRaces.MountainDwarfId, 30, 34)]
        [TestCase(RaceConstants.BaseRaces.WoodElfId, 36, 41)]
        [TestCase(RaceConstants.BaseRaces.HalfElfId, 42, 46)]
        [TestCase(RaceConstants.BaseRaces.HalfOrcId, 49, 58)]
        [TestCase(RaceConstants.BaseRaces.HumanId, 59, 96)]
        [TestCase(EmptyContent, 99, 100)]
        public override void Percentile(String content, Int32 lower, Int32 upper)
        {
            base.Percentile(content, lower, upper);
        }

        [TestCase(RaceConstants.BaseRaces.HighElfId, 35)]
        [TestCase(RaceConstants.BaseRaces.LightfootHalflingId, 47)]
        [TestCase(RaceConstants.BaseRaces.DeepHalflingId, 48)]
        [TestCase(RaceConstants.BaseRaces.LizardfolkId, 97)]
        [TestCase(RaceConstants.BaseRaces.DoppelgangerId, 98)]
        public override void Percentile(String content, Int32 roll)
        {
            base.Percentile(content, roll);
        }
    }
}