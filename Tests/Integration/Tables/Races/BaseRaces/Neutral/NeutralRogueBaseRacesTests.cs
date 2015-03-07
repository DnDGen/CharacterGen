using System;
using NPCGen.Common.Alignments;
using NPCGen.Common.CharacterClasses;
using NPCGen.Common.Races;
using NPCGen.Tables.Interfaces;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Tables.Races.BaseRaces.Neutral
{
    [TestFixture]
    public class NeutralRogueBaseRacesTests : PercentileTests
    {
        protected override String tableName
        {
            get { return String.Format(TableNameConstants.Formattable.Percentile.GOODNESSCLASSBaseRaces, AlignmentConstants.Neutral, CharacterClassConstants.Rogue); }
        }

        [Test]
        public override void TableIsComplete()
        {
            AssertTableIsComplete();
        }

        [TestCase(RaceConstants.BaseRaces.HillDwarfId, 2, 4)]
        [TestCase(RaceConstants.BaseRaces.HighElfId, 5, 8)]
        [TestCase(RaceConstants.BaseRaces.HalfElfId, 11, 25)]
        [TestCase(RaceConstants.BaseRaces.LightfootHalflingId, 26, 53)]
        [TestCase(RaceConstants.BaseRaces.DeepHalflingId, 54, 58)]
        [TestCase(RaceConstants.BaseRaces.TallfellowHalflingId, 59, 63)]
        [TestCase(RaceConstants.BaseRaces.HalfOrcId, 64, 73)]
        [TestCase(RaceConstants.BaseRaces.HumanId, 74, 97)]
        [TestCase(EmptyContent, 99, 100)]
        public override void Percentile(String content, Int32 lower, Int32 upper)
        {
            base.Percentile(content, lower, upper);
        }

        [TestCase(RaceConstants.BaseRaces.DeepDwarfId, 1)]
        [TestCase(RaceConstants.BaseRaces.WoodElfId, 9)]
        [TestCase(RaceConstants.BaseRaces.RockGnomeId, 10)]
        [TestCase(RaceConstants.BaseRaces.DoppelgangerId, 98)]
        public override void Percentile(String content, Int32 roll)
        {
            base.Percentile(content, roll);
        }
    }
}