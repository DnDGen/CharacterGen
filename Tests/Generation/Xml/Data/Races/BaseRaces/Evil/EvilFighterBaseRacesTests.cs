using NPCGen.Core.Data.Races;
using NUnit.Framework;

namespace NPCGen.Tests.Generation.Xml.Data.Races.BaseRaces.Evil
{
    [TestFixture]
    public class EvilFighterBaseRacesTests : PercentileTests
    {
        [SetUp]
        public void Setup()
        {
            tableName = "EvilFighterBaseRaces";
        }

        [Test]
        public void EvilFighterDeepDwarfPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.DeepDwarf, 1, 2);
        }

        [Test]
        public void EvilFighterHillDwarfPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.HillDwarf, 3, 4);
        }

        [Test]
        public void EvilFighterHighElfPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.HighElf, 5);
        }

        [Test]
        public void EvilFighterWoodElfPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.WoodElf, 6, 7);
        }

        [Test]
        public void EvilFighterHalfElfPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.HalfElf, 8, 12);
        }

        [Test]
        public void EvilFighterLightfootHalfling()
        {
            AssertContent(RaceConstants.BaseRaces.LightfootHalfling, 13);
        }

        [Test]
        public void EvilFighterDeepHalflingPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.DeepHalfling, 14);
        }

        [Test]
        public void EvilFighterHalfOrcPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.HalfOrc, 15, 23);
        }

        [Test]
        public void EvilFighterHumanPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.Human, 24, 53);
        }

        [Test]
        public void EvilFighterLizardfolkPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.Lizardfolk, 54);
        }

        [Test]
        public void EvilFighterGoblinPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.Goblin, 55);
        }

        [Test]
        public void EvilFighterHobgoblinPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.Hobgoblin, 56, 80);
        }

        [Test]
        public void EvilFighterKoboldPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.Kobold, 81);
        }

        [Test]
        public void EvilFighterOrcPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.Orc, 82, 86);
        }

        [Test]
        public void EvilFighterDrowPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.Drow, 87, 88);
        }

        [Test]
        public void EvilFighterDuergarDwarfPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.DuergarDwarf, 89);
        }

        [Test]
        public void EvilFighterDerroPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.Derro, 90);
        }

        [Test]
        public void EvilFighterGnollPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.Gnoll, 91);
        }

        [Test]
        public void EvilFighterTroglodytePercentile()
        {
            AssertContent(RaceConstants.BaseRaces.Troglodyte, 92);
        }

        [Test]
        public void EvilFighterBugbearPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.Bugbear, 93);
        }

        [Test]
        public void EvilFighterOgrePercentile()
        {
            AssertContent(RaceConstants.BaseRaces.Ogre, 94);
        }

        [Test]
        public void EvilFighterMindFlayerPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.MindFlayer, 95);
        }

        [Test]
        public void EvilFighterOgreMagePercentile()
        {
            AssertContent(RaceConstants.BaseRaces.OgreMage, 96);
        }

        [Test]
        public void EvilFighterEmptyPercentile()
        {
            AssertEmpty(97, 100);
        }
    }
}