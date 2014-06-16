using System;
using NPCGen.Common.Races;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Tables.Races.Metaraces.Good
{
    [TestFixture]
    public class GoodPaladinMetaracesTests : PercentileTests
    {
        protected override String tableName
        {
            get { return "GoodPaladinMetaraces"; }
        }

        [Test]
        public void GoodPaladinEmptyPercentile()
        {
            AssertPercentile(EmptyContent, 1, 97);
        }

        [Test]
        public void GoodPaladinHalfCelestialPercentile()
        {
            AssertPercentile(RaceConstants.Metaraces.HalfCelestial, 98);
        }

        [Test]
        public void GoodPaladinHalfDragonPercentile()
        {
            AssertPercentile(RaceConstants.Metaraces.HalfDragon, 99);
        }

        [Test]
        public void GoodPaladinWerebearPercentile()
        {
            AssertPercentile(RaceConstants.Metaraces.Werebear, 100);
        }
    }
}
