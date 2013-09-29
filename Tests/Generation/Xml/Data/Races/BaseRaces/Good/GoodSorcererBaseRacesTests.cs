using NPCGen.Core.Data.Races;
using NUnit.Framework;

namespace NPCGen.Tests.Generation.Xml.Data.Races.BaseRaces.Good
{
    [TestFixture]
    public class GoodSorcererBaseRacesTests : PercentileTests
    {
        [SetUp]
        public void Setup()
        {
            tableName = "GoodSorcererBaseRaces";
        }

        [Test]
        public void GoodSorcererAasimarPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.Aasimar, 1, 2);
        }

        [Test]
        public void GoodSorcererDeepDwarfPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.DeepDwarf, 3);
        }

        [Test]
        public void GoodSorcererHillDwarfPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.HillDwarf, 4, 5);
        }

        [Test]
        public void GoodSorcererMountainDwarfPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.MountainDwarf, 6);
        }

        [Test]
        public void GoodSorcererGrayElfPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.GrayElf, 7, 8);
        }

        [Test]
        public void GoodSorcererHighElfPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.HighElf, 9, 11);
        }

        [Test]
        public void GoodSorcererWildElfPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.WildElf, 12, 36);
        }

        [Test]
        public void GoodSorcererWoodElfPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.WoodElf, 37);
        }

        [Test]
        public void GoodSorcererForestGnomePercentile()
        {
            AssertContent(RaceConstants.BaseRaces.ForestGnome, 38);
        }

        [Test]
        public void GoodSorcererRockGnomeDwarfPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.RockGnome, 39, 40);
        }

        [Test]
        public void GoodSorcererHalfElfPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.HalfElf, 41, 45);
        }

        [Test]
        public void GoodSorcererLightfootHalflingPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.LightfootHalfling, 46, 54);
        }

        [Test]
        public void GoodSorcererDeepHalflingPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.DeepHalfling, 55);
        }

        [Test]
        public void GoodSorcererTallfellowHalflingPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.TallfellowHalfling, 56);
        }

        [Test]
        public void GoodSorcererHalfOrcPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.HalfOrc, 57, 58);
        }

        [Test]
        public void GoodSorcererHumanPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.Human, 59, 95);
        }

        [Test]
        public void GoodSorcererSvirfneblinPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.Svirfneblin, 96);
        }

        [Test]
        public void GoodSorcererEmptyPercentile()
        {
            AssertEmpty(97, 100);
        }
    }
}