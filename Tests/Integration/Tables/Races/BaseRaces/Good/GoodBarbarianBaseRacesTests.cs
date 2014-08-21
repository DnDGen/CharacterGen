using System;
using NPCGen.Common.Races;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Tables.Races.BaseRaces.Good
{
    [TestFixture]
    public class GoodBarbarianBaseRacesTests : PercentileTests
    {
        protected override String tableName
        {
            get { return "GoodBarbarianBaseRaces"; }
        }

        [TestCase(RaceConstants.BaseRaces.HillDwarf, 1, 2)]
        [TestCase(RaceConstants.BaseRaces.WildElf, 3, 32)]
        [TestCase(RaceConstants.BaseRaces.WoodElf, 33, 34)]
        [TestCase(RaceConstants.BaseRaces.HalfOrc, 37, 61)]
        [TestCase(RaceConstants.BaseRaces.Human, 62, 98)]
        [TestCase(EmptyContent, 99, 100)]
        public override void Percentile(String content, Int32 lower, Int32 upper)
        {
            base.Percentile(content, lower, upper);
        }

        [TestCase(RaceConstants.BaseRaces.HalfElf, 35)]
        [TestCase(RaceConstants.BaseRaces.LightfootHalfling, 36)]
        public override void Percentile(String content, Int32 roll)
        {
            base.Percentile(content, roll);
        }
    }
}