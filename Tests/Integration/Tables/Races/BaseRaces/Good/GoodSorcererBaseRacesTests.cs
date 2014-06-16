using System;
using NPCGen.Common.Races;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Tables.Races.BaseRaces.Good
{
    [TestFixture]
    public class GoodSorcererBaseRacesTests : PercentileTests
    {
        protected override String tableName
        {
            get { return "GoodSorcererBaseRaces"; }
        }

        [Test]
        public void GoodSorcererAasimarPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.Aasimar, 1, 2);
        }

        [Test]
        public void GoodSorcererDeepDwarfPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.DeepDwarf, 3);
        }

        [Test]
        public void GoodSorcererHillDwarfPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.HillDwarf, 4, 5);
        }

        [Test]
        public void GoodSorcererMountainDwarfPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.MountainDwarf, 6);
        }

        [Test]
        public void GoodSorcererGrayElfPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.GrayElf, 7, 8);
        }

        [Test]
        public void GoodSorcererHighElfPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.HighElf, 9, 11);
        }

        [Test]
        public void GoodSorcererWildElfPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.WildElf, 12, 36);
        }

        [Test]
        public void GoodSorcererWoodElfPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.WoodElf, 37);
        }

        [Test]
        public void GoodSorcererForestGnomePercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.ForestGnome, 38);
        }

        [Test]
        public void GoodSorcererRockGnomeDwarfPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.RockGnome, 39, 40);
        }

        [Test]
        public void GoodSorcererHalfElfPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.HalfElf, 41, 45);
        }

        [Test]
        public void GoodSorcererLightfootHalflingPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.LightfootHalfling, 46, 54);
        }

        [Test]
        public void GoodSorcererDeepHalflingPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.DeepHalfling, 55);
        }

        [Test]
        public void GoodSorcererTallfellowHalflingPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.TallfellowHalfling, 56);
        }

        [Test]
        public void GoodSorcererHalfOrcPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.HalfOrc, 57, 58);
        }

        [Test]
        public void GoodSorcererHumanPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.Human, 59, 95);
        }

        [Test]
        public void GoodSorcererSvirfneblinPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.Svirfneblin, 96);
        }

        [Test]
        public void GoodSorcererEmptyPercentile()
        {
            AssertPercentile(EmptyContent, 97, 100);
        }
    }
}