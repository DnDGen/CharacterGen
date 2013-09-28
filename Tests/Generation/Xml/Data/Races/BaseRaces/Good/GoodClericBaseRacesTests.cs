using NPCGen.Core.Data.Races;
using NUnit.Framework;

namespace NPCGen.Tests.Generation.Xml.Data.Races.BaseRaces.Good
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
            AssertContentOnSingleRoll(RaceConstants.BaseRaces.Aasimar, 1);
        }

        [Test]
        public void GoodClericDeepDwarfPercentile()
        {
            AssertContentOnSingleRoll(RaceConstants.BaseRaces.DeepDwarf, 2);
        }

        [Test]
        public void GoodClericHillDwarfPercentile()
        {
            AssertContentIsInRange(RaceConstants.BaseRaces.HillDwarf, 3, 22);
        }

        [Test]
        public void GoodClericMountainDwarfPercentile()
        {
            AssertContentIsInRange(RaceConstants.BaseRaces.MountainDwarf, 23, 24);
        }

        [Test]
        public void GoodClericGrayElfPercentile()
        {
            AssertContentOnSingleRoll(RaceConstants.BaseRaces.GrayElf, 25);
        }

        [Test]
        public void GoodClericHighElfPercentile()
        {
            AssertContentIsInRange(RaceConstants.BaseRaces.HighElf, 26, 35);
        }

        [Test]
        public void GoodClericWildElfPercentile()
        {
            AssertContentIsInRange(RaceConstants.BaseRaces.WildElf, 36, 40);
        }

        [Test]
        public void GoodClericWoodElfPercentile()
        {
            AssertContentOnSingleRoll(RaceConstants.BaseRaces.WoodElf, 41);
        }

        [Test]
        public void GoodClericForestGnomePercentile()
        {
            AssertContentOnSingleRoll(RaceConstants.BaseRaces.ForestGnome, 42);
        }

        [Test]
        public void GoodClericRockGnomePercentile()
        {
            AssertContentIsInRange(RaceConstants.BaseRaces.RockGnome, 43, 51);
        }

        [Test]
        public void GoodClericHalfElfPercentile()
        {
            AssertContentIsInRange(RaceConstants.BaseRaces.HalfElf, 52, 56);
        }

        [Test]
        public void GoodClericLightfootHalflingPercentile()
        {
            AssertContentIsInRange(RaceConstants.BaseRaces.LightfootHalfling, 57, 66);
        }

        [Test]
        public void GoodClericDeepHalflingPercentile()
        {
            AssertContentOnSingleRoll(RaceConstants.BaseRaces.DeepHalfling, 67);
        }

        [Test]
        public void GoodClericTallfellowHalflingPercentile()
        {
            AssertContentIsInRange(RaceConstants.BaseRaces.TallfellowHalfling, 68, 69);
        }

        [Test]
        public void GoodClericHalfOrcPercentile()
        {
            AssertContentOnSingleRoll(RaceConstants.BaseRaces.HalfOrc, 70);
        }

        [Test]
        public void GoodClericHumanPercentile()
        {
            AssertContentIsInRange(RaceConstants.BaseRaces.Human, 71, 95);
        }

        [Test]
        public void GoodClericSvirfneblinPercentile()
        {
            AssertContentOnSingleRoll(RaceConstants.BaseRaces.Svirfneblin, 96);
        }

        [Test]
        public void GoodClericEmptyPercentile()
        {
            AssertEmpty(97, 100);
        }
    }
}