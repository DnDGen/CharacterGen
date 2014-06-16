using System;
using NPCGen.Common.Races;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Tables.Races.BaseRaces.Good
{
    [TestFixture]
    public class GoodBarbarianBaseRacesTests : PercentileTests
    {
        protected override String tableName
        {
            get { return "GoodBarbarianBaseRaces"; }
        }

        [Test]
        public void GoodBarbarianHillDwarfPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.HillDwarf, 1, 2);
        }

        [Test]
        public void GoodBarbarianWildElfPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.WildElf, 3, 32);
        }

        [Test]
        public void GoodBarbarianWoodElfPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.WoodElf, 33, 34);
        }

        [Test]
        public void GoodBarbarianHalfElfPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.HalfElf, 35);
        }

        [Test]
        public void GoodBarbarianLightfootHalflingPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.LightfootHalfling, 36);
        }

        [Test]
        public void GoodBarbarianHalfOrcPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.HalfOrc, 37, 61);
        }

        [Test]
        public void GoodBarbarianHumanPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.Human, 62, 98);
        }

        [Test]
        public void GoodBarbarianEmptyPercentile()
        {
            AssertPercentile(EmptyContent, 99, 100);
        }
    }
}