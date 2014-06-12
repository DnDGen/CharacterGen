using NPCGen.Core.Data.Races;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Tables.Races.Metaraces.Good
{
    [TestFixture]
    public class GoodDruidMetaracesTests : PercentileTests
    {
        [SetUp]
        public void Setup()
        {
            tableName = "GoodDruidMetaraces";
        }

        [Test]
        public void GoodDruidEmptyPercentile()
        {
            AssertEmpty(1, 99);
        }

        [Test]
        public void GoodDruidHalfCelestialPercentile()
        {
            AssertContent(RaceConstants.Metaraces.HalfCelestial, 100);
        }
    }
}
