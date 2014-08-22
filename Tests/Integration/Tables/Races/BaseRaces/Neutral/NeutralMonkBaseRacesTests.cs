using System;
using NPCGen.Common.Races;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Tables.Races.BaseRaces.Neutral
{
    [TestFixture]
    public class NeutralMonkBaseRacesTests : PercentileTests
    {
        protected override String tableName
        {
            get { return "NeutralMonkBaseRaces"; }
        }

        [TestCase(RaceConstants.BaseRaces.HighElf, 1, 2)]
        [TestCase(RaceConstants.BaseRaces.HalfElf, 4, 13)]
        [TestCase(RaceConstants.BaseRaces.HalfOrc, 16, 25)]
        [TestCase(RaceConstants.BaseRaces.Human, 26, 100)]
        public override void Percentile(String content, Int32 lower, Int32 upper)
        {
            base.Percentile(content, lower, upper);
        }

        [TestCase(RaceConstants.BaseRaces.WoodElf, 3)]
        [TestCase(RaceConstants.BaseRaces.LightfootHalfling, 14)]
        [TestCase(RaceConstants.BaseRaces.DeepHalfling, 15)]
        public override void Percentile(String content, Int32 roll)
        {
            base.Percentile(content, roll);
        }
    }
}