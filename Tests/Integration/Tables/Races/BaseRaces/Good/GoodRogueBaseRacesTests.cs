using System;
using NPCGen.Common.Races;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Tables.Races.BaseRaces.Good
{
    [TestFixture]
    public class GoodRogueBaseRacesTests : PercentileTests
    {
        protected override String tableName
        {
            get { return "GoodRogueBaseRaces"; }
        }

        [Test]
        public void GoodRogueHillDwarfPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.HillDwarf, 1, 5);
        }

        [Test]
        public void GoodRogueMountainDwarfPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.MountainDwarf, 6);
        }

        [Test]
        public void GoodRogueHighElfPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.HighElf, 7, 19);
        }

        [Test]
        public void GoodRogueForestGnomePercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.ForestGnome, 20);
        }

        [Test]
        public void GoodRogueRockGnomeDwarfPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.RockGnome, 21, 25);
        }

        [Test]
        public void GoodRogueHalfElfPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.HalfElf, 26, 35);
        }

        [Test]
        public void GoodRogueLightfootHalflingPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.LightfootHalfling, 36, 60);
        }

        [Test]
        public void GoodRogueDeepHalflingPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.DeepHalfling, 61, 66);
        }

        [Test]
        public void GoodRogueTallfellowHalflingPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.TallfellowHalfling, 67, 72);
        }

        [Test]
        public void GoodRogueHalfOrcPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.HalfOrc, 73, 77);
        }

        [Test]
        public void GoodRogueHumanPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.Human, 78, 96);
        }

        [Test]
        public void GoodRogueSvirfneblinPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.Svirfneblin, 97);
        }

        [Test]
        public void GoodRogueEmptyPercentile()
        {
            AssertPercentile(EmptyContent, 98, 100);
        }
    }
}