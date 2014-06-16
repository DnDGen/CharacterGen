using System;
using NPCGen.Common.Races;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Tables.Races.BaseRaces.Neutral
{
    [TestFixture]
    public class NeutralRangerBaseRacesTests : PercentileTests
    {
        protected override String tableName
        {
            get { return "NeutralRangerBaseRaces"; }
        }

        [Test]
        public void NeutralRangerHillDwarfPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.HillDwarf, 1);
        }

        [Test]
        public void NeutralRangerHighElfPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.HighElf, 2, 6);
        }

        [Test]
        public void NeutralRangerWildElfPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.WildElf, 7);
        }

        [Test]
        public void NeutralRangerWoodElfPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.WoodElf, 8, 36);
        }

        [Test]
        public void NeutralRangerForestGnomePercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.ForestGnome, 37);
        }

        [Test]
        public void NeutralRangerRockGnomePercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.RockGnome, 38);
        }

        [Test]
        public void NeutralRangerHalfElfPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.HalfElf, 39, 55);
        }

        [Test]
        public void NeutralRangerLightfootHalflingPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.LightfootHalfling, 56);
        }

        [Test]
        public void NeutralRangerTallfellowHalflingPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.TallfellowHalfling, 57);
        }

        [Test]
        public void NeutralRangerHalfOrcPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.HalfOrc, 58, 67);
        }

        [Test]
        public void NeutralRangerHumanPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.Human, 68, 96);
        }

        [Test]
        public void NeutralRangerLizardfolkPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.Lizardfolk, 97, 98);
        }

        [Test]
        public void NeutralRangerEmptyPercentile()
        {
            AssertPercentile(EmptyContent, 99, 100);
        }
    }
}