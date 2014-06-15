using System;
using NPCGen.Common.Races;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Tables.Races.Metaraces.Good
{
    [TestFixture]
    public class GoodBarbarianMetaracesTests : PercentileTests
    {
        protected override String tableName
        {
            get { return "GoodBarbarianMetaraces"; }
        }

        [Test]
        public void GoodBarbarianEmptyPercentile()
        {
            AssertPercentile(EmptyContent, 1, 98);
        }

        [Test]
        public void GoodBarbarianHalfCelestialPercentile()
        {
            AssertPercentile(RaceConstants.Metaraces.HalfCelestial, 99);
        }

        [Test]
        public void GoodBarbarianHalfDragonPercentile()
        {
            AssertPercentile(RaceConstants.Metaraces.HalfDragon, 100);
        }
    }
}