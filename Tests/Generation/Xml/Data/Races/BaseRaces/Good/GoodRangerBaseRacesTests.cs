using NPCGen.Core.Data.Races;
using NUnit.Framework;

namespace NPCGen.Tests.Generation.Xml.Data.Races.BaseRaces.Good
{
    [TestFixture]
    public class GoodRangerBaseRacesTests : PercentileTests
    {
        [SetUp]
        public void Setup()
        {
            tableName = "GoodRangerBaseRaces";
        }

        [Test]
        public void GoodRangerHillDwarfPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.HillDwarf, 1, 5);
        }

        [Test]
        public void GoodRangerHighElfPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.HighElf, 6, 20);
        }

        [Test]
        public void GoodRangerWildElfPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.WildElf, 21);
        }

        [Test]
        public void GoodRangerWoodElfPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.WoodElf, 22, 36);
        }

        [Test]
        public void GoodRangerForestGnomePercentile()
        {
            AssertContent(RaceConstants.BaseRaces.ForestGnome, 37, 41);
        }

        [Test]
        public void GoodRangerRockGnomeDwarfPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.RockGnome, 42);
        }

        [Test]
        public void GoodRangerHalfElfPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.HalfElf, 43, 57);
        }

        [Test]
        public void GoodRangerLightfootHalflingPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.LightfootHalfling, 58);
        }

        [Test]
        public void GoodRangerTallfellowHalflingPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.TallfellowHalfling, 59);
        }

        [Test]
        public void GoodRangerHalfOrcPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.HalfOrc, 60, 64);
        }

        [Test]
        public void GoodRangerHumanPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.Human, 65, 97);
        }

        [Test]
        public void GoodRangerEmptyPercentile()
        {
            AssertEmpty(98, 100);
        }
    }
}