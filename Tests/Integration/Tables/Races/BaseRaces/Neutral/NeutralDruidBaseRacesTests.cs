using System;
using NPCGen.Common.Races;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Tables.Races.BaseRaces.Neutral
{
    [TestFixture]
    public class NeutralDruidBaseRacesTests : PercentileTests
    {
        protected override String tableName
        {
            get { return "NeutralDruidBaseRaces"; }
        }

        [Test]
        public void NeutralDruidGrayElfPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.GrayElf, 1);
        }

        [Test]
        public void NeutralDruidHighElfPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.HighElf, 2, 6);
        }

        [Test]
        public void NeutralDruidWildElfPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.WildElf, 7, 11);
        }

        [Test]
        public void NeutralDruidWoodElfPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.WoodElf, 12, 31);
        }

        [Test]
        public void NeutralDruidForestGnomePercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.ForestGnome, 32);
        }

        [Test]
        public void NeutralDruidHalfElfPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.HalfElf, 33, 37);
        }

        [Test]
        public void NeutralDruidLightfootHalflingPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.LightfootHalfling, 38);
        }

        [Test]
        public void NeutralDruidTallfellowHalflingPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.TallfellowHalfling, 39);
        }

        [Test]
        public void NeutralDruidHalfOrcPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.HalfOrc, 40);
        }

        [Test]
        public void NeutralDruidHumanPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.Human, 41, 88);
        }

        [Test]
        public void NeutralDruidLizardfolkPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.Lizardfolk, 89, 98);
        }

        [Test]
        public void NeutralDruidEmptyPercentile()
        {
            AssertPercentile(EmptyContent, 99, 100);
        }
    }
}