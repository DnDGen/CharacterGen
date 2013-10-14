using NPCGen.Core.Data.Races;
using NUnit.Framework;

namespace NPCGen.Tests.Generation.Xml.Data.Races.Metaraces.Good
{
    [TestFixture]
    public class GoodMonkMetaracesTests : PercentileTests
    {
        [SetUp]
        public void Setup()
        {
            tableName = "GoodMonkMetaraces";
        }

        [Test]
        public void GoodMonkEmptyPercentile()
        {
            AssertEmpty(1, 97);
        }

        [Test]
        public void GoodMonkHalfCelestialPercentile()
        {
            AssertContent(RaceConstants.Metaraces.HalfCelestial, 98);
        }

        [Test]
        public void GoodMonkHalfDragonPercentile()
        {
            AssertContent(RaceConstants.Metaraces.HalfDragon, 99);
        }

        [Test]
        public void GoodMonkWerebearPercentile()
        {
            AssertContent(RaceConstants.Metaraces.Werebear, 100);
        }
    }
}
