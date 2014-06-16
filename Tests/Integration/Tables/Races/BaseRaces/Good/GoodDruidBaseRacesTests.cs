using System;
using NPCGen.Common.Races;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Tables.Races.BaseRaces.Good
{
    [TestFixture]
    public class GoodDruidBaseRacesTests : PercentileTests
    {
        protected override String tableName
        {
            get { return "GoodDruidBaseRaces"; }
        }

        [Test]
        public void GoodDruidGrayElfPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.GrayElf, 1);
        }

        [Test]
        public void GoodDruidHighElfPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.HighElf, 2, 11);
        }

        [Test]
        public void GoodDruidWildElfPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.WildElf, 12, 21);
        }

        [Test]
        public void GoodDruidWoodElfPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.WoodElf, 22, 31);
        }

        [Test]
        public void GoodDruidForestGnomePercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.ForestGnome, 32, 36);
        }

        [Test]
        public void GoodDruidRockGnomePercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.RockGnome, 37);
        }

        [Test]
        public void GoodDruidHalfElfPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.HalfElf, 38, 46);
        }

        [Test]
        public void GoodDruidLightfootHalflingPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.LightfootHalfling, 47);
        }

        [Test]
        public void GoodDruidTallfellowHalflingPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.TallfellowHalfling, 48);
        }

        [Test]
        public void GoodDruidHalfOrcPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.HalfOrc, 49);
        }

        [Test]
        public void GoodDruidHumanPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.Human, 50, 99);
        }

        [Test]
        public void GoodDruidEmptyPercentile()
        {
            AssertPercentile(EmptyContent, 100);
        }
    }
}