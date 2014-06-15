using System;
using NPCGen.Common.Races;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Tables.Races.BaseRaces.Good
{
    [TestFixture]
    public class GoodClericBaseRacesTests : PercentileTests
    {
        protected override String tableName
        {
            get { return "GoodClericBaseRaces"; }
        }

        [Test]
        public void GoodClericAasimarPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.Aasimar, 1);
        }

        [Test]
        public void GoodClericDeepDwarfPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.DeepDwarf, 2);
        }

        [Test]
        public void GoodClericHillDwarfPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.HillDwarf, 3, 22);
        }

        [Test]
        public void GoodClericMountainDwarfPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.MountainDwarf, 23, 24);
        }

        [Test]
        public void GoodClericGrayElfPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.GrayElf, 25);
        }

        [Test]
        public void GoodClericHighElfPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.HighElf, 26, 35);
        }

        [Test]
        public void GoodClericWildElfPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.WildElf, 36, 40);
        }

        [Test]
        public void GoodClericWoodElfPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.WoodElf, 41);
        }

        [Test]
        public void GoodClericForestGnomePercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.ForestGnome, 42);
        }

        [Test]
        public void GoodClericRockGnomePercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.RockGnome, 43, 51);
        }

        [Test]
        public void GoodClericHalfElfPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.HalfElf, 52, 56);
        }

        [Test]
        public void GoodClericLightfootHalflingPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.LightfootHalfling, 57, 66);
        }

        [Test]
        public void GoodClericDeepHalflingPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.DeepHalfling, 67);
        }

        [Test]
        public void GoodClericTallfellowHalflingPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.TallfellowHalfling, 68, 69);
        }

        [Test]
        public void GoodClericHalfOrcPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.HalfOrc, 70);
        }

        [Test]
        public void GoodClericHumanPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.Human, 71, 95);
        }

        [Test]
        public void GoodClericSvirfneblinPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.Svirfneblin, 96);
        }

        [Test]
        public void GoodClericEmptyPercentile()
        {
            AssertPercentile(EmptyContent, 97, 100);
        }
    }
}