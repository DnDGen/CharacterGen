using System;
using NPCGen.Common.Races;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Tables.Races.Metaraces.Good
{
    [TestFixture]
    public class GoodRangerMetaracesTests : PercentileTests
    {
        protected override String tableName
        {
            get { return "GoodRangerMetaraces"; }
        }

        [Test]
        public void GoodRangerEmptyPercentile()
        {
            AssertPercentile(EmptyContent, 1, 97);
        }

        [Test]
        public void GoodRangerHalfCelestialPercentile()
        {
            AssertPercentile(RaceConstants.Metaraces.HalfCelestial, 98);
        }

        [Test]
        public void GoodRangerHalfDragonPercentile()
        {
            AssertPercentile(RaceConstants.Metaraces.HalfDragon, 99);
        }

        [Test]
        public void GoodRangerWerebearPercentile()
        {
            AssertPercentile(RaceConstants.Metaraces.Werebear, 100);
        }
    }
}
