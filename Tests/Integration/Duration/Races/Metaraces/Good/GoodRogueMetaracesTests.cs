using NPCGen.Core.Data.Races;
using NUnit.Framework;

namespace NPCGen.Tests.Unit.Generation.Xml.Data.Races.Metaraces.Good
{
    [TestFixture]
    public class GoodRogueMetaracesTests : PercentileTests
    {
        [SetUp]
        public void Setup()
        {
            tableName = "GoodRogueMetaraces";
        }

        [Test]
        public void GoodRogueEmptyPercentile()
        {
            AssertEmpty(1, 97);
        }

        [Test]
        public void GoodRogueHalfCelestialPercentile()
        {
            AssertContent(RaceConstants.Metaraces.HalfCelestial, 98);
        }

        [Test]
        public void GoodRogueHalfDragonPercentile()
        {
            AssertContent(RaceConstants.Metaraces.HalfDragon, 99);
        }

        [Test]
        public void GoodRogueWerebearPercentile()
        {
            AssertContent(RaceConstants.Metaraces.Werebear, 100);
        }
    }
}
