using System;
using NPCGen.Common.Races;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Tables.Races.BaseRaces.Good
{
    [TestFixture]
    public class GoodMonkBaseRacesTests : PercentileTests
    {
        protected override String tableName
        {
            get { return "GoodMonkBaseRaces"; }
        }

        [Test]
        public void GoodMonkAasimarPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.Aasimar, 1, 2);
        }

        [Test]
        public void GoodMonkHillDwarfPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.HillDwarf, 3);
        }

        [Test]
        public void GoodMonkHighElfPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.HighElf, 4, 13);
        }

        [Test]
        public void GoodMonkHalfElfPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.HalfElf, 14, 18);
        }

        [Test]
        public void GoodMonkLightfootHalflingPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.LightfootHalfling, 19);
        }

        [Test]
        public void GoodMonkDeepHalflingPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.DeepHalfling, 20);
        }

        [Test]
        public void GoodMonkHalfOrcPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.HalfOrc, 21, 25);
        }

        [Test]
        public void GoodMonkHumanPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.Human, 26, 97);
        }

        [Test]
        public void GoodMonkEmptyPercentile()
        {
            AssertPercentile(EmptyContent, 98, 100);
        }
    }
}