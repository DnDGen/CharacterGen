using System;
using NPCGen.Common.Races;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Tables.Races.Metaraces.Good
{
    [TestFixture]
    public class GoodRogueMetaracesTests : PercentileTests
    {
        protected override String tableName
        {
            get { return "GoodRogueMetaraces"; }
        }

        [Test]
        public void GoodRogueEmptyPercentile()
        {
            AssertPercentile(EmptyContent, 1, 97);
        }

        [Test]
        public void GoodRogueHalfCelestialPercentile()
        {
            AssertPercentile(RaceConstants.Metaraces.HalfCelestial, 98);
        }

        [Test]
        public void GoodRogueHalfDragonPercentile()
        {
            AssertPercentile(RaceConstants.Metaraces.HalfDragon, 99);
        }

        [Test]
        public void GoodRogueWerebearPercentile()
        {
            AssertPercentile(RaceConstants.Metaraces.Werebear, 100);
        }
    }
}
