using System;
using NPCGen.Common.Races;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Tables.Races.Metaraces.Good
{
    [TestFixture]
    public class GoodDruidMetaracesTests : PercentileTests
    {
        protected override String tableName
        {
            get { return "GoodDruidMetaraces"; }
        }

        [Test]
        public void GoodDruidEmptyPercentile()
        {
            AssertPercentile(EmptyContent, 1, 99);
        }

        [Test]
        public void GoodDruidHalfCelestialPercentile()
        {
            AssertPercentile(RaceConstants.Metaraces.HalfCelestial, 100);
        }
    }
}
