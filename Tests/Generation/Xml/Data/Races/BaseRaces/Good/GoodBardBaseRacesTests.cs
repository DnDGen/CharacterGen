using NPCGen.Core.Data.Races;
using NUnit.Framework;

namespace NPCGen.Tests.Generation.Xml.Data.Races.BaseRaces.Good
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
            AssertContentOnSingleRoll(RaceConstants.BaseRaces.Aasimar, 1);
        }

        [Test]
        public void GoodBardHillDwarfPercentile()
        {
            AssertContentIsInRange(RaceConstants.BaseRaces.HillDwarf, 2, 6);
        }

        [Test]
        public void GoodBardGrayElfPercentile()
        {
            AssertContentIsInRange(RaceConstants.BaseRaces.GrayElf, 7, 11);
        }

        [Test]
        public void GoodBardHighElfPercentile()
        {
            AssertContentIsInRange(RaceConstants.BaseRaces.HighElf, 12, 36);
        }

        [Test]
        public void GoodBardWildElfPercentile()
        {
            AssertContentOnSingleRoll(RaceConstants.BaseRaces.WildElf, 37);
        }

        [Test]
        public void GoodBardWoodElfPercentile()
        {
            AssertContentOnSingleRoll(RaceConstants.BaseRaces.WoodElf, 38);
        }

        [Test]
        public void GoodBardForestGnomePercentile()
        {
            AssertContentOnSingleRoll(RaceConstants.BaseRaces.ForestGnome, 39);
        }

        [Test]
        public void GoodBardRockGnomePercentile()
        {
            AssertContentIsInRange(RaceConstants.BaseRaces.RockGnome, 40, 44);
        }

        [Test]
        public void GoodBardHalfElfPercentile()
        {
            AssertContentIsInRange(RaceConstants.BaseRaces.HalfElf, 45, 53);
        }

        [Test]
        public void GoodBardLightfootHalflingPercentile()
        {
            AssertContentOnSingleRoll(RaceConstants.BaseRaces.LightfootHalfling, 54);
        }

        [Test]
        public void GoodBardDeepHalflingPercentile()
        {
            AssertContentOnSingleRoll(RaceConstants.BaseRaces.DeepHalfling, 55);
        }

        [Test]
        public void GoodBardTallfellowHalflingPercentile()
        {
            AssertContentIsInRange(RaceConstants.BaseRaces.TallfellowHalfling, 56, 57);
        }

        [Test]
        public void GoodBardHumanPercentile()
        {
            AssertContentIsInRange(RaceConstants.BaseRaces.Human, 58, 97);
        }

        [Test]
        public void GoodBardSvirfneblinPercentile()
        {
            AssertContentOnSingleRoll(RaceConstants.BaseRaces.Svirfneblin, 98);
        }

        [Test]
        public void GoodBardEmptyPercentile()
        {
            AssertEmpty(99, 100);
        }
    }
}