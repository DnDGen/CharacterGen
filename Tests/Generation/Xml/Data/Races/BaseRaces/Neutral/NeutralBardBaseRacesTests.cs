using NPCGen.Core.Data.Races;
using NUnit.Framework;

namespace NPCGen.Tests.Generation.Xml.Data.Races.BaseRaces.Neutral
{
    [TestFixture]
    public class NeutralBardBaseRacesTests : PercentileTests
    {
        [SetUp]
        public void Setup()
        {
            tableName = "NeutralBardBaseRaces";
        }

        [Test]
        public void NeutralBardDeepDwarfPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.DeepDwarf, 1);
        }

        [Test]
        public void NeutralBardHillDwarfPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.HillDwarf, 2, 3);
        }

        [Test]
        public void NeutralBardGrayElfPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.GrayElf, 4, 5);
        }

        [Test]
        public void NeutralBardHighElfPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.HighElf, 6, 15);
        }

        [Test]
        public void NeutralBardWildElfPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.WildElf, 16);
        }

        [Test]
        public void NeutralBardWoodElfPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.WoodElf, 17, 21);
        }

        [Test]
        public void NeutralBardRockGnomePercentile()
        {
            AssertContent(RaceConstants.BaseRaces.RockGnome, 22, 23);
        }

        [Test]
        public void NeutralBardHalfElfPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.HalfElf, 24, 33);
        }

        [Test]
        public void NeutralBardLightfootHalflingPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.LightfootHalfling, 34, 36);
        }

        [Test]
        public void NeutralBardDeepHalflingPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.DeepHalfling, 37);
        }

        [Test]
        public void NeutralBardTallfellowHalflingPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.TallfellowHalfling, 38);
        }

        [Test]
        public void NeutralBardHalfOrcPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.HalfOrc, 39, 40);
        }

        [Test]
        public void NeutralBardHumanPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.Human, 41, 98);
        }

        [Test]
        public void NeutralBardEmptyPercentile()
        {
            AssertEmpty(99, 100);
        }
    }
}