using System;
using NPCGen.Common.Races;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Tables.Races.BaseRaces.Neutral
{
    [TestFixture]
    public class NeutralMonkBaseRacesTests : PercentileTests
    {
        protected override String tableName
        {
            get { return "NeutralMonkBaseRaces"; }
        }

        [Test]
        public void NeutralMonkHighElfPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.HighElf, 1, 2);
        }

        [Test]
        public void NeutralMonkWoodElfPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.WoodElf, 3);
        }

        [Test]
        public void NeutralMonkHalfElfPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.HalfElf, 4, 13);
        }

        [Test]
        public void NeutralMonkLightfootHalflingPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.LightfootHalfling, 14);
        }

        [Test]
        public void NeutralMonkDeepHalflingPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.DeepHalfling, 15);
        }

        [Test]
        public void NeutralMonkHalfOrcPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.HalfOrc, 16, 25);
        }

        [Test]
        public void NeutralMonkHumanPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.Human, 26, 100);
        }
    }
}