using NPCGen.Core.Data.Races;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Tables.Races.BaseRaces.Good
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
            AssertContent(RaceConstants.BaseRaces.HillDwarf, 1, 2);
        }

        [Test]
        public void GoodBarbarianWildElfPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.WildElf, 3, 32);
        }

        [Test]
        public void GoodBarbarianWoodElfPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.WoodElf, 33, 34);
        }

        [Test]
        public void GoodBarbarianHalfElfPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.HalfElf, 35);
        }

        [Test]
        public void GoodBarbarianLightfootHalflingPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.LightfootHalfling, 36);
        }

        [Test]
        public void GoodBarbarianHalfOrcPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.HalfOrc, 37, 61);
        }

        [Test]
        public void GoodBarbarianHumanPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.Human, 62, 98);
        }

        [Test]
        public void GoodBarbarianEmptyPercentile()
        {
            AssertEmpty(99, 100);
        }
    }
}