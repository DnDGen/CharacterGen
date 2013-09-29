using NPCGen.Core.Data.Races;
using NUnit.Framework;

namespace NPCGen.Tests.Generation.Xml.Data.Races.BaseRaces.Good
{
    [TestFixture]
    public class GoodPaladinBaseRacesTests : PercentileTests
    {
        [SetUp]
        public void Setup()
        {
            tableName = "GoodPaladinBaseRaces";
        }

        [Test]
        public void GoodPaladinAasimarPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.Aasimar, 1, 10);
        }

        [Test]
        public void GoodPaladinHillDwarfPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.HillDwarf, 11, 20);
        }

        [Test]
        public void GoodPaladinMountainDwarfPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.MountainDwarf, 21);
        }

        [Test]
        public void GoodPaladinRockGnomeDwarfPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.RockGnome, 22);
        }

        [Test]
        public void GoodPaladinHalfElfPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.HalfElf, 23, 27);
        }

        [Test]
        public void GoodPaladinLightfootHalflingPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.LightfootHalfling, 28);
        }

        [Test]
        public void GoodPaladinDeepHalflingPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.DeepHalfling, 29);
        }

        [Test]
        public void GoodPaladinHalfOrcPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.HalfOrc, 30);
        }

        [Test]
        public void GoodPaladinHumanPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.Human, 31, 97);
        }

        [Test]
        public void GoodPaladinEmptyPercentile()
        {
            AssertEmpty(98, 100);
        }
    }
}