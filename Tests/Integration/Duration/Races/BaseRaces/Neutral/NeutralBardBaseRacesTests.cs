using System;
using NPCGen.Common.Races;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Tables.Races.BaseRaces.Neutral
{
    [TestFixture]
    public class NeutralBardBaseRacesTests : PercentileTests
    {
        protected override String tableName
        {
            get { return "NeutralBardBaseRaces"; }
        }

        [Test]
        public void NeutralBardDeepDwarfPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.DeepDwarf, 1);
        }

        [Test]
        public void NeutralBardHillDwarfPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.HillDwarf, 2, 3);
        }

        [Test]
        public void NeutralBardGrayElfPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.GrayElf, 4, 5);
        }

        [Test]
        public void NeutralBardHighElfPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.HighElf, 6, 15);
        }

        [Test]
        public void NeutralBardWildElfPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.WildElf, 16);
        }

        [Test]
        public void NeutralBardWoodElfPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.WoodElf, 17, 21);
        }

        [Test]
        public void NeutralBardRockGnomePercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.RockGnome, 22, 23);
        }

        [Test]
        public void NeutralBardHalfElfPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.HalfElf, 24, 33);
        }

        [Test]
        public void NeutralBardLightfootHalflingPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.LightfootHalfling, 34, 36);
        }

        [Test]
        public void NeutralBardDeepHalflingPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.DeepHalfling, 37);
        }

        [Test]
        public void NeutralBardTallfellowHalflingPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.TallfellowHalfling, 38);
        }

        [Test]
        public void NeutralBardHalfOrcPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.HalfOrc, 39, 40);
        }

        [Test]
        public void NeutralBardHumanPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.Human, 41, 98);
        }

        [Test]
        public void NeutralBardEmptyPercentile()
        {
            AssertPercentile(EmptyContent, 99, 100);
        }
    }
}