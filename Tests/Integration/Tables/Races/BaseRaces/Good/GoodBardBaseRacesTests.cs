using System;
using NPCGen.Common.Races;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Tables.Races.BaseRaces.Good
{
    [TestFixture]
    public class GoodBardBaseRacesTests : PercentileTests
    {
        protected override String tableName
        {
            get { return "GoodBardBaseRaces"; }
        }

        [Test]
        public void GoodBardAasimarPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.Aasimar, 1);
        }

        [Test]
        public void GoodBardHillDwarfPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.HillDwarf, 2, 6);
        }

        [Test]
        public void GoodBardGrayElfPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.GrayElf, 7, 11);
        }

        [Test]
        public void GoodBardHighElfPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.HighElf, 12, 36);
        }

        [Test]
        public void GoodBardWildElfPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.WildElf, 37);
        }

        [Test]
        public void GoodBardWoodElfPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.WoodElf, 38);
        }

        [Test]
        public void GoodBardForestGnomePercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.ForestGnome, 39);
        }

        [Test]
        public void GoodBardRockGnomePercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.RockGnome, 40, 44);
        }

        [Test]
        public void GoodBardHalfElfPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.HalfElf, 45, 53);
        }

        [Test]
        public void GoodBardLightfootHalflingPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.LightfootHalfling, 54);
        }

        [Test]
        public void GoodBardDeepHalflingPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.DeepHalfling, 55);
        }

        [Test]
        public void GoodBardTallfellowHalflingPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.TallfellowHalfling, 56, 57);
        }

        [Test]
        public void GoodBardHumanPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.Human, 58, 97);
        }

        [Test]
        public void GoodBardSvirfneblinPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.Svirfneblin, 98);
        }

        [Test]
        public void GoodBardEmptyPercentile()
        {
            AssertPercentile(EmptyContent, 99, 100);
        }
    }
}