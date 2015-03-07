using System;
using NPCGen.Common.Alignments;
using NPCGen.Common.CharacterClasses;
using NPCGen.Common.Races;
using NPCGen.Tables.Interfaces;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Tables.Races.BaseRaces.Neutral
{
    [TestFixture]
    public class NeutralBarbarianBaseRacesTests : PercentileTests
    {
        protected override String tableName
        {
            get { return String.Format(TableNameConstants.Formattable.Percentile.GOODNESSCLASSBaseRaces, AlignmentConstants.Neutral, CharacterClassConstants.Barbarian); }
        }

        [Test]
        public override void TableIsComplete()
        {
            AssertTableIsComplete();
        }

        [TestCase(RaceConstants.BaseRaces.WildElfId, 3, 13)]
        [TestCase(RaceConstants.BaseRaces.HalfElfId, 15, 16)]
        [TestCase(RaceConstants.BaseRaces.LightfootHalflingId, 17, 18)]
        [TestCase(RaceConstants.BaseRaces.HalfOrcId, 20, 58)]
        [TestCase(RaceConstants.BaseRaces.HumanId, 59, 87)]
        [TestCase(RaceConstants.BaseRaces.LizardfolkId, 88, 98)]
        [TestCase(EmptyContent, 99, 100)]
        public override void Percentile(String content, Int32 lower, Int32 upper)
        {
            base.Percentile(content, lower, upper);
        }

        [TestCase(RaceConstants.BaseRaces.DeepDwarfId, 1)]
        [TestCase(RaceConstants.BaseRaces.HillDwarfId, 2)]
        [TestCase(RaceConstants.BaseRaces.WoodElfId, 14)]
        [TestCase(RaceConstants.BaseRaces.DeepHalflingId, 19)]
        public override void Percentile(String content, Int32 roll)
        {
            base.Percentile(content, roll);
        }
    }
}