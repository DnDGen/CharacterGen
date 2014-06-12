using NPCGen.Core.Data.Races;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Tables.Races.BaseRaces.Evil
{
    [TestFixture]
    public class EvilRogueBaseRacesTests : PercentileTests
    {
        [SetUp]
        public void Setup()
        {
            tableName = "EvilRogueBaseRaces";
        }

        [Test]
        public void EvilRogueDeepDwarfPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.DeepDwarf, 1);
        }

        [Test]
        public void EvilRogueHighElfPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.HighElf, 2);
        }

        [Test]
        public void EvilRogueWoodElfPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.WoodElf, 3);
        }

        [Test]
        public void EvilRogueHalfElfPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.HalfElf, 4, 18);
        }

        [Test]
        public void EvilRogueLightfootHalflingPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.LightfootHalfling, 19, 38);
        }

        [Test]
        public void EvilRogueDeepHalflingPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.DeepHalfling, 39);
        }

        [Test]
        public void EvilRogueTallfellowHalflingPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.TallfellowHalfling, 40);
        }

        [Test]
        public void EvilRogueHalfOrcPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.HalfOrc, 41, 50);
        }

        [Test]
        public void EvilRogueHumanPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.Human, 51, 70);
        }

        [Test]
        public void EvilRogueGoblinPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.Goblin, 71, 85);
        }

        [Test]
        public void EvilRogueHobgoblinPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.Hobgoblin, 86);
        }

        [Test]
        public void EvilRogueKoboldPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.Kobold, 87);
        }

        [Test]
        public void EvilRogueTieflingPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.Tiefling, 88, 89);
        }

        [Test]
        public void EvilRogueBugbearPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.Bugbear, 90, 93);
        }

        [Test]
        public void EvilRogueMindFlayerPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.MindFlayer, 94);
        }

        [Test]
        public void EvilRogueEmptyPercentile()
        {
            AssertEmpty(95, 100);
        }
    }
}