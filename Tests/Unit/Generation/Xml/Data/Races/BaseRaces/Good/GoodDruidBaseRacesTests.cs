using NPCGen.Core.Data.Races;
using NUnit.Framework;

namespace NPCGen.Tests.Unit.Generation.Xml.Data.Races.BaseRaces.Good
{
    [TestFixture]
    public class GoodDruidBaseRacesTests : PercentileTests
    {
        [SetUp]
        public void Setup()
        {
            tableName = "GoodDruidBaseRaces";
        }

        [Test]
        public void GoodDruidGrayElfPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.GrayElf, 1);
        }

        [Test]
        public void GoodDruidHighElfPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.HighElf, 2, 11);
        }

        [Test]
        public void GoodDruidWildElfPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.WildElf, 12, 21);
        }

        [Test]
        public void GoodDruidWoodElfPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.WoodElf, 22, 31);
        }

        [Test]
        public void GoodDruidForestGnomePercentile()
        {
            AssertContent(RaceConstants.BaseRaces.ForestGnome, 32, 36);
        }

        [Test]
        public void GoodDruidRockGnomePercentile()
        {
            AssertContent(RaceConstants.BaseRaces.RockGnome, 37);
        }

        [Test]
        public void GoodDruidHalfElfPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.HalfElf, 38, 46);
        }

        [Test]
        public void GoodDruidLightfootHalflingPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.LightfootHalfling, 47);
        }

        [Test]
        public void GoodDruidTallfellowHalflingPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.TallfellowHalfling, 48);
        }

        [Test]
        public void GoodDruidHalfOrcPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.HalfOrc, 49);
        }

        [Test]
        public void GoodDruidHumanPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.Human, 50, 99);
        }

        [Test]
        public void GoodDruidEmptyPercentile()
        {
            AssertEmpty(100);
        }
    }
}