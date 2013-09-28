using NPCGen.Core.Data.Races;
using NUnit.Framework;

namespace NPCGen.Tests.Generation.Xml.Data.Races.BaseRaces.Good
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
            AssertContentOnSingleRoll(RaceConstants.BaseRaces.GrayElf, 1);
        }

        [Test]
        public void GoodDruidHighElfPercentile()
        {
            AssertContentIsInRange(RaceConstants.BaseRaces.HighElf, 2, 11);
        }

        [Test]
        public void GoodDruidWildElfPercentile()
        {
            AssertContentIsInRange(RaceConstants.BaseRaces.WildElf, 12, 21);
        }

        [Test]
        public void GoodDruidWoodElfPercentile()
        {
            AssertContentIsInRange(RaceConstants.BaseRaces.WoodElf, 22, 31);
        }

        [Test]
        public void GoodDruidForestGnomePercentile()
        {
            AssertContentIsInRange(RaceConstants.BaseRaces.ForestGnome, 32, 36);
        }

        [Test]
        public void GoodDruidRockGnomePercentile()
        {
            AssertContentOnSingleRoll(RaceConstants.BaseRaces.RockGnome, 37);
        }

        [Test]
        public void GoodDruidHalfElfPercentile()
        {
            AssertContentIsInRange(RaceConstants.BaseRaces.HalfElf, 38, 46);
        }

        [Test]
        public void GoodDruidLightfootHalflingPercentile()
        {
            AssertContentOnSingleRoll(RaceConstants.BaseRaces.LightfootHalfling, 47);
        }

        [Test]
        public void GoodDruidTallfellowHalflingPercentile()
        {
            AssertContentOnSingleRoll(RaceConstants.BaseRaces.TallfellowHalfling, 48);
        }

        [Test]
        public void GoodDruidHalfOrcPercentile()
        {
            AssertContentOnSingleRoll(RaceConstants.BaseRaces.HalfOrc, 49);
        }

        [Test]
        public void GoodDruidHumanPercentile()
        {
            AssertContentIsInRange(RaceConstants.BaseRaces.Human, 50, 99);
        }

        [Test]
        public void GoodDruidEmptyPercentile()
        {
            AssertEmpty(100);
        }
    }
}