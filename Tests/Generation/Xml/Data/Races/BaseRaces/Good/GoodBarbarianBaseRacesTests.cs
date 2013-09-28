using NPCGen.Core.Data.Races;
using NUnit.Framework;

namespace NPCGen.Tests.Generation.Xml.Data.Races.BaseRaces.Good
{
    [TestFixture]
    public class GoodBarbarianBaseRacesTests : PercentileTests
    {
        [SetUp]
        public void Setup()
        {
            tableName = "GoodBarbarianBaseRaces";
        }

        [Test]
        public void GoodBarbarianHillDwarfPercentile()
        {
            AssertContentIsInRange(RaceConstants.BaseRaces.HillDwarf, 1, 2);
        }

        [Test]
        public void GoodBarbarianWildElfPercentile()
        {
            AssertContentIsInRange(RaceConstants.BaseRaces.WildElf, 3, 32);
        }

        [Test]
        public void GoodBarbarianWoodElfPercentile()
        {
            AssertContentIsInRange(RaceConstants.BaseRaces.WoodElf, 33, 34);
        }

        [Test]
        public void GoodBarbarianHalfElfPercentile()
        {
            AssertContentOnSingleRoll(RaceConstants.BaseRaces.HalfElf, 35);
        }

        [Test]
        public void GoodBarbarianLightfootHalflingPercentile()
        {
            AssertContentOnSingleRoll(RaceConstants.BaseRaces.LightfootHalfling, 36);
        }

        [Test]
        public void GoodBarbarianHalfOrcPercentile()
        {
            AssertContentIsInRange(RaceConstants.BaseRaces.HalfOrc, 37, 61);
        }

        [Test]
        public void GoodBarbarianHumanPercentile()
        {
            AssertContentIsInRange(RaceConstants.BaseRaces.Human, 62, 98);
        }

        [Test]
        public void GoodBarbarianEmptyPercentile()
        {
            AssertEmpty(99, 100);
        }
    }
}