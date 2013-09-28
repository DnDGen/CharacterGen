using NPCGen.Core.Data.Races;
using NUnit.Framework;

namespace NPCGen.Tests.Generation.Xml.Data.Races.BaseRaces.Good
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
            AssertContentIsInRange(RaceConstants.BaseRaces.DeepDwarf, 1, 3);
        }

        [Test]
        public void GoodFighterHillDwarfPercentile()
        {
            AssertContentIsInRange(RaceConstants.BaseRaces.HillDwarf, 4, 33);
        }

        [Test]
        public void GoodFighterMountainDwarfPercentile()
        {
            AssertContentIsInRange(RaceConstants.BaseRaces.MountainDwarf, 34, 41);
        }

        [Test]
        public void GoodFighterGrayElfPercentile()
        {
            AssertContentOnSingleRoll(RaceConstants.BaseRaces.GrayElf, 42);
        }

        [Test]
        public void GoodFighterHighElfPercentile()
        {
            AssertContentIsInRange(RaceConstants.BaseRaces.HighElf, 43, 47);
        }

        [Test]
        public void GoodFighterRockGnomePercentile()
        {
            AssertContentOnSingleRoll(RaceConstants.BaseRaces.RockGnome, 48);
        }

        [Test]
        public void GoodFighterHalfElfPercentile()
        {
            AssertContentIsInRange(RaceConstants.BaseRaces.HalfElf, 49, 50);
        }

        [Test]
        public void GoodFighterLightfootHalflingPercentile()
        {
            AssertContentOnSingleRoll(RaceConstants.BaseRaces.LightfootHalfling, 51);
        }

        [Test]
        public void GoodFighterDeepHalflingPercentile()
        {
            AssertContentOnSingleRoll(RaceConstants.BaseRaces.DeepHalfling, 52);
        }

        [Test]
        public void GoodFighterHalfOrcPercentile()
        {
            AssertContentIsInRange(RaceConstants.BaseRaces.HalfOrc, 53, 57);
        }

        [Test]
        public void GoodFighterHumanPercentile()
        {
            AssertContentIsInRange(RaceConstants.BaseRaces.Human, 58, 97);
        }

        [Test]
        public void GoodFighterEmptyPercentile()
        {
            AssertEmpty(98, 100);
        }
    }
}