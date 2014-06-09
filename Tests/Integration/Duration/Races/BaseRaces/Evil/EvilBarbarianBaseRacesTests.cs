using NPCGen.Core.Data.Races;
using NUnit.Framework;

namespace NPCGen.Tests.Unit.Generation.Xml.Data.Races.BaseRaces.Evil
{
    [TestFixture]
    public class EvilBarbarianBaseRacesTests : PercentileTests
    {
        [SetUp]
        public void Setup()
        {
            tableName = "EvilBarbarianBaseRaces";
        }

        [Test]
        public void EvilBarbarianWildElfPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.WildElf, 1);
        }

        [Test]
        public void EvilBarbarianWoodElfPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.WoodElf, 2, 3);
        }

        [Test]
        public void EvilBarbarianHalfElfPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.HalfElf, 4);
        }

        [Test]
        public void EvilBarbarianLightfootHalflingPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.LightfootHalfling, 5);
        }

        [Test]
        public void EvilBarbarianDeepHalflingPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.DeepHalfling, 6);
        }

        [Test]
        public void EvilBarbarianHalfOrcPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.HalfOrc, 7, 29);
        }

        [Test]
        public void EvilBarbarianHumanPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.Human, 30, 39);
        }

        [Test]
        public void EvilBarbarianLizardfolkPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.Lizardfolk, 40, 44);
        }

        [Test]
        public void EvilBarbarianGoblinPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.Goblin, 45);
        }

        [Test]
        public void EvilBarbarianHobgoblinPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.Hobgoblin, 46);
        }

        [Test]
        public void EvilBarbarianKoboldPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.Kobold, 47);
        }

        [Test]
        public void EvilBarbarianOrcPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.Orc, 48, 77);
        }

        [Test]
        public void EvilBarbarianTieflingPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.Tiefling, 78);
        }

        [Test]
        public void EvilBarbarianGnollPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.Gnoll, 79, 83);
        }

        [Test]
        public void EvilBarbarianTrogolodytePercentile()
        {
            AssertContent(RaceConstants.BaseRaces.Troglodyte, 84);
        }

        [Test]
        public void EvilBarbarianBugbearPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.Bugbear, 85, 86);
        }

        [Test]
        public void EvilBarbarianOgrePercentile()
        {
            AssertContent(RaceConstants.BaseRaces.Ogre, 87, 90);
        }

        [Test]
        public void EvilBarbarianMinotaurPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.Minotaur, 91, 94);
        }

        [Test]
        public void EvilBarbarianEmptyPercentile()
        {
            AssertEmpty(95, 100);
        }
    }
}