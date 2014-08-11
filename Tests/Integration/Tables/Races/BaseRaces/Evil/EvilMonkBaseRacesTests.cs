using System;
using NPCGen.Common.Races;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Tables.Races.BaseRaces.Evil
{
    [TestFixture]
    public class EvilMonkBaseRacesTests : PercentileTests
    {
        protected override String tableName
        {
            get { return "EvilMonkBaseRaces"; }
        }

        [TestCase(RaceConstants.BaseRaces.HalfElf, 1, 10)]
        [TestCase(RaceConstants.BaseRaces.HalfOrc, 11, 20)]
        [TestCase(RaceConstants.BaseRaces.Human, 21, 90)]
        [TestCase(RaceConstants.BaseRaces.Hobgoblin, 91, 93)]
        [TestCase(RaceConstants.BaseRaces.OgreMage, 95, 96)]
        [TestCase(EmptyContent, 97, 100)]
        public void Percentile(String content, Int32 lower, Int32 upper)
        {
            AssertPercentile(content, lower, upper);
        }

        [Test]
        public void EvilMonkTieflingPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.Tiefling, 94);
        }
    }
}