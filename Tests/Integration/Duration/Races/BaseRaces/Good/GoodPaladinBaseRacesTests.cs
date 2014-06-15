using System;
using NPCGen.Common.Races;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Tables.Races.BaseRaces.Good
{
    [TestFixture]
    public class GoodPaladinBaseRacesTests : PercentileTests
    {
        protected override String tableName
        {
            get { return "GoodPaladinBaseRaces"; }
        }

        [Test]
        public void GoodPaladinAasimarPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.Aasimar, 1, 10);
        }

        [Test]
        public void GoodPaladinHillDwarfPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.HillDwarf, 11, 20);
        }

        [Test]
        public void GoodPaladinMountainDwarfPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.MountainDwarf, 21);
        }

        [Test]
        public void GoodPaladinRockGnomeDwarfPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.RockGnome, 22);
        }

        [Test]
        public void GoodPaladinHalfElfPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.HalfElf, 23, 27);
        }

        [Test]
        public void GoodPaladinLightfootHalflingPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.LightfootHalfling, 28);
        }

        [Test]
        public void GoodPaladinDeepHalflingPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.DeepHalfling, 29);
        }

        [Test]
        public void GoodPaladinHalfOrcPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.HalfOrc, 30);
        }

        [Test]
        public void GoodPaladinHumanPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.Human, 31, 97);
        }

        [Test]
        public void GoodPaladinEmptyPercentile()
        {
            AssertPercentile(EmptyContent, 98, 100);
        }
    }
}