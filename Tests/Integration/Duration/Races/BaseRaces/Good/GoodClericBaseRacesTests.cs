using NPCGen.Core.Data.Races;
using NUnit.Framework;

namespace NPCGen.Tests.Unit.Generation.Xml.Data.Races.BaseRaces.Good
{
    [TestFixture]
    public class GoodClericBaseRacesTests : PercentileTests
    {
        [SetUp]
        public void Setup()
        {
            tableName = "GoodClericBaseRaces";
        }

        [Test]
        public void GoodClericAasimarPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.Aasimar, 1);
        }

        [Test]
        public void GoodClericDeepDwarfPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.DeepDwarf, 2);
        }

        [Test]
        public void GoodClericHillDwarfPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.HillDwarf, 3, 22);
        }

        [Test]
        public void GoodClericMountainDwarfPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.MountainDwarf, 23, 24);
        }

        [Test]
        public void GoodClericGrayElfPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.GrayElf, 25);
        }

        [Test]
        public void GoodClericHighElfPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.HighElf, 26, 35);
        }

        [Test]
        public void GoodClericWildElfPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.WildElf, 36, 40);
        }

        [Test]
        public void GoodClericWoodElfPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.WoodElf, 41);
        }

        [Test]
        public void GoodClericForestGnomePercentile()
        {
            AssertContent(RaceConstants.BaseRaces.ForestGnome, 42);
        }

        [Test]
        public void GoodClericRockGnomePercentile()
        {
            AssertContent(RaceConstants.BaseRaces.RockGnome, 43, 51);
        }

        [Test]
        public void GoodClericHalfElfPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.HalfElf, 52, 56);
        }

        [Test]
        public void GoodClericLightfootHalflingPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.LightfootHalfling, 57, 66);
        }

        [Test]
        public void GoodClericDeepHalflingPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.DeepHalfling, 67);
        }

        [Test]
        public void GoodClericTallfellowHalflingPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.TallfellowHalfling, 68, 69);
        }

        [Test]
        public void GoodClericHalfOrcPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.HalfOrc, 70);
        }

        [Test]
        public void GoodClericHumanPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.Human, 71, 95);
        }

        [Test]
        public void GoodClericSvirfneblinPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.Svirfneblin, 96);
        }

        [Test]
        public void GoodClericEmptyPercentile()
        {
            AssertEmpty(97, 100);
        }
    }
}