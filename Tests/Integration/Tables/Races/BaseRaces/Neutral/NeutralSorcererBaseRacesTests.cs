using System;
using NPCGen.Common.Alignments;
using NPCGen.Common.CharacterClasses;
using NPCGen.Common.Races;
using NPCGen.Tables.Interfaces;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Tables.Races.BaseRaces.Neutral
{
    [TestFixture]
    public class NeutralSorcererBaseRacesTests : PercentileTests
    {
        protected override String tableName
        {
            get { return String.Format(TableNameConstants.Formattable.Percentile.GOODNESSCLASSBaseRaces, AlignmentConstants.Neutral, CharacterClassConstants.Sorcerer); }
        }

        [Test]
        public override void TableIsComplete()
        {
            AssertTableIsComplete();
        }

        [TestCase(RaceConstants.BaseRaces.WildElfId, 3, 12)]
        [TestCase(RaceConstants.BaseRaces.WoodElfId, 13, 15)]
        [TestCase(RaceConstants.BaseRaces.HalfElfId, 17, 31)]
        [TestCase(RaceConstants.BaseRaces.LightfootHalflingId, 32, 41)]
        [TestCase(RaceConstants.BaseRaces.HalfOrcId, 44, 48)]
        [TestCase(RaceConstants.BaseRaces.HumanId, 49, 95)]
        [TestCase(RaceConstants.BaseRaces.LizardfolkId, 96, 97)]
        [TestCase(EmptyContent, 99, 100)]
        public override void Percentile(String content, Int32 lower, Int32 upper)
        {
            base.Percentile(content, lower, upper);
        }

        [TestCase(RaceConstants.BaseRaces.HillDwarfId, 1)]
        [TestCase(RaceConstants.BaseRaces.HighElfId, 2)]
        [TestCase(RaceConstants.BaseRaces.RockGnomeId, 16)]
        [TestCase(RaceConstants.BaseRaces.DeepHalflingId, 42)]
        [TestCase(RaceConstants.BaseRaces.TallfellowHalflingId, 43)]
        [TestCase(RaceConstants.BaseRaces.DoppelgangerId, 98)]
        public override void Percentile(String content, Int32 roll)
        {
            base.Percentile(content, roll);
        }
    }
}