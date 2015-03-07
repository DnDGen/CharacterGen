using System;
using NPCGen.Common.Alignments;
using NPCGen.Common.CharacterClasses;
using NPCGen.Common.Races;
using NPCGen.Tables.Interfaces;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Tables.Races.BaseRaces.Neutral
{
    [TestFixture]
    public class NeutralRangerBaseRacesTests : PercentileTests
    {
        protected override String tableName
        {
            get { return String.Format(TableNameConstants.Formattable.Percentile.GOODNESSCLASSBaseRaces, AlignmentConstants.Neutral, CharacterClassConstants.Ranger); }
        }

        [Test]
        public override void TableIsComplete()
        {
            AssertTableIsComplete();
        }

        [TestCase(RaceConstants.BaseRaces.HighElfId, 2, 6)]
        [TestCase(RaceConstants.BaseRaces.WoodElfId, 8, 36)]
        [TestCase(RaceConstants.BaseRaces.HalfElfId, 39, 55)]
        [TestCase(RaceConstants.BaseRaces.HalfOrcId, 58, 67)]
        [TestCase(RaceConstants.BaseRaces.HumanId, 68, 96)]
        [TestCase(RaceConstants.BaseRaces.LizardfolkId, 97, 98)]
        [TestCase(EmptyContent, 99, 100)]
        public override void Percentile(String content, Int32 lower, Int32 upper)
        {
            base.Percentile(content, lower, upper);
        }

        [TestCase(RaceConstants.BaseRaces.HillDwarfId, 1)]
        [TestCase(RaceConstants.BaseRaces.WildElfId, 7)]
        [TestCase(RaceConstants.BaseRaces.ForestGnomeId, 37)]
        [TestCase(RaceConstants.BaseRaces.RockGnomeId, 38)]
        [TestCase(RaceConstants.BaseRaces.LightfootHalflingId, 56)]
        [TestCase(RaceConstants.BaseRaces.TallfellowHalflingId, 57)]
        public override void Percentile(String content, Int32 roll)
        {
            base.Percentile(content, roll);
        }
    }
}