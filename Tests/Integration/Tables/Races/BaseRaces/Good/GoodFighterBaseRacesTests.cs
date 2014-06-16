using System;
using NPCGen.Common.Races;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Tables.Races.BaseRaces.Good
{
    [TestFixture]
    public class GoodFighterBaseRacesTests : PercentileTests
    {
        protected override String tableName
        {
            get { return "GoodFighterBaseRaces"; }
        }

        [Test]
        public void GoodFighterDeepDwarfPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.DeepDwarf, 1, 3);
        }

        [Test]
        public void GoodFighterHillDwarfPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.HillDwarf, 4, 33);
        }

        [Test]
        public void GoodFighterMountainDwarfPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.MountainDwarf, 34, 41);
        }

        [Test]
        public void GoodFighterGrayElfPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.GrayElf, 42);
        }

        [Test]
        public void GoodFighterHighElfPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.HighElf, 43, 47);
        }

        [Test]
        public void GoodFighterRockGnomePercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.RockGnome, 48);
        }

        [Test]
        public void GoodFighterHalfElfPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.HalfElf, 49, 50);
        }

        [Test]
        public void GoodFighterLightfootHalflingPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.LightfootHalfling, 51);
        }

        [Test]
        public void GoodFighterDeepHalflingPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.DeepHalfling, 52);
        }

        [Test]
        public void GoodFighterHalfOrcPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.HalfOrc, 53, 57);
        }

        [Test]
        public void GoodFighterHumanPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.Human, 58, 97);
        }

        [Test]
        public void GoodFighterEmptyPercentile()
        {
            AssertPercentile(EmptyContent, 98, 100);
        }
    }
}