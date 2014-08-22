using System;
using NPCGen.Common.Races;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Tables.Races.BaseRaces.Good
{
    [TestFixture]
    public class GoodMonkBaseRacesTests : PercentileTests
    {
        protected override String tableName
        {
            get { return "GoodMonkBaseRaces"; }
        }

        [TestCase(RaceConstants.BaseRaces.Aasimar, 1, 2)]
        [TestCase(RaceConstants.BaseRaces.HighElf, 4, 13)]
        [TestCase(RaceConstants.BaseRaces.HalfElf, 14, 18)]
        [TestCase(RaceConstants.BaseRaces.HalfOrc, 21, 25)]
        [TestCase(RaceConstants.BaseRaces.Human, 26, 97)]
        [TestCase(EmptyContent, 98, 100)]
        public override void Percentile(String content, Int32 lower, Int32 upper)
        {
            base.Percentile(content, lower, upper);
        }

        [TestCase(RaceConstants.BaseRaces.HillDwarf, 3)]
        [TestCase(RaceConstants.BaseRaces.LightfootHalfling, 19)]
        [TestCase(RaceConstants.BaseRaces.DeepHalfling, 20)]
        public override void Percentile(String content, Int32 roll)
        {
            base.Percentile(content, roll);
        }
    }
}