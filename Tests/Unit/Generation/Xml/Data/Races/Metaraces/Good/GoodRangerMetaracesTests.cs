using NPCGen.Core.Data.Races;
using NUnit.Framework;

namespace NPCGen.Tests.Unit.Generation.Xml.Data.Races.Metaraces.Good
{
    [TestFixture]
    public class GoodRangerMetaracesTests : PercentileTests
    {
        [SetUp]
        public void Setup()
        {
            tableName = "GoodRangerMetaraces";
        }

        [Test]
        public void GoodRangerEmptyPercentile()
        {
            AssertEmpty(1, 97);
        }

        [Test]
        public void GoodRangerHalfCelestialPercentile()
        {
            AssertContent(RaceConstants.Metaraces.HalfCelestial, 98);
        }

        [Test]
        public void GoodRangerHalfDragonPercentile()
        {
            AssertContent(RaceConstants.Metaraces.HalfDragon, 99);
        }

        [Test]
        public void GoodRangerWerebearPercentile()
        {
            AssertContent(RaceConstants.Metaraces.Werebear, 100);
        }
    }
}
