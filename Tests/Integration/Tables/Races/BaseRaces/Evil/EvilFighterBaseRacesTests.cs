using System;
using NPCGen.Common.Races;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Tables.Races.BaseRaces.Evil
{
    [TestFixture]
    public class EvilFighterBaseRacesTests : PercentileTests
    {
        protected override String tableName
        {
            get { return "EvilFighterBaseRaces"; }
        }

        [Test]
        public void EvilFighterDeepDwarfPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.DeepDwarf, 1, 2);
        }

        [Test]
        public void EvilFighterHillDwarfPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.HillDwarf, 3, 4);
        }

        [Test]
        public void EvilFighterHighElfPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.HighElf, 5);
        }

        [Test]
        public void EvilFighterWoodElfPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.WoodElf, 6, 7);
        }

        [Test]
        public void EvilFighterHalfElfPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.HalfElf, 8, 12);
        }

        [Test]
        public void EvilFighterLightfootHalfling()
        {
            AssertPercentile(RaceConstants.BaseRaces.LightfootHalfling, 13);
        }

        [Test]
        public void EvilFighterDeepHalflingPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.DeepHalfling, 14);
        }

        [Test]
        public void EvilFighterHalfOrcPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.HalfOrc, 15, 23);
        }

        [Test]
        public void EvilFighterHumanPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.Human, 24, 53);
        }

        [Test]
        public void EvilFighterLizardfolkPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.Lizardfolk, 54);
        }

        [Test]
        public void EvilFighterGoblinPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.Goblin, 55);
        }

        [Test]
        public void EvilFighterHobgoblinPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.Hobgoblin, 56, 80);
        }

        [Test]
        public void EvilFighterKoboldPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.Kobold, 81);
        }

        [Test]
        public void EvilFighterOrcPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.Orc, 82, 86);
        }

        [Test]
        public void EvilFighterDrowPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.Drow, 87, 88);
        }

        [Test]
        public void EvilFighterDuergarDwarfPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.DuergarDwarf, 89);
        }

        [Test]
        public void EvilFighterDerroPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.Derro, 90);
        }

        [Test]
        public void EvilFighterGnollPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.Gnoll, 91);
        }

        [Test]
        public void EvilFighterTroglodytePercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.Troglodyte, 92);
        }

        [Test]
        public void EvilFighterBugbearPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.Bugbear, 93);
        }

        [Test]
        public void EvilFighterOgrePercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.Ogre, 94);
        }

        [Test]
        public void EvilFighterMindFlayerPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.MindFlayer, 95);
        }

        [Test]
        public void EvilFighterOgreMagePercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.OgreMage, 96);
        }

        [Test]
        public void EvilFighterEmptyPercentile()
        {
            AssertPercentile(EmptyContent, 97, 100);
        }
    }
}