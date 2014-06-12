using NPCGen.Core.Data.Races;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Tables.Races.Metaraces.Good
{
    [TestFixture]
    public class GoodPaladinMetaracesTests : PercentileTests
    {
        [SetUp]
        public void Setup()
        {
            tableName = "GoodPaladinMetaraces";
        }

        [Test]
        public void GoodPaladinEmptyPercentile()
        {
            AssertEmpty(1, 97);
        }

        [Test]
        public void GoodPaladinHalfCelestialPercentile()
        {
            AssertContent(RaceConstants.Metaraces.HalfCelestial, 98);
        }

        [Test]
        public void GoodPaladinHalfDragonPercentile()
        {
            AssertContent(RaceConstants.Metaraces.HalfDragon, 99);
        }

        [Test]
        public void GoodPaladinWerebearPercentile()
        {
            AssertContent(RaceConstants.Metaraces.Werebear, 100);
        }
    }
}
