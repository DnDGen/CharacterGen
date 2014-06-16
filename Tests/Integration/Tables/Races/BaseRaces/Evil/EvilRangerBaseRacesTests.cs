using System;
using NPCGen.Common.Races;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Tables.Races.BaseRaces.Evil
{
    [TestFixture]
    public class EvilRangerBaseRacesTests : PercentileTests
    {
        protected override String tableName
        {
            get { return "EvilRangerBaseRaces"; }
        }

        [Test]
        public void EvilRangerHighElfPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.HighElf, 1);
        }

        [Test]
        public void EvilRangerWoodElfPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.WoodElf, 2, 11);
        }

        [Test]
        public void EvilRangerHalfElfPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.HalfElf, 12, 28);
        }

        [Test]
        public void EvilRangerLightfootHalflingPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.LightfootHalfling, 29);
        }

        [Test]
        public void EvilRangerTallfellowHalflingPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.TallfellowHalfling, 30);
        }

        [Test]
        public void EvilRangerHalfOrcPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.HalfOrc, 31, 39);
        }

        [Test]
        public void EvilRangerHumanPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.Human, 40, 69);
        }

        [Test]
        public void EvilRangerLizardfolkPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.Lizardfolk, 70, 71);
        }

        [Test]
        public void EvilRangerHobgoblinPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.Hobgoblin, 72);
        }

        [Test]
        public void EvilRangerGnollPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.Gnoll, 73, 92);
        }

        [Test]
        public void EvilRangerTroglodytePercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.Troglodyte, 93);
        }

        [Test]
        public void EvilRangerBugbearPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.Bugbear, 94);
        }

        [Test]
        public void EvilRangerOgrePercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.Ogre, 95);
        }

        [Test]
        public void EvilRangerEmptyPercentile()
        {
            AssertPercentile(EmptyContent, 96, 100);
        }
    }
}