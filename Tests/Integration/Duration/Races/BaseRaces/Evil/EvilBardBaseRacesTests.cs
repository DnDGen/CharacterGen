using NPCGen.Core.Data.Races;
using NUnit.Framework;

namespace NPCGen.Tests.Unit.Generation.Xml.Data.Races.BaseRaces.Evil
{
    [TestFixture]
    public class EvilBardBaseRacesTests : PercentileTests
    {
        [SetUp]
        public void Setup()
        {
            tableName = "EvilBardBaseRaces";
        }

        [Test]
        public void EvilBardHighElfPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.HighElf, 1);
        }

        [Test]
        public void EvilBardWoodElfPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.WoodElf, 2);
        }

        [Test]
        public void EvilBardHalfElfPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.HalfElf, 3, 17);
        }

        [Test]
        public void EvilBardLightfootHalflingPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.LightfootHalfling, 18);
        }

        [Test]
        public void EvilBardDeepHalflingPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.DeepHalfling, 19);
        }

        [Test]
        public void EvilBardTallfellowHalflingPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.TallfellowHalfling, 20);
        }

        [Test]
        public void EvilBardHalfOrcPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.HalfOrc, 21, 22);
        }

        [Test]
        public void EvilBardHumanPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.Human, 23, 97);
        }

        [Test]
        public void EvilBardGoblinPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.Goblin, 98);
        }

        [Test]
        public void EvilBardTieflingPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.Tiefling, 99);
        }

        [Test]
        public void EvilBardEmptyPercentile()
        {
            AssertEmpty(100);
        }
    }
}