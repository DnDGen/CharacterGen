using System;
using NPCGen.Common.Races;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Tables.Races.Metaraces.Good
{
    [TestFixture]
    public class GoodFighterMetaracesTests : PercentileTests
    {
        protected override String tableName
        {
            get { return "GoodFighterMetaraces"; }
        }

        [Test]
        public void GoodFighterEmptyPercentile()
        {
            AssertPercentile(EmptyContent, 1, 98);
        }

        [Test]
        public void GoodFighterHalfCelestialPercentile()
        {
            AssertPercentile(RaceConstants.Metaraces.HalfCelestial, 99);
        }

        [Test]
        public void GoodFighterHalfDragonPercentile()
        {
            AssertPercentile(RaceConstants.Metaraces.HalfDragon, 100);
        }
    }
}