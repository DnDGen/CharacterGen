using NPCGen.Core.Data.Races;
using NUnit.Framework;

namespace NPCGen.Tests.Unit.Generation.Xml.Data.Races.Metaraces.Good
{
    [TestFixture]
    public class GoodSorcererMetaracesTests : PercentileTests
    {
        [SetUp]
        public void Setup()
        {
            tableName = "GoodSorcererMetaraces";
        }

        [Test]
        public void GoodSorcererEmptyPercentile()
        {
            AssertEmpty(1, 96);
        }

        [Test]
        public void GoodSorcererHalfCelestialPercentile()
        {
            AssertContent(RaceConstants.Metaraces.HalfCelestial, 97);
        }

        [Test]
        public void GoodSorcererHalfDragonPercentile()
        {
            AssertContent(RaceConstants.Metaraces.HalfDragon, 98, 99);
        }

        [Test]
        public void GoodSorcererWerebearPercentile()
        {
            AssertContent(RaceConstants.Metaraces.Werebear, 100);
        }
    }
}