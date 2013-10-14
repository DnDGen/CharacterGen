using NPCGen.Core.Data.Races;
using NUnit.Framework;

namespace NPCGen.Tests.Generation.Xml.Data.Races.Metaraces.Good
{
    [TestFixture]
    public class GoodFighterMetaracesTests : PercentileTests
    {
        [SetUp]
        public void Setup()
        {
            tableName = "GoodFighterMetaraces";
        }

        [Test]
        public void GoodFighterEmptyPercentile()
        {
            AssertEmpty(1, 98);
        }

        [Test]
        public void GoodFighterHalfCelestialPercentile()
        {
            AssertContent(RaceConstants.Metaraces.HalfCelestial, 99);
        }

        [Test]
        public void GoodFighterHalfDragonPercentile()
        {
            AssertContent(RaceConstants.Metaraces.HalfDragon, 100);
        }
    }
}