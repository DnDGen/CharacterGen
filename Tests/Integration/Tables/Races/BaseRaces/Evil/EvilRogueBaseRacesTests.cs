using System;
using NPCGen.Common.Races;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Tables.Races.BaseRaces.Evil
{
    [TestFixture]
    public class EvilRogueBaseRacesTests : PercentileTests
    {
        protected override String tableName
        {
            get { return "EvilRogueBaseRaces"; }
        }

        [Test]
        public void EvilRogueDeepDwarfPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.DeepDwarf, 1);
        }

        [Test]
        public void EvilRogueHighElfPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.HighElf, 2);
        }

        [Test]
        public void EvilRogueWoodElfPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.WoodElf, 3);
        }

        [Test]
        public void EvilRogueHalfElfPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.HalfElf, 4, 18);
        }

        [Test]
        public void EvilRogueLightfootHalflingPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.LightfootHalfling, 19, 38);
        }

        [Test]
        public void EvilRogueDeepHalflingPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.DeepHalfling, 39);
        }

        [Test]
        public void EvilRogueTallfellowHalflingPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.TallfellowHalfling, 40);
        }

        [Test]
        public void EvilRogueHalfOrcPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.HalfOrc, 41, 50);
        }

        [Test]
        public void EvilRogueHumanPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.Human, 51, 70);
        }

        [Test]
        public void EvilRogueGoblinPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.Goblin, 71, 85);
        }

        [Test]
        public void EvilRogueHobgoblinPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.Hobgoblin, 86);
        }

        [Test]
        public void EvilRogueKoboldPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.Kobold, 87);
        }

        [Test]
        public void EvilRogueTieflingPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.Tiefling, 88, 89);
        }

        [Test]
        public void EvilRogueBugbearPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.Bugbear, 90, 93);
        }

        [Test]
        public void EvilRogueMindFlayerPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.MindFlayer, 94);
        }

        [Test]
        public void EvilRogueEmptyPercentile()
        {
            AssertPercentile(EmptyContent, 95, 100);
        }
    }
}