using NPCGen.Core.Data.Races;
using NUnit.Framework;

namespace NPCGen.Tests.Generation.Xml.Data.Races.BaseRaces.Evil
{
    [TestFixture]
    public class EvilClericBaseRacesTests : PercentileTests
    {
        [SetUp]
        public void Setup()
        {
            tableName = "EvilClericBaseRaces";
        }

        [Test]
        public void EvilClericDeepDwarfPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.DeepDwarf, 1, 2);
        }

        [Test]
        public void EvilClericHillDwarfPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.HillDwarf, 3);
        }

        [Test]
        public void EvilClericHighElfPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.HighElf, 4);
        }

        [Test]
        public void EvilClericWildElfPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.WildElf, 5);
        }

        [Test]
        public void EvilClericWoodElfPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.WoodElf, 6, 8);
        }

        [Test]
        public void EvilClericHalfElfPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.HalfElf, 9, 18);
        }

        [Test]
        public void EvilClericLightfootHalflingPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.LightfootHalfling, 19, 20);
        }

        [Test]
        public void EvilClericDeepHalflingPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.DeepHalfling, 21);
        }

        [Test]
        public void EvilClericTallfellowHalflingPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.TallfellowHalfling, 22);
        }

        [Test]
        public void EvilClericHalfOrcPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.HalfOrc, 23, 25);
        }

        [Test]
        public void EvilClericHumanPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.Human, 26, 56);
        }

        [Test]
        public void EvilClericLizardfolkPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.Lizardfolk, 57, 63);
        }

        [Test]
        public void EvilClericGoblinPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.Goblin, 64);
        }

        [Test]
        public void EvilClericHobgoblinPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.Hobgoblin, 65);
        }

        [Test]
        public void EvilClericKoboldPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.Kobold, 66);
        }

        [Test]
        public void EvilClericOrcPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.Orc, 67);
        }

        [Test]
        public void EvilClericTieflingPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.Tiefling, 68);
        }

        [Test]
        public void EvilClericDrowPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.Drow, 69, 71);
        }

        [Test]
        public void EvilClericDuergarPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.DuergarDwarf, 72);
        }

        [Test]
        public void EvilClericGnollPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.Gnoll, 73, 74);
        }

        [Test]
        public void EvilClericTrogolodytePercentile()
        {
            AssertContent(RaceConstants.BaseRaces.Troglodyte, 75, 89);
        }

        [Test]
        public void EvilClericBugbearPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.Bugbear, 90, 91);
        }

        [Test]
        public void EvilClericOgrePercentile()
        {
            AssertContent(RaceConstants.BaseRaces.Ogre, 92);
        }

        [Test]
        public void EvilClericMinotaurPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.Minotaur, 93);
        }

        [Test]
        public void EvilClericMindFlayerPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.MindFlayer, 94);
        }

        [Test]
        public void EvilClericOgreMagePercentile()
        {
            AssertContent(RaceConstants.BaseRaces.OgreMage, 95);
        }

        [Test]
        public void EvilClericEmptyPercentile()
        {
            AssertEmpty(96, 100);
        }
    }
}