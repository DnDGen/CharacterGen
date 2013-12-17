using NPCGen.Core.Data.Races;
using NUnit.Framework;

namespace NPCGen.Tests.Unit.Generation.Xml.Data.Races.BaseRaces.Evil
{
    [TestFixture]
    public class EvilRangerBaseRacesTests : PercentileTests
    {
        [SetUp]
        public void Setup()
        {
            tableName = "EvilRangerBaseRaces";
        }

        [Test]
        public void EvilRangerHighElfPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.HighElf, 1);
        }

        [Test]
        public void EvilRangerWoodElfPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.WoodElf, 2, 11);
        }

        [Test]
        public void EvilRangerHalfElfPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.HalfElf, 12, 28);
        }

        [Test]
        public void EvilRangerLightfootHalflingPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.LightfootHalfling, 29);
        }

        [Test]
        public void EvilRangerTallfellowHalflingPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.TallfellowHalfling, 30);
        }

        [Test]
        public void EvilRangerHalfOrcPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.HalfOrc, 31, 39);
        }

        [Test]
        public void EvilRangerHumanPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.Human, 40, 69);
        }

        [Test]
        public void EvilRangerLizardfolkPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.Lizardfolk, 70, 71);
        }

        [Test]
        public void EvilRangerHobgoblinPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.Hobgoblin, 72);
        }

        [Test]
        public void EvilRangerGnollPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.Gnoll, 73, 92);
        }

        [Test]
        public void EvilRangerTroglodytePercentile()
        {
            AssertContent(RaceConstants.BaseRaces.Troglodyte, 93);
        }

        [Test]
        public void EvilRangerBugbearPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.Bugbear, 94);
        }

        [Test]
        public void EvilRangerOgrePercentile()
        {
            AssertContent(RaceConstants.BaseRaces.Ogre, 95);
        }

        [Test]
        public void EvilRangerEmptyPercentile()
        {
            AssertEmpty(96, 100);
        }
    }
}