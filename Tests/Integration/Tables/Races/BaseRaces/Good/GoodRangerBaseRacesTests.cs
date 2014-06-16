using System;
using NPCGen.Common.Races;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Tables.Races.BaseRaces.Good
{
    [TestFixture]
    public class GoodRangerBaseRacesTests : PercentileTests
    {
        protected override String tableName
        {
            get { return "GoodRangerBaseRaces"; }
        }

        [Test]
        public void GoodRangerHillDwarfPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.HillDwarf, 1, 5);
        }

        [Test]
        public void GoodRangerHighElfPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.HighElf, 6, 20);
        }

        [Test]
        public void GoodRangerWildElfPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.WildElf, 21);
        }

        [Test]
        public void GoodRangerWoodElfPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.WoodElf, 22, 36);
        }

        [Test]
        public void GoodRangerForestGnomePercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.ForestGnome, 37, 41);
        }

        [Test]
        public void GoodRangerRockGnomeDwarfPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.RockGnome, 42);
        }

        [Test]
        public void GoodRangerHalfElfPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.HalfElf, 43, 57);
        }

        [Test]
        public void GoodRangerLightfootHalflingPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.LightfootHalfling, 58);
        }

        [Test]
        public void GoodRangerTallfellowHalflingPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.TallfellowHalfling, 59);
        }

        [Test]
        public void GoodRangerHalfOrcPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.HalfOrc, 60, 64);
        }

        [Test]
        public void GoodRangerHumanPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.Human, 65, 97);
        }

        [Test]
        public void GoodRangerEmptyPercentile()
        {
            AssertPercentile(EmptyContent, 98, 100);
        }
    }
}