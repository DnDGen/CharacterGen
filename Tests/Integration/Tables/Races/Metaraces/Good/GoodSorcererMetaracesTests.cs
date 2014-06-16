using System;
using NPCGen.Common.Races;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Tables.Races.Metaraces.Good
{
    [TestFixture]
    public class GoodSorcererMetaracesTests : PercentileTests
    {
        protected override String tableName
        {
            get { return "GoodSorcererMetaraces"; }
        }

        [Test]
        public void GoodSorcererEmptyPercentile()
        {
            AssertPercentile(EmptyContent, 1, 96);
        }

        [Test]
        public void GoodSorcererHalfCelestialPercentile()
        {
            AssertPercentile(RaceConstants.Metaraces.HalfCelestial, 97);
        }

        [Test]
        public void GoodSorcererHalfDragonPercentile()
        {
            AssertPercentile(RaceConstants.Metaraces.HalfDragon, 98, 99);
        }

        [Test]
        public void GoodSorcererWerebearPercentile()
        {
            AssertPercentile(RaceConstants.Metaraces.Werebear, 100);
        }
    }
}