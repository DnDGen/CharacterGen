using NPCGen.Core.Data.Races;
using NUnit.Framework;

namespace NPCGen.Tests.Unit.Generation.Xml.Data.Races.BaseRaces.Good
{
    [TestFixture]
    public class GoodFighterBaseRacesTests : PercentileTests
    {
        [SetUp]
        public void Setup()
        {
            tableName = "GoodFighterBaseRaces";
        }

        [Test]
        public void GoodFighterDeepDwarfPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.DeepDwarf, 1, 3);
        }

        [Test]
        public void GoodFighterHillDwarfPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.HillDwarf, 4, 33);
        }

        [Test]
        public void GoodFighterMountainDwarfPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.MountainDwarf, 34, 41);
        }

        [Test]
        public void GoodFighterGrayElfPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.GrayElf, 42);
        }

        [Test]
        public void GoodFighterHighElfPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.HighElf, 43, 47);
        }

        [Test]
        public void GoodFighterRockGnomePercentile()
        {
            AssertContent(RaceConstants.BaseRaces.RockGnome, 48);
        }

        [Test]
        public void GoodFighterHalfElfPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.HalfElf, 49, 50);
        }

        [Test]
        public void GoodFighterLightfootHalflingPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.LightfootHalfling, 51);
        }

        [Test]
        public void GoodFighterDeepHalflingPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.DeepHalfling, 52);
        }

        [Test]
        public void GoodFighterHalfOrcPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.HalfOrc, 53, 57);
        }

        [Test]
        public void GoodFighterHumanPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.Human, 58, 97);
        }

        [Test]
        public void GoodFighterEmptyPercentile()
        {
            AssertEmpty(98, 100);
        }
    }
}