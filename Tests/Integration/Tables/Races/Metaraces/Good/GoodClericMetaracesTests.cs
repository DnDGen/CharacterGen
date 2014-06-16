using System;
using NPCGen.Common.Races;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Tables.Races.Metaraces.Good
{
    [TestFixture]
    public class GoodClericMetaracesTests : PercentileTests
    {
        protected override String tableName
        {
            get { return "GoodClericMetaraces"; }
        }

        [Test]
        public void GoodClericEmptyPercentile()
        {
            AssertPercentile(EmptyContent, 1, 96);
        }

        [Test]
        public void GoodClericHalfCelestialPercentile()
        {
            AssertPercentile(RaceConstants.Metaraces.HalfCelestial, 97, 98);
        }

        [Test]
        public void GoodClericHalfDragonPercentile()
        {
            AssertPercentile(RaceConstants.Metaraces.HalfDragon, 99);
        }

        [Test]
        public void GoodClericWerebearPercentile()
        {
            AssertPercentile(RaceConstants.Metaraces.Werebear, 100);
        }
    }
}
