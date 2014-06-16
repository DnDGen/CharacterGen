using System;
using NPCGen.Common.Races;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Tables.Races.Metaraces.Good
{
    [TestFixture]
    public class GoodMonkMetaracesTests : PercentileTests
    {
        protected override String tableName
        {
            get { return "GoodMonkMetaraces"; }
        }

        [Test]
        public void GoodMonkEmptyPercentile()
        {
            AssertPercentile(EmptyContent, 1, 97);
        }

        [Test]
        public void GoodMonkHalfCelestialPercentile()
        {
            AssertPercentile(RaceConstants.Metaraces.HalfCelestial, 98);
        }

        [Test]
        public void GoodMonkHalfDragonPercentile()
        {
            AssertPercentile(RaceConstants.Metaraces.HalfDragon, 99);
        }

        [Test]
        public void GoodMonkWerebearPercentile()
        {
            AssertPercentile(RaceConstants.Metaraces.Werebear, 100);
        }
    }
}
