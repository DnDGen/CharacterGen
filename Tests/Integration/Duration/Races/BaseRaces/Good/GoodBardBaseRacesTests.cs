using NPCGen.Core.Data.Races;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Tables.Races.BaseRaces.Good
{
    [TestFixture]
    public class GoodBardBaseRacesTests : PercentileTests
    {
        [SetUp]
        public void Setup()
        {
            tableName = "GoodBardBaseRaces";
        }

        [Test]
        public void GoodBardAasimarPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.Aasimar, 1);
        }

        [Test]
        public void GoodBardHillDwarfPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.HillDwarf, 2, 6);
        }

        [Test]
        public void GoodBardGrayElfPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.GrayElf, 7, 11);
        }

        [Test]
        public void GoodBardHighElfPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.HighElf, 12, 36);
        }

        [Test]
        public void GoodBardWildElfPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.WildElf, 37);
        }

        [Test]
        public void GoodBardWoodElfPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.WoodElf, 38);
        }

        [Test]
        public void GoodBardForestGnomePercentile()
        {
            AssertContent(RaceConstants.BaseRaces.ForestGnome, 39);
        }

        [Test]
        public void GoodBardRockGnomePercentile()
        {
            AssertContent(RaceConstants.BaseRaces.RockGnome, 40, 44);
        }

        [Test]
        public void GoodBardHalfElfPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.HalfElf, 45, 53);
        }

        [Test]
        public void GoodBardLightfootHalflingPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.LightfootHalfling, 54);
        }

        [Test]
        public void GoodBardDeepHalflingPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.DeepHalfling, 55);
        }

        [Test]
        public void GoodBardTallfellowHalflingPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.TallfellowHalfling, 56, 57);
        }

        [Test]
        public void GoodBardHumanPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.Human, 58, 97);
        }

        [Test]
        public void GoodBardSvirfneblinPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.Svirfneblin, 98);
        }

        [Test]
        public void GoodBardEmptyPercentile()
        {
            AssertEmpty(99, 100);
        }
    }
}