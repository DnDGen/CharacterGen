using System;
using NPCGen.Common.Races;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Tables.Races.BaseRaces.Neutral
{
    [TestFixture]
    public class NeutralSorcererBaseRacesTests : PercentileTests
    {
        protected override String tableName
        {
            get { return "NeutralSorcererBaseRaces"; }
        }

        [TestCase(RaceConstants.BaseRaces.WildElf, 3, 12)]
        [TestCase(RaceConstants.BaseRaces.WoodElf, 13, 15)]
        [TestCase(RaceConstants.BaseRaces.HalfElf, 17, 31)]
        [TestCase(RaceConstants.BaseRaces.LightfootHalfling, 32, 41)]
        [TestCase(RaceConstants.BaseRaces.HalfOrc, 44, 48)]
        [TestCase(RaceConstants.BaseRaces.Human, 49, 95)]
        [TestCase(RaceConstants.BaseRaces.Lizardfolk, 96, 97)]
        [TestCase(EmptyContent, 99, 100)]
        public override void Percentile(String content, Int32 lower, Int32 upper)
        {
            base.Percentile(content, lower, upper);
        }

        [TestCase(RaceConstants.BaseRaces.HillDwarf, 1)]
        [TestCase(RaceConstants.BaseRaces.HighElf, 2)]
        [TestCase(RaceConstants.BaseRaces.RockGnome, 16)]
        [TestCase(RaceConstants.BaseRaces.DeepHalfling, 42)]
        [TestCase(RaceConstants.BaseRaces.TallfellowHalfling, 43)]
        [TestCase(RaceConstants.BaseRaces.Doppelganger, 98)]
        public override void Percentile(String content, Int32 roll)
        {
            base.Percentile(content, roll);
        }
    }
}