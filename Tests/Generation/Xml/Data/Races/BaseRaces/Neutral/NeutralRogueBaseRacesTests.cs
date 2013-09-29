using NPCGen.Core.Data.Races;
using NUnit.Framework;

namespace NPCGen.Tests.Generation.Xml.Data.Races.BaseRaces.Neutral
{
    [TestFixture]
    public class NeutralRogueBaseRacesTests : PercentileTests
    {
        [SetUp]
        public void Setup()
        {
            tableName = "NeutralRogueBaseRaces";
        }

        [Test]
        public void NeutralRogueDeepDwarfPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.DeepDwarf, 1);
        }

        [Test]
        public void NeutralRogueHillDwarfPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.HillDwarf, 2, 4);
        }

        [Test]
        public void NeutralRogueHighElfPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.HighElf, 5, 8);
        }

        [Test]
        public void NeutralRogueWoodElfPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.WoodElf, 9);
        }

        [Test]
        public void NeutralRogueRockGnomePercentile()
        {
            AssertContent(RaceConstants.BaseRaces.RockGnome, 10);
        }

        [Test]
        public void NeutralRogueHalfElfPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.HalfElf, 11, 25);
        }

        [Test]
        public void NeutralRogueLightfootHalflingPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.LightfootHalfling, 26, 53);
        }

        [Test]
        public void NeutralRogueDeepHalflingPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.DeepHalfling, 54, 58);
        }

        [Test]
        public void NeutralRogueTallfellowHalflingPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.TallfellowHalfling, 59, 63);
        }

        [Test]
        public void NeutralRogueHalfOrcPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.HalfOrc, 64, 73);
        }

        [Test]
        public void NeutralRogueHumanPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.Human, 74, 97);
        }

        [Test]
        public void NeutralRogueDoppelgangerPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.Doppelganger, 98);
        }

        [Test]
        public void NeutralRogueEmptyPercentile()
        {
            AssertEmpty(99, 100);
        }
    }
}