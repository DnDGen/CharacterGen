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
            AssertContentOnSingleRoll(RaceConstants.BaseRaces.DeepDwarf, 1);
        }

        [Test]
        public void NeutralBardHillDwarfPercentile()
        {
            AssertContentIsInRange(RaceConstants.BaseRaces.HillDwarf, 2, 3);
        }

        [Test]
        public void NeutralBardGrayElfPercentile()
        {
            AssertContentIsInRange(RaceConstants.BaseRaces.GrayElf, 4, 5);
        }

        [Test]
        public void NeutralBardHighElfPercentile()
        {
            AssertContentIsInRange(RaceConstants.BaseRaces.HighElf, 6, 15);
        }

        [Test]
        public void NeutralBardWildElfPercentile()
        {
            AssertContentOnSingleRoll(RaceConstants.BaseRaces.WildElf, 16);
        }

        [Test]
        public void NeutralBardWoodElfPercentile()
        {
            AssertContentIsInRange(RaceConstants.BaseRaces.WoodElf, 17, 21);
        }

        [Test]
        public void NeutralBardRockGnomePercentile()
        {
            AssertContentIsInRange(RaceConstants.BaseRaces.RockGnome, 22, 23);
        }

        [Test]
        public void NeutralBardHalfElfPercentile()
        {
            AssertContentIsInRange(RaceConstants.BaseRaces.HalfElf, 24, 33);
        }

        [Test]
        public void NeutralBardLightfootHalflingPercentile()
        {
            AssertContentIsInRange(RaceConstants.BaseRaces.LightfootHalfling, 34, 36);
        }

        [Test]
        public void NeutralBardDeepHalflingPercentile()
        {
            AssertContentOnSingleRoll(RaceConstants.BaseRaces.DeepHalfling, 37);
        }

        [Test]
        public void NeutralBardTallfellowHalflingPercentile()
        {
            AssertContentOnSingleRoll(RaceConstants.BaseRaces.TallfellowHalfling, 38);
        }

        [Test]
        public void NeutralBardHalfOrcPercentile()
        {
            AssertContentIsInRange(RaceConstants.BaseRaces.HalfOrc, 39, 40);
        }

        [Test]
        public void NeutralBardHumanPercentile()
        {
            AssertContentIsInRange(RaceConstants.BaseRaces.Human, 41, 98);
        }

        [Test]
        public void NeutralBardEmptyPercentile()
        {
            AssertEmpty(99, 100);
        }
    }
}