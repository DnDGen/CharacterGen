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
            AssertContentIsInRange(RaceConstants.BaseRaces.Aasimar, 1, 2);
        }

        [Test]
        public void GoodSorcererDeepDwarfPercentile()
        {
            AssertContentOnSingleRoll(RaceConstants.BaseRaces.DeepDwarf, 3);
        }

        [Test]
        public void GoodSorcererHillDwarfPercentile()
        {
            AssertContentIsInRange(RaceConstants.BaseRaces.HillDwarf, 4, 5);
        }

        [Test]
        public void GoodSorcererMountainDwarfPercentile()
        {
            AssertContentOnSingleRoll(RaceConstants.BaseRaces.MountainDwarf, 6);
        }

        [Test]
        public void GoodSorcererGrayElfPercentile()
        {
            AssertContentIsInRange(RaceConstants.BaseRaces.GrayElf, 7, 8);
        }

        [Test]
        public void GoodSorcererHighElfPercentile()
        {
            AssertContentIsInRange(RaceConstants.BaseRaces.HighElf, 9, 11);
        }

        [Test]
        public void GoodSorcererWildElfPercentile()
        {
            AssertContentIsInRange(RaceConstants.BaseRaces.WildElf, 12, 36);
        }

        [Test]
        public void GoodSorcererWoodElfPercentile()
        {
            AssertContentOnSingleRoll(RaceConstants.BaseRaces.WoodElf, 37);
        }

        [Test]
        public void GoodSorcererForestGnomePercentile()
        {
            AssertContentOnSingleRoll(RaceConstants.BaseRaces.ForestGnome, 38);
        }

        [Test]
        public void GoodSorcererRockGnomeDwarfPercentile()
        {
            AssertContentIsInRange(RaceConstants.BaseRaces.RockGnome, 39, 40);
        }

        [Test]
        public void GoodSorcererHalfElfPercentile()
        {
            AssertContentIsInRange(RaceConstants.BaseRaces.HalfElf, 41, 45);
        }

        [Test]
        public void GoodSorcererLightfootHalflingPercentile()
        {
            AssertContentIsInRange(RaceConstants.BaseRaces.LightfootHalfling, 46, 54);
        }

        [Test]
        public void GoodSorcererDeepHalflingPercentile()
        {
            AssertContentOnSingleRoll(RaceConstants.BaseRaces.DeepHalfling, 55);
        }

        [Test]
        public void GoodSorcererTallfellowHalflingPercentile()
        {
            AssertContentOnSingleRoll(RaceConstants.BaseRaces.TallfellowHalfling, 56);
        }

        [Test]
        public void GoodSorcererHalfOrcPercentile()
        {
            AssertContentIsInRange(RaceConstants.BaseRaces.HalfOrc, 57, 58);
        }

        [Test]
        public void GoodSorcererHumanPercentile()
        {
            AssertContentIsInRange(RaceConstants.BaseRaces.Human, 59, 95);
        }

        [Test]
        public void GoodSorcererSvirfneblinPercentile()
        {
            AssertContentOnSingleRoll(RaceConstants.BaseRaces.Svirfneblin, 96);
        }

        [Test]
        public void GoodSorcererEmptyPercentile()
        {
            AssertEmpty(97, 100);
        }
    }
}