using NPCGen.Core.Data.Races;
using NUnit.Framework;

namespace NPCGen.Tests.Unit.Generation.Xml.Data.Races.Metaraces.Good
{
    [TestFixture]
    public class GoodClericMetaracesTests : PercentileTests
    {
        [SetUp]
        public void Setup()
        {
            tableName = "GoodClericMetaraces";
        }

        [Test]
        public void GoodClericEmptyPercentile()
        {
            AssertEmpty(1, 96);
        }

        [Test]
        public void GoodClericHalfCelestialPercentile()
        {
            AssertContent(RaceConstants.Metaraces.HalfCelestial, 97, 98);
        }

        [Test]
        public void GoodClericHalfDragonPercentile()
        {
            AssertContent(RaceConstants.Metaraces.HalfDragon, 99);
        }

        [Test]
        public void GoodClericWerebearPercentile()
        {
            AssertContent(RaceConstants.Metaraces.Werebear, 100);
        }
    }
}
