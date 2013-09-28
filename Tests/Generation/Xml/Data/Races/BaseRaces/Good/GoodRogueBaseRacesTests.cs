using NPCGen.Core.Data.Races;
using NUnit.Framework;

namespace NPCGen.Tests.Generation.Xml.Data.Races.BaseRaces.Good
{
    [TestFixture]
    public class GoodRogueBaseRacesTests : PercentileTests
    {
        [SetUp]
        public void Setup()
        {
            tableName = "GoodRogueBaseRaces";
        }

        [Test]
        public void GoodRogueHillDwarfPercentile()
        {
            AssertContentIsInRange(RaceConstants.BaseRaces.HillDwarf, 1, 5);
        }

        [Test]
        public void GoodRogueMountainDwarfPercentile()
        {
            AssertContentOnSingleRoll(RaceConstants.BaseRaces.MountainDwarf, 6);
        }

        [Test]
        public void GoodRogueHighElfPercentile()
        {
            AssertContentIsInRange(RaceConstants.BaseRaces.HighElf, 7, 19);
        }

        [Test]
        public void GoodRogueForestGnomePercentile()
        {
            AssertContentOnSingleRoll(RaceConstants.BaseRaces.ForestGnome, 20);
        }

        [Test]
        public void GoodRogueRockGnomeDwarfPercentile()
        {
            AssertContentIsInRange(RaceConstants.BaseRaces.RockGnome, 21, 25);
        }

        [Test]
        public void GoodRogueHalfElfPercentile()
        {
            AssertContentIsInRange(RaceConstants.BaseRaces.HalfElf, 26, 35);
        }

        [Test]
        public void GoodRogueLightfootHalflingPercentile()
        {
            AssertContentIsInRange(RaceConstants.BaseRaces.LightfootHalfling, 36, 60);
        }

        [Test]
        public void GoodRogueDeepHalflingPercentile()
        {
            AssertContentIsInRange(RaceConstants.BaseRaces.DeepHalfling, 61, 66);
        }

        [Test]
        public void GoodRogueTallfellowHalflingPercentile()
        {
            AssertContentIsInRange(RaceConstants.BaseRaces.TallfellowHalfling, 67, 72);
        }

        [Test]
        public void GoodRogueHalfOrcPercentile()
        {
            AssertContentIsInRange(RaceConstants.BaseRaces.HalfOrc, 73, 77);
        }

        [Test]
        public void GoodRogueHumanPercentile()
        {
            AssertContentIsInRange(RaceConstants.BaseRaces.Human, 78, 96);
        }

        [Test]
        public void GoodRogueSvirfneblinPercentile()
        {
            AssertContentOnSingleRoll(RaceConstants.BaseRaces.Svirfneblin, 97);
        }

        [Test]
        public void GoodRogueEmptyPercentile()
        {
            AssertEmpty(98, 100);
        }
    }
}