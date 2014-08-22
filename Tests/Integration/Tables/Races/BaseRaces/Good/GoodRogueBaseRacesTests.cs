using System;
using NPCGen.Common.Races;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Tables.Races.BaseRaces.Good
{
    [TestFixture]
    public class GoodRogueBaseRacesTests : PercentileTests
    {
        protected override String tableName
        {
            get { return "GoodRogueBaseRaces"; }
        }

        [TestCase(RaceConstants.BaseRaces.HillDwarf, 1, 5)]
        [TestCase(RaceConstants.BaseRaces.HighElf, 7, 19)]
        [TestCase(RaceConstants.BaseRaces.RockGnome, 21, 25)]
        [TestCase(RaceConstants.BaseRaces.HalfElf, 26, 35)]
        [TestCase(RaceConstants.BaseRaces.LightfootHalfling, 36, 60)]
        [TestCase(RaceConstants.BaseRaces.DeepHalfling, 61, 66)]
        [TestCase(RaceConstants.BaseRaces.TallfellowHalfling, 67, 72)]
        [TestCase(RaceConstants.BaseRaces.HalfOrc, 73, 77)]
        [TestCase(RaceConstants.BaseRaces.Human, 78, 96)]
        [TestCase(EmptyContent, 98, 100)]
        public override void Percentile(String content, Int32 lower, Int32 upper)
        {
            base.Percentile(content, lower, upper);
        }

        [TestCase(RaceConstants.BaseRaces.MountainDwarf, 6)]
        [TestCase(RaceConstants.BaseRaces.ForestGnome, 20)]
        [TestCase(RaceConstants.BaseRaces.Svirfneblin, 97)]
        public override void Percentile(String content, Int32 roll)
        {
            base.Percentile(content, roll);
        }
    }
}