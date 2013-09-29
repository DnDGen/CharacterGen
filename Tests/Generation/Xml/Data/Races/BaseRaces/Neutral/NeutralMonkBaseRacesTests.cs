using NPCGen.Core.Data.Races;
using NUnit.Framework;

namespace NPCGen.Tests.Generation.Xml.Data.Races.BaseRaces.Neutral
{
    [TestFixture]
    public class NeutralMonkBaseRacesTests : PercentileTests
    {
        [SetUp]
        public void Setup()
        {
            tableName = "NeutralMonkBaseRaces";
        }

        [Test]
        public void NeutralMonkHighElfPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.HighElf, 1, 2);
        }

        [Test]
        public void NeutralMonkWoodElfPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.WoodElf, 3);
        }

        [Test]
        public void NeutralMonkHalfElfPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.HalfElf, 4, 13);
        }

        [Test]
        public void NeutralMonkLightfootHalflingPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.LightfootHalfling, 14);
        }

        [Test]
        public void NeutralMonkDeepHalflingPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.DeepHalfling, 15);
        }

        [Test]
        public void NeutralMonkHalfOrcPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.HalfOrc, 16, 25);
        }

        [Test]
        public void NeutralMonkHumanPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.Human, 26, 100);
        }
    }
}